using System;
using System.Collections.Generic;
using System.IO;

abstract class Notification
{
    private string text;

    public string Recipient { get; set; }
    public string Status { get; protected set; }

    public string Text
    {
        get { return text; }
        set
        {
            if (value != "")
            {
                text = value;
            }
        }
    }

    public Notification(string recipient, string text)
    {
        Recipient = recipient;
        this.text = text;
        Status = "Создано";
    }

    
    public abstract string GetChannel();
    protected abstract bool Deliver();

    public bool Send()
    {
        if (Deliver() == true)
        {
            Status = "Отправлено";
            return true;
        }
        else
        {
            Status = "Ошибка";
            return false;
        }
    }

    public bool Retry()
    {
        if (Status == "Ошибка")
        {
            return Send();
        }
        return false;
    }

    public void MarkAsRead()
    {
        if (Status == "Отправлено")
        {
            Status = "Прочитано";
        }
    }

    public string GetInfo()
    {
        return GetChannel() + " -> " + Recipient + ", статус: " + Status;
    }
}


class EmailNotification : Notification
{
    public string Subject { get; set; }
    public int AttachmentsKb { get; private set; }

    public EmailNotification(string address, string text) : base(address, text)
    {
        Subject = "Без темы";
    }

    public EmailNotification(string address, string text, string subject) : base(address, text)
    {
        Subject = subject;
    }

    public override string GetChannel()
    {
        return "Email";
    }

    public bool CheckAddress()
    {
        return Recipient.Contains("@");
    }

    protected override bool Deliver()
    {
        if (CheckAddress())
        {
            Console.WriteLine("  Письмо \"" + Subject + "\" отправлено");
            return true;
        }
        Console.WriteLine("  Неверный email");
        return false;
    }

    public void AddAttachment(int kb)
    {
        AttachmentsKb = AttachmentsKb + kb;
    }

    public void ShowAttachments()
    {
        Console.WriteLine("  Вложений: " + AttachmentsKb + " КБ");
    }
}


class SmsNotification : Notification
{
    public string SenderName { get; set; }

    public SmsNotification(string phone, string text) : base(phone, text)
    {
        SenderName = "INFO";
    }

    public SmsNotification(string phone, string text, string sender) : base(phone, text)
    {
        SenderName = sender;
    }

    public override string GetChannel()
    {
        return "SMS";
    }

    public bool CheckPhone()
    {
        return Recipient.StartsWith("+");
    }

    protected override bool Deliver()
    {
        if (CheckPhone())
        {
            Console.WriteLine("  SMS от " + SenderName + " отправлено");
            return true;
        }
        Console.WriteLine("  Номер должен начинаться с +");
        return false;
    }

    public int CountLength()
    {
        return Text.Length;
    }

    public double GetCost()
    {
        
        if (CountLength() <= 70)
        {
            return 2.5;
        }
        return 5.0;
    }
}


class PushNotification : Notification
{
    public string Title { get; set; }
    public bool IsUrgent { get; set; }

    public PushNotification(string token, string text) : base(token, text)
    {
        Title = "Уведомление";
        IsUrgent = false;
    }

    public PushNotification(string token, string text, string title) : base(token, text)
    {
        Title = title;
        IsUrgent = false;
    }

    public override string GetChannel()
    {
        return "Push";
    }

    public bool CheckToken()
    {
        return Recipient.Length >= 8;
    }

    protected override bool Deliver()
    {
        if (CheckToken())
        {
            Console.WriteLine("  Push \"" + Title + "\" отправлено");
            return true;
        }
        Console.WriteLine("  Токен слишком короткий");
        return false;
    }

    public string GetShortText()
    {
        if (Text.Length > 10)
        {
            return Text.Substring(0, 10) + "...";
        }
        return Text;
    }

    public void MakeUrgent()
    {
        IsUrgent = true;
    }
}


class NotificationCenter
{
    private List<Notification> list = new List<Notification>();

    public string Name { get; set; }

