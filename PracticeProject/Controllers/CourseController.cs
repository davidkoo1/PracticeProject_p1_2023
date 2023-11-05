using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeProject.Data;
using PracticeProject.Data.Enum;
using PracticeProject.Interface;
using PracticeProject.Models;
using PracticeProject.ViewModels;

namespace PracticeProject.Controllers
{
    public class CourseController : Controller
    {

        private readonly ICourseRepository _courseRepository;
        private readonly IPhotoService _photoservice;
        private readonly RoleManager<IdentityRole> _roleManager;
        public CourseController(ICourseRepository courseRepository, IPhotoService photoService, RoleManager<IdentityRole> roleManager)
        {
            _courseRepository = courseRepository;
            _photoservice = photoService;
            _roleManager = roleManager;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("admin"))
            {
                var coursesUser = await _courseRepository.GetAllCourseByUserGrupa();
                return View(coursesUser);
            }
            var courses = await _courseRepository.GetAllCourse();
            return View(courses);
        }

        [Authorize(Roles = "admin,Teacher")]
        public async Task<IActionResult> MyCourses()
        {
            var coursesUser = await _courseRepository.GetAllCourseByUser();
            return View(coursesUser);
        }


        [Authorize(Roles = "admin,Teacher")]
        public IActionResult Create()
        {
            var grups = _courseRepository.GetAllGrups();
            ViewBag.Grups = grups;
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "admin,Teacher")]
        public async Task<IActionResult> Create(CreateCourseViewModel courseVM, string[] selectedGrups, string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoservice.AddPhotoAsync(courseVM.Image);


                courseVM.CourseGrupas = new List<CourseGrupa>();
                for (int i = 0; i < selectedGrups.Length; i++)
                {
                    CourseGrupa tmp = new CourseGrupa()
                    {
                        IdGrupa = selectedGrups[i]
                    };
                    courseVM.CourseGrupas.Add(tmp);
                }

                if(selectedRoles != null)
                {
                    courseVM.CourseRoles = new List<CourseRole>();
                    for (int i = 0; i < selectedRoles.Length; i++)
                    {
                        CourseRole tmp = new CourseRole()
                        {
                            RoleId = selectedRoles[i]
                        };
                        courseVM.CourseRoles.Add(tmp);
                    }

                }

                Course course = new Course()
                {
                    Name = courseVM.Name,
                    IsOpen = courseVM.IsOpen,
                    Image = result.Url.ToString(),
                    courseGrupas = courseVM.CourseGrupas,
                    courseRoles = courseVM.CourseRoles,
                    User = await _courseRepository.GetUser(User.GetUserId().ToString()) //Над исправить(долго грузит мб?)(типо присвоит Id)
                };

                _courseRepository.Add(course);
                return RedirectToAction("MyCourses", "Course");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }

            return View(courseVM);

        }


        //!!User transfer data and in view Hiden
        [Authorize(Roles = "admin,Teacher")]
        public async Task<IActionResult> Edit(int id)
        {

            var grups = _courseRepository.GetAllGrups();
            ViewBag.Grups = grups;
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;
            var course = await _courseRepository.GetByIdAsync(id);


            if(course == null)
                return View("Error");
            var courseVM = new EditCourseViewModel
            {
                Name= course.Name,
                URL = course.Image,
                IsOpen = course.IsOpen,
                CourseGrupas = course.courseGrupas.ToList(),
                CourseRoles = course.courseRoles.ToList()
            };

            return View(courseVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin,Teacher")]
        public async Task<IActionResult> Edit(int id, EditCourseViewModel courseVM, string[] selectedGrups, string[] selectedRoles)
        {
            /*if (courseVM.CourseGrupas.Any())
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Failed to edit course");
                    return View("Edit", courseVM);
                }
            }*/


            var courseEdit = await _courseRepository.GetByIdAsyncNoTracking(id);
            if (courseEdit != null)
            {
                string imgUrl = "";
                if (courseVM.Image != null)
                {
                    try
                    {
                        if (courseEdit.Image != null)
                            await _photoservice.DeletePhotoAsync(courseEdit.Image);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Could not delete photo");
                        return View(courseVM);
                    }

                    var photoResult = await _photoservice.AddPhotoAsync(courseVM.Image);
                    imgUrl = photoResult.Url.ToString();
                }
                else
                    imgUrl = courseEdit.Image;



                foreach (var newItem in selectedGrups)
                {
                    CourseGrupa newCourseGrupa = new CourseGrupa()
                    {
                        IdCourse = id,
                        IdGrupa = newItem,
                    };
                    courseVM.CourseGrupas.Add(newCourseGrupa);
                }

                foreach (var newItem in selectedRoles)
                {
                    CourseRole newCourseRole = new CourseRole()
                    {
                        IdCourse = id,
                        RoleId = newItem,
                    };
                    courseVM.CourseRoles.Add(newCourseRole);
                }
                var course = new Course
                {
                    Id = id,
                    Name = courseVM.Name,
                    IsOpen = courseVM.IsOpen,
                    Image = imgUrl,
                    courseGrupas = courseVM.CourseGrupas,
                    courseRoles = courseVM.CourseRoles
                };

                _courseRepository.Update(course);

                return RedirectToAction("MyCourses", "Course");
            }

            return View(courseVM);

        }


        [Authorize(Roles = "admin,Teacher")]
        public async Task<IActionResult> Delete(int id)
        {
            var courseDetails = await _courseRepository.GetByIdAsync(id);
            if (courseDetails == null)
                return View("Error");
            return View(courseDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var courseDetails = await _courseRepository.GetByIdAsync(id);
            if (courseDetails == null)
                return View("Error");

            _courseRepository.Delete(courseDetails);
            return RedirectToAction("MyCourses", "Course");
        }
    }
}
