﻿#pragma checksum "..\..\..\Pages\AddWaypoint.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "EF1BE7E592F480BE64B0DF1F80FBEF8ACA5E47B8E315CC1D51A03BE5AF23BCAE"
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
using TravelPlannerDesktopApp.Pages;
using Xceed.Wpf.Toolkit;


namespace TravelPlannerDesktopApp.Pages {
    
    
    /// <summary>
    /// AddWaypoint
    /// </summary>
    public partial class AddWaypoint : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\Pages\AddWaypoint.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TravelPlannerDesktopApp.Controls.NavButton backButton;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Pages\AddWaypoint.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox locationTextBox;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\AddWaypoint.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button createWaypointButton;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Pages\AddWaypoint.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Xceed.Wpf.Toolkit.DateTimePicker dateTimePicker;
        
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
            System.Uri resourceLocater = new System.Uri("/TravelPlannerDesktopApp;component/pages/addwaypoint.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\AddWaypoint.xaml"
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
            this.backButton = ((TravelPlannerDesktopApp.Controls.NavButton)(target));
            return;
            case 2:
            this.locationTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.createWaypointButton = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Pages\AddWaypoint.xaml"
            this.createWaypointButton.Click += new System.Windows.RoutedEventHandler(this.CreateWaypointButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 4:
            this.dateTimePicker = ((Xceed.Wpf.Toolkit.DateTimePicker)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

