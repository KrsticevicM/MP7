﻿using System.ComponentModel.DataAnnotations;

namespace MP7_progi.Models
{
    public class Ad : Table
    {
        enum names
        {
            adID,
            catAd,
            userID,
            location,
            dateHourMis,
            lat,
            lon
        }
        private Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"adID", "int"},
            {"catAd", "string" },
            {"userID", "int" },
            {"location", "string"},
            {"dateHourMis", "DateTime"},
            {"lat", "string"},
            {"lon", "string"}
        };
        private readonly string _id = "Ad";
        public int adID { get; set; }
        public string catAd { get; set; }
        public int userID { get; set; }

        [DataType(DataType.Date)]
        public DateTime dateHourMis { get; set; }
        public string location { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        public Ad() { }

        override
        public String returnTable()
        { return _id; }

        override
        public String returnColumnType(string column)
        {
            return types[column];
        }
        override
        public Dictionary<string, string> returnColumnTypes()
        {
            return types;
        }
    }
}
