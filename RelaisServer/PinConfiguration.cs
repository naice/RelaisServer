namespace RelaisServer
{
    public class PinConfiguration
    {
        public string Name { get; set; }
        public int Pin { get; set; }
        /// <summary>
        /// true = high, false = low.
        /// </summary>
        public bool State { get; set; }
    }
}