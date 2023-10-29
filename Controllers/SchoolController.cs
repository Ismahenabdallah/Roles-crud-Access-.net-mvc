using atelier3.Models;
using atelier3.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace atelier3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]

    public class SchoolController : Controller
         
    {
        private ISchoolRepository schoolRepository;
        public SchoolController(ISchoolRepository schoolRepository)
        {
            this.schoolRepository = schoolRepository;
        }
        [AllowAnonymous]
        // GET: SchoolController
        public ActionResult Index()
        {
            var model = schoolRepository.GetAll();
            return View(model);
        }

        // GET: SchoolController/Details/5
        public ActionResult Details(int id)
        {
            var model = schoolRepository.GetById(id);
            return View(model);
        }

        // GET: SchoolController/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: SchoolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(School s)
        {
            try
            {
                schoolRepository.Add(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SchoolController/Edit/5
        public ActionResult Edit(int id)
        {
            var sh = schoolRepository.GetById(id);
            if (sh == null)
            {
                return NotFound(); 
            }
            return View(sh);
        }

        // POST: SchoolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, School s)
        {
            try
            {
                schoolRepository.Edit(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }


        }

        // GET: SchoolController/Delete/5
        public ActionResult Delete(int id)
        {
            var s = schoolRepository.GetById(id);
            return View(s);
        }

        // POST: SchoolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, School s)
        {
            try
            {
                schoolRepository.Delete(s);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
