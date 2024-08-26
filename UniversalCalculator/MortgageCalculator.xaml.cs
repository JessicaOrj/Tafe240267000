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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MortgageCalculator : Page
	{
		public MortgageCalculator()
		{
			this.InitializeComponent();
		}

		private void calculateButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				// Retrieve input values from TextBoxes
				double principal = Convert.ToDouble(principalTextBox.Text);
				int years = Convert.ToInt32(yearsTextBox.Text);
				int months = Convert.ToInt32(monthsTextBox.Text);
				double annualInterestRate = Convert.ToDouble(yearInterestTextBox.Text) / 100;

				// Calculate the total number of months for the loan
				int totalMonths = (years * 12) + months;

				// Calculate the monthly interest rate
				double monthlyInterestRate = annualInterestRate / 12;

				// Display the monthly interest rate in the TextBox as a decimal
				monthlyInterestTextBox.Text = monthlyInterestRate.ToString("F6");

				// Use the formula to calculate the monthly repayment amount
				double M = principal * (monthlyInterestRate * Math.Pow(1 + monthlyInterestRate, totalMonths)) / (Math.Pow(1 + monthlyInterestRate, totalMonths) - 1);

				// Display the result in the monthlyRepaymentTextBox as currency
				monthlyRepaymentTextBox.Text = M.ToString("C2");
			}
			catch (Exception ex)
			{
				// Handle potential errors, such as invalid input
				ContentDialog errorDialog = new ContentDialog
				{
					Title = "Error",
					Content = "An error occurred during calculation. Please check your input values.",
					CloseButtonText = "OK"
				};
				_ = errorDialog.ShowAsync();
			}
		}

		private void exitButton_Click(object sender, RoutedEventArgs e)

			{
				Application.Current.Exit();
			}
		}
	}

