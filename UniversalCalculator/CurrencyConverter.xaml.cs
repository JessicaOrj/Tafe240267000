using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation.Collections;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class CurrencyConverter : Page
	{
		const double US_EURO = 0.85189982;
		const double US_BRITISH = 0.72872436;
		const double US_INDIAN = 74.257327;
		const double EURO_US = 1.1739732;
		const double EURO_BRITISH = 0.8556672;
		const double EURO_INDIAN = 87.00755;
		const double BRITISH_US = 1.371907;
		const double BRITISH_EURO = 1.1686692;
		const double BRITISH_INDIAN = 101.68635;
		const double INDIAN_US = 0.011492628;
		const double INDIAN_EURO = 0.013492774;
		const double INDIAN_BRITISH = 0.0098339397;

		public CurrencyConverter()
		{
			this.InitializeComponent();
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu));
		}

		private async void currencyConversionButton_Click(object sender, RoutedEventArgs e)
		{
			double amount = 0;
			double total = 0;
			//Validate if the inserted ammoun is double, otherwise shows an error
			try
			{
				amount = double.Parse(amountTextBox.Text);
			}
			catch (Exception)
			{
				var dialogMessage = new MessageDialog("Error! Please Enter a number.");
				await dialogMessage.ShowAsync();
				amountTextBox.Focus(FocusState.Programmatic);
				amountTextBox.SelectAll();
				return;
			}
			//If both currencys are the same do not do any calculation 
			if (convertFromComboBox.SelectedIndex == convertToComboBox.SelectedIndex)
			{
				string currencySymbol = GetCurrencySymbol(convertFromComboBox.SelectedIndex);
				convertedAmountTextBlock.Text = $"{amount:F2} {currencySymbol} = {amount:F2} {currencySymbol}";
				fromRateTextBlock.Text = "";
				toRateTextBlock.Text = "";
				return;
			}
			switch (convertFromComboBox.SelectedIndex)
			{
				case 0: // If the user selects US Dollar
					switch (convertToComboBox.SelectedIndex)
					{
						case 1:
							//To euro
							total = amount * US_EURO;
							convertedAmountTextBlock.Text = $"{amount:F2} US Dollars = € {total:F2} Euro";
							fromRateTextBlock.Text = $"1 US Dollar = €{US_EURO:F8} Euro";
							toRateTextBlock.Text = $"1 Euro = ${EURO_US:F8} US Dollar";
							break;
						//To british pound
						case 2:
							total = amount * US_BRITISH;
							convertedAmountTextBlock.Text = $"{amount:F2} US Dollars = £ {total:F2} British Pounds";
							fromRateTextBlock.Text = $"1 US Dollar = £{US_BRITISH:F8} British Pounds";
							toRateTextBlock.Text = $"1 British Pound = ${BRITISH_US:F8} US Dollar";
							break;
						//To Indian Rupee
						case 3:
							total = amount * US_INDIAN;
							convertedAmountTextBlock.Text = $"{amount:F2} US Dollars = ₹ {total:F2} Indian Rupees";
							fromRateTextBlock.Text = $"1 US Dollar = ₹{US_INDIAN:F8} Indian Rupees";
							toRateTextBlock.Text = $"1 Indian Rupee = ${INDIAN_US:F8} US Dollar";
							break;
					}
					break;
				case 1: // If the user selects Euro
					switch (convertToComboBox.SelectedIndex)
					{
						case 0:
							//To US Dollar
							total = amount * EURO_US;
							convertedAmountTextBlock.Text = $"{amount:F2} Euro = ${total:F2} US Dollars";
							fromRateTextBlock.Text = $"1 Euro = ${EURO_US:F8} US Dollar";
							toRateTextBlock.Text = $"1 US Dollar = €{US_EURO:F8} Euro";
							break;
						case 2:
							//To british pound
							total = amount * EURO_BRITISH;
							convertedAmountTextBlock.Text = $"{amount:F2} Euro = £ {total:F2} British Pounds";
							fromRateTextBlock.Text = $"1 Euro = £{EURO_BRITISH:F8} British Pound";
							toRateTextBlock.Text = $"1 British Pound = €{BRITISH_EURO:F8} Euro";
							break;
						case 3:
							//To Indian Rupee
							total = amount * EURO_INDIAN;
							convertedAmountTextBlock.Text = $"{amount:F2} Euro = ₹ {total:F2} Indian Rupees";
							fromRateTextBlock.Text = $"1 Euro = ₹{EURO_INDIAN:F8} Indian Rupee";
							toRateTextBlock.Text = $"1 Indian Rupee = €{INDIAN_EURO:F8} Euro";
							break;
					}
					break;
				case 2: // If the user selects British Pounds
					switch (convertToComboBox.SelectedIndex)
					{
						case 0:
							//To US Dollar
							total = amount * BRITISH_US;
							convertedAmountTextBlock.Text = $"{amount:F2} British Pounds = ${total:F2} US Dollars";
							fromRateTextBlock.Text = $"1 British Pound = ${BRITISH_US:F8} US Dollar";
							toRateTextBlock.Text = $"1 US Dollar = £{US_BRITISH:F8} British Pound";
							break;
						case 1:
							//To euro
							total = amount * BRITISH_EURO;
							convertedAmountTextBlock.Text = $"{amount:F2} British Pounds = € {total:F2} Euro";
							fromRateTextBlock.Text = $"1 British Pound = €{BRITISH_EURO:F8} Euro";
							toRateTextBlock.Text = $"1 Euro = £{EURO_BRITISH:F8} British Pound";
							break;
						case 3:
							//To Indian Rupee
							total = amount * BRITISH_INDIAN;
							convertedAmountTextBlock.Text = $"{amount:F2} British Pounds = ₹ {total:F2} Indian Rupees";
							fromRateTextBlock.Text = $"1 British Pound = ₹{BRITISH_INDIAN:F8} Indian Rupees";
							toRateTextBlock.Text = $"1 Indian Rupee = £{INDIAN_BRITISH:F8} British Pound";
							break;
					}
					break;
				case 3: // If the user selects Indian Rupees
					switch (convertToComboBox.SelectedIndex)
					{
						case 0:
							//To US Dollar
							total = amount * INDIAN_US;
							convertedAmountTextBlock.Text = $"{amount:F2} Indian Rupees = ${total:F2} US Dollars";
							fromRateTextBlock.Text = $"1 Indian Rupee = ${INDIAN_US:F8} US Dollar";
							toRateTextBlock.Text = $"1 US Dollar = ₹{US_INDIAN:F8} Indian Rupees";
							break;
						case 1:
							//To euro
							total = amount * INDIAN_EURO;
							convertedAmountTextBlock.Text = $"{amount:F2} Indian Rupees = € {total:F2} Euro";
							fromRateTextBlock.Text = $"1 Indian Rupee = €{INDIAN_EURO:F8} Euro";
							toRateTextBlock.Text = $"1 Euro = ₹{EURO_INDIAN:F8} Indian Rupees";
							break;
						case 2:
							//To british pound
							total = amount * INDIAN_BRITISH;
							convertedAmountTextBlock.Text = $"{amount:F2} Indian Rupees = £ {total:F2} British Pounds";
							fromRateTextBlock.Text = $"1 Indian Rupee = £{INDIAN_BRITISH:F8} British Pound";
							toRateTextBlock.Text = $"1 British Pound = ₹{BRITISH_INDIAN:F8} Indian Rupees";
							break;
					}
					break;
			}

		}
		//This method is being used just when the user selects the same Currency on From and To options
		private string GetCurrencySymbol(int index)
		{
			switch (index)
			{
				case 0: return "$ US Dolar"; // US Dollar
				case 1: return "€ Euro"; // Euro
				case 2: return "£ British Pound"; // British Pound
				case 3: return "₹ Indian Rupee"; // Indian Rupee
				default: return "";
			}
		}

	}
}
