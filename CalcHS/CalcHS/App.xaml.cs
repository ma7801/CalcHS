using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace CalcHS
{
	public partial class App : Application
	{
        public App ()
		{
           
            // If colors haven't yet been set
            if(!Properties.ContainsKey("MainBGColor"))
            {
                Properties.Add("MainBGColor", "#111");
                Properties.Add("DisplayBGColor", "#777");
                Properties.Add("DisplayTextColor", "#EEE");
                Properties.Add("ButtonBGColor", "#444");
                Properties.Add("ButtonTextColor", "#EEE");
            }
            InitializeComponent();

			MainPage = new CalcHS.MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
