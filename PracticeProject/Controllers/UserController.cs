using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeProject.Data;
using PracticeProject.Interface;
using PracticeProject.Migrations;
using PracticeProject.Models;
using PracticeProject.Repository;
using PracticeProject.Services;
using PracticeProject.ViewModels;

namespace PracticeProject.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IPhotoService _photoService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserController(IUserRepository userRepository, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IPhotoService photoService, ICourseRepository courseRepository)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
            _userManager = userManager;
            _photoService = photoService;
            _courseRepository = courseRepository;
        }

        [HttpGet("users")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsers();
            List<UserViewModel> result = new List<UserViewModel>();
            foreach (var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Grupa = user.GrupaId,
                    Image = user.ProfileImage ?? "/img/avatar-male-4.jpg",
                    RolesName = userRoles.ToList()
                };
                result.Add(userViewModel);
            }
            return View(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null)
            {
                return RedirectToAction("Index", "Users");
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var userDetailViewModel = new UserDetailViewModel()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Grupa = user.GrupaId,
                Email = user.Email,
                Image = user.ProfileImage ?? "/img/avatar-male-4.jpg",
                Roles = userRoles.ToList()
            };
            return View(userDetailViewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditProfile(string id)
        {
            //var user = await _userManager.GetUserAsync(User);
            var user = await _userRepository.GetByIdAsyncNoTracking(id);
            if (user == null)
            {
                return View("Error");
            }
            var userRoles = await _userManager.GetRolesAsync(user);

            var editMV = new EditProfileViewModel()
            {

                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Grupa = user.GrupaId,
                ProfileImageUrl = user.ProfileImage,
                Roles = userRoles.ToList()
            };


            var grups = _courseRepository.GetAllGrups();
            ViewBag.Grups = grups;
            var roles = _roleManager.Roles.ToList();
            ViewBag.Roles = roles;

            return View(editMV);
        }
        /*
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editVM)
        {
            /*
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditProfile", editVM);
            }
            *
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return View("Error");
            }

            if (editVM.Image != null) // only update profile image
            {
                var photoResult = await _photoService.AddPhotoAsync(editVM.Image);

                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Failed to upload image");
                    return View("EditProfile", editVM);
                }

                if (!string.IsNullOrEmpty(user.ProfileImage))
                {
                    _ = _photoService.DeletePhotoAsync(user.ProfileImage);
                }

                user.ProfileImage = photoResult.Url.ToString();
                editVM.ProfileImageUrl = user.ProfileImage;

                await _userManager.UpdateAsync(user);

                //return View(editVM);//Типо обновить фото и вернутся чтоб обновить другие данные
                return RedirectToAction("Detail", "User", new { user.Id });
            }

            /*Other data
            user.City = editVM.City;
            user.State = editVM.State;
            user.Pace = editVM.Pace;
            user.Mileage = editVM.Mileage;
            *
            await _userManager.UpdateAsync(user);

            return RedirectToAction("Detail", "User", new { user.Id });
        }
        */
        [HttpPost]
        [Authorize(Roles = "admin, Teacher")]
        public async Task<IActionResult> EditProfile(string id, EditProfileViewModel editVM, string selectedGrupa, string[] selectedRoles)
        {
            var user = await _userRepository.GetByIdAsyncNoTracking(id);
            if (user != null)
            {
                //Img upload
                string imgUrl = "";
                if (editVM.Image != null)
                {
                    try
                    {
                        if (user.ProfileImage != null)
                            await _photoService.DeletePhotoAsync(user.ProfileImage);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Could not delete photo");
                        return View(editVM);
                    }

                    var photoResult = await _photoService.AddPhotoAsync(editVM.Image);
                    imgUrl = photoResult.Url.ToString();
                }
                else
                    imgUrl = user.ProfileImage;


                //Other
                if (selectedGrupa != editVM.Grupa)
                    editVM.Grupa = selectedGrupa;
                /*
                foreach (var newItem in selectedRoles)
                {
                    editVM.Roles.Add(newItem);
                }
                */
                var User = new User
                { 
                    Id = id,
                    Name = editVM.Name,
                    Surname = editVM.Surname,
                    Email = editVM.Email,
                    GrupaId = editVM.Grupa,
                    ProfileImage = imgUrl
                };

                var userRoles = await _userManager.GetRolesAsync(user);

                var result = await _userManager.RemoveFromRolesAsync(User, userRoles.ToList());

                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(User, selectedRoles);
                    await _userManager.UpdateAsync(User);
                    return RedirectToAction("Detail", "User", new { user.Id });
                }


                return View(editVM);
            }

            return View(editVM);

            
        }
    }
}
