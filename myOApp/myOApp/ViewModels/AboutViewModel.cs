using myOApp.Definitions;
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
                    To = new System.Collections.Generic.List<string> { Constants.Email },
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException)
            {
                await this.DialogService.ShowMessage("We could not find e-mail app. Please try contacting us manually: contact@capricode.ch", "Alert");
            }
            catch (Exception)
            {
                await this.DialogService.ShowMessage("We could not open e-mail app. Please try contacting us manually: contact@capricode.ch", "Alert");
            }
        }

        ICommand goToWebsiteCommand;
        public ICommand GoToWebsiteCommand => goToWebsiteCommand ?? (goToWebsiteCommand = new Command(async () => await ExecuteGoToWebsiteCommand()));

        private async Task ExecuteGoToWebsiteCommand()
        {
            var websiteUrl = Constants.WebsiteUrl;
            var uri = new Uri(websiteUrl);
            await Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
    }
}
