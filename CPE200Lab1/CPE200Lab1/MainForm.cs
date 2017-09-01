﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool hasDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private string firstOperand;
        private string operate;
        private CalculatorEngine engine;
        private bool symbolcheck = false;
        private string symbol;
        private string remember;
        private double memory;
        // private string num;

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            hasDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            symbolcheck = false;
        }

        public MainForm()
        {
            InitializeComponent();
            memory = 0;
            engine = new CalculatorEngine();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)//Num
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if (lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnUnaryOperator_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
            operate = ((Button)sender).Text;
            firstOperand = lblDisplay.Text;
            string result = engine.unaryCalculate(operate, firstOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }

        }

        private void btnOperator_Click(object sender, EventArgs e)
        {

            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterOperater)
            {
                return;
            }
            if (symbolcheck == false)
            {

                operate = ((Button)sender).Text;
                switch (operate)
                {
                    case "+":

                    case "-":

                    case "X":

                    case "÷":
                        firstOperand = lblDisplay.Text;
                        isAfterOperater = true;
                        break;
                    //case "%":
                    // break;
                    case "√":
                        string square;
                        double squareRoot;
                        square = lblDisplay.Text;
                        string root;
                        squareRoot = System.Math.Sqrt(Convert.ToDouble(square));
                        lblDisplay.Text = squareRoot.ToString();
                        symbolcheck = true;
                        break;
                    case "1/x":
                        if (lblDisplay.Text is "Error")
                        {
                            return;
                        }
                        string con;
                        double convert;
                        con = lblDisplay.Text;
                        convert = 1 / Convert.ToDouble(con);
                        lblDisplay.Text = convert.ToString();
                        break;
                    case "%":
                        return;



                }
                symbol = operate;
                symbolcheck = true;

            }
            else if (symbolcheck == true)
            {
                operate = ((Button)sender).Text;
                if (operate == "%")
                {
                    string second;
                    double x;
                    second = lblDisplay.Text;
                    x = (Convert.ToDouble(firstOperand) * (Convert.ToDouble(second) / 100));
                    lblDisplay.Text = x.ToString();
                }
                else
                {
                    btnEqual_Click(sender, e);
                    isAfterEqual = false;
                    {
                        switch (operate)
                        {
                            case "+":
                            case "-":
                            case "X":
                            case "÷":
                                firstOperand = lblDisplay.Text;
                                isAfterOperater = true;
                                break;
                        }
                    }
                }
            }
            isAllowBack = false;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (operate == "%")
            {
                if (isAfterEqual)
                {
                    return;
                }
                if (lblDisplay.Text is "Error")
                {
                    return;
                }
                operate = symbol;
                string secondOperand = lblDisplay.Text;
                string result = engine.calculate(operate, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                {
                    lblDisplay.Text = result;
                }
                isAfterEqual = true;
            }
            else
            {
                if (isAfterEqual)
                {
                    return;
                }
                if (lblDisplay.Text is "Error")
                {
                    return;
                }
                string secondOperand = lblDisplay.Text;
                string result = engine.calculate(operate, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                {
                    lblDisplay.Text = result;
                }
                isAfterEqual = true;
            }

        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!hasDot)
            {
                lblDisplay.Text += ".";
                hasDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)//บวกลบ
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
            firstOperand = string.Empty;
            operate = string.Empty;
        }

        private void btnBack_Click(object sender, EventArgs e)//กลับ
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if (lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if (rightMost is '.')
                {
                    hasDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if (lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }
    }

        /*public void lblDisplay_Click(object sender, EventArgs e)
        {

        }

        private void memmory_Click(object sender, EventArgs e)
        {
            
            string memoryOperate = ((Button)sender).Text;
            switch (memoryOperate)
            {
                case "MC":
                    remember = "0";
                    lblDisplay.Text = "0";
                }
            }

        private void btnMC_Click(object sender, EventArgs e)
        {

        }
    }

    private void btnMP_Click(object sender, EventArgs e)
        {
            if(lblDisplay.Text is "Error")
            {
                return;
            }
            memory += Convert.ToDouble(lblDisplay.Text);
            isAfterOperater = true;
        }

        private void btnMC_Click(object sender, EventArgs e)
        {
            memory = 0;
        }

        private void btnMM_Click(object sender, EventArgs e)
        {
            if(lblDisplay.Text is "Error")
            {
                return;
            }
            memory -= Convert.ToDouble(lblDisplay.Text);
            isAfterOperater = true;
        }

       /* private void btnMR_Click(object sender, EventArgs e)
        {
            if(lblDisplay.Text is "error")
            {
                return;
            }
            lblDisplay.Text = memory.ToString();
                    label1.Text = remember;
                    break;
                case "MR":
                    lblDisplay.Text = remember;
                    break;
                case "MS":
                    remember = lblDisplay.Text;
                    label1.Text = remember;
                    break;
                case "M+":
                    if (remember == "0") remember = lblDisplay.Text;
                    remember = (Convert.ToDouble(remember) + Convert.ToDouble(lblDisplay.Text)).ToString();
                    label1.Text = remember;
                    break;
                case "M-":
                    remember = (Convert.ToDouble(remember) - Convert.ToDouble(lblDisplay.Text)).ToString();
                    label1.Text = remember;
                    break;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }*/
    }

