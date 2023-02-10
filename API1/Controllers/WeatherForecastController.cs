using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Controllers
{
    public class WeatherData
    {
        public int id { get; set; }
        public string date { get; set; }
        public int Degree { get; set; }
        public string Location { get; set; }
    }

    [ApiController]
    [Route("")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static List<WeatherData> weatherDatas = new()
        {
            new WeatherData() { id = 1, date = "21.01.2022", Degree = 10, Location = "Москва" },
            new WeatherData() { id = 2, date = "22.02.2022", Degree = 10, Location = "Питер" },
            new WeatherData() { id = 3, date = "23.01.2022", Degree = 10, Location = "Москва" },
            new WeatherData() { id = 4, date = "24.02.2022", Degree = 10, Location = "Омск" },
            new WeatherData() { id = 5, date = "25.02.2022", Degree = 10, Location = "Москва" },
            new WeatherData() { id = 6, date = "26.01.2022", Degree = 10, Location = "Ростов" },
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("Вся погода")]
        public List<WeatherData> GetAll()
        {
            return weatherDatas;
        }

        [HttpGet("Вывод данных о погоде")]
        public IActionResult GetById(int id)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
                if (weatherDatas[i].id == id)
                    return Ok(weatherDatas[i]);

            return BadRequest("Такой записи не найдено");
        }

        [HttpPost("Добавдление погоды")]
        public IActionResult AddWeather(WeatherData data)
        {
            if (data.id < 0)
            {
                return BadRequest("ID меньше 0");
            }

            for (int i = 0; i < weatherDatas.Count; i++)
                if (weatherDatas[i].id == data.id | weatherDatas[i].id < 0)
                    return BadRequest("Такой ID уже есть или он меньше 0");

            weatherDatas.Add(data);
            return Ok(GetAll());
        }

        [HttpPut("Изменить погоду")]

        public IActionResult UpdateWeather(WeatherData data)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
                if (weatherDatas[i].id == data.id)
                {
                    weatherDatas[i] = data;
                    return Ok(GetAll());
                }
            return BadRequest("Такая запись не обнаружена");
        }

        [HttpDelete("Удаляемя запись о погоде")]

        public IActionResult DeleteWeather(int id)
        {
            if (id < 0)
            {
                return BadRequest("ID меньше 0");
            }
            for (int i = 0; i < weatherDatas.Count; i++)
                if (weatherDatas[i].id == id)
                {
                    weatherDatas.RemoveAt(id);
                    return Ok(GetAll());
                }
            return BadRequest("Такая запись не обнаружена или попытка удалить значение меньше нуля");
        }

        [HttpGet("Поиск по городу")]
        public IActionResult City(string city)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
                if (weatherDatas[i].Location == city)
                    return Ok("Такой город есть в нашем списке");

            return BadRequest("Такой записи не найдено");
        }

        // Старая часть 

        [HttpGet("Все Summaries")]
        public List<string> Get()
        {
            return Summaries;
        }

        [HttpPost("Добваить имя")]
        public IActionResult Add(string name)
        {
            if (name == null)
            {
                return BadRequest("Напиши какое-то имя");
            }

            Summaries.Add(name);
            return Ok(Get());
        }

        [HttpPut("Сменить имя")]
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

        [HttpDelete("Удалить имя")]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index > Summaries.Count)
            {
                return BadRequest("Покажи мне такой элемент и я его удалю");
            }

            Summaries.RemoveAt(index);
            return Ok(Get());
        }

        [HttpGet("Поиск имени по индексу")]

        public string Select(int index)
        {
            if (index < 0 || index > Summaries.Count)
            {
                return null;
            }

            return Summaries[index];
        }

        [HttpGet("Поиск по имени")]

        public int Skolko(string name)
        {
            int sum = 0;
            for (int i = 0; i < Summaries.Count; i++)
                if (Summaries[i] == name)
                    sum++;
            return sum;
        }

        []
    }
}
