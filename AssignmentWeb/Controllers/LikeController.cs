using Assignment.DataAccess.Repository.IRepository;
using Assignment.Models;
using Assignment.Utility;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;

namespace AssignmentWeb.Controllers
{
    public class LikeController : Controller
    {

        private readonly ILikeRepository _likeRepo;
        public LikeController(ILikeRepository likeRepo)
        {
            _likeRepo = likeRepo;
        }

        public IActionResult Index()
        {
           // List<Like> list = new List<Like>();
            //list.Add(new Like(1, "About", "Description"));
            //list.Add(new Like(2, "About2", "Description2"));
            //list.Add(new Like(3, "About3", "Description3"));
            List<Like> list = _likeRepo.GetAll();
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

            //  return RedirectToAction("Index");
            return View();
        }

        public IActionResult Edit(string? id)
        // public IActionResult EditView(Like like)
        {
            //if (id == null || id=="")
            if (String.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            //List<Like> list = new List<Like>();
            //list.Add(new Like(1, "About", "Description"));
            //list.Add(new Like(2, "About2", "Description2"));
            //list.Add(new Like(3, "About3", "Description3"));

            //Like? like = list.FirstOrDefault(x => x.Id==id);
            Like? like = _likeRepo.Get(id);

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
                //TODO: Save
                _likeRepo.Update(obj);
                TempData["success"] = $"Like Id {obj.Id} Updated successfully and Saved at {@SD.FolderPath}";
                return RedirectToAction("Index");
            }

            return View();
            
        }
    }
}
