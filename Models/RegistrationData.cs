using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eSprzedazZadanieRekrutacyjne.Models
{
    internal class RegistrationData
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string EncryptedPassword { get; set; }
        public string DecryptedPassword { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
