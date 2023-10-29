using atelier3.Models;
using atelier3.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace atelier3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StudentController : Controller
    {
        private IStudentRepository StudentRepository;
        private ISchoolRepository schoolRepository;
        public StudentController(IStudentRepository StudentRepository  , ISchoolRepository schoolRepository)
        {
            this.StudentRepository = StudentRepository;
            this.schoolRepository = schoolRepository;
        }
        [AllowAnonymous]

        // GET: StudentController
        public ActionResult Index()
        {
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            var model = StudentRepository.GetAll();
            return View(model);
        }

        // GET: StudentController/Details/5
        public ActionResult Details(int id)
        {
            var model = StudentRepository.GetById(id);
            return View(model);
        }

        // GET: StudentController/Create
        public ActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(),"SchoolID","SchoolName"); 
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student collection)
        {
            try
            {
                ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName" , collection.SchoolID);
                StudentRepository.Add(collection);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            var sh = StudentRepository.GetById(id);
            if (sh == null)
            {
                return NotFound();
            }
            return View(sh);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student collection)
        {
            try
            {
                ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName", collection.SchoolID);
                StudentRepository.Edit(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentController/Delete/5
        public ActionResult Delete(int id)
        {
            var s = StudentRepository.GetById(id);
            return View(s);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Student collection)
        {
            try
            {
                StudentRepository.Delete(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Search(string name, int? schoolid)
        {
            var result = StudentRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = StudentRepository.FindByName(name);
            else
            if (schoolid != null)
                result = StudentRepository.GetStudentsBySchoolID(schoolid);
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View("Index", result);
        }

    }
}
