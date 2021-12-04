namespace Infrastructure.Model
{
    public class WeatherInfo
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public WeatherDetailInfo current { get; set; }
    }
}