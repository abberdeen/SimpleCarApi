using SimpleCarApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCarApi.Services
{
    public interface ICarService
    {
         int GetNextObjectSequence(string objectName);

         List<Car> Get();

         Car Get(int id);

         Car Create(Car car);

         void Update(int id, Car carIn);

         void Remove(Car carIn);

         void Remove(int id);
    }
}
