﻿#pragma checksum "..\..\..\Pages\TripInfo.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "95335DE533E5DA3A0BC0CDA416F15838B7279091B40C3C729CFA5BD1FEA62AD6"
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


namespace TravelPlannerDesktopApp.Pages {
    
    
    /// <summary>
    /// TripInfo
    /// </summary>
    public partial class TripInfo : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Pages\TripInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid pageGrid;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\TripInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid waypointsAndTransportDataGrid;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Pages\TripInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid lodgingDataGrid;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Pages\TripInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock selectedTripTextBlock;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\Pages\TripInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tripStartDateTextBlock;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Pages\TripInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tripEndDateTextBlock;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Pages\TripInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock descriptionTextBlock;
        
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
            System.Uri resourceLocater = new System.Uri("/TravelPlannerDesktopApp;component/pages/tripinfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\TripInfo.xaml"
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
            this.pageGrid = ((System.Windows.Controls.Grid)(target));
            
            #line 10 "..\..\..\Pages\TripInfo.xaml"
            this.pageGrid.AddHandler(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, new System.Windows.RoutedEventHandler(this.Grid_Click));
            
            #line default
            #line hidden
            return;
            case 2:
            this.waypointsAndTransportDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 23 "..\..\..\Pages\TripInfo.xaml"
            this.waypointsAndTransportDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.WaypointsAndTransportDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.lodgingDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 38 "..\..\..\Pages\TripInfo.xaml"
            this.lodgingDataGrid.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.LodgingDataGrid_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.selectedTripTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.tripStartDateTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.tripEndDateTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.descriptionTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

