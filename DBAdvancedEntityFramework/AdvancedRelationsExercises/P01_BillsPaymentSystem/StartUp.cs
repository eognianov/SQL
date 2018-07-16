using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using P01_BillsPaymentSystem.Data;
using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data.Models.Constants;
using P01_BillsPaymentSystem.Data.Models.Enums;
using P01_BillsPaymentSystem.Initializer;

namespace P01_BillsPaymentSystem
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (BPSContext context = new BPSContext())
            {

                //Initialize.Seed(context);
                //User user = GetUser(context);
                //GetUserInfo(user);


                PayBills(5, 1000000, context);
                Console.WriteLine();
            }
            
           
        }

        private static void GetUserInfo(User user)
        {
            Console.WriteLine($"User: {user.FirstName} {user.LastName}");
            var userBankAccs = user.PaymentMethods.Where(x => x.Type == PaymentType.BankAccount);
            if (userBankAccs.Any())
            {
                Console.WriteLine("BankAccounts:");
                foreach (var bankAcc in userBankAccs)
                {
                    Console.WriteLine($"-- ID: {bankAcc.BankAccountId}");
                    Console.WriteLine($"--- Balance: {bankAcc.BankAccount.Balance:F2}");
                    Console.WriteLine($"--- Bank: {bankAcc.BankAccount.BankName}");
                    Console.WriteLine($"--- SWIFT: {bankAcc.BankAccount.SWIFTCode}");
                }
            }
            var userCreditCards = user.PaymentMethods.Where(x => x.Type == PaymentType.CreditCard);
            if (userCreditCards.Any())
            {
                Console.WriteLine("CreditCards:");
                foreach (var creditCard in userCreditCards)
                {
                    Console.WriteLine($"-- ID: {creditCard.CreditCardId}");
                    Console.WriteLine($"--- Balance: {creditCard.CreditCard.Limit:F2}");
                    Console.WriteLine($"--- Bank: {creditCard.CreditCard.MoneyOwed:F2}");
                    Console.WriteLine($"--- SWIFT: {creditCard.CreditCard.LimitLeft:f2}");
                    Console.WriteLine($"--- Expiration Date: {creditCard.CreditCard.ExpirationDate.Year}/{creditCard.CreditCard.ExpirationDate.Month}");
                }
            }

        }

        private static User GetUser(BPSContext context)
        {
            User user = null;

            while (true)
            {
                Console.WriteLine("Enter UserId:");
                int userId = int.Parse(Console.ReadLine());
                user = context.Users.Where(x => x.UserId == userId).Include(x => x.PaymentMethods)
                    .ThenInclude(x => x.BankAccount).Include(x => x.PaymentMethods).ThenInclude(x => x.CreditCard)
                    .FirstOrDefault();

                if (user==null)
                {
                    Console.WriteLine($"User with id {userId} not found!");

                    userId = int.Parse(Console.ReadLine());
                    continue;
                    
                }
                break;
            }

            return user;
        }

        private static void Withdraw(decimal amount, PaymentMethod[] accountsMethods)
        {
            foreach (var account in accountsMethods)
            {
                var moneyToBeTaken = 0.0m;
                if (account.Type.Equals(PaymentType.CreditCard))
                {
                    moneyToBeTaken = Math.Min(amount, account.CreditCard.LimitLeft);
                    account.CreditCard.Withdraw(moneyToBeTaken);
                }
                else
                {
                    moneyToBeTaken = Math.Min(amount, account.BankAccount.Balance);
                    account.BankAccount.Withdraw(moneyToBeTaken);
                }

                amount -= moneyToBeTaken;

                if (amount == 0)
                {
                    break;
                }
            }
        }

        private static void PayBills(int userId, decimal amount, BPSContext context)
        {
            var accountsMethods = context
                .PaymentMethods
                .Include(pm => pm.BankAccount)
                .Where(pm => pm.UserId == userId && pm.Type == PaymentType.BankAccount)
                .OrderBy(pm => pm.BankAccountId)
                .ToArray();

            var cardsMethods = context
                .PaymentMethods
                .Include(pm => pm.CreditCard)
                .Where(pm => pm.UserId == userId && pm.Type == PaymentType.CreditCard)
                .OrderBy(pm => pm.CreditCardId)
                .ToArray();

            var totalMoney = accountsMethods.Sum(pm => pm.BankAccount.Balance) +
                             cardsMethods.Sum(c => c.CreditCard.LimitLeft);

            if (totalMoney >= amount)
            {
                Withdraw(amount, accountsMethods);
                Withdraw(amount, cardsMethods);

                context.SaveChanges();
                return;
            }

            Console.WriteLine(ErrorMessages.NotEnoughMoney);
        }
    }
}
