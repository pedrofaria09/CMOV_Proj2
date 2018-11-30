using System;

namespace stock.Models
{
    public class StockDetails
    {
        public DateTime date { get; set; }
        public float openValue { get; set; }
        public float highValue { get; set; }
        public float lowValue { get; set; }
        public float closeValue { get; set; }
        public int volume { get; set; }
    }
}