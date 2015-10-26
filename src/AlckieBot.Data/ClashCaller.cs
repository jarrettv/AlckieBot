using AlckieBot.Data.Base;
using AlckieBot.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Data
{
    public class ClashCaller
    {
        public static async Task<string> GetClashCallerCode()
        {
            var client = new MongoClient(DatabaseSettings.MongoUrl);
            var database = client.GetDatabase("alckiebot");
            var collection = database.GetCollection<Model.ClashCaller>("clashcaller");
            var clashCaller = await collection.Find(_ => true).FirstOrDefaultAsync();

            return clashCaller.Code;
        }

        public static async Task SetClashCaller(string code)
        {
            var client = new MongoClient(DatabaseSettings.MongoUrl);
            var database = client.GetDatabase("alckiebot");
            var collection = database.GetCollection<BsonDocument>("clashcaller");
            
            var clashCaller = new BsonDocument
            {
                { "Code" , code }
            };
            await collection.InsertOneAsync(clashCaller);
        }
    }
}
