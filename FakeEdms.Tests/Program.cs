using System;
using FakeEdms;

namespace FakeEdms.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataGenerator = new DataGenerator<SimpleData>();
            dataGenerator
                .RuleFor(sd => sd.RegistrationNumberAdd, (faker, data) => 
                    GeneratorUtils.RegistrationNumber(@"^\d{2,3}-WD$", @"^[А-Яа-я]{4,10}-\d{4}-УД$"))
                .RuleFor(sd => sd.Date, (faker, data) => GeneratorUtils.Date(faker, DateTime.Now.AddDays(5)))
                .RuleFor(sd => sd.Age, (faker, data) => GeneratorUtils.ConsecutiveNumber(faker));
            
            var generated = dataGenerator.Generate(20);
            Console.WriteLine(dataGenerator.Seed);
            foreach (var data in generated)
            {
                Console.WriteLine($"Age: {data.Age}, Date: {data.Date}, RegistrationNumberAdd: {data.RegistrationNumberAdd}, Anno: {data.DocumentAnnotation}, Text: {data.Text}");
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