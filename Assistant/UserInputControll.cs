using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistant
{
    class UserInputControll
    {
        public const string InsuranceNumberMissing = "Not recorded";
        public const string InsuranceNumberInvalid = "Invalid";
        public static int InsuranceNumToInt(string insNum)
        {
            if (insNum.Contains("-"))
            {
                string tmp = "";
                foreach (var substr in insNum.Split('-'))
                {
                    tmp += substr;
                }
                insNum = tmp;
            }
            if (insNum.Equals(InsuranceNumberMissing))
            {
                return -1;
            }
            if (insNum.Equals(InsuranceNumberInvalid))
            {
                return -2;
            }

            return int.Parse(insNum);
        }

        public static string InsuranceNumToString(int? insNum)
        {
            if(insNum is null)
            {
                return InsuranceNumberMissing;
            }
            string str = insNum.ToString();
            while(str.Length < 9)
            {
                str = str.PadLeft(str.Length + 1, '0');
            }
            string[] token = new string[3];
            for (int i = 0; i < 3; i++)
            {
                token[i] = str.Substring(i * 3, 3);
            }
            return token[0] + "-" + token[1] + "-" + token[2];
        }
    }
}
