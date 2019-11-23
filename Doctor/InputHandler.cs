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
        public bool IsInsuranceNumber(int insNum)
        {
            int sum = 0;
            int crc = insNum % 10;
            for(int i = 0; i<8; i++)
            {
                insNum /= 10;
                int digit = insNum % 10;
                _ = (i % 2 == 0) ? sum += digit * 3 : sum += digit * 7;
            }
            return crc == sum % 10;
        }
        public bool InsuranceNumToInt(string insNum, out Int32 num)
        {
            bool retVal;
            if (Regex.IsMatch(insNum, "^([0-9]{3}-){2}[0-9]{3}$"))
            {
                string[] members = insNum.Split('-');
                retVal = int.TryParse(string.Concat(members), out num) &&
                    IsInsuranceNumber(num);


            }
            else if (Regex.IsMatch(insNum, "[0-9]{9}"))
            {
                retVal = int.TryParse(insNum, out num) &&
                    IsInsuranceNumber(num);
            }
            else
            {
                num = -1;
                retVal = false;
            }
            return retVal;
        }

        public bool InsuranceNumToString(int insNum, out string str)
        {
            if (!IsInsuranceNumber(insNum))
            {
                str = "Erroneous Number";
                return false;
            }
            str = insNum.ToString();
            string[] members = new string[3];
            for (int i = 0; i < 3; i++)
            {
                members[i] = str.Substring(i * 3, 3);
            }
            str = members[0] + "-" + members[1] + "-" + members[2];
            return true;
        }

        public bool IsName(string name)
        {
            name = name.Trim();
            return Regex.IsMatch(name, "^(Dr.|dr.)?[A-ZÁÉÍÓÚŐŰöü][a-záéíóúőűöü]+ ){1,2}[A-ZÁÉÍÓÚŐŰ][a-záéíóúőűöü]+$");
        }
    }
}
