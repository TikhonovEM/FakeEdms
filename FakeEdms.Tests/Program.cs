using System;
using FakeEdms;

namespace FakeEdmsQ.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var generated = DataGenerator.Generate<SimpleData>(10);
            foreach (var data in generated)
            {
                Console.WriteLine($"Age: {data.Age}, Date: {data.Date}, RegistrationNumberAdd: {data.RegistrationNumberAdd} Text: {data.Text}");
            }
            
        }
    }
}