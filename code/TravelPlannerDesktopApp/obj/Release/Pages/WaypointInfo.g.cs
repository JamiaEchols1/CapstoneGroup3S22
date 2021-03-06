#pragma checksum "..\..\..\Pages\WaypointInfo.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "9F80896979213879D4162FE4E2D1CCE00D8363C6B26018A770DEF7A2FC2BA9D8"
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
    /// WaypointInfo
    /// </summary>
    public partial class WaypointInfo : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\Pages\WaypointInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal TravelPlannerDesktopApp.Controls.NavButton backButton;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Pages\WaypointInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock descriptionTextBlock;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\WaypointInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock locationTextBlock;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Pages\WaypointInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock timeTextBlock;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\WaypointInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button editWaypointButton;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\WaypointInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button removeWaypointButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\WaypointInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock endDateTextBlock;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\Pages\WaypointInfo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WebBrowser wbMaps;
        
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
            System.Uri resourceLocater = new System.Uri("/TravelPlannerDesktopApp;component/pages/waypointinfo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\WaypointInfo.xaml"
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
            this.descriptionTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.locationTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.timeTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.editWaypointButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\Pages\WaypointInfo.xaml"
            this.editWaypointButton.Click += new System.Windows.RoutedEventHandler(this.EditWaypointButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 6:
            this.removeWaypointButton = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\Pages\WaypointInfo.xaml"
            this.removeWaypointButton.Click += new System.Windows.RoutedEventHandler(this.RemoveWaypointButton_OnClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.endDateTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.wbMaps = ((System.Windows.Controls.WebBrowser)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

