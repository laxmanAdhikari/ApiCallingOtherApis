using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

class A

{

    public int a { get; set; }

    public int b { get; set; }

}

class B

{

    //public const A a; // A const field requires a value to be provided

    public readonly A a = new A(); // Fix

    public B() { a.a = 10; }

}

class Program
{
    //static int Main() // Syntax error
    //{
    //    B b = new B();
    //    Console.WriteLine("%d %d\n", b.a.a, b.a.b); // not supported format // Member 'B.a' cannot be accessed with an instance reference; qualify it with a type name
    //    return 0;
    //}
    static int Main()
    {
        B b = new B();
        Console.WriteLine("{0} {1}\n", b.a.a, b.a.b); 
        return 0;

    }
}
