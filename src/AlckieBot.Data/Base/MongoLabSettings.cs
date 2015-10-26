using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Data.Base
{
    public class MongoLabSettings
    {
        public const string MONGOLAB_API = @"https://api.mongolab.com/api/1/";
        public static string MongoLabAPIKey
        {
            get
            {
                return ConfigurationManager.AppSettings["MONGOLAB_APIKEY"];
            }
        }
    }
}
