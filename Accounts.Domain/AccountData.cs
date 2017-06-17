﻿using Banking.Domain;
using Base.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain
{
    public class AccountData
    {
        public AccountGuid Id { get; private set; }
        public SortCode SortCode { get; private set; }
        public Money Overdraft { get; private set; }
        public Money Balance { get; private set; }
        private ICollection<NewTransaction> Transactions { get; set; }

        public AccountData(AccountGuid id, SortCode sortCode, Money overdraft, Money balance, ICollection<NewTransaction> transactions)
        {
            Id = id;
            SortCode =  sortCode;
            Overdraft = overdraft;
            Balance = balance;
            Transactions = transactions;
        }

        public void AddTransaction(NewTransaction transaction)
        {
            if(transaction.InputAccountId == Id)
            {
                Balance = new Money(Balance.Amount - transaction.Amount);
            }
            else
            {
                Balance = new Money(Balance.Amount + transaction.Amount);
            }

            Transactions.Add(transaction);
        }
    }
}
