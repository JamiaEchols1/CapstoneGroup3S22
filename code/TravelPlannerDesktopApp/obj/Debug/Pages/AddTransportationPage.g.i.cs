﻿#pragma checksum "..\..\..\Pages\AddTransportationPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "347728177931A84E1BD479637A734AEB2D7BB65392DE68187A6ADD20CCAA180E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using TravelPlannerDesktopApp.Controls;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Chromes;
using Xceed.Wpf.Toolkit.Core.Converters;
using Xceed.Wpf.Toolkit.Core.Input;
using Xceed.Wpf.Toolkit.Core.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Panels;
using Xceed.Wpf.Toolkit.Primitives;
using Xceed.Wpf.Toolkit.PropertyGrid;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using Xceed.Wpf.Toolkit.PropertyGrid.Commands;
using Xceed.Wpf.Toolkit.PropertyGrid.Converters;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.Zoombox;


namespace TravelPlannerDesktopApp.Pages {
    
    
    /// <summary>
    /// AddTransportationPage
    /// </summary>
    public partial class AddTransportationPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label addTransportTitle;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TravelPlannerDesktopApp.Controls.NavButton backButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label descriptionLabel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.DateTimePicker startDateTimePicker;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.DateTimePicker endDateTimePicker;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock overlappingLabel;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox overlappingListBox;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox originLocationTextBox;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox destinationLocationTextBox;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label OriginLabel;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label destinationLabel;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox typeComboBox;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\Pages\AddTransportationPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox descriptionTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TravelPlannerDesktopApp;component/pages/addtransportationpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\AddTransportationPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.addTransportTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.backButton = ((TravelPlannerDesktopApp.Controls.NavButton)(target));
            return;
            case 3:
            this.descriptionLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.saveButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Pages\AddTransportationPage.xaml"
            this.saveButton.Click += new System.Windows.RoutedEventHandler(this.createTransportationButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.startDateTimePicker = ((Xceed.Wpf.Toolkit.DateTimePicker)(target));
            
            #line 26 "..\..\..\Pages\AddTransportationPage.xaml"
            this.startDateTimePicker.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.datePicker_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.endDateTimePicker = ((Xceed.Wpf.Toolkit.DateTimePicker)(target));
            
            #line 28 "..\..\..\Pages\AddTransportationPage.xaml"
            this.endDateTimePicker.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.datePicker_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.overlappingLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.overlappingListBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 9:
            this.originLocationTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.destinationLocationTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 11:
            this.OriginLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.destinationLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 13:
            this.typeComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 14:
            this.descriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

