using System;

enum AccountType
{
    Текущий,
    Сберегательный,
    Кредитный
}

class BankAccount
{
    private string accountNumber;
    private decimal balance;
    private AccountType accountType; 

    public void SetBalance(decimal value)
    {
        balance=value;
    }

    public void SetAccountNumber(string number)
    {
        accountNumber=number;
    }

    public void SetAccountType(AccountType type)
    {
        accountType=type;
    }

    public decimal GetBalance()
    {
        return balance;
    }

    public string GetAccountNumber()
    {
        return accountNumber;
    }

    public AccountType GetAccountType()
    {
        return accountType;
    }

    public void Print()
    {
        Console.WriteLine($"Баланс:{balance:F2}");
        Console.WriteLine($"Номер счета:{accountNumber}");
        Console.WriteLine($"Тип счета:{accountType}");


    }
}

class Program
{
    static void Main()
    {
        BankAccount account = new BankAccount();
        account.SetBalance(122313.03m);
        account.SetAccountNumber("13278903499");
        account.SetAccountType(AccountType.Сберегательный);
        account.Print();
        
        Console.WriteLine();
        Console.WriteLine("Чтение через методы доступа:");
        Console.WriteLine("Номер: " + account.GetAccountNumber());
        Console.WriteLine("Баланс: " + account.GetBalance());
        Console.WriteLine("Тип: " + account.GetAccountType());
    }
}
