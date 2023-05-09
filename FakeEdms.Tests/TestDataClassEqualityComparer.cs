using System;
using System.Collections.Generic;

namespace FakeEdms.Tests
{
    public class TestDataClassEqualityComparer : IEqualityComparer<TestDataClass>
    {
        public bool Equals(TestDataClass x, TestDataClass y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Age == y.Age &&
                   // PROBLEM: Faker генеририует даты с разными миллисекундами, поэтому даем небольшую погрешность
                   (Nullable.Equals(x.Date, y.Date) || (x.Date.HasValue && y.Date.HasValue && Math.Abs((x.Date.Value - y.Date.Value).TotalSeconds) < 5)) && 
                   x.Text == y.Text && 
                   x.RegistrationNumberAdd == y.RegistrationNumberAdd && 
                   x.DocumentAnnotation == y.DocumentAnnotation && 
                   x.DoubleValue.Equals(y.DoubleValue) && 
                   Nullable.Equals(x.FloatValue, y.FloatValue);
        }

        public int GetHashCode(TestDataClass obj)
        {
            return HashCode.Combine(obj.Age, obj.Date, obj.Text, obj.RegistrationNumberAdd, obj.DocumentAnnotation, obj.DoubleValue, obj.FloatValue);
        }
    }
}