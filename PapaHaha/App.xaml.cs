﻿namespace PapaHaha
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Routing.RegisterRoute("SettingsPage", typeof(SettingsPage));
        }
    }
}
