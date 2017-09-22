using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    public class RPNCalculatorEngine : CalculatorEngine
    {
        //int numcheck;
        int opcheck;
        public new string Process(string str)
        {
            if (str == null || str == "") { return "E"; }
            Stack<string> rpnStack = new Stack<string>();
            List<string> parts = str.Split(' ').ToList<string>();
            string result;
            string firstOperand, secondOperand;


            if (parts[0] == "x" || parts[0] == "/" || parts[0] == "-" || parts[0] == "+")
            {
                return "E";
            }

            foreach (string token in parts)
            {
                if (isNumber(token))
                {
                    rpnStack.Push(token);
                    // numcheck++;

                }
                else if (isOperator(token))
                {
                    //if (numcheck > 0) { return "E"; }
                    if (rpnStack.Count <= 1) { return "E"; }
                    opcheck++;
                    secondOperand = rpnStack.Pop();
                    firstOperand = rpnStack.Pop();
                    result = calculate(token, firstOperand, secondOperand, 4);

                    if (result is "E")
                    {
                        return result;
                    }

                    rpnStack.Push(result);
                }
                else if (isOver9000(token)) { return "E"; }
            }
            //FIXME, what if there is more than one, or zero, items in the stack?
            //if (numcheck > 0) { return "E"; }
            if (rpnStack.Count == 0 || rpnStack.Count > 1) { return "E"; }
            result = rpnStack.Pop();
            if (result == "0") { return result; } else if (opcheck == 0) { return "E"; }

            //if (numcheck + 1 == opcheck) { return "E"; }

            return result;
        }
    }
}
