using System;

public class Order
{
    public bool cheese, sauce, pep, ssg;

    public static readonly string[] OrderStrs = { "Cheese", "Pepperoni", "Sausage" };
    public static readonly Order[] basicOrders = { new(true, true, false, false), new(true, true, true, false), new(true, true, false, true) };

    public Order() { }
    public Order(bool c, bool s, bool p, bool sg)
    {
        cheese = c;
        sauce = s;
        pep = p;
        ssg = sg;
    }

    public bool compare(Order other)
    {
        return this.cheese == other.cheese && this.sauce == other.sauce && this.pep == other.pep && this.ssg == other.ssg;
    }

    public String toString()
    {
        return $"Cheese: {cheese}, Sauce: {sauce}, Pep: {pep}, Ssg:{ssg}";
    }
}

public class InvalidOrder : Exception
{
    public InvalidOrder(string msg)
    {

    }
}