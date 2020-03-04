using System;
using System.Collections.Generic; 
using System.Text.Json; 
using Microsoft.AspNetCore.Mvc; 
using SimpleCarApi.Model;
using SimpleCarApi.Services;

namespace SimpleCarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarService _carService;

        public CarsController(ICarService carService)
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
        public ActionResult<Car> Query([FromBody] JsonElement data)
        {           
            if (JsonContainsKey(ref data,"Id")) 
            {
                //на апдейт высылаем, то передаем только те поля, что нужно обновить + Id.
                int id = data.GetProperty("Id").GetInt32();

                var car = _carService.Get(id);

                if (car == null)
                {
                    return NotFound();
                }

                if (JsonContainsKey(ref data, "Name")) 
                {
                    car.Name = data.GetProperty("Name").GetString();
                }

                if (JsonContainsKey(ref data, "Description"))
                {
                    car.Description = data.GetProperty("Description").GetString();
                }

                _carService.Update(id, car);

                return car;
            }
            else
            {
                //Если Id не передается, значит, создаем объект
                Car car = new Car();

                var counter = _carService.GetNextObjectSequence("carId");

                car.Id = counter;

                if (JsonContainsKey(ref data, "Name"))
                {
                    car.Name = data.GetProperty("Name").GetString();
                }

                if (car.Name == null || car.Name == "")
                { 
                    return BadRequest();
                }

                if (JsonContainsKey(ref data, "Description"))
                {
                    car.Description = data.GetProperty("Description").GetString();
                }

                _carService.Create(car);
                return car;
            } 
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
