using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CarsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json; 
using SimpleCarApi.Model;

namespace SimpleCarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarService _carService;

        public CarsController(CarService carService)
        {
            _carService = carService;
        }

        // GET: api/Cars
        [HttpGet]
        public ActionResult<List<Car>> Get() =>
           _carService.Get();

        // GET: api/Cars/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<Car> Get(int id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public IActionResult Query([FromBody] JsonElement data)
        {           
            if (JsonContainsKey(ref data,"id")) 
            {
                //на апдейт высылаем, то передаем только те поля, что нужно обновить + Id.
                int id = data.GetProperty("id").GetInt32();

                var car = _carService.Get(id);

                if (car == null)
                {
                    return NotFound();
                }

                if (JsonContainsKey(ref data, "name")) 
                {
                    car.Name = data.GetProperty("name").GetString();
                }

                if (JsonContainsKey(ref data, "description"))
                {
                    car.Description = data.GetProperty("description").GetString();
                }

                _carService.Update(id, car);
            }
            else
            {
                //Если Id не передается, значит, создаем объект
                Car car = new Car();

                var counter = _carService.GetNextObjectSequence("carId");

                car.Id = counter;

                if (JsonContainsKey(ref data, "name"))
                {
                    car.Name = data.GetProperty("name").GetString();
                }

                if (car.Name == null || car.Name == "")
                { 
                    return BadRequest();
                }

                if (JsonContainsKey(ref data, "description"))
                {
                    car.Description = data.GetProperty("description").GetString();
                }

                _carService.Create(car);
            }
            return NoContent();
        }

        private bool JsonContainsKey(ref JsonElement data, string key)
        {
            JsonElement element = new JsonElement();
            data.TryGetProperty(key, out element);
            if (!element.Equals(new JsonElement()))
            {
                return true;
            }
            return false;
        }
       

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            _carService.Remove(car);

            return NoContent();
        }
    }
}
