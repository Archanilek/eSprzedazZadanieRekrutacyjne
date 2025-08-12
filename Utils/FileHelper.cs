using eSprzedazZadanieRekrutacyjne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace eSprzedazZadanieRekrutacyjne.Utils
{
    internal class FileHelper
    {
        private static readonly string ResultsFolder = Path.Combine(Directory.GetCurrentDirectory(), "UsedData");
        private static readonly string JsonFilePath = Path.Combine(ResultsFolder, "RegistredUsers.json");
        private static readonly string JsonFilePathToMultiRegistration = Path.Combine(ResultsFolder, "DataSetToRegistredUsers.json");

        public static void SaveRegistredUserToJsonFile(string email, string passsword)
        {
            if (!Directory.Exists(ResultsFolder))
                Directory.CreateDirectory(ResultsFolder);

            List<RegistrationData> registredUserList = new List<RegistrationData>();

            if (File.Exists(JsonFilePath))
            {
                String existingJson = File.ReadAllText(JsonFilePath);

                //sprawdzenie czy wczytane dane nie sa nullem/białymi znakami/pustym stringiem ""
                if (!string.IsNullOrWhiteSpace(existingJson))
                {
                    registredUserList = JsonSerializer.Deserialize<List<RegistrationData>>(existingJson);
                }
            }
            registredUserList.Add(new RegistrationData { Email = email, CreatedAt = DateTime.Now, Password = passsword, EncryptedPassword = EncryptedHepler.EncryptStringAES(passsword), DecryptedPassword = EncryptedHepler.DecryptStringAES(EncryptedHepler.EncryptStringAES(passsword)) });

            JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
            String json = JsonSerializer.Serialize(registredUserList, options);
            File.WriteAllText(JsonFilePath, json);
        }

        public static List<RegistrationData> LoadRegisteredUsersFromJsonFile()
        {
            if (!File.Exists(JsonFilePath))
                throw new FileNotFoundException($"Nie znaleziono pliku {JsonFilePath}");

            String json = File.ReadAllText(JsonFilePath);
            if (string.IsNullOrWhiteSpace(json))
                return new List<RegistrationData>();

            List<RegistrationData> users = JsonSerializer.Deserialize<List<RegistrationData>>(json);

            foreach(RegistrationData user in users)
            {
                if (!String.IsNullOrWhiteSpace(user.EncryptedPassword))
                {
                    user.EncryptedPassword = EncryptedHepler.DecryptStringAES(user.EncryptedPassword);
                }
            }

            return users;
        }
    }
}
