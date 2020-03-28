using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            GetTokenAsync(TBoxLogin.Text, TBoxPassword.Text);
        }


        public async void GetTokenAsync(string userName, string userPassword)
        {
            Uri uri = new Uri("http://localhost:63183/api/token");  

            //Uri uri = new Uri("https://localhost:44357/api/token");

            //string body = @"{""email"" : ""{userName}"",""password"" :""Password1@""}";
            string body = "{\"email\" : \"" + userName + "\",\"password\":\"" + userPassword + "\"}";

            //JsonConvert.SerializeObject()


            HttpStringContent contentBody = new HttpStringContent(body, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");

            HttpClient _client = new HttpClient();

            HttpResponseMessage responseMessage = await _client.PostAsync(uri, contentBody);

            if (responseMessage.IsSuccessStatusCode)
            {
                var tokenResponse = await responseMessage.Content.ReadAsStringAsync();

                TxtJwtToken.Text = tokenResponse;
            }



        }

    }
}
