using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSI.HOSP.AlaAllegro.Application.Users.Queries
{
    public class UsersAllViewModel
    {
        public UsersAllViewModel(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
    }
}
