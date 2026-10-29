using System;

enum AccountType
{
    Текущий,
    Сберегательный,
    Кредитный
}

class BankAccount
{
    private static int nextNumber = 1000;

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
        balance = value;
    }

    public void SetAccountType(AccountType type)
    {
        accountType = type;
    }

    public int GetAccountNumber()
    {
        return accountNumber;
    }

    public decimal GetBalance()
    {
        return balance;
    }

    public AccountType GetAccountType()
    {
        return accountType;
    }

    
    public bool Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            return false;
        }

        balance += amount;
        return true;
    }

    
    public bool Withdraw(decimal amount)
    {
        if (amount <= 0 || amount > balance)
        {
            return false;
        }

        balance -= amount;
        return true;
    }

    public void Print()
    {
        Console.WriteLine($"Номер счета: {accountNumber}");
        Console.WriteLine($"Баланс:      {balance:F2}");
        Console.WriteLine($"Тип счета:   {accountType}");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        BankAccount acc = new BankAccount();
        acc.SetBalance(1000m);
        acc.SetAccountType(AccountType.Текущий);
        Console.WriteLine("Начальное состояние:");
        acc.Print();

        acc.Deposit(500m);
        Console.WriteLine("После пополнения на 500:");
        acc.Print();

        if (acc.Withdraw(300m))
            Console.WriteLine("Снято 300:");
        else
            Console.WriteLine("Снятие 300 отклонено:");
        acc.Print();

        bool ok = acc.Withdraw(5000m);
        Console.WriteLine(ok ? "Снято 5000:" : "Снятие 5000 отклонено (недостаточно средств):");
        acc.Print();
    }
}

