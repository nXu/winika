namespace Winika.Gui
{
    using System;
    using System.Windows;
    using Lib.Profile;

    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string baseUri;
        private string userAgent;

        public LoginWindow(string baseUri, string userAgent)
        {
            this.baseUri = baseUri;
            this.userAgent = userAgent;
            InitializeComponent();
        }

        private async void Login_OnClick(object sender, RoutedEventArgs e)
        {
            PlayerProfile profile;

            try
            {
                profile = await PlayerProfile.Login(this.ServerTextBox.Text, this.baseUri, this.userAgent,
                    this.UsernameTextBox.Text, this.PasswordTextBox.Password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
