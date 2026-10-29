using System;

class Building
{
    
    private static int lastNumber = 0;

    private int number;
    private double height;
    private int floors;
    private int apartments;
    private int entrances;

    
    private static int GenerateNumber()
    {
        return ++lastNumber;
    }

    public Building()
    {
        number = GenerateNumber();
    }

    
    public void SetHeight(double value)
    {
        height = value;
    }

    public void SetFloors(int value)
    {
        floors = value;
    }

    public void SetApartments(int value)
    {
        apartments = value;
    }

    public void SetEntrances(int value)
    {
        entrances = value;
    }

    
    public int GetNumber()
    {
        return number;
    }

    public double GetHeight()
    {
        return height;
    }

    public int GetFloors()
    {
        return floors;
    }

    public int GetApartments()
    {
        return apartments;
    }

    public int GetEntrances()
    {
        return entrances;
    }

    
    public double GetFloorHeight()
    {
        if (floors == 0) return 0;
        return height / floors;
    }

    public double GetApartmentsPerEntrance()
    {
        if (entrances == 0) return 0;
        return (double)apartments / entrances;
    }

    public double GetApartmentsPerFloor()
    {
        if (entrances == 0 || floors == 0) return 0;
        return (double)apartments / entrances / floors;
    }

    public void Print()
    {
        Console.WriteLine($"Здание №{number}");
        Console.WriteLine($"Высота:                 {height:F1} м");
        Console.WriteLine($"Этажность:              {floors}");
        Console.WriteLine($"Квартир всего:          {apartments}");
        Console.WriteLine($"Подъездов:              {entrances}");
        Console.WriteLine($"Высота этажа:           {GetFloorHeight():F2} м");
        Console.WriteLine($"Квартир в подъезде:     {GetApartmentsPerEntrance():F0}");
        Console.WriteLine($"Квартир на этаже:       {GetApartmentsPerFloor():F0}");
        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Building b1 = new Building();
        b1.SetHeight(27);
        b1.SetFloors(9);
        b1.SetApartments(108);
        b1.SetEntrances(3);

        Building b2 = new Building();
        b2.SetHeight(45);
        b2.SetFloors(15);
        b2.SetApartments(240);
        b2.SetEntrances(4);

        b1.Print();
        b2.Print();
    }
}