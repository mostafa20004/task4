using System.Security.Principal;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Task
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string Name = "Unnamed Account", double Balance = 0.0)
        {
            this.Name = Name;
            this.Balance = Balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Name : {Name} ,Balance {Balance} "; ;
        }

        public static double operator+(Account lhs, Account rhs)
        {
            return lhs.Balance + rhs.Balance;
        }
    }

    class SavingsAccount : Account
    {
        public SavingsAccount( string name = "none", double balance = 0.0, double rate = 0.0) : base( name, balance)
        {
            Rate = rate;
        }

        public double Rate { get; set; }

        public override bool Withdraw(double amount) //drive
        {
            return base.Withdraw(amount + Rate);
        }

        public override string ToString()
        {
            return $"{base.ToString()},{Rate}";
        }
    }

    class TrustAccount : SavingsAccount
    {
        public TrustAccount(string name = "none", double balance = 0.0, double rate = 0.0,double bouns = 0.0) : base (name , balance,rate )
        {
            Bouns = bouns;
        }

        public double Bouns { get; set; }
        public int minYear = 0;
        public int maxYear = 3;
         public override bool Deposit(double amount)
        {
            if(amount >= 5000)
            {
                amount += 50;
                return base.Deposit(amount);
            }
            return false; 
        }

        public override bool Withdraw(double amount)
        {
           if (minYear >= maxYear || amount > Balance * 0.2)
                return false;

            if (base.Withdraw(amount))
            {
                minYear++;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"[Trust Account: {Name}, Balance: {Balance}, Bouns: {Bouns}%, Withdrawals: {minYear}]";
        }
    }

    class CheckingAccount : Account
    {
        public CheckingAccount(string name ="none", double balance =0.0, double fee = 0.0) : base(name, balance)
        {
            Fee = fee;
        }
        public double Fee = 1.50;

        public override bool Withdraw(double amount) //drive
        {
            return base.Withdraw(amount - Fee);
        }

        public override string ToString()
        {
            return $"{base.ToString()},{Fee}";
        }
    }
    public static class AccountUtil
    {
        // Utility helper functions for Account class

        public  static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
            {
                Console.WriteLine(acc);
            }
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount} from {acc}");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            // Accounts
            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Savings
            var savAccounts = new List<SavingsAccount>();
            savAccounts.Add(new SavingsAccount());
            savAccounts.Add(new SavingsAccount("Superman"));
            savAccounts.Add(new SavingsAccount("Batman", 2000));
            savAccounts.Add(new SavingsAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            // Checking
            var checAccounts = new List<CheckingAccount>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);
            AccountUtil.Withdraw(accounts, 2000);

            // Trust
            var trustAccounts = new List<TrustAccount>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Deposit(accounts, 6000);
            AccountUtil.Withdraw(accounts, 2000);
            AccountUtil.Withdraw(accounts, 3000);
            AccountUtil.Withdraw(accounts, 500);

            

            Console.WriteLine();
        }
        //public static void Display (Account account)
        //{
        //    Console.WriteLine(account);
        //}   
    }
}
