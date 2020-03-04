using SimpleCarApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCarApi.Services
{
    public interface ICarService
    {
        public int GetNextObjectSequence(string objectName);

        public List<Car> Get();

        public Car Get(int id);

        public Car Create(Car car);

        public void Update(int id, Car carIn);

        public void Remove(Car carIn);

        public void Remove(int id);
    }
}
