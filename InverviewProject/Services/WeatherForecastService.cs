using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InverviewProject.Services
{
    public class WeatherForecastService : IWeathereForecaseService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IConfiguration _configuration;

        public WeatherForecastService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<WeatherForecast>> GetForcastAsync()
        {
           return _configuration.GetSection("ForecastProviderType").Value == "remote" ?
                await GetForcastFromRemouteProviderAsync() :
                GetForcastFromLocalProvider();
        }

        private IEnumerable<WeatherForecast> GetForcastFromLocalProvider()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        private async Task<IEnumerable<WeatherForecast>> GetForcastFromRemouteProviderAsync()
        {
            
            var host = _configuration.GetSection("RemoteForecastProvider:host").Value;
            var password = _configuration.GetSection("RemoteForecastProvider:authenticationHeader").Value;
            if(password != "correct_password")
            {
                throw new Exception("incorrect credentials for api");
            }

            Thread.Sleep(1000);

            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });

            return await Task.FromResult(result);
        }
    }

    public interface IWeathereForecaseService
    {
        Task<IEnumerable<WeatherForecast>> GetForcastAsync();
    }
}
