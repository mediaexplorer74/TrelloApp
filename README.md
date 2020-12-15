# TrelloApp
TrelloApp for Windows 10 Mobile, Android, iOS (Xamarin edition) . Developers only ;) "Login" needs to be hardcoded (no config, sorry))

Pre-build notes:
Find file:
\TrelloApp\TrelloAp\Repositories\TrelloRepository.cs

Correct strings:
private const string _APIKEY = ""; // app-key (xxxx95187ce8c3ea5c2ea1504598xxxx)
private const string _USERTOKEN = ""; // user-token (xxxxc35ce1bceaf306be266d131a7c8c42d92c0d95bddf0e8f6d921fa8e9xxxx) 

How to get appkey and app usertoken :

step 1: login into Your Trello and go to 
https://trello.com/app-key/

step 2

assemble the string 
https://trello.com/1/authorize?expiration=never&scope=read,write,account&response_type=token&name=Server%20Token&key=xxxx95187ce8c3ea5c2ea1504598xxxx
and go to result page to obtain application (user) token.

step 3

Open TrelloRepository.cs file and correct this strings :
private const string _APIKEY = ""; // your app-key (xxxx95187ce8c3ea5c2ea1504598xxxx)
private const string _USERTOKEN = ""; // your user-token (xxxxc35ce1bceaf306be266d131a7c8c42d92c0d95bddf0e8f6d921fa8e9xxxx) 
