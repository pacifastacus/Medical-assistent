using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Assistant
{
    public class UserInputControlTest
    {
        [Theory]
        [InlineData("123-123-123",123123123,true)]
        [InlineData("123123123", 123123123, true)]
        [InlineData("000-123-123", 000123123, true)]
        [InlineData("000123123", 000123123, true)]
        [InlineData("000-000-000", 000000000, true)]
        [InlineData("123123-123", null, false)]
        [InlineData("123-123123", null, false)]
        [InlineData("-123-123-123", null, false)]
        [InlineData("123-123-123-", null, false)]
        [InlineData("123123-123-", null, false)]
        [InlineData("-123-123123", null, false)]
        [InlineData("123-123-1a3", null, false)]
        [InlineData("123-23-13", null, false)]
        [InlineData("1231231230", null, false)]
        [InlineData("12312312", null, false)]
        public void Convert_InsuranceNumber_Test(string input, int? result, bool output)
        {
            //Arrange
            //Act
            int? actualResult;
            bool actualOutput = UserInputControl.InsuranceNumToInt(input, out actualResult);

            //Assert
            Assert.Equal(result, actualResult);
            Assert.Equal(output, actualOutput);
        }

        [Theory]
        [InlineData("Albert",true)]
        [InlineData(" Albert", false)]
        [InlineData("Albert ", false)]
        [InlineData(" Albert ", false)]
        [InlineData("albert", false)]
        [InlineData("ALbert", false)]
        [InlineData("Ödön",true)]
        [InlineData("ödön", false)]
        [InlineData("ÖdÖn", false)]
        [InlineData("ödÖn", false)]
        [InlineData("4lbert", false)]
        [InlineData("A1bert", false)]
        [InlineData("Albert 1", false)]
        [InlineData("Szent-Györgyi", true)]
        [InlineData("Szent-györgyi", false)]
        public void Name_Checked_correctly(string str, bool expected)
        {
            //Act
            bool actual = UserInputControl.CheckName(str);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
