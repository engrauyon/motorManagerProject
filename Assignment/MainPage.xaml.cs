using Assignment_Summary_Page;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Assignment
{
    /// <summary>
    /// First Page for the Assignment of Calculating Vehicle Price.
    /// Student Name: Mofizul Islam.
    /// Student ID: 000989582.
    /// Date Created : 08/10/2017
    /// </summary>
    /// 

    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        //Global Variable declaration
        int totalNumberOfVehicles=0;
        double totalFinalAmount =0;

        const decimal WINDOW_TINTING = 150.00m;
        const decimal BODY_PROTECTION = 180.00m;
        const decimal GPS_NAVIGATION = 320.00m;
        const decimal SOUND_SYSTEM = 350.00m;

        const decimal WARRANTY_5YEAR = 0.2m;
        const decimal WARRANTY_3YEAR = 0.1m;
        const decimal WARRANTY_2YEAR = 0.05m;

        // Method to check customer details input
        // After checking, this method returns control to the caller
        private async Task<bool> CheckCustomerInput()
        {
            string customerName;
            int customerPhone;
            try
            {
                customerName = customerNameTextBox.Text;
                if (customerName == null || customerName == "")
                {
                    var dialogMessage = new MessageDialog("Error! Name can not be blank");
                    await dialogMessage.ShowAsync();
                    customerNameTextBox.Focus(FocusState.Programmatic);
                    customerNameTextBox.SelectAll();
                    return true;
                }
            }

            catch (Exception)
            {
                var dialogMessage = new MessageDialog("Please enter a valid Name ");
                await dialogMessage.ShowAsync();
                customerNameTextBox.Focus(FocusState.Programmatic);
                customerNameTextBox.SelectAll();
                return true;
            }

            try
            {
                if (customerPhoneTextBox.Text == null || customerPhoneTextBox.Text == "")
                {
                    var dialogMessage = new MessageDialog("Error! Phone Number can not be blank");
                    await dialogMessage.ShowAsync();
                    customerPhoneTextBox.Focus(FocusState.Programmatic);
                    customerPhoneTextBox.SelectAll();
                    return true;
                }
                customerPhone = int.Parse(customerPhoneTextBox.Text);
                
             }
            catch (Exception)
            {
                var dialogMessage = new MessageDialog("Error! Please enter a valid phone number ");
                await dialogMessage.ShowAsync();
                customerPhoneTextBox.Focus(FocusState.Programmatic);
                customerPhoneTextBox.SelectAll();
                return true;
            }
            return false;
        }

        //Method to calculate Warranty
        // returns only the warranty price
        private decimal calcVehicleWarranty(decimal vehiclePrice)
        {
            decimal warrantyPrice;

            if (warranty5YearRadioButton.IsChecked==true)
            {
                warrantyPrice =vehiclePrice * WARRANTY_5YEAR;
            }
            else if (warranty3YearRadioButton.IsChecked == true)
            {
                warrantyPrice = vehiclePrice * WARRANTY_3YEAR;
            }
            else if (warranty2YearRadioButton.IsChecked == true)
            {
                warrantyPrice = vehiclePrice * WARRANTY_2YEAR;
            }
            else
            {
                warrantyPrice = 0;
            }

            return warrantyPrice;
        }

        //Method to calculate optional extras
        private decimal calcOptionalExtras()
        {
            decimal optionalExtra=0;
            if (windowTintingCheckBox.IsChecked == true)
            {
                optionalExtra += WINDOW_TINTING;
            }
            if (bodyProtectionCheckBox.IsChecked == true)
            {
                optionalExtra += BODY_PROTECTION;
            }
            if (gpsNavigationCheckBox.IsChecked == true)
            {
                optionalExtra += GPS_NAVIGATION;
            }
            if (soundSystemCheckBox.IsChecked == true)
            {
                optionalExtra += SOUND_SYSTEM;
            }
           
            return optionalExtra;
        }

        private async void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //calls checkCustomerInput method to check inputs and dims the customer details input
            // and sets focus on vehiclePriceTextBox
            bool check = await CheckCustomerInput();
           if ( check)
            {
                return;
            }

           //disables customer input text boxes
            customerNameTextBox.IsEnabled = false;
            customerPhoneTextBox.IsEnabled = false;

            // sets focus on vehiclePrice TextBox
            vehiclePriceTextBox.Focus(FocusState.Programmatic);
            vehiclePriceTextBox.SelectAll();    
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            //clears all the input text boxes
            customerNameTextBox.Text=string.Empty;
            customerPhoneTextBox.Text = string.Empty;
            vehiclePriceTextBox.Text = string.Empty;
            tradeInTextBox.Text = string.Empty;

            //enables customer input text boxes
            customerNameTextBox.IsEnabled = true;
            customerPhoneTextBox.IsEnabled = true;


            //clears the warranty radio buttons
            warranty1YearRadioButton.IsChecked = true;
            warranty2YearRadioButton.IsChecked = false;
            warranty3YearRadioButton.IsChecked = false;
            warranty5YearRadioButton.IsChecked = false;

            //clears optional extras checkboxes
            windowTintingCheckBox.IsChecked = false;
            bodyProtectionCheckBox.IsChecked = false;
            gpsNavigationCheckBox.IsChecked = false;
            soundSystemCheckBox.IsChecked = false;

            //sets all the output text boxes
            subAmountTextBox.Text=string.Empty;
            gstAmountTextBox.Text = string.Empty;
            totalAmountTextBox.Text = string.Empty;

            //sets focus on customerNameTextBox
            customerNameTextBox.Focus(FocusState.Programmatic);
            customerNameTextBox.SelectAll();

        }

        private async void calcButton_Click(object sender, RoutedEventArgs e)
        {

            // Variable Declartion

            decimal vehiclePrice,tradeInValue, warrantyPrice, optionalExtra;
            double subAmount, gstAmount, totalAmount;
            const double GST_RATE = 0.1;

            //calls checkCustomerInput method to check inputs
            bool check = await CheckCustomerInput();
            if (check)
            {
                return;
            }


            //***************** Check all the numeric inputs are decimal ***************
            try
            {
                if (vehiclePriceTextBox.Text == null || vehiclePriceTextBox.Text == "")
                {
                    var dialogMessage = new MessageDialog("Error! Vehicle Price can not be blank");
                    await dialogMessage.ShowAsync();
                    vehiclePriceTextBox.Focus(FocusState.Programmatic);
                    vehiclePriceTextBox.SelectAll();
                    return;
                }
                vehiclePrice = decimal.Parse(vehiclePriceTextBox.Text);
            }

            catch (Exception)
            {
                var dialogMessage = new MessageDialog("Error! Please enter a valid Vehicle Price amount ");
                await dialogMessage.ShowAsync();
                vehiclePriceTextBox.Focus(FocusState.Programmatic);
                vehiclePriceTextBox.SelectAll();
                return;
            }

            try
                {
                    if (tradeInTextBox.Text == null || tradeInTextBox.Text == "")
                    {
                        tradeInTextBox.Text = "0";
                    }

                    tradeInValue = decimal.Parse(tradeInTextBox.Text);

                    if (tradeInValue >= vehiclePrice)
                    {
                        var dialogMessage = new MessageDialog("Error! Trade In Price must be lower than the Vehicle Price");
                        await dialogMessage.ShowAsync();
                        tradeInTextBox.Focus(FocusState.Programmatic);
                        tradeInTextBox.SelectAll();
                        return;
                    }
                }
                catch (Exception)
                {
                    var dialogMessage = new MessageDialog("Error! Please enter a valid Trade In amount ");
                    await dialogMessage.ShowAsync();
                    tradeInTextBox.Focus(FocusState.Programmatic);
                    tradeInTextBox.SelectAll();
                    return;
                }
            //************ end of Numeric input checking block ***********

           

            warrantyPrice = calcVehicleWarranty(vehiclePrice);  //call for calculating warranty price
            optionalExtra = calcOptionalExtras(); //call for optional extra
          
          /*    // message for testing warranty and optional extras
            var msg = new MessageDialog("Warranty Price is " + warrantyPrice);
            await msg.ShowAsync();

            var msg1 = new MessageDialog("Optional Extra amount is " + optionalExtra);
            await msg1.ShowAsync();
            */

            subAmount =  ((double)vehiclePrice + (double)warrantyPrice+ (double)optionalExtra) - (double)tradeInValue; // calculates sub amount

            gstAmount = subAmount * GST_RATE; //calculates GST Amount

            totalAmount = subAmount + gstAmount; //calculates total Amount

            

            //Displaying the calculation in appropriate text boxes
            
            subAmountTextBox.Text= String.Format("{0:C}", subAmount);
            gstAmountTextBox.Text = String.Format("{0:C}",gstAmount);
            totalAmountTextBox.Text = String.Format("{0:C}",totalAmount);

            //Adding values for daily summary
            totalNumberOfVehicles += 1;
            totalFinalAmount = totalFinalAmount + totalAmount;

            dailySummaryButton.IsEnabled = true;

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            mainSplitView.IsPaneOpen = !mainSplitView.IsPaneOpen;
        }

        private void dailySummaryButton_Click(object sender, RoutedEventArgs e)
        {
          /*  var msg = new MessageDialog("Vehicle Sold: " + totalNumberOfVehicles +"\nAmount Sold: " +totalFinalAmount);
            await msg.ShowAsync();*/

            string passValue = totalNumberOfVehicles.ToString() + "A" + totalFinalAmount.ToString();

           /* string url = "/DailySummaryPage.xaml" +
        "?number=" + System.Net.WebUtility.UrlEncode(totalNumberOfVehicles.ToString()) +
        "&amount=" + System.Net.WebUtility.UrlEncode(totalFinalAmount.ToString());

            NavigationService.Navigate(new Uri(url, UriKind.Relative));*/

            this.Frame.Navigate(typeof(DailySummaryPage), passValue);
        }
    }
}