    public NotificationCenter(string name)
    {
        Name = name;
    }

    public void Add(Notification n)
    {
        list.Add(n);
    }

    public void SendAll()
    {
        foreach (Notification n in list)
        {
            Console.WriteLine(n.GetChannel() + ":");
            n.Send();
        }
    }

    public void RetryAll()
    {
        foreach (Notification n in list)
        {
            if (n.Status == "Ошибка")
            {
                Console.WriteLine(n.GetChannel() + " (повтор):");
                n.Retry();
            }
        }
    }

    public void PrintAll()
    {
        foreach (Notification n in list)
        {
            Console.WriteLine("  " + n.GetInfo());
        }
    }

    public void PrintStatistics()
    {
        int sent = 0;
        int errors = 0;
        int read = 0;

        foreach (Notification n in list)
        {
            if (n.Status == "Отправлено") sent++;
            if (n.Status == "Ошибка") errors++;
            if (n.Status == "Прочитано") read++;
        }

        Console.WriteLine("  Всего: " + list.Count);
        Console.WriteLine("  Отправлено: " + sent);
        Console.WriteLine("  Прочитано: " + read);
        Console.WriteLine("  Ошибок: " + errors);
    }

    public void ExportLog(string path)
    {
        List<string> lines = new List<string>();
        foreach (Notification n in list)
        {
            lines.Add(n.GetInfo());
        }
        File.WriteAllLines(path, lines);
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Виды уведомлений ===");
        Console.WriteLine("Уведомления бывают Email, SMS и Push.");
        Console.WriteLine("У всех есть получатель, текст и статус, но отправка разная.");
        Console.WriteLine();

        
        Console.Write("Введите email получателя: ");
        string email = Console.ReadLine();

        
        NotificationCenter center = new NotificationCenter("Центр");

        EmailNotification e1 = new EmailNotification(email, "Ваш заказ оформлен", "Заказ 1024");
        SmsNotification s1 = new SmsNotification("+79991234567", "Код: 4821");
        SmsNotification s2 = new SmsNotification("89990001122", "Скидка 20%", "SHOP");
        PushNotification p1 = new PushNotification("device-ABC123", "Новое сообщение в чате");

        center.Add(e1);
        center.Add(s1);
        center.Add(s2);
        center.Add(p1);

        Console.WriteLine();
        Console.WriteLine("--- Абстракция ---");
        Console.WriteLine("Класс Notification абстрактный, объект от него создать нельзя.");

        Console.WriteLine();
        Console.WriteLine("--- Инкапсуляция ---");
        e1.Text = "";
        Console.WriteLine("Пустой текст не принят, остался: " + e1.Text);

        Console.WriteLine();
        Console.WriteLine("--- Наследование ---");
        Console.WriteLine("Email, SMS и Push берут Send, Retry, MarkAsRead у Notification.");

        Console.WriteLine();
        Console.WriteLine("--- Полиморфизм: отправляем все одним циклом ---");
        center.SendAll();

        Console.WriteLine();
        Console.WriteLine("--- Повторная отправка ---");
        s2.Recipient = "+79990001122";
        center.RetryAll();

        Console.WriteLine();
        Console.WriteLine("--- Прочитано ---");
        s1.MarkAsRead();

        Console.WriteLine();
        Console.WriteLine("--- Другие методы ---");
        e1.AddAttachment(250);
        e1.ShowAttachments();
        Console.WriteLine("  Длина SMS: " + s1.CountLength() + ", цена: " + s1.GetCost());
        Console.WriteLine("  Короткий текст push: " + p1.GetShortText());

        Console.WriteLine();
        Console.WriteLine("--- Итог ---");
        center.PrintAll();

        Console.WriteLine();
        Console.WriteLine("--- Статистика ---");
        center.PrintStatistics();

        center.ExportLog("log.txt");
        Console.WriteLine();
        Console.WriteLine("Журнал сохранен в файл log.txt");
    }
}