using TrelloApp.Models;
using TrelloApp.Repositories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TrelloApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrelloListPage : ContentPage
    {
        public TrelloBoard Board { get; set; }
        public TrelloListPage(TrelloBoard board)
        {
            InitializeComponent();
            this.Board = board;
            Title = Board.Name;

            //BackgroundColor = Color.Aqua;
            BackgroundColor = Color.FromHex(Board.ColorHex);

            loadBordAsync();
        }

        private async Task loadBordAsync()
        {
            lvwTrelloLists.ItemsSource = await TrelloRepository.GetTrelloListsAsync(Board.BoardId);
            //lvwBoards.ItemsSource = await TrelloRepository.GetTrelloBoards();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await DisplayAlert("Item Tapped", "An item was tapped.", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private void lvwTrelloLists_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            TrelloList list = lvwTrelloLists.SelectedItem as TrelloList;
            if (list != null)
            {
                list.Board = this.Board;
                Navigation.PushAsync(new TrelloCardPage(list));
                lvwTrelloLists.SelectedItem = null;
            }
        }
    }
}
