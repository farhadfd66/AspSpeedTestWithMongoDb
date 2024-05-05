using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using AspSpeedTestWithMongoDb.Models;
using Newtonsoft.Json;

namespace AspSpeedTestWithMongoDb.GenericService
{
    public class GenericeRepository<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> _TEntityCollection;
        private readonly string TDatabase;

        public GenericeRepository( string tDatabase)
        {
            TDatabase = tDatabase;
            var mongoClient = new MongoClient("mongodb://root:F123456D@localhost:27017/");

            var mongoDatabase = mongoClient.GetDatabase("SpeedTestWithMongoDb");
            
            _TEntityCollection = mongoDatabase.GetCollection<TEntity>(TDatabase);

        }

        public async Task<List<TEntity>> GetAsync()
        {
           return await  _TEntityCollection.Find(_ => true).ToListAsync();
          
        }
          

   

        public async Task CreateAsync(TEntity tEntity)
        {
         
            await _TEntityCollection.InsertOneAsync(tEntity);
           
        }
        public async Task CreateManyAsync(List<TEntity> tEntity)
        {
         
            await _TEntityCollection.InsertManyAsync(tEntity);
           
        }



     

    }

}
