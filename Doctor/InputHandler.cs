using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Doctor
{
    public class InputHandler
    {
        public bool InsuranceNumStrToInt(string insNum, out Int32 num)
        {
            bool retVal;
            if (Regex.IsMatch(insNum, "^([0-9]{3}-){2}[0-9]{3}$"))
            {
                string[] members = insNum.Split('-');
                retVal = int.TryParse(string.Concat(members), out num);
            }
            else if (Regex.IsMatch(insNum, "[0-9]{9}"))
            {
                retVal = int.TryParse(insNum, out num);
            }
            else
            {
                num = -1;
                retVal = false;
            }
            return retVal;
        }

        public bool InsuranceNumIntToStr(int insNum, out string str)
        {
            str = insNum.ToString();
            if(str.Length > 9)
            {
                str = "Erroneous number";
                return false;
            }
            while (str.Length < 9)
            {
                str = str.PadLeft(str.Length + 1, '0');
            }
            string[] members = new string[3];
            for (int i = 0; i < 3; i++)
            {
                members[i] = str.Substring(i * 3, 3);
            }
            str = members[0] + "-" + members[1] + "-" + members[2];
            return true;
        }
    }
}
