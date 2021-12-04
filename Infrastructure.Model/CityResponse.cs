namespace Infrastructure.Model
{
    public class CityResponse
    {
        public bool error { get; set; }
        public string msg { get; set; }
        public string[] data { get; set; }
    }
}