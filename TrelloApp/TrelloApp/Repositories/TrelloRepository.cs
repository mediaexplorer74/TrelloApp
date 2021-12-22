using TrelloApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Net.Http;

using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace TrelloApp.Repositories
{
    // RnD: static
    public static class TrelloRepository
    {
        public static string _APIKEY = "";    // app-key (i.e., xxxx95187ce8c3ea5c2ea1504598xxxx)
        public static string _USERTOKEN = ""; // user-token (i.e., xxxxc35ce1bceaf306be266d131a7c8c42d92c0d95bddf0e8f6d921fa8e9xxxx) 

        public static bool vars_init()
        {

            object apikey = "";

            if (App.Current.Properties.TryGetValue("apikey", out apikey))
            {
                _APIKEY = (string)apikey;
            }

            object usertoken = "";

            if (App.Current.Properties.TryGetValue("usertoken", out usertoken))
            {
                _USERTOKEN = (string)usertoken;
            }
            return true;
        }


        private static HttpClient GetHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            return httpClient;
        }

        public async static Task<List<TrelloBoard>> GetTrelloBoards()
        {
            // -----------
            vars_init();
            // -----------

            List<TrelloBoard> list = new List<TrelloBoard>();
            string url = $"https://trello.com/1/members/my/boards?key={_APIKEY}&token={_USERTOKEN}";
            using(HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    list = JsonConvert.DeserializeObject<List<TrelloBoard>>(json);
                }
                catch(Exception ex)
                {
                    //throw ex;
                    Debug.WriteLine("!!! EXCEPTION !!! " + ex.Message);
                    list = null;
                }
            }
            return list;
        }

        public static async Task<List<TrelloList>> GetTrelloListsAsync(string boardId)
        {
            // -----------
            vars_init();
            // -----------

            List<TrelloList> list = new List<TrelloList>();
            string url = $"https://trello.com/1/boards/{boardId}/lists?key={_APIKEY}&token={_USERTOKEN}";
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    list = JsonConvert.DeserializeObject<List<TrelloList>>(json);
                }
                catch (Exception ex)
                {
                    //throw ex;
                    Debug.WriteLine("!!! EXCEPTION !!! " + ex.Message);
                }
            }
            return list;
        }

        public static async Task<List<TrelloCard>> GetTrelloCardsAsync(string listId)
        {
            // -----------
            vars_init();
            // -----------

            List<TrelloCard> list = new List<TrelloCard>();
            string url = $"https://trello.com/1/lists/{listId}/cards?key={_APIKEY}&token={_USERTOKEN}";
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    list = JsonConvert.DeserializeObject<List<TrelloCard>>(json);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return list;
        }

        public static async Task<TrelloCard> GetTrelloCardByIdAsync(string cardId)
        {
            // -----------
            vars_init();
            // -----------

            TrelloCard c = new TrelloCard();
            string url = $"https://trello.com/1/cards/{cardId}?key={_APIKEY}&token={_USERTOKEN}";
            using(HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    c = JsonConvert.DeserializeObject<TrelloCard>(json);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return c;
        }

        public static async Task UpdateCardAsync(TrelloCard card)
        {
            // -----------
            vars_init();
            // -----------

            string url = $"https://trello.com/1/cards/{card.CardId}?key={_APIKEY}&token={_USERTOKEN}";
            using(HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(card);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(url, content);
                    if(!response.IsSuccessStatusCode)
                    {
                        string errorMsg = $"Unsuccesfull PUT to url: {url}, object {json}";
                        throw new Exception(errorMsg);
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = $"Something went wrong with url: {url} ({ex.Message})";
                    throw new Exception(errorMsg);
                }
            }
        }

        public static async Task AddCardAsync(string listId, TrelloCard card)
        {
            // -----------
            vars_init();
            // -----------

            string url = $"https://api.trello.com/1/cards?idList={listId}&key={_APIKEY}&token={_USERTOKEN}";
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = JsonConvert.SerializeObject(card);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMsg = $"Unsuccesfull POST to url: {url}, object {json}";
                        throw new Exception(errorMsg);
                    }
                }
                catch (Exception ex)
                {
                    string errorMsg = $"Something went wrong with url: {url} ({ex.Message})";
                    throw new Exception(errorMsg);
                }
            }
        }

        public static async Task<TrelloMember> GetMemberDataAsync(TrelloCard card)
        {
            // -----------
            vars_init();
            // -----------

            TrelloMember list = new TrelloMember();
            string url = $"https://api.trello.com/1/members/{card.List.Board.memberId}?key={_APIKEY}&token={_USERTOKEN}";
            using (HttpClient client = GetHttpClient())
            {
                try
                {
                    string json = await client.GetStringAsync(url);
                    list = JsonConvert.DeserializeObject<TrelloMember>(json);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return list;
        }
    }
}
