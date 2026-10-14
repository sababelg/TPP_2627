class Program
{
    static void ShowBalance(int balance, char currency)
    {
        Console.WriteLine($"Your balance is {balance}{currency}");
    }
    static void Deposit(ref string[] history, ref int balance, int query, char currency)
    {
        if(query > 0)
        {
            Console.WriteLine($"You successfully deposited {query}{currency}");
            balance+=query;
            Array.Resize(ref history, history.Length+1);
            history[^1] = $"Deposited {query}{currency}";
        }
        else Console.WriteLine("You can not deposit 0");
        
    } 
    static void Withdraw(ref string[] history, ref int balance, int query, char currency)
    {
        if(query < 0) Console.WriteLine("You can not withdraw 0");
        else if(query > balance) Console.WriteLine("You can not withdraw more than your balance");
        else
        {
            Console.WriteLine($"You successfully withdrew {query}{currency}");
            balance-=query;
            Array.Resize(ref history, history.Length+1);
            history[^1] = $"Withdrew {query}{currency}";
        }
    }
    static void ShowHistory(ref string[] history)
    {
        if(!(history.Length==0)) foreach(string x in history) Console.WriteLine(x);
        
        else Console.WriteLine("You did not do any operation yet");
    }
    static void Main()
    {
        Console.WriteLine("Hello, that is ATM program. Please type your ballance");
        int balance = int.Parse(Console.ReadLine());
        Console.WriteLine("Now, chose your currency (Ꝑ default):\n[1] $\n[2] €\n[3] £\n[4] ¥\n[5] ₹");
        char currency;
        bool c = int.TryParse(Console.ReadLine(), out int cur);
        if(!c) currency = 'Ꝑ';
        else currency = cur == 1 ? '$' : cur == 2 ? '€' : cur == 3 ? '£' : cur == 4 ? '¥' : '₹';
        int operation = -1;
        string[] history = [];
        while (operation != 0)
        {
            Console.WriteLine("\nSelect operation:\n[0] Exit\n[1] See your ballance\n[2] Deposit money\n[3] Withdraw Money\n[4] See operations history");
            operation = int.Parse(Console.ReadLine());
            if(operation == 0)
            {
                Console.WriteLine("Bye");
                break;
            }
            else if (operation == 1) ShowBalance(balance, currency);
            else if (operation == 2)
            {
                Console.WriteLine("Please type amount you want to deposit:");
                int query = int.Parse(Console.ReadLine());
                Deposit(ref history, ref balance, query, currency);
            }
            else if (operation == 3)
            {
                Console.WriteLine("Please type amount you want to withdraw:");
                int query = int.Parse(Console.ReadLine());
                Withdraw(ref history, ref balance, query, currency);
            }
            else if (operation == 4) ShowHistory(ref history);
            else Console.WriteLine("Unknown command, please type number 0-4");
        }
    }
}