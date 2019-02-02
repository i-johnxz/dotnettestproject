using System;
using System.Collections.Generic;
using System.Text;

namespace UseExplanatoryVariables
{
    class BankAccount
    {
        private double _balance = 0.0D;

        public BankAccount(double balance = 1000)
        {
            _balance = balance;
        }

        public double WithdrawBalance(int amount)
        {
            if (amount > _balance)
            {
                throw new Exception("Amount greater than available balance");
            }

            _balance -= amount;

            return _balance;
        }

        public void DepositBalance(int amount)
        {
            _balance += amount;
        }

        public double getBalance()
        {
            return _balance;
        }
    }
}
