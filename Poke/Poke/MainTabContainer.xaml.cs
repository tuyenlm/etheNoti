using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Poke
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainTabContainer : TabbedPage
	{
		public MainTabContainer()
		{
			InitializeComponent();
		}
	}
}