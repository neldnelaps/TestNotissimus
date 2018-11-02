using Plugin.Connectivity;

using System.Linq;
using System.Text;
using System.Net.Http;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestNotissimus
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<string> Items { get; set; }
        List<XElement> offerList = new List<XElement>();

        public MainPage()
        {
            InitializeComponent();

            GetListOfferAndId();
            Items = new ObservableCollection<string>();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

            await Navigation.PushAsync(new ViewJsonById(offerList[Items.IndexOf(e.Item.ToString())]));

            ((ListView)sender).SelectedItem = null;     
        }
    
        protected async void GetListOfferAndId()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                using (HttpClient client = new HttpClient())
                {
                    var text = await client.GetByteArrayAsync("https://yastatic.net/market-export/_/partner/help/YML.xml");
                    var result = Encoding.UTF8.GetString(Encoding.Convert(Encoding.GetEncoding("Windows-1251"), Encoding.GetEncoding("UTF-8"), text));

                    var xDocument = XDocument.Parse(result);

                    offerList = xDocument.Element("yml_catalog").Element("shop").Element("offers").Elements().ToList();

                    foreach (var item in offerList)
                        Items.Add(item.FirstAttribute.Value);

                    idList.ItemsSource = Items;
                }
            }
            else
            {
                DependencyService.Get<IMessage>().ShortAlert($"No connection!");
            }
        }    

        //private void GetID(string document) // первоначально парсила с помощью регулярных выражений
        //{
        //    Regex rgx = new Regex(@"<offer (.*)>");
        //    foreach (Match match in rgx.Matches(document))
        //        Items.Add(match.Groups[1].Value.Split().First());          
        //    idList.ItemsSource = Items;
        //}
    }
}
