using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo.Styles
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DefaultTheme : ResourceDictionary
    {
        public DefaultTheme()
        {
            InitializeComponent();
        }
    }
}
