﻿using System;
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
            List<string> part = str.Split(' ').ToList<string>();
            while(part.Count > 1)
            {

            }





            return "0";
        }
    }
}
