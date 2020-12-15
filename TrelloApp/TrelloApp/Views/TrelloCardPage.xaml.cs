using TrelloApp.Models;
using TrelloApp.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrelloApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrelloCardPage : ContentPage
    {
        public TrelloList List { get; set; }
        public TrelloCardPage(TrelloList list)
        {
            InitializeComponent();
            this.List = list;
            
            //BackgroundColor = Color.LightSeaGreen; 
            BackgroundColor = Color.FromHex(List.Board.ColorHex);

            Title = "Cards";

            lblListName.Text = List.Name;

            TapGestureRecognizer gesture = new TapGestureRecognizer();

            gesture.Tapped += AddCard_Tapped;

            lblAddCard.GestureRecognizers.Add(gesture);
        }

        private void AddCard_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SingleCardPage(List));
            lvwCards.SelectedItem = null;
        }

        private async Task loadListAsync()
        {
            lvwCards.ItemsSource = await TrelloRepository.GetTrelloCardsAsync(List.ListId);
            lblListName.Text = List.Name;
        }

        private async void btnCloseCard_Clicked(object sender, EventArgs e)
        {
            TrelloCard card = (sender as Button).BindingContext as TrelloCard;
            card.IsClosed = true;
            await TrelloRepository.UpdateCardAsync(card);
            loadListAsync();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            loadListAsync();
        }

        private void lvwCards_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            TrelloCard card = lvwCards.SelectedItem as TrelloCard;
            if (card != null)
            {
                card.List = this.List;
                Navigation.PushAsync(new SingleCardPage(card));
                lvwCards.SelectedItem = null;
            }
        }
    }
}