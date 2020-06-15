using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HastaneYonetim.Core.Models;
using HastaneYonetim.Persistence;
using HastaneYonetim.Models;
using Microsoft.AspNetCore.Http.Authentication;
using Ninject.Injection;


namespace HastaneYonetim
{
    // Configure the  rolemanager used in the application.  Rolemanager is defined in asp.net identity core assembly
    public class UygulamaRolYöneticisi : RoleManager<IdentityRole>
    {
        public UygulamaRolYöneticisi(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore) { }
        public static UygulamaRolYöneticisi Olustur(IdentityFactoryOptions<UygulamaRolYöneticisi> options, IOwinContext context)
        {
            return new UygulamaRolYöneticisi(new RoleStore<IdentityRole>(context.Get<UygulamaDbContext>()));
        }
    }

    public class EpostaServisi : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage mesaj)
        {
  
            return Task.FromResult(0);
        }
    }

    public class SmsServisi : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
     
            return Task.FromResult(0);
        }
    }


    public class UygulamaKullaniciYoneticisi : UserManager<UygulamaKullanici>
    {

        public UygulamaKullaniciYoneticisi(IUserStore<UygulamaKullanici> store)
            : base(store)
        {

        }

        public static UygulamaKullaniciYoneticisi Olustur(IdentityFactoryOptions<UygulamaKullaniciYoneticisi> options, IOwinContext context)
        {
            var yonetici = new UygulamaKullaniciYoneticisi(new UserStore<UygulamaKullanici>(context.Get<UygulamaDbContext>()));
  
            yonetici.UserValidator = new UserValidator<UygulamaKullanici>(yonetici)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };


            yonetici.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

      
            yonetici.UserLockoutEnabledByDefault = true;
            yonetici.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            yonetici.MaxFailedAccessAttemptsBeforeLockout = 5;

         
            yonetici.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<UygulamaKullanici>
            {
                MessageFormat = "Your security code is {0}"
            });
            yonetici.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<UygulamaKullanici>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            yonetici.EmailService = new EpostaServisi();
            yonetici.SmsService = new SmsServisi();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                yonetici.UserTokenProvider =
                    new DataProtectorTokenProvider<UygulamaKullanici>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return yonetici;
        }
    }

  
    public class UygulamaOturumAcmaYoneticisi : SignInManager <UygulamaKullanici, string>
    {
        public UygulamaOturumAcmaYoneticisi(UygulamaKullaniciYoneticisi kullaniciYoneticisi, IAuthenticationManager authenticationManager)
            : base(kullaniciYoneticisi, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(UygulamaKullanici kullanici)
        {
            return kullanici.GenerateUserIdentityAsync((UygulamaKullaniciYoneticisi)UserManager);
        }

        public static UygulamaOturumAcmaYoneticisi Olustur(IdentityFactoryOptions<UygulamaOturumAcmaYoneticisi> options, IOwinContext context)
        {
            return new UygulamaOturumAcmaYoneticisi(context.GetUserManager<UygulamaKullaniciYoneticisi>(), context.Authentication);
        }



        //override PasswordSignInAsyc aktif ve kilitli kullanıcılar için

        public override Task<SignInStatus> PasswordSignInAsync(string kullaniciAdi, string sifre, bool hatirlaBeni, bool kilitlenmeliMi)
        {
            var kullanici = UserManager.FindByEmailAsync(kullaniciAdi).Result;

            if ((kullanici.aktifMi.HasValue && !kullanici.aktifMi.HasValue) || !kullanici.aktifMi.HasValue)
                return Task.FromResult<SignInStatus>(SignInStatus.LockedOut);

            return base.PasswordSignInAsync(kullaniciAdi, sifre, hatirlaBeni, kilitlenmeliMi);
        }
    }
}
