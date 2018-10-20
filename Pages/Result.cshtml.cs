using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Secret.Model;

namespace Secret.Pages
{
    public class ResultModel : PageModel
    {
       
        private readonly PersonContext _db;

        public ResultModel(PersonContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Person Person { get; set; }

        public IList<Person> Persons { get; private set; }

        [BindProperty]
        public string Recipient { get; set; }

        public void OnGet()
        {
            Person = _db.Persons.First(p => p.Name.Equals(Request.Query["Name"], StringComparison.OrdinalIgnoreCase) && p.Phrase.Equals(Request.Query["Phrase"], StringComparison.OrdinalIgnoreCase));
            Recipient = new Shuffler(_db).GetMatchFor(Person.Name);
            Persons =  _db.Persons.AsNoTracking().ToList();

        }
    }


}