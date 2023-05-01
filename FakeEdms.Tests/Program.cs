using System;
using FakeEdms;

namespace FakeEdms.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataGenerator = new DataGenerator<SimpleData>();
            /*dataGenerator
                .RuleFor(sd => sd.RegistrationNumberAdd, (faker, data) => GeneratorUtils.AsRegistrationNumber(@"^\d{2,3}-WD$"))
                .RuleFor(sd => sd.Text, (faker, data) => GeneratorUtils.AsSubject(faker, 1000));*/
            
            var generated = dataGenerator.Generate(1000);
            Console.WriteLine(dataGenerator.Seed);
            foreach (var data in generated)
            {
                Console.WriteLine($"Age: {data.Age}, Date: {data.Date}, RegistrationNumberAdd: {data.RegistrationNumberAdd} Text: {data.Text}");
            }

            /*var dataGenerator2 = new DataGenerator<SimpleData>(dataGenerator.Seed);
            var generated2 = dataGenerator2.Generate(10);
            Console.WriteLine(dataGenerator2.Seed);
            foreach (var data in generated2)
            {
                Console.WriteLine($"Age: {data.Age}, Date: {data.Date}, RegistrationNumberAdd: {data.RegistrationNumberAdd} Text: {data.Text}");
            }*/
        }
    }
}