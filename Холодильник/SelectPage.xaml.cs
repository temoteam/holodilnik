using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using AppStudio;
using AppStudio.DataProviders.RestApi;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Холодильник
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SelectPage : Page
    {
        private RestApiDataProvider _dataProvider;

        Dictionary<String, int> a;
        String enabled = "";
        public SelectPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;

            a  = new Dictionary<string, int>();
            a.Add("vjkjrj", 32);

        }

        public void getFood()
        {
            this.Frame.Navigate(typeof(RcpPage), enabled);
            _dataProvider = new RestApiDataProvider();

        }

        private void checkBox_Click(object sender, System.EventArgs e)
        {
            CheckBox checkBox = new CheckBox();
            checkBox = sender as CheckBox;
            if (checkBox.IsChecked.Value)
            {
                enabled = enabled + a[checkBox.Content.ToString()] + ",";
                
            }
            else
            {
                enabled.Replace(a[checkBox.Content.ToString()]+",", "");
            }
        }




    }
}
