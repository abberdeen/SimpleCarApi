using SimpleCarApi.Model;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace CarsApi.Services
{
    public class CarService
    {
        private readonly IMongoCollection<Car> _cars;
        private IMongoDatabase _database;
        public CarService(ICarsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);

            _cars = _database.GetCollection<Car>(settings.CarsCollectionName);
        }

        [BsonIgnoreExtraElements]
        private class ObjectSequence
        {
            public string Name { get; set; }
            public int Sequence { get; set; }
        }
        public int GetNextObjectSequence(string objectName)
        {
            var collection = _database.GetCollection<ObjectSequence>("Counters");
            var filter = new FilterDefinitionBuilder<ObjectSequence>().Where(x => x.Name == objectName);
            var update = new UpdateDefinitionBuilder<ObjectSequence>().Inc(x => x.Sequence, 1);
            var options = new FindOneAndUpdateOptions<ObjectSequence, ObjectSequence>() { ReturnDocument = ReturnDocument.After, IsUpsert = true };

            ObjectSequence seq = collection.FindOneAndUpdate<ObjectSequence>(filter, update, options);

            return seq.Sequence;
        }

        public List<Car> Get() =>
            _cars.Find(car => true).ToList();

        public Car Get(int id) =>
            _cars.Find<Car>(car => car.Id == id).FirstOrDefault();

        public Car Create(Car car)
        {
            _cars.InsertOne(car);
            return car;
        }

        public void Update(int id, Car carIn) =>
            _cars.ReplaceOne(car => car.Id == id, carIn);

        public void Remove(Car carIn) =>
            _cars.DeleteOne(car => car.Id == carIn.Id);

        public void Remove(int id) =>
            _cars.DeleteOne(car => car.Id == id);
    }
}