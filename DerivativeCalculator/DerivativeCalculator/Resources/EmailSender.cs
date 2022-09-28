using System;
using System.Collections.Generic;
using System.Text;
using FluentEmail.Smtp;
using System.Threading.Tasks;
using FluentEmail;
using System.Net;
using System.Net.Mail;
using FluentEmail.Core;
namespace DerivativeCalculator.Resources
{
    public static class EmailSender
    {
        
        private static  string username = "derivativebot@gmail.com";
        private static  string password = "Teodora123";
        
         static EmailSender()
        {
            
            NetworkCredential myCredentials = new NetworkCredential();
            myCredentials.UserName = username;
            myCredentials.Password = password;
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = myCredentials,
                Port = 587,
                EnableSsl = true,
                

            };
            var sender = new SmtpSender(() => smtp);
            Email.DefaultSender = sender;
        }
        public static async Task SendEmail(string body)
        {
            FluentEmail.Core.Models.SendResponse email = null;
            try
            {


                 email = await Email
                    .From(username)
                    .To("cosmingamer2004@gmail.com")
                    .Subject("NEW BUG(derivative calculator)")
                    .Body(body)
                    .SendAsync();
            }
            catch(Exception ex)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert(ex.Message, "plm", "k");
            }
            if (email.Successful)
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Your message was sent!", "Succesful", "Ok");
            }
            else
            {
                Acr.UserDialogs.UserDialogs.Instance.Alert("Error", email.ErrorMessages.ToString(), "Ok");
            }
        }
        
        }

    }

