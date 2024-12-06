using System;

public class Order
{
    public bool cheese, sauce, pep, ssg;

    public bool compare(Order other)
    {
        return this.cheese == other.cheese && this.sauce == other.sauce && this.pep == other.pep && this.ssg == other.ssg;
    }

    public String toString(){
        return $"Cheese: {cheese}, Sauce: {sauce}, Pep: {pep}, Ssg:{ssg}";
    }
}

public class InvalidOrder : Exception {
    public InvalidOrder(string msg){

    }
}