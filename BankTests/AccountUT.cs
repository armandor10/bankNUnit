using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;

namespace BankTests
{
    [TestClass]
    public class AccountUT
    {

        [TestInitialize()]
        public void MyTestInitialize()
        {
        }

        [TestCleanup()]
        public void MyTestCleanup()
        {
            //GC.SuppressFinalize(this);
        }

        [TestMethod]
        public void TransferFundsTest()
        {
            Account source = new Account();
            source.Deposit(200m);

            Account destination = new Account();
            destination.Deposit(150m);

            var sourceBalanceExpected = 100m;
            var destinationBalanceExpected = 250m;

            source.TransferFunds(destination, 100m);

            Assert.AreEqual(sourceBalanceExpected, source.Balance);
            Assert.AreEqual(destinationBalanceExpected, destination.Balance);
        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void TransferWithInsufficientFundsTest()
        {
            Account source = new Account();
            source.Deposit(100m);

            Account destination = new Account();
            destination.Deposit(150m);

            var sourceBalanceExpected = 100m;
            var destinationBalanceExpected = 250m;

            source.TransferFunds(destination, 100m);

            Assert.AreEqual(sourceBalanceExpected, source.Balance);
            Assert.AreEqual(destinationBalanceExpected, destination.Balance);
        }

        [TestMethod]
        public void TransferWithInsufficientFundsAtomicity()
        {
            Account source = new Account();
            source.Deposit(100m);

            Account destination = new Account();
            destination.Deposit(150m);

            var sourceBalanceExpected = 100m;
            var destinationBalanceExpected = 150m;

            try
            {
                source.TransferFunds(destination, 100m);
            }
            catch (InsufficientFundsException e)
            {
            }

            Assert.AreEqual(sourceBalanceExpected, source.Balance);
            Assert.AreEqual(destinationBalanceExpected, destination.Balance);
        }

    }
}
