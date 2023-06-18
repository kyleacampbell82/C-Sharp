using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortfolioTaxCalculatorProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<string> errorList = new List<string>();
        string errorOutput = "";

        decimal i10 = 0; decimal c10 = 0;
        decimal i12 = 0; decimal c12 = 0;
        decimal i22 = 0; decimal c22 = 0;
        decimal i24 = 0; decimal c24 = 0;
        decimal i32 = 0; decimal c32 = 0;
        decimal i35 = 0; decimal c35 = 0;

        decimal adjustedGrossIncome = 0;
        decimal standardDeduction = 0;
        decimal taxableIncome = 0;
        decimal taxesDue = 0;


        // Event handler that updates taxable income every time the user adjusts taxable income.
        private void DisplayTaxableIncome(object sender, EventArgs e)
        {
            adjustedGrossIncome = Validator(box_adjustedGrossIncome.Text);
            string adjustedGrossIncomeStr = $"{adjustedGrossIncome}";
            box_adjustedGrossIncome.Text = adjustedGrossIncomeStr;

            lbl_StandardDeductionDisplay.Text = standardDeduction.ToString();
            
            if (standardDeduction == 0)
            {
                lbl_TaxableIncomeDisplay.Text = "0";
            }
            else
            {
                TaxableIncome();
                lbl_TaxableIncomeDisplay.Text = taxableIncome.ToString();
            }     
        }

        public decimal TaxableIncome()
        {
            taxableIncome = adjustedGrossIncome - standardDeduction;
            return taxableIncome;
        }


        // Takes user inputs and sums taxable income, sets dollar amounts for tax brackets based on radio button selection
        // Then passes to  SelectBracket to process and output the solution
        private void btn_Calculate_Click(object sender, EventArgs e)
        {
            string sumDisplay = $"{'$'}{taxableIncome}";
            lbl_TaxableIncomeDisplay.Text = sumDisplay;

            SelectBracket(taxableIncome);
            int taxesDueToInt = (int)Math.Round(taxesDue);
            string duesOutputStr = taxesDueToInt.ToString();
            lbl_FedDueDisplay.Text = duesOutputStr;
        }


        //  Setting dolloar amounts of each tax bracket based on IRS 2022 Federal Tax Brackets --
        //  Source https://www.nerdwallet.com/article/taxes/federal-income-tax-brackets
        private void SetFilingStatus(object sender, EventArgs e)
        {
            btn_Calculate.Enabled = true;
            if (rdo_Single.Checked == true)
            {
                i10 = 10275;    c10 = 1027.50m;
                i12 = 41775;    c12 = 4807.50m;
                i22 = 89075;    c22 = 15213.50m;
                i24 = 170050;   c24 = 34647.50m;
                i32 = 215950;   c32 = 49335.50m;
                i35 = 539900;   c35 = 162718;
                standardDeduction = 12950;
                lbl_StandardDeductionDisplay.Text = standardDeduction.ToString();
                if (adjustedGrossIncome > standardDeduction)
                {
                    taxableIncome = adjustedGrossIncome - standardDeduction;
                }
                else
                {
                    taxableIncome = 0;
                }
            }
            if (rdo_Joint.Checked == true)
            {
                i10 = 20550;    c10 = 2055;
                i12 = 83550;    c12 = 9615;
                i22 = 178150;   c22 = 30427;
                i24 = 340100;   c24 = 69295;
                i32 = 431900;   c32 = 98671;
                i35 = 647850;   c35 = 174253.50m;
                standardDeduction = 25900;
                lbl_StandardDeductionDisplay.Text = standardDeduction.ToString();
                if (adjustedGrossIncome > standardDeduction)
                    {
                    taxableIncome = adjustedGrossIncome - standardDeduction;
                }
                else
                {
                    taxableIncome = 0;
                }
            }
            if (rdo_Seperate.Checked == true)
            {
                i10 = 10275;    c10 = 1027.50m;
                i12 = 41775;    c12 = 4807.50m;
                i22 = 89075;    c22 = 15213.50m;
                i24 = 170050;   c24 = 34647.50m;
                i32 = 215950;   c32 = 49335.50m;
                i35 = 323925;   c35 = 87126.75m;
                standardDeduction = 12950;
                lbl_StandardDeductionDisplay.Text = standardDeduction.ToString();
                if (adjustedGrossIncome > standardDeduction)
                {
                    taxableIncome = adjustedGrossIncome - standardDeduction;
                }
                else
                {
                    taxableIncome = 0;
                }
            }
            if (rdo_Head.Checked == true)
            {
                i10 = 14650;    c10 = 1465;
                i12 = 55900;    c12 = 6415;
                i22 = 89050;    c22 = 13708;
                i24 = 170050;   c24 = 33148;
                i32 = 215950;   c32 = 47836;
                i35 = 539900;   c35 = 161218.50m;
                standardDeduction = 19400;
                lbl_StandardDeductionDisplay.Text = standardDeduction.ToString();
                if (adjustedGrossIncome > standardDeduction)
                {
                    taxableIncome = adjustedGrossIncome - standardDeduction;
                }
                else
                {
                    taxableIncome = 0;
                }
                lbl_TaxableIncomeDisplay.Text = taxableIncome.ToString();
            }
        }


        // Determining which tax bracket it falls under and passing data to correct method below
        public void SelectBracket(decimal income)
        {
            decimal c = 0;
            decimal overBracket = 0;
            if (income <= i10)
            {
                c = taxableIncome * .1m;
                taxesDue = c;
            }
            else if (income <= i12)
            {
                overBracket = taxableIncome - i10;
                c = (overBracket * .12m) + c10;
                taxesDue = c;
            }
            else if (income <= i22)
            {
                overBracket = taxableIncome - i12;
                c = (overBracket * .22m) + c12;
                taxesDue = c;
            }
            else if (income <= i24)
            {
                overBracket = taxableIncome - i22;
                c = (overBracket * .24m) + c22;
                taxesDue = c;
            }
            else if (income <= i32)
            {
                overBracket = taxableIncome - i24;
                c = (overBracket * .32m) + c24;
                taxesDue = c;
            }
            else if (income <= i35)
            {
                overBracket = taxableIncome - i32;
                c = (overBracket * .35m) + c32;
                taxesDue = c;
            }
            else if (income > i35)
            {
                overBracket = taxableIncome - i35;
                c = (overBracket * .37m) + c35;
                taxesDue = c;
            }
        }


        // Data Validation
        public decimal Validator(string userInput)
        {
            string checkDec = "";
            decimal userDec = 0;
            if (string.IsNullOrEmpty(userInput))
            {
                return 0;
            }
            else
            {
                checkDec = userInput;
                if (!(decimal.TryParse(checkDec, out userDec)))
                {
                    Error(1);
                }
            }
            if (errorList.Count > 0)
            {
                Error(0);
            }
            return userDec;
        }


        // Exception Handling
        public void Error(int a)
        {
            if (a == 0)
            {
                errorOutput = String.Join("\n", errorList);
                MessageBox.Show(errorOutput);
            }
            if (a == 1)
            {
                errorList.Add("You must use numbers only.");
            }
            if (a == 2)
            {
                errorList.Add("Input cannot be empty.");
            }
            if (errorList.Count > 0)
                return;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            lbl_TaxableIncomeDisplay.Text = "";
            lbl_FedDueDisplay.Text = "";
            taxableIncome = 0;
            taxesDue = 0;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
