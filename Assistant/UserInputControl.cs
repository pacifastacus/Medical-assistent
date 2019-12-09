using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Assistant
{
    class UserInputControl
    {
        public const string InsuranceNumberMissing = "Not recorded";
        public const string InsuranceNumberInvalid = "Invalid";
        public static bool InsuranceNumToInt(string insNum, out int? retVal)
        {
            insNum = insNum.Trim();
            if (Regex.IsMatch(insNum, "^([0-9]{3}-){2}[0-9]{3}$"))
            {
                string tmp = "";
                foreach (var substr in insNum.Split('-'))
                {
                    tmp += substr;
                }
                retVal = int.Parse(tmp);
                return true;
            }
            else if (Regex.IsMatch(insNum, "^[0-9]{9}$"))
            {
                retVal = int.Parse(insNum);
                return true;
            }
            else
            {
                retVal = null;
                return false;
            }
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

        public static bool CheckName(string name)
        {
            return Regex.IsMatch(name, "^[A-ZÓÚÖÜŐŰ][a-zóúöüőű']*(-[A-ZÓÚÖÜŐŰ][a-zóúöüőű']*)*$");
        }
    }
}
