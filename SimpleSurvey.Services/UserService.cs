using SimpleSurvey.Data;
using SimpleSurvey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSurvey.Services
{
    public class UserService
    {
        public UserService()
        {

        }

        // GET all active users
        public IEnumerable<ApplicationUser> GetUsers()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Users;

                return query.ToArray();
                    
            }
        }
        // GET users by id
        public ApplicationUser GetUserById(string id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Single(e => e.Id == id);
                return entity;
            }
        }

    }
}
