using Amazon.Runtime.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using YandexUploadService.Data.Entities;

namespace YandexUploadService.Helpers
{
    public static class PersonsHelper
    {
        public static IEnumerable<Person> GeneratePersons(IEnumerable<string> keys, string email)
        {
            if (keys == null) return null;
            if (keys.Count() == 0) return null;
            if (string.IsNullOrEmpty(email)) return null;

            List<Person> persons = new List<Person>();

            foreach (var k in keys)
            {
                Key key = new Key { PublicKey = k };
                Person person = new Person { Email = email, Key = key };
                persons.Add(person);
            }

            return persons;
        }
    }
}
