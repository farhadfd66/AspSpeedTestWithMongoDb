using AspSpeedTestWithMongoDb.Database;
using AspSpeedTestWithMongoDb.GenericService;
using AspSpeedTestWithMongoDb.Models;
using Microsoft.Extensions.Options;

namespace AspSpeedTestWithMongoDb.Services
{
    public class DataService : GenericeRepository<DataDto>
    {
        public DataService(IOptions<MongoDatabase> options) : base(options.Value.DataTestCollectionName)
        {
        }
    }
}
