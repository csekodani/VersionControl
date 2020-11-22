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
        [Test,
        TestCase("csekogmail.com",false),
        TestCase("daniel.cseko@stud.uni-corvinus.hu",true),
        TestCase("csekodaniel0@gmail.com",true),
        TestCase("csdani@@gmail.com",false)
        ]
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
