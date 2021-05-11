using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Test_Json.Net.Model;
using System.IO;

namespace Test_Json.Net
{
    class Program
    {
        private static string _path = @"C:\Json Sample\Contacts.json";
        static void Main(string[] args)
        {
            //var contacts = GetContacts();
            //SerializeJsonFile(contacts);

            var contacts = GetContactsJsonFromFile();
            //DeserializeJsonFile(contacts);

            ReadingJsonWithJSONTextReader(contacts);

        }

        #region "Writing JSON"
        public static void SerializeJsonFile(List<Contact> contacts)
        {
            string contactJson = JsonConvert.SerializeObject(contacts.ToArray(), Formatting.Indented);

            File.WriteAllText(_path, contactJson);
        }

        public static List<Contact> GetContacts()
        {
            List<Contact> contacts = new List<Contact> {
                new Contact
                {
                    Name = "John Wick",
                    DateOfBirth = new DateTime(1980, 05, 17),
                    Address = new Address {
                        Calle = "Centennial Dr",
                        Number = 15,
                        City = new City {
                            Name = "Chicago",
                            Country = new Country { Code = "USA", Name = "United States" }
                        }
                    },
                    Phones = new List<Phone>{
                        new Phone { Name = "Personal", Number = "02978641" },
                        new Phone { Name = "Work", Number = "865168468" }
                    }
                },
                new Contact
                {
                    Name = "Alfred Hitchcok",
                    DateOfBirth = new DateTime(1999, 08, 13),
                    Address = new Address {
                        Calle = "Av. Mariscal la Mar",
                        Number = 1260,
                        City = new City {
                            Name = "Lima",
                            Country = new Country { Code = "PER", Name = "Peru" }
                        }
                    },
                    Phones = new List<Phone>{
                        new Phone { Name = "Personal", Number = "029238641" },
                        new Phone { Name = "Work", Number = "213468468" }
                    }
                },
                new Contact
                {
                    Name = "Paul T. Anderson",
                    DateOfBirth = new DateTime(1970, 06, 26),
                    Address = new Address {
                        Calle = "Av. 9 de Julio",
                        Number = 513,
                        City = new City {
                            Name = "Buenos Aires",
                            Country = new Country { Code = "ARG", Name = "Argentina" }
                        }
                    },
                    Phones = new List<Phone>{
                        new Phone { Name = "Personal", Number = "50065561561" },
                        new Phone { Name = "Work", Number = "4566535416" }
                    }
                }
            };

            return contacts;
        }
        #endregion

        #region "Reading JSON"
        public static string GetContactsJsonFromFile()
        {
            string contactsJsonFromFile;
            using (var reader = new StreamReader(_path))
            {
                contactsJsonFromFile = reader.ReadToEnd();
            }
               
            return contactsJsonFromFile;
        }

        public static void DeserializeJsonFile(string contactsJsonFromFile)
        {
            Console.WriteLine(contactsJsonFromFile);

            var contacts = JsonConvert.DeserializeObject<List<Contact>>(contactsJsonFromFile);

            Console.WriteLine(string.Format("Paul T. Anderson's Address is: {0} {1}",
                contacts[2].Address.Calle, contacts[2].Address.Number));

            Console.WriteLine(string.Format("John Wick's Address is: {0}",
                contacts[0].DateOfBirth.ToShortDateString()));

        }
        #endregion

        public static void ReadingJsonWithJSONTextReader(string contactsJsonFromFile)
        {
            JsonTextReader reader = new JsonTextReader(new StringReader(contactsJsonFromFile));

            //while (reader.Read())
            //{
            //    if (reader.Value != null)
            //    {
            //        Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
            //    }
            //    else
            //    {
            //        Console.WriteLine("Token: {0}", reader.TokenType);
            //    }
            //}

            string dateofBirth = string.Empty;
            while (reader.Read())
            {
                if ((string.IsNullOrEmpty(dateofBirth)))
                {
                    if (reader.Value != null && reader.TokenType == JsonToken.Date)
                    {
                        dateofBirth = DateTime.Parse(reader.Value.ToString()).ToShortDateString();
                    }
                }
            }

            Console.WriteLine("John Wick's birthday is on " + dateofBirth);
        }
    }
}
