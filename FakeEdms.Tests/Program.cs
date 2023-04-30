using System;
using FakeEdms;

namespace FakeEdmsQ.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataGenerator = new DataGenerator();
            var generated = dataGenerator.Generate<SimpleData>(10);
            Console.WriteLine(dataGenerator.Seed);
            foreach (var data in generated)
            {
                Console.WriteLine($"Age: {data.Age}, Date: {data.Date}, RegistrationNumberAdd: {data.RegistrationNumberAdd} Text: {data.Text}");
            }

            var dataGenerator2 = new DataGenerator(dataGenerator.Seed);
            var generated2 = dataGenerator2.Generate<SimpleData>(10);
            Console.WriteLine(dataGenerator2.Seed);
            foreach (var data in generated2)
            {
                Console.WriteLine($"Age: {data.Age}, Date: {data.Date}, RegistrationNumberAdd: {data.RegistrationNumberAdd} Text: {data.Text}");
            }
        }
    }
}