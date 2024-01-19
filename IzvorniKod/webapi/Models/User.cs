﻿using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace MP7_progi.Models
{
    public class User : Table
    {
        enum names
        {
            userID,
            userName,
            email,
            phoneNum,
            psw,
            userType
        }
        private Dictionary<string, string> types = new Dictionary<string, string>()
        {
            {"userID", "int"},
            {"userName", "string"},
            {"email", "string"},
            {"phoneNum", "string"},
            {"psw", "string"},
            {"userType", "string" }
        };
        private readonly string _id = "User";
        public int userID { get; set; }
        public string userName { get; set; }
        public string email { get; set; }

        public string phoneNum { get; set; }
        public string psw { get; set; }

        public string? userType { get; set; }

        public User() {}

        override
        public String returnTable() { return _id; }

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
