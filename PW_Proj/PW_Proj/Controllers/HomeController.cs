using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PW_Proj.Models;
using PW_Proj.Models.ProductModel;
using PW_Proj.ViewModels;

namespace PW_Proj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProductRepository _productRepository;

        public object HomeDetailsViewModels { get; private set; }

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository,
                                IHostingEnvironment hostingEnvironment, IProductRepository productRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _hostingEnvironment = hostingEnvironment;
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var model = _userRepository.GetAllUsers();
            return View(model);
        }

        public ViewResult GetAllUsers()
        {
            var model = _userRepository.GetAllUsers();
            return View(model);
        }

        public ViewResult GetAllProducts()
        {
            var model = _productRepository.GetAllProducts();
            return View(model);
        }

        public ViewResult Details(int? id)
        {
            User user = _userRepository.GetUser(id.Value);

            if (user == null)
            {
                Response.StatusCode = 404;
                return View("UserNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModels = new HomeDetailsViewModel()
            {
                User = user,
                PageTitle = "User Details"
            };

            return View(homeDetailsViewModels);
        }

        public ViewResult ProductDetails(int? id)
        {
            Product product = _productRepository.GetProduct(id.Value);

            if (product == null)
            {
                Response.StatusCode = 404;
                return View("ProductNotFound", id.Value);
            }

            HomeProductDetailsViewModel homeProductDetailsViewModel = new HomeProductDetailsViewModel()
            {
                Product = product,
                PageTitle = "Product Details"
            };

            return View(homeProductDetailsViewModel);
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            User user = _userRepository.GetUser(id);
            UserEditViewModel userEditViewModel = new UserEditViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Gender = user.Gender,
                Password = user.Password,
                CofirmPassword = user.CofirmPassword,
                ExistingPhotoPath = user.PhotoPath
            };
            return View(userEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _userRepository.GetUser(model.Id);
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.Password = model.Password;
                user.Gender = model.Gender;
                user.CofirmPassword = model.CofirmPassword;
                if (model.Photo != null)
                {
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    user.PhotoPath = ProcessUploadedFile(model);
                }

                _userRepository.Update(user);
                return RedirectToAction("details", new { id = user.Id });
            }

            return View();
        }

        private string ProcessUploadedFile(UserCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        private string ProcessProductUploadedFile(ProductCreateViewModel model)
        {
            string uniqueFileName = null;
            if(model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [HttpGet]
        public ViewResult CreateAccount()
        {
            return View();
        }

        [HttpGet]
        public ViewResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = ProcessProductUploadedFile(model);
                Product newProduct = new Product
                { 
                    ProductName = model.ProductName,
                    ProductCount = model.ProductCount,
                    PhotoPath = uniqueFileName
                };

                _productRepository.AddProduct(newProduct);
                return RedirectToAction("productdetails", new { id = newProduct.Id });
            }

            ;
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                User newUser = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Gender = model.Gender,
                    Password = model.Password,
                    CofirmPassword = model.CofirmPassword,
                    PhotoPath = uniqueFileName
                };

                _userRepository.AddUser(newUser);
                return RedirectToAction("details", new { id = newUser.Id });
            }

            return View();
        }

        public IActionResult Privacy()
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
