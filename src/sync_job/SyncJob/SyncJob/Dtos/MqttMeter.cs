namespace SyncJob.Dtos
{
    internal class MqttMeter
    {
        public double Value { get; set; }
        public double Raw { get; set; }
        public double Pre { get; set; }
        public string Rate { get; set; }
        public string Error { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
