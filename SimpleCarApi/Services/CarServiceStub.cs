using SimpleCarApi.Model;
using SimpleCarApi.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleCarApi.Services
{
    public class CarServiceStub : ICarService
    {
        private List<Car> _cars = new List<Car>(); 
        public CarServiceStub()
        {
            _cars.Add(new Car { 
                Id = 0, 
                Name = "Ford", 
                Description = "Yellow" 
            });

            _cars.Add(new Car
            {
                Id = 1,
                Name = "Nissan",
                Description = "Gold"
            });
        }

        public int GetNextObjectSequence(string objectName)
        {
            return _cars.Count;
        }

        public List<Car> Get() =>
           _cars;

        public Car Get(int id)
        {
            return _cars.Find(car => car.Id == id);
        }

        public Car Create(Car car)
        {
            _cars.Add(car);
            return car;
        }

        public void Update(int id, Car carIn)
        {
            _cars[_cars.FindIndex(car => car.Id == id)] = carIn;
        }

        public void Remove(Car carIn) =>
            _cars.Remove(carIn);

        public void Remove(int id) =>
            _cars.Remove(Get(id));
    }
}
