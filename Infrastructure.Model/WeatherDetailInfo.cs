using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class WeatherDetailInfo
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public decimal temp { get; set; }
        public decimal temp_celcius { get; set; }
        public decimal feels_like { get; set; }
        public decimal pressure { get; set; }
        public decimal humidity { get; set; }
        public decimal dew_point { get; set; }
        public decimal uvi { get; set; }
        public decimal clouds { get; set; }
        public decimal visibility { get; set; }
        public decimal wind_speed { get; set; }
        public decimal wind_deg { get; set; }
        public decimal wind_gust { get; set; }
        public List<Weather> weather { get; set; }
    }
}