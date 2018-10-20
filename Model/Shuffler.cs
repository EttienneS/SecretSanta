using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secret.Model
{
    public class Shuffler
    {
        private PersonContext _db;
        private Dictionary<string, string> _shuffled;

        private Random _r = new Random(1);

        public Shuffler(PersonContext db)
        {
            _db = db;
            _shuffled = new Dictionary<string, string>();

            var shuffledNames = Shuffle(_db.Persons.Select(n => n.Name).ToList());
            foreach (var person in _db.Persons)
            {
                var recipient = shuffledNames.First(n => n != person.Name);
                _shuffled.Add(person.Name, recipient);
                shuffledNames.Remove(recipient);
            }
        }

        private List<string> Shuffle(List<string> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                var k = _r.Next(n + 1);
                var value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

        public string GetMatchFor(string name)
        {
            return _shuffled[name];
        }

    }
}
