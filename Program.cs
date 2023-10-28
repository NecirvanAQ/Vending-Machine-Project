using System;
using System.Collections.Generic;
using System.Threading;

/*
THE CODE TO ENTER THE ADMIN PANEL IS 'necircode'!!!!!
*/

class Item // class for each item in the vending machine
{
    public string name;
    public int stock;
    public double price;
    public Item(string name, int stock, double price) // constructor which assigns all necassary attribues to each object created under the class Item
    {
        this.name = name;
        this.stock = stock;
        this.price = price;
    }

    public void Buy(ref int stock) // decreases stock by 1
    {
        stock--;
    }

    public void Restock(int stock) // restocks to max capacity
    {
        stock = 10;
    }

    public bool SoldOut(int stock)  // checks if item is sold out
    {
        if (stock == 0) { return true; }
        return false;
    }  
}
class Program
{
    static void Main(string[] args) // main method
    {
        // essential variables
        string choice = "";
        double total = 0;
        int selected = 0;

        Item twix = new Item("twix", 10, 0.8); Item doritos = new Item("doritos", 10, 1.2); Item snickers = new Item("bean", 10, 0.8); Item skittles = new Item("dog", 10, 0.6); Item niknaks = new Item("crust", 10, 1.2); Item crunchie = new Item("geeb", 10, 0.8); Item steak = new Item("steak", 10, 12); // making items

        List<Item> items = new List<Item> { twix, doritos, snickers, skittles, niknaks, crunchie, steak };  // list of items (in vending machine)  

        while (true) // main loop
        {
            OutputVendingMachine(items,total);
            WelcomeScreen(ref choice);

            if (choice == "1")
            {
                InsertCoins(ref total, items);
            }

            if (choice == "2")
            {
                Dispense(ref total, ref items, ref selected);
            }

            if (choice == "3")
            {
                Exit();
                choice = "";
                total = 0;
                selected = 0;
            }

            if (choice == "necircode") // admin code
            {
                Admin(ref items);
            }
        }
    }
    static void Admin(ref List<Item> items) // unlocks admin panel
    {
        string choice = "";

        while (choice != "3")
        {
            Console.Clear();
            Console.WriteLine("Admin Panel\n1) Restock items\n2) Add new item\n3) Exit\n");
            choice = Console.ReadLine();

            if (choice == "1")
            {
                foreach (Item item in items)
                {
                    item.Restock(item.stock);
                }
            }
            if (choice == "2")
            {

                Console.WriteLine("Enter name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter price: ");
                double price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter stock: ");
                int stock = Convert.ToInt32(Console.ReadLine());

                Item a = new Item(name, stock, price);

                items.Add(a);
            }
        }

    }
    public static void OutputVendingMachine(List<Item> items, double total) //outputs the vending machine
    {
        int i = 1;

        Console.Clear();
        Console.WriteLine("\nNecirvan's Vending Machine\n");
        Console.WriteLine("Number\tItem\tPrice\tStock");
        foreach (Item item in items)
        {
            Console.WriteLine($"{i}\t{item.name}\t{item.price}\t{item.stock}");
            i++;
        }
        Console.WriteLine($"\nTotal money entered: £{total}");
    }

    static void WelcomeScreen(ref string choice) // prints the welcome screen
    {
        Console.WriteLine("\n1) Insert coins\n2) Select product\n3) Exit\n");
        choice = Console.ReadLine();
    }

    static void Dispense(ref double total, ref List<Item> items, ref int selected) // handles user input for dispensing
    {

        OutputVendingMachine(items, total);
        Console.WriteLine("Enter choice: ");

        try { selected = Convert.ToInt32(Console.ReadLine());
        if (items[selected - 1].SoldOut(items[selected - 1].stock) == true)
        {
            Console.WriteLine("This item is sold out!");
            Console.ReadKey();
        }
        if (total > items[selected - 1].price && items[selected - 1].SoldOut(items[selected - 1].stock) == false)
        {
            total -= items[selected].price;
            items[selected].Buy(ref items[selected - 1].stock);
        }
        if (total < items[selected - 1].price)
        {
            Console.WriteLine("You do not have enough money!");
            Console.ReadKey();
        }
        else
        {

        }
        }
        catch { }
    }

    static void InsertCoins(ref double total, List<Item> items) // lets the user insert coins (with validation)
    {
        string tempTotal = "bean";
        while (tempTotal != "")
        {
            OutputVendingMachine(items, total);
            Console.WriteLine("Insert coins: ");
            tempTotal = Console.ReadLine();
            try
            {
                double insert = Convert.ToDouble(tempTotal);

                if (insert == 0.05 || insert == 0.10 || insert == 0.2 || insert == 0.5 || insert == 1 || insert == 2)
                {
                    total += insert;
                }
            }
            catch
            {
            }
        }
    }

    static void Exit() // refunds money and resets for next user
    {
        Console.Clear();
        Console.WriteLine("All money refunded!");
        Thread.Sleep(5000);
    }


}

