using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary.LogicClasses
{
    internal class Mail
    {
        private static MailMessage CreateMail(string name, string emailFrom, string emailTo, string subject, string body)
        {
            var from = new MailAddress(emailFrom, name);
            var to = new MailAddress(emailTo);
            var mail = new MailMessage(from, to);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            return mail;
        }

        private static void SendMail(string host, int smptPort, string emailFrom, string pass, MailMessage mess)
        {
            SmtpClient smtp = new SmtpClient(host,smptPort);
            smtp.Credentials = new NetworkCredential(emailFrom, pass);
            smtp.EnableSsl = true;
            smtp.Send(mess);
        }

        public static void SendMailNewLibraryCard(Person p, string pass)
        {
            Console.WriteLine(p.FirstName + " " + p.LastName + " " + p.Pytronymic + p.Adress);
            string textMail = "";
            textMail += p.FirstName + " " + p.LastName + " " + p.Pytronymic + ", спасибо за регистрацию в нашей библиотеке!<br>" + "<b>Логин: </b>" + p.Login + "<br><b>Пароль: </b>" + pass;

            var mail = CreateMail("Спасибо за регистрастрацию!", "mylibrary233@gmail.com", p.Email, "MyLibrary", textMail);
            SendMail("smtp.gmail.com", 587, "mylibrary233@gmail.com", "behzrbgxvxkyrpmf", mail);
        }

        

    }
}
