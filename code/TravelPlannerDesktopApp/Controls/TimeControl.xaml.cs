using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TravelPlannerDesktopApp.Controls
{
    /// <summary>
    ///     Interaction logic for TimeControl.xaml
    /// </summary>
    public partial class TimeControl : UserControl
    {
        #region Data members

        /// <summary>
        ///     The value property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(TimeSpan), typeof(TimeControl),
                new FrameworkPropertyMetadata(DateTime.Now.TimeOfDay,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged));

        /// <summary>
        ///     The hours property
        /// </summary>
        public static readonly DependencyProperty HoursProperty =
            DependencyProperty.Register("Hours", typeof(int), typeof(TimeControl),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTimeChanged));

        /// <summary>
        ///     The minutes property
        /// </summary>
        public static readonly DependencyProperty MinutesProperty =
            DependencyProperty.Register("Minutes", typeof(int), typeof(TimeControl),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTimeChanged));

        /// <summary>
        ///     The seconds property
        /// </summary>
        public static readonly DependencyProperty SecondsProperty =
            DependencyProperty.Register("Seconds", typeof(int), typeof(TimeControl),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTimeChanged));

        /// <summary>
        ///     The milliseconds property
        /// </summary>
        public static readonly DependencyProperty MillisecondsProperty =
            DependencyProperty.Register("Milliseconds", typeof(int), typeof(TimeControl),
                new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTimeChanged));

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public TimeSpan Value
        {
            get => (TimeSpan)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        ///     Gets or sets the hours.
        /// </summary>
        /// <value>
        ///     The hours.
        /// </value>
        public int Hours
        {
            get => (int)GetValue(HoursProperty);
            set => SetValue(HoursProperty, value);
        }

        /// <summary>
        ///     Gets or sets the minutes.
        /// </summary>
        /// <value>
        ///     The minutes.
        /// </value>
        public int Minutes
        {
            get => (int)GetValue(MinutesProperty);
            set => SetValue(MinutesProperty, value);
        }

        /// <summary>
        ///     Gets or sets the seconds.
        /// </summary>
        /// <value>
        ///     The seconds.
        /// </value>
        public int Seconds
        {
            get => (int)GetValue(SecondsProperty);
            set => SetValue(SecondsProperty, value);
        }

        /// <summary>
        ///     Gets or sets the milliseconds.
        /// </summary>
        /// <value>
        ///     The milliseconds.
        /// </value>
        public int Milliseconds
        {
            get => (int)GetValue(MillisecondsProperty);
            set => SetValue(MillisecondsProperty, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimeControl" /> class.
        /// </summary>
        public TimeControl()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Methods

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as TimeControl;
            var newTime = (TimeSpan)e.NewValue;

            if (control != null)
            {
                control.Hours = newTime.Hours;
                control.Minutes = newTime.Minutes;
                control.Seconds = newTime.Seconds;
                control.Milliseconds = newTime.Milliseconds;
            }
        }

        private static void OnTimeChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var control = obj as TimeControl;
            if (control != null)
            {
                control.Value = new TimeSpan(0, control.Hours, control.Minutes, control.Seconds, control.Milliseconds);
            }
        }

        private Tuple<int, int> getMaxAndCurentValues(string name)
        {
            var maxValue = 0;
            var currValue = 0;

            switch (name)
            {
                case "ff":
                    maxValue = 1000;
                    currValue = this.Milliseconds;
                    break;

                case "ss":
                    maxValue = 60;
                    currValue = this.Seconds;
                    break;

                case "mm":
                    maxValue = 60;
                    currValue = this.Minutes;
                    break;

                case "hh":
                    maxValue = 24;
                    currValue = this.Hours;
                    break;
            }

            return new Tuple<int, int>(maxValue, currValue);
        }

        private void updateTimeValue(string name, int delta)
        {
            var (maxValue, currValue) = this.getMaxAndCurentValues(name);

            // Set new value
            var newValue = currValue + delta;

            if (newValue == maxValue)
            {
                newValue = 0;
            }
            else if (newValue < 0)
            {
                newValue += maxValue;
            }

            switch (name)
            {
                case "ff":
                    this.Milliseconds = newValue;

                    break;

                case "ss":
                    this.Seconds = newValue;
                    break;

                case "mm":
                    this.Minutes = newValue;
                    break;

                case "hh":
                    this.Hours = newValue;
                    break;
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs args)
        {
            try
            {
                var delta = 0;
                var name = ((TextBox)sender).Name;

                switch (args.Key)
                {
                    case Key.Up:
                        delta = 1;
                        break;
                    case Key.Down:
                        delta = -1;
                        break;
                }

                this.updateTimeValue(name, delta);
            }
            catch
            {
                // ignored
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                var g = (Grid)sender;
                var value = g.Children.OfType<TextBox>().FirstOrDefault();

                if (value != null)
                {
                    this.updateTimeValue(value.Name, e.Delta / Math.Abs(e.Delta));
                }
            }
            catch
            {
                // ignored
            }
        }

        private bool isTextAllowed(string name, string text)
        {
            try
            {
                if (text.Any(c => !char.IsDigit(c) && !char.IsControl(c)))
                {
                    return false;
                }

                var values = this.getMaxAndCurentValues(name);
                var maxValue = values.Item1;

                var newValue = Convert.ToInt32(text);

                if (newValue < 0 || newValue >= maxValue)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                var tb = (TextBox)sender;

                e.Handled = !this.isTextAllowed(tb.Name, tb.Text + e.Text);
            }
            catch
            {
                // ignored
            }
        }

        private void OnTextPasting(object sender, DataObjectPastingEventArgs e)
        {
            try
            {
                var name = ((TextBox)sender).Name;

                if (e.DataObject.GetDataPresent(typeof(string)))
                {
                    var text = (string)e.DataObject.GetData(typeof(string));
                    if (!this.isTextAllowed(name, text))
                    {
                        e.CancelCommand();
                    }
                }
                else
                {
                    e.CancelCommand();
                }
            }
            catch
            {
                // ignored
            }
        }

        #endregion
    }
}