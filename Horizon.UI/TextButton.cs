using System.Windows;
using System.Windows.Controls;

namespace Horizon.UI
{
    public class TextButton : Button
    {
        static TextButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextButton), new FrameworkPropertyMetadata(typeof(TextButton)));
        }
    }
}
