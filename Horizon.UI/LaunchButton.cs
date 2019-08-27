using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Horizon.UI
{
    public class LaunchButton : Button
    {
        public static DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(ImageSource), typeof(LaunchButton));
        public static DependencyProperty TitleProperty = DependencyProperty.Register(nameof(Title), typeof(string), typeof(LaunchButton));
        public static DependencyProperty ExecutablePathProperty = DependencyProperty.Register(nameof(ExecutablePath), typeof(string), typeof(LaunchButton));
        public static DependencyProperty CommandLineArgumentsProperty = DependencyProperty.Register(nameof(CommandLineArguments), typeof(List<string>), typeof(LaunchButton));

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string ExecutablePath
        {
            get => (string)GetValue(ExecutablePathProperty);
            set => SetValue(ExecutablePathProperty, value);
        }

        public List<string> CommandLineArguments
        {
            get => (List<string>)GetValue(CommandLineArgumentsProperty);
            set => SetValue(CommandLineArgumentsProperty, value);
        }

        static LaunchButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LaunchButton), new FrameworkPropertyMetadata(typeof(LaunchButton)));
        }

        protected override void OnClick()
        {
            base.OnClick();

            if (string.IsNullOrWhiteSpace(ExecutablePath))
                return;

            new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ExecutablePath,
                    Arguments = string.Join(" ", CommandLineArguments ?? new List<string>()),
                    UseShellExecute = true,
                    WorkingDirectory = Path.GetDirectoryName(ExecutablePath)
                }
            }.Start(); 
        }
    }
}
