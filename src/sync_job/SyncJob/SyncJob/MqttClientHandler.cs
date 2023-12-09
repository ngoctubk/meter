using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Formatter;
using SyncJob.Dtos;
using SyncJob.Helpers;
using SyncJob.Models;
using System;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace SyncJob
{
    internal class MqttClientHandler
    {
        private static IServiceProvider _serviceProvider;
        private static string _waterTopic = "water";
        private static string _gasTopic = "gas";
        public MqttClientHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Func<MqttApplicationMessageReceivedEventArgs, Task> HandleMessageAsync = async e =>
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();


            // Get the topic and the payload from the message
            var topic = e.ApplicationMessage.Topic;
            var topics = topic.Split("/");
            var stallCode = topics[1];
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString
            };
            options.Converters.Add(new DateTimeJsonConverterUsingDateTimeParse());

            // If has error, save to Errors table and do nothing
            var meter = JsonSerializer.Deserialize<MqttMeter>(e.ApplicationMessage.PayloadSegment.AsSpan(), options);
            if (!meter.Error.Equals("no error"))
            {
                Error error = new()
                {
                    StallCode = stallCode,
                    Topic = topic,
                    Payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment),
                    Timestamp = DateTime.UtcNow,
                };
                context.Errors.Add(error);
                await context.SaveChangesAsync();
                return;
            }

            // If new stall with meter add to MQTT Broker, automatically add to Stalls table
            bool exists = await context.Stalls.AnyAsync(c => c.StallCode == stallCode);
            var stall = new Stall { StallCode = stallCode };
            if (!exists)
            {
                stall.Name = stallCode;
                context.Stalls.Add(stall);
            }
            else
            {
                stall = await context.Stalls.FirstOrDefaultAsync(c => c.StallCode == stallCode);
            }

            if (topics[0].Equals(_waterTopic))
            {
                // If message is from water meter

                // Get cycle of stall's water
                int currentWaterCycle = 1;
                if (stall.LastWaterMeterId != null)
                {
                    currentWaterCycle = await context.WaterMeters.Where(c => c.StallCode == stallCode)
                                                                .MaxAsync(c => c.Cycle);

                    // If meter is new, the value is small, need to register new cycle
                    bool resetMeter = await context.WaterMeters.AnyAsync(c => c.StallCode == stallCode
                                                      && c.Cycle == currentWaterCycle
                                                      && c.Value > meter.Value);
                    if (resetMeter)
                    {
                        currentWaterCycle++;
                    }
                }

                // Check if meter value in current cycle exists
                bool meterExists = await context.WaterMeters.AnyAsync(c => c.StallCode == stallCode
                                                                      && c.Value == meter.Value
                                                                      && c.Cycle == currentWaterCycle);
                WaterMeter waterMeter = new();
                if (meterExists)
                {
                    // Update ToTimestamp instead of create new meter with the same value
                    waterMeter = await context.WaterMeters.FirstOrDefaultAsync(c => c.StallCode == stallCode
                                                                                  && c.Value == meter.Value
                                                                                  && c.Cycle == currentWaterCycle);
                    waterMeter.ToTimestamp = meter.Timestamp;
                }
                else
                {
                    // Add new meter
                    waterMeter = new()
                    {
                        StallCode = stallCode,
                        Cycle = currentWaterCycle,
                        Value = meter.Value,
                        Raw = meter.Raw,
                        Pre = meter.Pre,
                        Error = meter.Error,
                        Rate = meter.Rate,
                        FromTimestamp = meter.Timestamp,
                        ToTimestamp = meter.Timestamp
                    };
                    context.WaterMeters.Add(waterMeter);
                }

                if (stall.LastWaterMeterId == null)
                {
                    stall.LastWaterMeterId = waterMeter.Id;
                    stall.LastWaterMeter = waterMeter.Value;
                    stall.LastWaterMeterDate = waterMeter.ToTimestamp;
                    stall.UseWaterMeter = true;
                }
                stall.LatestWaterMeterId = waterMeter.Id;
                stall.LatestWaterMeter = waterMeter.Value;
                stall.LatestWaterMeterDate = waterMeter.ToTimestamp;
            }
            else if (topics[0].Equals(_gasTopic))
            {
                // If message is from gas meter

                // Get cycle of stall's gas
                int currentGasCycle = 1;
                if (stall.LastGasMeterId != null)
                {
                    currentGasCycle = await context.GasMeters.Where(c => c.StallCode == stallCode)
                                                                .MaxAsync(c => c.Cycle);

                    // If meter is new, the value is small, need to register new cycle
                    bool resetMeter = await context.GasMeters.AnyAsync(c => c.StallCode == stallCode
                                                      && c.Cycle == currentGasCycle
                                                      && c.Value > meter.Value);
                    if (resetMeter)
                    {
                        currentGasCycle++;
                    }
                }

                // Check if meter value in current cycle exists
                bool meterExists = await context.GasMeters.AnyAsync(c => c.StallCode == stallCode
                                                                      && c.Value == meter.Value
                                                                      && c.Cycle == currentGasCycle);
                GasMeter gasMeter = new();
                if (meterExists)
                {
                    // Update ToTimestamp instead of create new meter with the same value
                    gasMeter = await context.GasMeters.FirstOrDefaultAsync(c => c.StallCode == stallCode
                                                                                  && c.Value == meter.Value
                                                                                  && c.Cycle == currentGasCycle);
                    gasMeter.ToTimestamp = meter.Timestamp;
                }
                else
                {
                    // Add new meter
                    gasMeter = new()
                    {
                        StallCode = stallCode,
                        Cycle = currentGasCycle,
                        Value = meter.Value,
                        Raw = meter.Raw,
                        Pre = meter.Pre,
                        Error = meter.Error,
                        Rate = meter.Rate,
                        FromTimestamp = meter.Timestamp,
                        ToTimestamp = meter.Timestamp
                    };
                    context.GasMeters.Add(gasMeter);
                }

                if (stall.LastGasMeterId == null)
                {
                    stall.LastGasMeterId = gasMeter.Id;
                    stall.LastGasMeter = gasMeter.Value;
                    stall.LastGasMeterDate = gasMeter.ToTimestamp;
                    stall.UseGasMeter = true;
                }

                stall.LatestGasMeterId = gasMeter.Id;
                stall.LatestGasMeter = gasMeter.Value;
                stall.LatestGasMeterDate = gasMeter.ToTimestamp;
            }

            await context.SaveChangesAsync();
        };

        internal async Task InitClient()
        {
            var mqttFactory = new MqttFactory();

            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                string mqttClientId = Environment.GetEnvironmentVariable("MQTT_CLIENTID");
                string mqttServer = Environment.GetEnvironmentVariable("MQTT_SERVER");
                int mqttPort = Convert.ToInt16(Environment.GetEnvironmentVariable("MQTT_PORT"));
                string mqttUsername = Environment.GetEnvironmentVariable("MQTT_USERNAME");
                string mqttPassword = Environment.GetEnvironmentVariable("MQTT_PASSWORD");

                var mqttClientOptions = new MqttClientOptionsBuilder()
                                                        .WithClientId(mqttClientId)
                                                        .WithTcpServer(mqttServer, mqttPort)
                                                        .WithCredentials(mqttUsername, mqttPassword)
                                                        .WithCleanSession()
                                                        .WithProtocolVersion(MqttProtocolVersion.V500)
                                                        .Build();

                mqttClient.ApplicationMessageReceivedAsync += HandleMessageAsync;

                Console.WriteLine("Connecting to MQTT Broker...");

                _ = Task.Run(
                async () =>
                {
                    // User proper cancellation and no while(true).
                    while (true)
                    {
                        try
                        {
                            // This code will also do the very first connect! So no call to _ConnectAsync_ is required in the first place.
                            if (!await mqttClient.TryPingAsync())
                            {
                                var connectResult = await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                                // Subscribe to topics when session is clean etc.
                                Console.WriteLine("The MQTT client is connected.");

                                if (connectResult.ResultCode == MqttClientConnectResultCode.Success)
                                {
                                    var mqttSubscribeOptionsBuilder = mqttFactory.CreateSubscribeOptionsBuilder();
                                    string mqttTopics = Environment.GetEnvironmentVariable("MQTT_TOPICS");
                                    if (mqttTopics.Contains(";;"))
                                    {
                                        var topics = mqttTopics.Split(";;");
                                        foreach (var topic in topics)
                                        {
                                            mqttSubscribeOptionsBuilder
                                                .WithTopicFilter(f =>
                                                {
                                                    f.WithTopic(topic);
                                                });
                                        }
                                    }
                                    else
                                    {
                                        mqttSubscribeOptionsBuilder
                                        .WithTopicFilter(f =>
                                        {
                                            f.WithTopic(mqttTopics);
                                        });
                                    }
                                    var mqttSubscribeOptions = mqttSubscribeOptionsBuilder.Build();

                                    await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
                                    Console.WriteLine("MQTT client subscribed to topic.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            // Handle the exception properly (logging etc.).
                            Console.WriteLine($"Exception occurs: {ex.Message}");
                        }
                        finally
                        {
                            // Check the connection state every 3 seconds and perform a reconnect if required.
                            await Task.Delay(TimeSpan.FromSeconds(3));
                        }
                    }
                });


                await Console.Out.WriteLineAsync("Press Ctrl + C to exit.");
                var source = new CancellationTokenSource();

                Console.CancelKeyPress += (sender, eventArgs) =>
                {
                    eventArgs.Cancel = true;
                    source.Cancel();
                };
                await Task.Delay(Timeout.Infinite, source.Token);

                await mqttClient.DisconnectAsync(new MqttClientDisconnectOptionsBuilder().WithReason(MqttClientDisconnectOptionsReason.NormalDisconnection).Build());
                Console.WriteLine("Disconnected from MQTT Broker.");
            };
        }

    }
}
