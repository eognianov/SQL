using P01_BillsPaymentSystem.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Models.Enums;

namespace P01_BillsPaymentSystem.Initializer
{
    public class Initialize
    {
        public static void Seed(BPSContext dbContext)
        {
            var user = new User("John", "Smith", "smithJs@gmail.com", "somePassword");
            var cards = SeedCreditCards();
            var bankAccount = new BankAccount(1000m, "CoolBank", "SWPP252P");
            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod(user,cards[0],PaymentType.CreditCard),

                //======= test index ========// 
                //new PaymentMethod(user,cards[0],PaymentType.CreditCard),

                new PaymentMethod(user,cards[1],PaymentType.CreditCard),

                new PaymentMethod(user,bankAccount,PaymentType.BankAccount),

                //======test check constraint======//
                //new PaymentMethod(user,bankAccount,cards[1],PaymentType.BankAccount)

            };

            dbContext.Users.Add(user);
            dbContext.CreditCards.AddRange(cards);
            dbContext.BankAccounts.Add(bankAccount);
            dbContext.PaymentMethods.AddRange(paymentMethods);

            dbContext.SaveChanges();
        }

        private static CreditCard[] SeedCreditCards()
        {
            var cards = new CreditCard[]
            {
                new CreditCard(100m,50m,new DateTime(1999,02,05)),
                new CreditCard(20000,3000,new DateTime(2012,05,06)),
                new CreditCard(10m,0.5m,new DateTime(2018,02,05)),
                new CreditCard(3500m,500m,new DateTime(2013,06,04)),
                new CreditCard(2900m,2800m,new DateTime(2017,09,01)),
            };

            return cards;
        }
    }
}

