using Models.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Users.Interfaces
{
    public interface IUserDbContext
    {
        User Login(string email, string password);
    }
}
