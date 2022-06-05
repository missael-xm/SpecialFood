using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpecialFood.Models;
using System.Net.Mail;

namespace SpecialFood.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

     public IActionResult Platos()
        {
            return View();
        }

        public IActionResult Receta()
        {
            return View();
        }

        public IActionResult Sangrecita()
        {
            return View();
        }

        public IActionResult Jugo()
        {
            return View();
        }

        public IActionResult Tallarines()
        {
            return View();
        }

        public IActionResult Torrejita()
        {
            return View();
        }

        public IActionResult Mazamorra()
        {
            return View();
        }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
