using System;
using Xunit;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Threading.Tasks;
using SimpleCarApi.Model;
using System.Net;
using System.Text.Json;

namespace SimpleCarApi.TestHost
{
    public class CarApiTests : IClassFixture<TestFixture<StartupStub>>
	{
        private HttpClient Client;

        public CarApiTests(TestFixture<StartupStub> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task TestGetCarById()
        {
            // Arrange
            var request = "/api/cars/1";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        //CreateCar - создаем объект
        [Fact]
        public async Task TestPostCreateCarAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/cars",
                Body = new
                { 
                    Name = "Cadilac",
                    Description = "Black"
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            Car car = GetCar(value);

            // Assert
            Assert.Equal("Cadilac", car.Name);
            Assert.Equal("Black", car.Description);
            response.EnsureSuccessStatusCode();
        }

        //DeleteCar 
        [Fact]
        public async Task TestDeleteCarAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/cars/0", 
            };

            // Act
            var response = await Client.DeleteAsync(request.Url);
            var value = await response.Content.ReadAsStringAsync();

            // Assert 
            response.EnsureSuccessStatusCode();
        }

        //Апдейт поля Name по Id. Передаем только поле Name + Id
        [Fact]
        public async Task TestPostCarNameAsync()
        { 
            // Arrange
            var request = new
            {
                Url = "/api/cars",
                Body = new
                {
                   Id = 1,
                   Name = "Volvo"
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            Car car = GetCar(value);

            // Assert
            Assert.Equal("Volvo", car.Name);
            response.EnsureSuccessStatusCode();
        }

        //Апдейт поля Name по Id. Передаем только поле Name + Id
        [Fact]
        public async Task TestPostCarDescAsync()
        {
            // Arrange
            var request = new
            {
                Url = "/api/cars",
                Body = new
                {
                    Id = 1,
                    Description = (string)null
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();
            Car car = GetCar(value);
            // Assert
            Assert.Null(car.Description);
            response.EnsureSuccessStatusCode();
        }

        private Car GetCar(string value) 
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<Car>(value, options);
        }
    }
}
