using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bank;

namespace TestBank
{
    [TestClass]
    public class AccountUT
    {
        Account source;
        Account destination;

        [TestInitialize()]
        public void init()
        {
            source = new Account();
            destination = new Account();
        }

        [TestMethod]
        public void TransferFundsTest()
        {
            var sourceExpected = 100m;
            var destinationExpected = 250m;

            source.Deposit(200m);
            destination.Deposit(150m);

            source.TransferFunds(destination, 100m);

            Assert.AreEqual(source.Balance, sourceExpected);
            Assert.AreEqual(destination.Balance, destinationExpected);

        }

        [TestMethod]
        [ExpectedException(typeof(InsufficientFundsException))]
        public void InsufficientFundsTest()
        {
            var sourceExpected = 100m;
            var destinationExpected = 250m;

            source.Deposit(100m);
            destination.Deposit(150m);

            source.TransferFunds(destination, 100m);

            Assert.AreEqual(source.Balance, sourceExpected);
            Assert.AreEqual(destination.Balance, destinationExpected);

        }

        [TestMethod]
        public void InsufficientFundsStaticAccountsTest()
        {
            var sourceExpected = 100m;
            var destinationExpected = 150m;

            source.Deposit(100m);
            destination.Deposit(150m);

            try
            {
                source.TransferFunds(destination, 100m);
            }
            catch (InsufficientFundsException e)
            {
            }

            Assert.AreEqual(source.Balance, sourceExpected);
            Assert.AreEqual(destination.Balance, destinationExpected);
        }

    }
}
