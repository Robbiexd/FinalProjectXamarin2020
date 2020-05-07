using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DBLite.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendMailPage : ContentPage
    {
        public SendMailPage()
        {
            InitializeComponent();
        }

        void btnSend_Clicked(object sender, System.EventArgs e)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("susairajs18@gmail.com");
                //mail.To.Add(txtTo.Text);
                //mail.Subject = txtSubject.Text;
                //mail.Body = txtBody.Text;

                //pozn - tutorial nefunguje

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("susairajs18@gmail.com", "***********");

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                DisplayAlert("Faild", ex.Message, "OK");
            }
        }
    }
}