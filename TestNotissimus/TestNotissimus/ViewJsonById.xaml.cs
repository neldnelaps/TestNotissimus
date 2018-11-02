using Newtonsoft.Json;

using System.Xml.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestNotissimus
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ViewJsonById : ContentPage
	{
		public ViewJsonById ()
		{
			InitializeComponent ();
		}

        public ViewJsonById(XElement element)
        {
            InitializeComponent();
         
            LabelOffer.Text = JsonConvert.SerializeXNode(element, Formatting.Indented);
        }

    }
}