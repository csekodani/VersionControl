using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample
{
    class AccountControllerTestFixture
    {
        [Test]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            //Arrange
            var AccountController = new AccountController();
            //Act
            var actualResult = AccountController.ValidateEmail(email);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);

        }
    }
}
