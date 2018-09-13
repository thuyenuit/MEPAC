using MEPAC.Model.Models;
using MEPAC.Web.Controllers;
using MEPAC.Web.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MEPAC.Web.Provider
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public AuthorizationServerProvider()
        { }

        // đăng nhập -> gửi 1 requet lên server sẽ Validate tất cả request
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            await Task.FromResult<object>(null);
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            if (allowedOrigin == null)
                allowedOrigin = "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });
            UserManager<ApplicationUser> userManager = context.OwinContext.GetUserManager<UserManager<ApplicationUser>>();
            ApplicationUser user;
            try
            {
                MEPAC.Reportsitory.MEPACDbContext DbContext = new MEPAC.Reportsitory.MEPACDbContext();
                user = await userManager.FindAsync(context.UserName, context.Password);
            }
            catch
            {
                context.Rejected();
                context.SetError("server-error");
                context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không hợp lệ");
                return;
            }

            if (user != null)
            {
                /*ShopSMSDbcontext DbContext = new ShopSMSDbcontext();
                List<MenuGroup> lstResultMenu = DbContext.MenuGroup.ToList();
                List<Menu> lstMenu = DbContext.Menu.Where(x=>x.MenuStatus == true).ToList();

                List<ListGroupMenu> LstGroupMenu = new List<ListGroupMenu>();             
                ListGroupMenu objListGroupMenu = null;

                List<ListMenu> LstListMenu = null;
                ListMenu objListMenu = null;
                             
                foreach (var item in lstResultMenu)
                {
                    objListGroupMenu = new ListGroupMenu();                    
                    objListGroupMenu.MenuGroupName = item.MenuGroupName;

                    LstListMenu = new List<ListMenu>();
                    
                    var lstMenuDB = lstMenu.Where(x => x.MenuGroupID == item.MenuGroupID).ToList();

                    if (lstMenuDB.Count > 0)
                    {
                        foreach(var itemMe in lstMenuDB)
                        {
                            objListMenu = new ListMenu();
                            objListMenu.MenuName = itemMe.MenuName;
                            objListMenu.OrderBy = itemMe.MenuOrderBy;
                            objListMenu.Icon = itemMe.ImageURL;
                            LstListMenu.Add(objListMenu);
                        }
                    }
                    objListGroupMenu.ListMenu = LstListMenu;
                    LstGroupMenu.Add(objListGroupMenu);
                }*/

                ClaimsIdentity identity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ExternalBearer);

                string userCode = !string.IsNullOrEmpty(user.UserCode) ? user.UserCode : string.Empty;
                string email = !string.IsNullOrEmpty(user.Email) ? user.Email : string.Empty;
                string phoneNumber = !string.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : string.Empty;

                identity.AddClaim(new Claim("success", "true"));
                identity.AddClaim(new Claim("message", "Đăng nhập thành công"));
                identity.AddClaim(new Claim("userId", user.Id));
                identity.AddClaim(new Claim("fullName", user.FullName));
                identity.AddClaim(new Claim("userCode", userCode));
                identity.AddClaim(new Claim("email", email));
                identity.AddClaim(new Claim("phoneNumber", phoneNumber));

                var props = new AuthenticationProperties(new Dictionary<string, string> {
                        {"userId", user.Id },
                        {"fullName", user.FullName },
                        {"userCode", userCode },
                        {"email", email },
                        {"phoneNumber",phoneNumber },
                        {"success", "true" },
                        {"message", "Đăng nhập thành công" }
                    });

                AdminController.UserID = user.Id;
       
                UserInfoInstance.EmailInstance = email;
                UserInfoInstance.FullNameInstance = user.FullName;
                UserInfoInstance.PhoneInstance = phoneNumber;
                UserInfoInstance.UserCodeInstance = userCode;
                UserInfoInstance.UserNameInstance = user.UserName;
                //UserInfoInstance.UserIDInstance = user.Id;              
                context.Validated(new AuthenticationTicket(identity, props));
            }
            else
            {
                ClaimsIdentity identity2 = new ClaimsIdentity();
                identity2.AddClaim(new Claim("success", "false"));
                identity2.AddClaim(new Claim("message", "Tài khoản hoặc mật khẩu không đúng"));
                var props = new AuthenticationProperties(new Dictionary<string, string> {
                        {"success", "false" },
                        {"message", "Tài khoản hoặc mật khẩu không đúng" }
                    });
                context.Validated(new AuthenticationTicket(identity2, props));
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}