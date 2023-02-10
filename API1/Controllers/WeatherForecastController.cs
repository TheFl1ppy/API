using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;   
        }

        [HttpPost]
        public  IActionResult Add(string name)
        {
            if(name == null)
            {
                return BadRequest("Напиши какое-то имя");
            }

            Summaries.Add(name);
            return Ok(Get());
        }

        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index > Summaries.Count)
            {
                return BadRequest("Покажи мне такой элемент и я его изменю");
            }

            if (name == null)
            {
                return BadRequest("name не должно быть пустым");
            }

            Summaries[index] = name;
            return Ok(Get());
        }
            
        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index > Summaries.Count)
            {
                return BadRequest("Покажи мне такой элемент и я его удалю");
            }

            Summaries.RemoveAt(index);
            return Ok(Get());
        }

        [HttpGet("{index}")]

        public string Select(int index)
        {
            if (index < 0 || index > Summaries.Count)
            {
                return null;
            }

            string ans = Summaries[index];

            return ans;
        }
    }
}
