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
            try
            {
                //In case access token is not present we will store redirected url in order to extract CODE
                if (HttpContext.Request.GetDisplayUrl().Contains("code") && String.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("AccessToken")))
                    _contextAccessor.HttpContext.Session.SetString("UrlWithFacebookCode", HttpContext.Request.GetDisplayUrl());

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return BadRequest();
            }
        }

        public IActionResult AuthFacebook()
        {
            try
            {
                //Redirect to Facebook in order to get CODE needed to obtain the access token
                return Redirect("https://www.facebook.com/v6.0/dialog/oauth?client_id=978379916908004&redirect_uri=https%3A%2F%2Flocalhost%3A7061%2FHome%2F&state=987654321");
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return BadRequest();
            }
        }

        public IActionResult LikedPages()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return BadRequest();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            try
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return BadRequest();
            }
        }

    }
}