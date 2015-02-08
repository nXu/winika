namespace Winika.Gui
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string baseuri;

        public LoginWindow(string baseuri)
        {
            this.baseuri = baseuri;
            InitializeComponent();
        }
    }
}
