using Assignment.DataAccess.Repository.IRepository;
using Assignment.Models;
using Assignment.Utility;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;

namespace AssignmentWeb.Controllers
{
    public class LikedPageController : Controller
    {

        private readonly ILogger<LikedPageController> _logger;
        private readonly ILikedPageRepository _likedPageRepo;
        private readonly IHttpContextAccessor _contextAccessor;

        public LikedPageController(ILogger<LikedPageController> logger, ILikedPageRepository likedPageRepo, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _likedPageRepo = likedPageRepo;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            try
            {
                List<LikedPage> list = new List<LikedPage>();
                string? accessToken = _contextAccessor.HttpContext.Session.GetString("AccessToken");
                string? urlWithFacebookCode = _contextAccessor.HttpContext.Session.GetString("UrlWithFacebookCode");

                if (String.IsNullOrEmpty(accessToken) && !String.IsNullOrEmpty(urlWithFacebookCode))
                {
                    accessToken = _likedPageRepo.GetFacebookAccessToken(urlWithFacebookCode);
                    _contextAccessor.HttpContext.Session.SetString("AccessToken", accessToken);
                }

                if (!String.IsNullOrEmpty(accessToken))
                    list = _likedPageRepo.GetAll(accessToken);

                return View(list);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return BadRequest();
            }
        }

        public IActionResult Edit(string? id)
        {
            try
            {
                if (String.IsNullOrEmpty(id))
                {
                    return NotFound();
                }

                string accessToken = _contextAccessor.HttpContext.Session.GetString("AccessToken");
                LikedPage? like = _likedPageRepo.Get(id, accessToken);

                if (like == null)
                {
                    return NotFound();
                }

                return View(like);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult Edit(LikedPage obj)
        {
            try
            {
                if (obj.About == obj.Description)
                {
                    ModelState.AddModelError("", "About Page cannot exactly match the Page Description");
                }

                if (ModelState.IsValid)
                {
                    _likedPageRepo.Update(obj);
                    TempData["success"] = $"Like Id {obj.Id} Updated successfully and Saved at {@SD.FolderPath}";
                    return RedirectToAction("Index");
                }

                return View();
            }           
            catch (Exception ex)
            {
                _logger.LogError("Error", ex);
                return BadRequest();
            }
        }

        public IActionResult Create()
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

        [HttpPost]
        public IActionResult Create(LikedPage obj)
        {
            try
            {
                if (obj.About == obj.Description)
                {
                    ModelState.AddModelError("", "About Page cannot exactly match the Page Description");
                }

                if (ModelState.IsValid)
                {
                    //TODO: Save
                }

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
