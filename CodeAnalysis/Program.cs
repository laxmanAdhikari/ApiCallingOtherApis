﻿using System;

class Animal
{
    public virtual string Speak(int x) { return "silence"; }
}

class Cat : Animal
{
    public string Speak(int x) { return "meow"; }
}

class Dog : Animal
{
    public string Speak(short x) { return "bow-wow"; }
}


/// <summary>
    //Here, the block Animal d = new Dog(); creates a reference variable of type Animal that points to an instance of the Dog class.
    //The speak method is overridden in both the Cat and Dog classes, but there is a key difference in the method signatures.The speak method in 
    //the Cat class has the same method signature as the one in the base class Animal, which means it overrides the base class method with the same signature.
    //In contrast, the speak method in the Dog class has a different method signature because it takes a short parameter instead of an int parameter. 
    //In the statement Console.Write(d.speak(0));, d is of type Animal, and it refers to an instance of the Dog class. However, since d is declared as an Animal,
    //the compiler looks at the speak method declared in the Animal class, which is public virtual string speak(int x). This method does not have a direct override in 
    //the Dog class, and it has a different parameter type (int) than the speak method in the Dog class (short).Due to the difference in parameter types, 
    //the d.speak(0) call resolves to the base class's method, which is Animal's speak(int x) method.Therefore, the method speak of the Animal class is invoked,
    //and it returns "silence," not "bow-wow." In C#, method resolution is determined at compile-time based on the declared type of the reference variable (d), and 
    //not at runtime based on the actual object type (Dog). To invoke the speak method from the Dog class and get "bow-wow" as the output, 
    //you would need to declare d as Dog or cast it explicitly to Dog, like this:
/// </summary>
class Program
{
    static void Main()
    {
        Animal d = new Dog();
        Console.Write(d.Speak(0));

        // Eliciltly cast to dog
        Console.WriteLine(d.Speak(0));

        // Declare as Dog so that it can speak bow-bow
        Dog dog = new Dog();
        Console.WriteLine(dog.Speak(0));


        
    }
}
