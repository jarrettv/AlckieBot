using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Data.Base
{
    public class DatabaseSettings
    {

        private static MongoUrl _mongoURL;
        public static MongoUrl MongoUrl
        {
            get
            {
                if (_mongoURL == null)
                {

                    var server = ConfigurationManager.AppSettings["MONGO_SERVER"];
                    var user = ConfigurationManager.AppSettings["MONGO_USER"];
                    var password = ConfigurationManager.AppSettings["MONGO_PASSWORD"];
                    var database = ConfigurationManager.AppSettings["MONGO_DATABASE"];
                    var url = String.Format(@"mongodb://{0}:{1}@{2}/{3}", user, password, server, database);

                    _mongoURL = new MongoUrl(url);
                }
                return _mongoURL;
            }
        }
    }
}
