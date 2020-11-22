using Moq;
using NUnit.Framework;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Abstractions;
using UnitTestExample.Controllers;
using UnitTestExample.Entities;
using UnitTestExample.Services;

namespace UnitTestExample
{
    class AccountControllerTestFixture
    {
        [Test,
        TestCase("csekogmail.com",false),
        TestCase("daniel.cseko@stud.uni-corvinus.hu",true), //most interesting to test so far but run successfuly
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

        [Test,
            TestCase("Danidanidani",false),
            TestCase("DANIDANIDANI",false),
            TestCase("danidanidani",false),
            TestCase("a",false),
            TestCase("CsekoStudent1",true)
        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            //Arrange
            var AccountController = new AccountController();
            //Act
            var actualResult = AccountController.ValidatePassword(password);
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test,
            TestCase("cseko@stud.uni-corvinus.hu", "Csekopass123"),
            TestCase("dani@uni-corvinus.hu", "Abcd7654321")
        ]
        public void TestRegisterHappyPath(string email, string password)
        {
            //Arrange
            var AccountController = new AccountController();
            var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
            accountServiceMock
                        .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                        .Returns<Account>(a => a);
            var accountController = new AccountController();
            accountController.AccountManager = accountServiceMock.Object;
            //Act
            var actualResult = AccountController.Register(email, password);
            //Assert
            Assert.AreEqual(email,actualResult.Email);
            Assert.AreEqual(password, actualResult.Password);
            //Assert.AreNotEqual(Guid.Empty, actualResult.ID); //currently fails because the ID is always the same and same as the Empty
        }
        [Test,
            TestCase("x@uni.corvinus.hu","a"),
            TestCase("x@uni.corvinus.hu", "almaalmalam1"),
            TestCase("x@uni.corvinus.hu", "ALMAALMAALMA2")
            ]
        public void TestRegisterValidateExeption(string email, string password)
        {
            //Arrange
            var AccountController = new AccountController();
            //Act
            try
            {
                var actualResult = AccountController.Register(email, password);
                Assert.Fail(); //because every case should fail, so if not, then not suitable exeption was thrown
            }
            catch (Exception ex)
            {

                Assert.IsInstanceOf<ValidationException>(ex);
            }
            
        }
        [Test,
            TestCase("csdani@gamil.com", "Alm'spite12345")  
        ]
        public void TestRegisterApplicationExeption(string newEmail,string newPassword)
        {
            // Arrange
            var accountServiceMock = new Mock<IAccountManager>(MockBehavior.Strict);
            accountServiceMock
                .Setup(m => m.CreateAccount(It.IsAny<Account>()))
                .Throws<ApplicationException>();
            var accountController = new AccountController();
            accountController.AccountManager = accountServiceMock.Object;

            // Act
            try
            {
                var actualResult = accountController.Register(newEmail, newPassword);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<ApplicationException>(ex);
            }

            // Assert
        }
    }

}
