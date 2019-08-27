using System;
using System.Threading.Tasks;

namespace Horizon
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SlideOutLeft();
            await Task.Delay(1000);
            SlideInLeft();
        }

        private void Sidebar_Deactivated(object sender, EventArgs e)
        {
            SlideOutLeft();
        }
    }
}
