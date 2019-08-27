using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using static Horizon.UI.WinAPI.Win32;

namespace Horizon.UI
{
    public class Sidebar : Window
    {
        private DoubleAnimation SlideOutLeftAnimation { get; set; }
        private DoubleAnimation SlideInLeftAnimation { get; set; }
        private DoubleAnimation FadeOutAnimation { get; set; }
        private DoubleAnimation FadeInAnimation { get; set; }

        public event EventHandler SlideOutComplete;
        public event EventHandler SlideInComplete;

        static Sidebar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Sidebar), new FrameworkPropertyMetadata(typeof(Sidebar)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PositionBottomLeft();
            CreateAnimations();
            EnableFrostGlassEffect();
            HideFromTaskSwitcher();
        }

        private void EnableFrostGlassEffect()
        {
            var helper = new WindowInteropHelper(this);
            var accent = new AccentPolicy { AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND };
            var accentSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentSize,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(helper.Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }

        private void HideFromTaskSwitcher()
        {
            var helper = new WindowInteropHelper(this);

            var windowLong = GetWindowLong(helper.Handle, GwlIndex.GWL_EXSTYLE);
            windowLong |= (int)GwlExStyle.WS_EX_TOOLWINDOW;
            SetWindowLong(helper.Handle, GwlIndex.GWL_EXSTYLE, windowLong);
        }

        private void CreateAnimations()
        {
            UpdateLayout();

            SlideOutLeftAnimation = new DoubleAnimation(Left, -MaxWidth, new Duration(new TimeSpan(0, 0, 0, 0, 500)))
            {
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut },
            };

            SlideInLeftAnimation = new DoubleAnimation(-MaxWidth, 0, new Duration(new TimeSpan(0, 0, 0, 0, 500)))
            {
                EasingFunction = new CircleEase { EasingMode = EasingMode.EaseOut },
            };

            FadeOutAnimation = new DoubleAnimation(0, new Duration(new TimeSpan(0, 0, 0, 0, 500)));
            FadeInAnimation = new DoubleAnimation(1, new Duration(new TimeSpan(0, 0, 0, 0, 400)));

            SlideOutLeftAnimation.Completed += (sender, e) => SlideOutComplete?.Invoke(this, EventArgs.Empty);
            SlideInLeftAnimation.Completed += (sender, e) => SlideInComplete?.Invoke(this, EventArgs.Empty);
        }

        private void PositionBottomLeft()
        {
            Top = SystemParameters.PrimaryScreenHeight - Height;
            Left = 0;
        }

        public void SlideOutLeft()
        {
            BeginAnimation(OpacityProperty, FadeOutAnimation);
            BeginAnimation(LeftProperty, SlideOutLeftAnimation, HandoffBehavior.Compose);
        }

        public void SlideInLeft()
        {
            BeginAnimation(LeftProperty, SlideInLeftAnimation, HandoffBehavior.Compose);
            BeginAnimation(OpacityProperty, FadeInAnimation);
        }
    }
}
