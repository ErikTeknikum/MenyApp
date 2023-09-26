using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using static System.Reflection.Metadata.BlobBuilder;

namespace MenyApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodayFoodController : Controller
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;uid=root;pwd=;database=foodmeny_app");
        [HttpGet]
        public ActionResult<List<TodayFood>> GetTodayFood()
        {
            List<TodayFood> todayFoods = new List<TodayFood>();

            try
            {
                connection.Open();
                MySqlCommand query = connection.CreateCommand();
                query.Prepare();
                query.CommandText = "SELECT date.Week, date.Day, date.Month, food.TextFood FROM date, food, todayfood WHERE todayfood.FoodID = food.ID AND todayfood.DateID = date.ID";
                MySqlDataReader data = query.ExecuteReader();
                while (data.Read())
                {
                    TodayFood todayFood = new TodayFood();
                    todayFood.TextFood = data.GetString("TextFood");
                    todayFood.dateDay = data.GetInt32("Day");
                    todayFood.dateMonth = data.GetInt32("Month");
                    todayFood.Week = data.GetString("Week");
                    todayFoods.Add(todayFood);

                }
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok(todayFoods);

        }
    }
}