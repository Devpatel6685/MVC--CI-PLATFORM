using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace CI_PLATFORM.repository.Repository {

    public class UserRepository : IUserInterface
    {
        private readonly CIPLATFORMDbContext _cIPLATFORMDbContext;
        public UserRepository(CIPLATFORMDbContext cIPLATFORMDbContext) {
            _cIPLATFORMDbContext = cIPLATFORMDbContext;
        }

        public string login(LoginViewModel user)
        {
           var email = _cIPLATFORMDbContext.Users.FirstOrDefault(c =>c.Email== user.Email.ToLower());
            if (email == null) {

                return "User Does not exist";
                    }
            var userpassword = _cIPLATFORMDbContext.Users.FirstOrDefault(c =>c.Email.Equals( user.Email.ToLower()) && c.Password.Equals( user.Password));   
            var match = string.Compare(user.Password,email.Password,StringComparison.Ordinal)==0;
                if (match == false || userpassword == null)
            {
                return "invalid password";
            }

            return userpassword.FirstName;

        }

        public string REGISTRATIONPAGE(registerviewmodel user)
        {
            var email = _cIPLATFORMDbContext.Users.FirstOrDefault(c => c.Email.Equals(user.Email));
            if (email != null)
            {
                return "user already exists";
            }

            var entry = _cIPLATFORMDbContext.Users.Add(
                new User()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber,
                    CityId = user.CityId,
                    CountryId = user.CountryId,
                }
                    );

            _cIPLATFORMDbContext.SaveChanges();
            return "successfully registered";
        }
      
        public string FORGOTPASSWORD(forgotviewmodel user)
        {
            var status = _cIPLATFORMDbContext.Users.FirstOrDefault(c => c.Email.Equals(user.Email.ToLower()));
            if (status != null)
            {
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringchar = new char[16];
                var random = new Random();
                for (int i = 0; i < stringchar.Length; i++)
                {
                    stringchar[i] = chars[random.Next(chars.Length)];
                }
                var finalString = new String(stringchar);
                PasswordReset entry = new PasswordReset()
                {
                    Email = user.Email,
                    Token = finalString
                };
                _cIPLATFORMDbContext.PasswordResets.Add(entry);
                _cIPLATFORMDbContext.SaveChanges();
                if ( _cIPLATFORMDbContext.PasswordResets.FirstOrDefault(c=> c.Email.Equals(user.Email.ToLower())) ==null)
                {
                    _cIPLATFORMDbContext.PasswordResets.Add(entry);

                }
                else
                {
                    _cIPLATFORMDbContext.PasswordResets.Update(entry);
                }

                var mailBody = "<h1>click link to reset password</h1><br><h2><a href='https://localhost:7093/Home/RESETPAGE?token=" + finalString + "'>Reset Password</h2>";

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("devppatel6685@gmail.com"));
                email.To.Add(MailboxAddress.Parse(user.Email));
                email.Subject = "reset your password";
                email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mailBody };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Authenticate("devppatel6685@gmail.com", "uvqekuakbgseptda");
                smtp.Send(email);
                smtp.Disconnect(true);
                return finalString;
            }
            return null;

        }

        public string RESETPAGE(resetviemodel entry, string token)
        {
           
            var validtoken = _cIPLATFORMDbContext.PasswordResets.FirstOrDefault(x => x.Token == token);
            if (validtoken != null)
            {
                var user = _cIPLATFORMDbContext.Users.FirstOrDefault(x => x.Email == validtoken.Email);
                user.Password = entry.Password;
                _cIPLATFORMDbContext.Users.Update(user);
                _cIPLATFORMDbContext.SaveChanges();
                return "Password  Changed";
            }
            return null;
        }

    }

}
