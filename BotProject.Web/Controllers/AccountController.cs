using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BotProject.Model.Models;
using BotProject.Web.Models;
using AutoMapper;
using BotProject.Web.Infrastructure.Core;
using BotProject.Service;
using BotProject.Common;
using System.Collections.Generic;
using BotProject.Model.Models.LiveChat;

namespace BotProject.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IApplicationGroupService _appGroupService;
        private IChannelService _channelService;
        public AccountController(ApplicationUserManager userManager,
                                 ApplicationSignInManager signInManager,
                                 IChannelService channelService,
                                 IApplicationGroupService appGroupService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _channelService = channelService;
            _appGroupService = appGroupService;
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
        public AccountController()
        {
        }

        

        // GET: Account
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
			// check login with remember me
			if (Request.IsAuthenticated)
			{
                string userId = User.Identity.GetUserId();
                if(String.IsNullOrEmpty(userId))
                {
                    LogOut();
                }
                ApplicationUser user = _userManager.FindById(userId);
                var applicationUserViewModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);
                var listGroup = _appGroupService.GetListGroupByUserId(applicationUserViewModel.Id);
                applicationUserViewModel.Groups = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(listGroup);

                if(applicationUserViewModel.Groups.Count() == 0)
                {
                    LogOut();
                }

                applicationUserViewModel.Channels = _channelService.GetChannelByUserId(user.Id);

                Session[CommonConstants.SessionUser] = applicationUserViewModel;

                if (!String.IsNullOrEmpty(returnUrl))
                    return Redirect(returnUrl);


                return RedirectToAction("Index", "Dashboard");
			}

			return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    var applicationUserViewModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);

                    var listGroup = _appGroupService.GetListGroupByUserId(applicationUserViewModel.Id);
                    applicationUserViewModel.Groups = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(listGroup);
                    if (applicationUserViewModel.Groups.Count() == 0)
                    {
                        //await _userManager.AddToRolesAsync(newUser.Id, new string[] { "User" });
                        // Permission add user to group owner
                        var appUserGroup = new ApplicationUserGroup();
                        appUserGroup.UserId = applicationUserViewModel.Id;
                        appUserGroup.GroupId = CommonConstants.GROUP_OWNER;
                        _appGroupService.AddUserToGroups(appUserGroup);
                        _appGroupService.Save();

                        listGroup = _appGroupService.GetListGroupByUserId(applicationUserViewModel.Id);

                        // Livechat add user owner group channel
                        var channelGroup = new ChannelGroup();
                        channelGroup.Name = "General-" + user.UserName;
                        channelGroup.OwnerId = applicationUserViewModel.Id;
                        _channelService.AddChannelGroup(channelGroup);
                        _channelService.Save();

                        //Livechat add user to channel
                        var lc_Channel = new Channel();
                        lc_Channel.ChannelGroupID = channelGroup.Id;
                        lc_Channel.UserID = applicationUserViewModel.Id;
                        _channelService.AddUserToChannel(lc_Channel);
                        _channelService.Save();
                    }

                    applicationUserViewModel.Groups = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(listGroup);
                    applicationUserViewModel.Channels = _channelService.GetChannelByUserId(user.Id);

                    Session[CommonConstants.SessionUser] = applicationUserViewModel;

                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties props = new AuthenticationProperties();
                    props.IsPersistent = model.RememberMe;
                    authenticationManager.SignIn(props, identity);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> LoginSocial(LoginSocialViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _userManager.FindByEmail(model.Email);
                if (user == null)
                {
                    var userInfo = new ApplicationUser()
                    {
                        UserName = model.Email,
                        Email = model.Email,
                        EmailConfirmed = true,
                        FullName = model.UserName,
                        Avatar = model.Avatar
                    };
                    var result = await _userManager.CreateAsync(userInfo);
                    if (result.Succeeded)
                    {
                        user = await _userManager.FindByEmailAsync(model.Email);
                        if (user != null)
                            await _userManager.AddToRolesAsync(user.Id, new string[] { "User" });
                    }
                    else
                    {
                        var errors = result.Errors;
                        var message = string.Join(", ", errors);
                        ModelState.AddModelError("", message);
                    }
                }

                var applicationUserViewModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(user);
                Session[CommonConstants.SessionUser] = applicationUserViewModel;
                IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                ClaimsIdentity identity = _userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                AuthenticationProperties props = new AuthenticationProperties();
                props.IsPersistent = false;
                authenticationManager.SignIn(props, identity);
                if (Url.IsLocalUrl(returnUrl))
                {
                }
                else
                {
                    returnUrl = "Dashboard/Index";
                }
                return Json(new
                {
                    returnUrl = returnUrl,
                    status = true
                });
            }
            return Json(new
            {
                status = false
            });
        }


        [HttpPost]
        public JsonResult UpdatePassword(string userId, string passwordCurrent, string passwordNew)
        {
            var userDb = _userManager.FindById(userId);
            var checkUser = _userManager.Find(userDb.UserName, passwordCurrent);
            if (checkUser != null)
            {
                _userManager.RemovePassword(userId);
                _userManager.AddPassword(userId, passwordNew);
            }
            else
            {
                return Json(new
                {
                    message = "Mật khẩu cũ không đúng",
                    status = false
                });
            }
            return Json(new
            {
                message = "Chỉnh sửa thành công",
                status = true
            });
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {

            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userByUserName = await _userManager.FindByNameAsync(model.UserName);
                if (userByUserName != null)
                {
                    ModelState.AddModelError("UserName", "Tài khoản đã tồn tại");
                    return View(model);
                }

                var userByEmail = await _userManager.FindByEmailAsync(model.Email);
                if (userByEmail != null)
                {
                    ModelState.AddModelError("Email", "Email này đã được đăng ký");
                    return View(model);
                }

                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    EmailConfirmed = false,
                    BirthDay = DateTime.Now,
                    FullName = model.FullName,
                    //PhoneNumber = model.PhoneNumber,
                    Address = model.Address

                };

                await _userManager.CreateAsync(user, model.Password);
                var newUser = await _userManager.FindByEmailAsync(model.Email);
                if (newUser != null)
                {
                    //await _userManager.AddToRolesAsync(newUser.Id, new string[] { "User" });
                    // Permission add user to group owner
                    var appUserGroup = new ApplicationUserGroup();
                    appUserGroup.UserId = newUser.Id;
                    appUserGroup.GroupId = CommonConstants.GROUP_OWNER;
                    _appGroupService.AddUserToGroups(appUserGroup);
                    _appGroupService.Save();

                    // Livechat add user owner group channel
                    var channelGroup = new ChannelGroup();
                    channelGroup.Name = "General-" + user.UserName;
                    channelGroup.OwnerId = newUser.Id;
                    _channelService.AddChannelGroup(channelGroup);
                    _channelService.Save();

                    //Livechat add user to channel
                    var lc_Channel = new Channel();
                    lc_Channel.ChannelGroupID = channelGroup.Id;
                    lc_Channel.UserID = newUser.Id;
                    _channelService.AddUserToChannel(lc_Channel);
                    _channelService.Save();
                }
                return RedirectToAction("Login");
            }

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login", "Account");
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