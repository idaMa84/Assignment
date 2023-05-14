using Assignment.DataAccess.Repository.IRepository;
using Assignment.Models;
using Assignment.Utility;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentWeb.Controllers
{
    public class LikeController : Controller
    {

        private readonly ILikeRepository _likeRepo;
        private readonly IHttpContextAccessor _contextAccessor;

        public LikeController(ILikeRepository likeRepo, IHttpContextAccessor contextAccessor)
        {
            _likeRepo = likeRepo;
            _contextAccessor = contextAccessor;
        }      

        public IActionResult Index()
        {
            List<Like> list = new List<Like>();
            string? accessToken=string.Empty;

            if (String.IsNullOrEmpty(SD.UrllWithFacebookCode) && !String.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("urlWithFacebookCode")))        
                SD.UrllWithFacebookCode = _contextAccessor.HttpContext.Session.GetString("urlWithFacebookCode");

            if (String.IsNullOrEmpty(_contextAccessor.HttpContext.Session.GetString("AccessToken")))
            {
                accessToken = _likeRepo.GetAccessToken();
                _contextAccessor.HttpContext.Session.SetString("AccessToken", accessToken);
            }
            else
                accessToken = _contextAccessor.HttpContext.Session.GetString("AccessToken");

            if (!String.IsNullOrEmpty(accessToken))
                list = _likeRepo.GetAll(accessToken);
           
            return View(list);
        }

        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Like obj)
        {
            if (obj.About == obj.Description) {
                ModelState.AddModelError("", "About Page cannot exactly match the Page Description");
            }

            if (ModelState.IsValid)
            {
                //TODO: Save
            }
            
            return View();
        }

        public IActionResult Edit(string? id)     
        {            
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }
                       
            string accessToken = _contextAccessor.HttpContext.Session.GetString("AccessToken");
            Like? like = _likeRepo.Get(id, accessToken);

            if (like == null)
            {
                return NotFound();
            }

            return View(like);
        }

        [HttpPost]
        public IActionResult Edit(Like obj)
        {
            if (obj.About == obj.Description)
            {
                ModelState.AddModelError("", "About Page cannot exactly match the Page Description");
            }

            if (ModelState.IsValid)
            {               
                _likeRepo.Update(obj);
                TempData["success"] = $"Like Id {obj.Id} Updated successfully and Saved at {@SD.FolderPath}";
                return RedirectToAction("Index");
            }

            return View();            
        }
    }
}
