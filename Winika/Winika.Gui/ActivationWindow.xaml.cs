namespace Winika.Gui
{
    using System;
    using System.Windows;
    using Newtonsoft.Json.Linq;
    using Winika.Lib.HttpCommunication;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var actMan = new ActivationManager();
            GuidTextBox.Text = actMan.GetMachineGuid();
            this.Continue_OnClick(null, null);
        }

        private async void Continue_OnClick(object sender, RoutedEventArgs e)
        {
            // Get GUID again
            var guid = (new ActivationManager()).GetMachineGuid();

            // Get ikariam base URI
            var requestManager = new HttpRequestManager();
            var uri = string.Format("https://winika.nxu.hu/client-auth/{0}", guid.ToUpper());

            string response;
            try
            {
                response = await requestManager.Get(uri);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
                return;
            }

            // Parse as JSON
            var data = JObject.Parse(response);
            if ((bool)data["success"] != true)
            {
                // Error
                this.ShowErrorMessage((string)data["errormessage"]);
                return;
            }

            // Save base URI and continue
            var baseuri = (string)data["data"]["baseuri"];
            var useragent = (string) data["data"]["useragent"];
            var loginWindow = new LoginWindow(baseuri, useragent);
            loginWindow.Show();
            this.Close();
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
