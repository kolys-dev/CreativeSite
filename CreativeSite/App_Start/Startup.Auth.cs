﻿using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using CreativeSite.Models;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security;

namespace CreativeSite
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            var twitterAuthOptions = new TwitterAuthenticationOptions
            {
                ConsumerKey = "gTABcXJyBdVpwBfTvr0giycFg",
                ConsumerSecret = "I2aeefsyPVh39rJS0d1hQ1gs1pg7wB0wwUeYow7nyjkuQs4Tqj",
                BackchannelCertificateValidator = new CertificateSubjectKeyIdentifierValidator(new[]
                {
                    "A5EF0B11CEC04103A34A659048B21CE0572D7D47",
                    "0D445C165344C1827E1D20AB25F40163D8BE79A5",
                    "7FD365A7C2DDECBBF03009F34339FA02AF333133",
                    "39A55D933676616E73A761DFA16A7E59CDE66FAD",
                    "5168FF90AF0207753CCCD9656462A212B859723B",
                    "B13EC36903F8BF4701D498261A0802EF63642BC3"
                })
            };

            app.UseTwitterAuthentication(twitterAuthOptions);

            var facebookAuthOptions = new FacebookAuthenticationOptions()
            {
                AppId = "1084468928298655",
                AppSecret = "95cdbcd614753a0be4fba107ba54a5e0"
            };
            // facebookAuthOptions.Scope.Add("email");
            app.UseFacebookAuthentication(facebookAuthOptions);

            app.UseVkontakteAuthentication("5540329", "XP2pImsqpRq3cVT9jWgD", "email");
        }
    }
}