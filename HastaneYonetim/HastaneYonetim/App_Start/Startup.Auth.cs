using System;
using HastaneYonetim.Core.Models;
using HastaneYonetim.Models;
using HastaneYonetim.Persistence;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace HastaneYonetim
{
    public partial class Startup
    {
    
        public void ConfigureAuth(IAppBuilder app)
        {
     
            app.CreatePerOwinContext(UygulamaDbContext.Olustur);
            app.CreatePerOwinContext<UygulamaKullaniciYoneticisi>(UygulamaKullaniciYoneticisi.Olustur);
            app.CreatePerOwinContext<UygulamaOturumAcmaYoneticisi>(UygulamaOturumAcmaYoneticisi.Olustur);

     
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Hesap/Giris"),
                Provider = new CookieAuthenticationProvider
                {
              
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UygulamaKullaniciYoneticisi, UygulamaKullanici>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

          
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

   
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);


        }
    }
}