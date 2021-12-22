using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrelloApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoConnectionPage : ContentPage
    {
        // On Page Init (Construct)
        public NoConnectionPage()
        {
            InitializeComponent();
        }

        // On Page Appearing Handling
        protected override void OnAppearing()
        {
            object apikey = "";
            
            if (App.Current.Properties.TryGetValue("apikey", out apikey))
            {
                ApiKey.Text = (string)apikey;
            }

            object usertoken = "";

            if (App.Current.Properties.TryGetValue("usertoken", out usertoken))
            {
                UserToken.Text = (string)usertoken;
            }


            base.OnAppearing();
        }

        // Save Settings Button Click Handling
        private void OnSaveSettingsButtonClick(object sender, EventArgs e)
        {
            string apikey = ApiKey.Text;
             
            App.Current.Properties["apikey"] = apikey;

            string usertoken = UserToken.Text;

            App.Current.Properties["usertoken"] = usertoken;

            App.FirstInit = false;

            Navigation.PopAsync(true);
        }
    }
}