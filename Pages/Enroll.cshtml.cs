using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Secret.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Secret.Pages
{
    public class EnrollModel : PageModel
    {
        private readonly PersonContext _db;

        public EnrollModel(PersonContext db)
        {
            _db = db;
        }

        [BindProperty]
        public Person Person { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var ck = _db.Persons.FirstOrDefaultAsync(p => p.Name.Equals(Person.Name, StringComparison.OrdinalIgnoreCase));

            if (ck.Result == null)
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _db.Persons.Add(Person);
                await _db.SaveChangesAsync();
                return RedirectToPage("/Result", Person);
            }
            else
            {
                var person = ck.Result;

                if (!person.Phrase.Equals(Person.Phrase, StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToPage("/Error", Person);
                }
                else
                {
                    return RedirectToPage("/Result", person);
                }
            }


        }
    }
}