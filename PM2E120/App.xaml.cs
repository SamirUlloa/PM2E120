using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Ejercicio1_3.Data;
using System.IO;

namespace PM2E120
{
    public partial class App : Application
    {
        static SQLiteHelper db;

        //public static object SQLiteDB { get; internal set; }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public static SQLiteHelper SQLiteDB
        {
            get
            {
                if (db == null)
                {
                    db = new SQLiteHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DBPersonas.db3"));
                }
                return db;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
