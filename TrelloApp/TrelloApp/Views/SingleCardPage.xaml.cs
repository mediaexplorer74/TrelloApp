using TrelloApp.Models;
using TrelloApp.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrelloApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SingleCardPage : ContentPage
    {
        public TrelloCard Card { get; set; }
        public bool IsNewCard { get; set; }
        public SingleCardPage(TrelloCard card)
        {
            InitializeComponent();
            this.Card = card;

            //BackgroundColor = Color.LightCyan; 
            BackgroundColor = Color.FromHex(Card.List.Board.ColorHex);

            lblBoard.Text = Card.List.Board.Name;
            lblList.Text = Card.List.Name;
            Title = "Edit";
            editName.Text = Card.Name;
            IsNewCard = false;
            loadMemberAsync();
        }

        private async Task loadMemberAsync()
        {
            TrelloMember tm = await TrelloRepository.GetMemberDataAsync(this.Card);
            lblFullName.Text = tm.FullName;
            

            //imgAvatar.Source = tm.AvatarImg;

            //HttpClient client = new HttpClient();
            //Stream stream = await client.GetStreamAsync(tm.AvatarImg);
            //imgAvatar.Source = ImageSource.FromStream(() => stream);

            imgAvatar.Source = new UriImageSource()
            {
                Uri = new Uri(tm.gravatarHash),
                CachingEnabled = false
            };
            lblUsername.Text = tm.UserName;
        }

        public SingleCardPage(TrelloList l)
        {
            this.Card = new TrelloCard { List = l };
            InitializeComponent();
            lblBoard.Text = l.Board.Name;
            lblList.Text = l.Name;
            Title = "Add";
            IsNewCard = true;
        }
        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            TrelloCard card = new TrelloCard() { Name = editName.Text };
            if (IsNewCard)
            {
                await TrelloRepository.AddCardAsync(this.Card.List.ListId, card);
            }
            else
            {
                Card.Name = card.Name;
                await TrelloRepository.UpdateCardAsync(Card);
            }
            Navigation.PopAsync();
        }
    }
}