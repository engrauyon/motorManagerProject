using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment_Summary_Page
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DailySummaryPage : Page
    {
        public DailySummaryPage()
        {
            this.InitializeComponent();
        }
       
        protected override void OnNavigatedTo(NavigationEventArgs e)
         {
            base.OnNavigatedTo(e);

            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                string passedValue = e.Parameter.ToString();

                int posOfA = passedValue.IndexOf("A");
                int startPosOfAmount = posOfA + 1;
                int lengthOfAmount = passedValue.Length - startPosOfAmount;
                string amount = passedValue.Substring(startPosOfAmount, lengthOfAmount);
                decimal totalAmount = Convert.ToDecimal(amount);

                totalNumberTextBox.Text = passedValue.Substring(0, posOfA);
                totalAmountTextBox.Text = String.Format("{0:C}",totalAmount);
                backButton.TabIndex = 0;
            }
                
            /* if (NavigationContext.QueryString.ContainsKey("number"))
            {
                string numberOfVehicle = NavigationContext.QueryString["number"];
                totalNumberTextBlock.Text = "Total Number of Vehicle Sold is : " + numberOfVehicle;
            }

            if (NavigationContext.QueryString.ContainsKey("amount"))
            {
                string totalFinalAmount = NavigationContext.QueryString["amount"];
                totalAmountTextBlock.Text = "Total Final Amount is : " + totalFinalAmount;
            }*/
        

            /*string data = e.Parameter.ToString();
            totalAmountTextBlock.Text = data;
             var msg = new MessageDialog(posOfA.ToString() + "Length "+lengthOfNumber.ToString()+passedValue );
                await msg.ShowAsync();
            */

            //LastIndexOf(@"\");
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();

            }
        }
    }
}
