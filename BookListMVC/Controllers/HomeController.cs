using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookListMVC.Models;
using System.Data.SqlClient;

namespace BookListMVC.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<Book> books = new List<Book>();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString = BookListMVC.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            FetchData();
            return View(books);
        }

        private void FetchData()
        {
            if (books.Count > 0)
            {
                books.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [Name],[Author],[ISBN] FROM[BookListMVC].[dbo].[Books]";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    books.Add(new Book()
                    {
                        Name = dr["Name"].ToString(),
                        Author = dr["Author"].ToString(),
                        ISBN = dr["ISBN"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
