using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistant
{
    class UserInputControll
    {
        public static int InsuranceNumToInt(string insNum)
        {
            return 0;
        }

        public static string InsuranceNumToString(int insNum)
        {
            string retVal = insNum.ToString();
            retVal.Insert(3,"-").Insert(7,"-");
            return retVal;
        }
    }
}
