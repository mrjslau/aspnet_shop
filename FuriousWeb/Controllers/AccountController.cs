using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using FuriousWeb.Models;
using FuriousWeb.Models.ViewModels;
using FuriousWeb.Data;
using System.Linq;
using System;
using System.Data.Entity;

namespace FuriousWeb.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [AllowAnonymous]
        public ActionResult Profile()
        {
            bool loggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (loggedIn)
            {
                var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                ProfileViewModel profile = new ProfileViewModel();
                profile.UserID = user;
                var orders = db.Orders
                   .Where(b => b.UserID == user).ToList();
                profile.Orders = orders;
                return View("../Store/Account/OrderHistory", profile);
            }
            else
            {
                var url = this.Url.Action("Profile", "Account");
                TempData["redirectTo"] = url;
                return RedirectToAction("login", "Account");
            }
        }


        [AllowAnonymous]
        public ActionResult Home()
        {
            bool loggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (loggedIn)
            {
                return View("../Store/Account/Home");
            }
            else
            {
                var url = this.Url.Action("Home", "Account");
                TempData["redirectTo"] = url;
                return RedirectToAction("login", "Account");
            }
        }

        [AllowAnonymous]
        public ActionResult EditProfileView(string message)
        {
            bool loggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (loggedIn)
            {
                var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                ProfileViewModel profile = new ProfileViewModel();
                profile.UserID = user;
                profile.User = db.Users.Find(user);
                if (message != null)
                    ViewBag.Message = message;
                return View("../Store/Account/EditProfile", profile);
            }
            else
            {
                var url = this.Url.Action("EditProfileView", "Account");
                TempData["redirectTo"] = url;
                return RedirectToAction("login", "Account");
            }
        }

        [AllowAnonymous]
        public ActionResult SubmitEditProfile()
        {
            var userID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            User user = db.Users.Find(userID);

            user.Name = Request.Form["name"];
            user.Lastname = Request.Form["lastname"];
            user.Email = Request.Form["email"];
            user.Phone = Request.Form["phone"];
            user.Address = Request.Form["address"];

            db.Entry(user).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                ViewBag.Error = "Ši paskyra yra redaguojama.";
                //return View("EditProfile");
                return RedirectToAction("EditProfileView", "Account", new { message = "Ši paskyra yra redaguojama." });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex;
                return RedirectToAction("EditProfileView", "Account", new { message = ex });
                //return View("EditProfile");
            }
            ViewBag.Success = "Profilis atnaujintas";
            ViewBag.Error = null;
            return RedirectToAction("EditProfileView", "Account", new { message = "Profilis atnaujintas" });
            //return View("EditProfile");
        }

        [AllowAnonymous]
        public ActionResult Order(int id)
        {
            bool loggedIn = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            
            if (loggedIn)
            {
                var user = System.Web.HttpContext.Current.User.Identity.GetUserId();
                ProfileViewModel profile = new ProfileViewModel();
                profile.UserID = user;
                try
                {
                    var order = db.Orders.Where(b => b.UserID == user && b.ID == id).First();
                    profile.Order = order;
                    var orderDetails = db.OrderDetails.Where(b => b.OrderID == id).ToList();
                    profile.OrderDetails = orderDetails;
                    if (orderDetails == null)
                    {
                        return View("../Store/Account/404");
                    }
                    else
                    {
                        ViewBag.order = order;
                        return View(profile);
                    }
                }
                catch(Exception ex)
                {
                    return View("../Store/Account/404");
                }
            }
            else
            {
                return View("../Store/Account/404");
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if(TempData["redirectTo"] != null)
            {
                ViewBag.ReturnUrl = TempData["redirectTo"];
                TempData.Keep();
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    ModelState.AddModelError("", "Jūsų paskyra buvo užblokuota");
                    return View(model);
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Neteisingai įvesti prisijungimo duomenys!");
                    return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email, Phone = model.Phone, Address = model.Address, Name = model.Name, Lastname = model.Lastname };
                /*user.Address = model.Address;
                user.Phone = model.Phone;*/
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    SignInManager.UserManager.AddToRole(user.Id, "User");

                    if(TempData["redirectTo"] != null)               
                        return Redirect(TempData["redirectTo"].ToString());
                    
                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult BanUser(string id, bool ban)
        {
            var user = UserManager.FindById(id);
            if (ban)
            {
                UserManager.SetLockoutEnabled(user.Id, true);
                UserManager.SetLockoutEndDate(user.Id, new DateTime(9999, 12, 30));
                db.SaveChanges();
            }
            else
            {
                UserManager.SetLockoutEnabled(user.Id, false);
            }
            return RedirectToAction("GetUsersListForAdmin", "Admin", new { isPartial = false, query = "", currentPage = 1});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}