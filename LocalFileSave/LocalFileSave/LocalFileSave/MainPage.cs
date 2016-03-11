using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalFileSave
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
             var buttonTestAPI = new Button
            {
                Text = "Save some Text!",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            buttonTestAPI.Clicked += async (sender, e) => {

                try
                {
                   await DependencyService.Get<IFileService>().SaveAsync("SomeFile", "Some Text", false);
                }
                catch (Exception ex)
                {
                    var textMessage = ex.Message;
                }
                

            };
            var panel = new StackLayout();
            panel.Children.Add(buttonTestAPI);
            Content = panel;
        }


      


    }
}
