using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class RPNCalculatorEngine : CalculatorEngine
    {
           public string Process(string str)
        {
            int opcheck = 0;
            int numcheck = 0;
            string[] parts = str.Split(' ');
            string result = null;
            Stack NUM = new Stack();
           
            while (parts.Length > numcheck)
            {
                //int i = parts.Length;

                if (isNumber(parts[numcheck]))
                {
                    NUM.Push(parts[numcheck]);
                }
                else if (isOperator(parts[numcheck]))
                {
                    
                    string s2 = NUM.Pop().ToString();
                    string s1 = NUM.Pop().ToString();
                    result = calculate(parts[numcheck], s1, s2);
                    NUM.Push(result);
                }
                    numcheck++;
            }
            numcheck = 0;
            opcheck = 0;
            return NUM.Pop().ToString();
        }
    }
}
