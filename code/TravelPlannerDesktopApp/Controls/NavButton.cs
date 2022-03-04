using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace TravelPlannerDesktopApp.Controls
{
    /// <summary>
    ///     The nav button control class
    /// </summary>
    /// <seealso cref="System.Windows.Controls.Primitives.ButtonBase" />
    public class NavButton : ButtonBase
    {
        #region Data members

        /// <summary>
        ///     The image source property
        /// </summary>
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource",
            typeof(ImageSource), typeof(NavButton), new PropertyMetadata(null));

        /// <summary>
        ///     The text property
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(NavButton), new PropertyMetadata(null));

        /// <summary>
        ///     The nav URI property
        /// </summary>
        public static readonly DependencyProperty NavUriProperty =
            DependencyProperty.Register("NavUri", typeof(Uri), typeof(NavButton), new PropertyMetadata(null));

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the image source.
        /// </summary>
        /// <value>
        ///     The image source.
        /// </value>
        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        /// <summary>
        ///     Gets or sets the nav URI.
        /// </summary>
        /// <value>
        ///     The nav URI.
        /// </value>
        public Uri NavUri
        {
            get => (Uri)GetValue(NavUriProperty);
            set => SetValue(NavUriProperty, value);
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes the <see cref="NavButton" /> class.
        /// </summary>
        static NavButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NavButton),
                new FrameworkPropertyMetadata(typeof(NavButton)));
        }

        #endregion
    }
}