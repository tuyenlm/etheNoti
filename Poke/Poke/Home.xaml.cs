using Microcharts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Entry = Microcharts.Entry;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Poke
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Home : ContentPage
	{

		public Home()
		{
			InitializeComponent();

			var entries = new[]
			{
				new Entry(200)
				{
					Label = "May1",
					ValueLabel = "200",
					Color = SKColor.Parse("#00ff72")
				},
				new Entry(400)
				{
				Label = "May2",
				ValueLabel = "400",
				Color = SKColor.Parse("#2A6487")
				},
				new Entry(300)
				{
				Label = "May3",
				ValueLabel = "300",
				Color = SKColor.Parse("#8FD484")
				},
				new Entry(150)
				{
				Label = "May4",
				ValueLabel = "150",
				Color = SKColor.Parse("#68B8BE")
				}
			};
			var chart = new BarChart() { Entries = entries };
			this.chartView.Chart = chart;
			this.chartView.HeightRequest = 200;
			this.chartView.Margin = 2;
			chart.BackgroundColor = SKColor.Parse("#333");


			for (int i = 0; i < 6; i++)
			{
				StackLayout abc = new StackLayout
				{
					HeightRequest = 30,
					Margin = 10,
					BackgroundColor = Color.FromHex("333")
				};
				HomeStack.Children.Add(abc);
			}
			Task.Run(async () => await RefreshDataAsync());



		}
		public async Task RefreshDataAsync()
		{
			var uri = new Uri("https://api.ethermine.org/miner/1d472570df89467cd9e6bb30bae5ef9eda85c4fa/currentStats");
			HttpClient myClient = new HttpClient();


			var response = await myClient.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				string content2 = await response.Content.ReadAsStringAsync();

				//Debug.WriteLine("content " + content);
				ContentJ Items = JsonConvert.DeserializeObject<ContentJ>(content2);
				Debug.WriteLine("Items " + Items.Data.Unpaid);
				Debug.WriteLine("ep int" + float.Parse(Items.Data.Unpaid));
				lblEth.Text = float.Parse(Items.Data.Unpaid) + " ETH";
				//float result = (int)(Items.Data.Unpaid) / 1000000;
				//Debug.WriteLine("Items count " + result);

				//foreach (var item in Items)
				//{
				//	Debug.WriteLine("item.content  " + item.Unpaid);
				//}

			}

		}



	}

	public class ContentJ
	{
		public Data Data { get; set; }
	}
	public class Data
	{
		public string Time { get; set; }
		public string Unpaid { get; set; }
		public string CoinsPerMin { get; set; }
		public object UsdPerMin { get; set; }
	}
}
