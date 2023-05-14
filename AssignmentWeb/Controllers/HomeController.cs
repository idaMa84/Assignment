using Assignment.Models;
using Assignment.Utility;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AssignmentWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _contextAccessor;



        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
           
            if (HttpContext.Request.GetDisplayUrl().Contains("code") && String.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("AccessToken")))
            _contextAccessor.HttpContext.Session.SetString("urlWithFacebookCode", HttpContext.Request.GetDisplayUrl());
          
            return View();
        }

        public IActionResult Privacy()
        {
            return Redirect("https://www.facebook.com/v6.0/dialog/oauth?client_id=978379916908004&redirect_uri=https%3A%2F%2Flocalhost%3A7061%2FHome%2F&state=987654321"); 
        }

        public IActionResult Likes()
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