using System;
using Xunit;

namespace Doctor.Tests
{
    public class InputFieldsTests
    {
        [Theory]
        [InlineData("123-123-123", 123123123)]
        [InlineData("123123123", 123123123)]
        public void InsuranceNumberConvertedCorrectly(string a, int expectedResult)
        {
            InputHandler handler = new InputHandler();
            int actualResult;
            handler.InsuranceNumToInt(a, out actualResult);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData(124226025, true)]
        [InlineData(044600174, true)]
        [InlineData(124226024, true)]
        public void InsuranceNumberCheckedCorrectly(int a, bool expectedResult)
        {
            InputHandler handler = new InputHandler();
            bool actualResult = handler.IsInsuranceNumber(a);
            Assert.Equal(expectedResult, actualResult);
        }

        [Theory]
        [InlineData("Kovács Tamás", true)]
        [InlineData("Nagy Attila", true)]
        [InlineData("Nagy  Attila", false)]
        [InlineData(" Nagy Attila", true)]
        [InlineData("Nagy Attila ", true)]
        [InlineData("nagy Attila", false)]
        [InlineData("Nagy attila", false)]
        [InlineData("NaGy Attile", false)]
        [InlineData("Dr. Nagy Attile", true)]
        [InlineData("dr. Nagy Attila", true)]
        [InlineData("Dr. nagy Attile", false)]
        [InlineData("Nagy Attila Gábor", true)]
        [InlineData("Nagy Attila Gábor Zoltán", false)]
        [InlineData("Dr. Nagy Attila Gábor",true)]
        public void NameStringCheckedCorrectly(string a, bool expectedResult)
        {

        }
    }
}
