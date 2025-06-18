using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;

namespace BankTests
{
    [TestClass]
    public class BankAccountTests
    {
        // Тест на положительный баланс
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act
            account.Debit(debitAmount);
            // Assert
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        // Тест на отрицательный баланс
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() =>
            account.Debit(debitAmount));
        }

        // Тест на превышение баланса суммой
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("The expected exception was not thrown.");
        }

        // Тест на добавление отрицательной суммы к балансу
        [TestMethod]
        public void Credit_WhenAmountIsNegative_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 100;
            double creditAmount = -100;
            BankAccount account = new BankAccount("Mr. Roman Abramovich", beginningBalance);
            // Act
            try
            {
                account.Credit(creditAmount);
            }
            catch (ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.CreditAmountLessThanZeroMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        // Тест на добавление положительной суммы к балансу
        [TestMethod]
        public void Credit_WithAmountIsPositive_UpdatesBalance()
        {
            // Arrange
            double beginningBalance = 100;
            double creditAmount = 1000;
            BankAccount account = new BankAccount("Mr.Roman Abramovich", beginningBalance);
            // Act
            account.Credit(creditAmount);
            // Assert
            Assert.AreEqual(expected: beginningBalance + creditAmount, actual: account.Balance, delta: 0.001, message: "Balance not updated correctly.");
        }

        // Тест на несколько последовательных зачислений
        [TestMethod]
        public void Cresit_MultipleTimes_AccumulatesBalance()
        {
            // Arrange
            double beginningBalance = 50.0;
            BankAccount account = new BankAccount("Test user", beginningBalance);
            // Act
            account.Credit(10.0);
            account.Credit(20.0);
            account.Credit(5.0);
            // Assert
            double excpected = 85.0;
            Assert.AreEqual(excpected, account.Balance, 0.001, "Balance should increase correctly after several deposits");
        }
    }
}
