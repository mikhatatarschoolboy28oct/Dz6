using System;

enum AccountType
{
    Текущий,
    Сберегательный,
    Кредитный
}

class BankAccount
{
    private static int nextNumber=1000;
    private int accountNumber;
    private decimal balance;
    private AccountType accountType; 

    private static int GenerateNumber()
    {
        return ++nextNumber;
    }

    public BankAccount()
    {
        accountNumber = GenerateNumber();
    }
    public void SetBalance(decimal value)
    {
        balance=value;
    }

    public void SetAccountType(AccountType type)
    {
        accountType=type;
    }

    public decimal GetBalance()
    {
        return balance;
    }

    public int GetAccountNumber()
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
        Console.WriteLine();


    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        BankAccount a1 = new BankAccount();
        a1.SetBalance(15250.75m);
        a1.SetAccountType(AccountType.Сберегательный);

        BankAccount a2 = new BankAccount();
        a2.SetBalance(500m);
        a2.SetAccountType(AccountType.Кредитный);

        a1.Print();
        a2.Print();
    }
}
