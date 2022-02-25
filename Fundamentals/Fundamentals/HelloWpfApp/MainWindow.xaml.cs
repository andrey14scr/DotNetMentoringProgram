using HelloLibrary;
using System.Windows;

namespace HelloWpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            var username = UserName.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Username value should contain at least one character!");
                return;
            }

            MessageBox.Show($"Hello {username}");

            var helloService = new HelloService();
            var greeting = helloService.GetHelloString(username);
            MessageBox.Show(greeting);
        }
    }
}
