using TrelloApp.Models;
using TrelloApp.Repositories;
using TrelloApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TrelloApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    //[DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        
        // Init (Construct)
        public MainPage()
        {
            InitializeComponent();
            Title = "Trello App";

            //TEMP
            //App.Current.Properties["apikey"] = "1";
            //App.Current.Properties["usertoken"] = "2";

            //RnD
            //LoadBoard();

            //EXPERIMENT
            //Navigation.PushAsync(new TrelloNoConnectionPage());
            //Navigation.PushAsync(new TrelloListPage(lvwBoards.));
            /*
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                Navigation.PushAsync(new TrelloNoConnectionPage());
            }
            */
        }



        //  Settings Button Click Handling
        private void OnSettingsButtonClick(object sender, EventArgs e)
        {
            // go to settings page
            Navigation.PushAsync(new NoConnectionPage());
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                // RnD
                Navigation.PushAsync(new TrelloNoConnectionPage());
            }
        }


        // Try to Load The Board
        private async Task LoadBoard()
        {
            List<TrelloBoard> aaa = await TrelloRepository.GetTrelloBoards();
            
            if (aaa == null)
            {
                // RnD: color patching =)
                //lvwBoards.BackgroundColor = Color.Green;

                // RnD :show settings ONLY ONCE (on First Init)
                if (App.FirstInit)
                {
                    Navigation.PushAsync(new NoConnectionPage());
                }
                else
                {
                    lvwBoards.BackgroundColor = Color.Gray;
                    lvwBoards.ItemsSource = null;

                    // show (enable) Settings button
                    Settings.IsEnabled = true;
                    Settings.IsVisible = true;
                }
            }
            else
            {
                lvwBoards.BackgroundColor = Color.White;
                lvwBoards.ItemsSource = aaa;
                //Debug.WriteLine("lvwBoards.ItemsSource: " + lvwBoards.ItemsSource);

                // All is OK - hide (disable) Settings button
                Settings.IsEnabled = true;//false;
                Settings.IsVisible = true;//false;
            }

        }//LoadBoard end

        
        //
        private void lvwBoards_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            TrelloBoard board = lvwBoards.SelectedItem as TrelloBoard;
            
            if(board != null)
            {
                Navigation.PushAsync(new TrelloListPage(board));
                lvwBoards.SelectedItem = null;
            }
            //TrelloListPage p = new TrelloListPage();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            
            //RnD
            LoadBoard();
        }//OnAppearing



        // RND / TEST 
        /*
        private async Task TestModels()
        {
            List<TrelloBoard> list = new List<TrelloBoard>();

            list = await TrelloRepository.GetTrelloBoards();
            
            foreach(TrelloBoard item in list)
            {
                Debug.WriteLine(item.Name);
                List<TrelloList> l = new List<TrelloList>();
                l = await TrelloRepository.GetTrelloListsAsync(item.BoardId);
                foreach(TrelloList tl in l)
                {
                    TrelloCard card = new TrelloCard() { Name = "Testerdetesterdetest" };
                    await TrelloRepository.AddCardAsync(tl.ListId, card);
                    Debug.WriteLine($"Listname: {tl.Name}");
                    List<TrelloCard> ltc = new List<TrelloCard>();
                    ltc = await TrelloRepository.GetTrelloCardsAsync(tl.ListId);
                    foreach(TrelloCard c in ltc)
                    {
                        Debug.WriteLine($"Cardname: {c.Name}");
                    }
                }
            }
            
            TrelloBoard b = list.Where(x => x.IsFavorite == true).ToList<TrelloBoard>().First();
            if(b != null)
            {
                Debug.WriteLine(b.Name);
            }
        }
        */

    }//class end

}//namespace end
