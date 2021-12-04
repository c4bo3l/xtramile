namespace Infrastructure.Model
{
    public class CountryResponse
    {
        public bool error { get; set; }
        public string msg { get; set; }
        public Country[] data { get; set; }
    }
}