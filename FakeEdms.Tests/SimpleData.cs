using System;

namespace FakeEdms.Tests
{
    public class SimpleData
    {
        public int? Age { get; set; }
        
        public DateTime? Date { get; set; }
        
        public string Text { get; set; }

        public string RegistrationNumberAdd { get; set; }

        public static SimpleData Create()
        {
            return new SimpleData();
        }
    }
}