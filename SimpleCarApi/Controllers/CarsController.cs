using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<Car> Get(string id)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public ActionResult<Car> Create(Car car)
        {
            _carService.Create(car);

            return CreatedAtRoute("GetCar", new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Car carId)
        {
            var car = _carService.Get(id);

            if (car == null)
            {
                return NotFound();
            }

            _carService.Update(id, carId);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
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
