using System;
using FakeEdms;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace FakeEdms.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataGenerator = new DataGenerator<SimpleData>();
            dataGenerator
                .AsRegistrationNumber(sd => sd.RegistrationNumberAdd, @"^\d{2,3}-WD$", @"^[А-Яа-я]{4,10}-\d{4}-УД$")
                .AsDateBetween(sd => sd.Date, DateTime.Now.AddDays(5))
                .AsConsecutiveNumber(sd => sd.Age);
            
            var generated = dataGenerator.Generate(20);
            Console.WriteLine(dataGenerator.Seed);
            foreach (var data in generated)
            {                
                Console.WriteLine(JsonSerializer.Serialize(data, 
                                  new JsonSerializerOptions(){
                                    WriteIndented = true,
                                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic)
                                  }));
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