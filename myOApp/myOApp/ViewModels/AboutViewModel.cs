using myOApp.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace myOApp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private readonly IDialogService DialogService = DependencyService.Get<IDialogService>();

        ICommand sendEmailCommand;
        public ICommand SendEmailCommand => sendEmailCommand ?? (sendEmailCommand = new Command(async () => await ExecuteSendEmailCommand()));

        private async Task ExecuteSendEmailCommand()
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = "MyOApp Feedback",
                    Body = "",
                    To = new System.Collections.Generic.List<string> { "contact@capricode.ch" },
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException)
            {
                await this.DialogService.ShowMessage("We could not find e-mail app. Please try contacting us manually: contact@capricode.ch", "Alert");
            }
            catch (Exception ex)
            {
                await this.DialogService.ShowMessage("We could not open e-mail app. Please try contacting us manually: contact@capricode.ch", "Alert");
            }
        }
    }
}
