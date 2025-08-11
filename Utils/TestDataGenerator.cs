using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSprzedazZadanieRekrutacyjne.Utils
{
    internal class TestDataGenerator
    {
        public static string GenerateUniqueEmail()
        {
            return $"testuser_{Guid.NewGuid():N}@example.com";
        }

        public static string GetDefaultPassword()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
