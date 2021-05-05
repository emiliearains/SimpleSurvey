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
        private readonly Guid _userId;

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

        // GET users by id range
        public List<ApplicationUser> GetUserByIdRange(List<string> idList)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Where(x => idList.Contains(x.Id))
                        .ToList();
                return entity;
            }
        }

        // GET users by department
        public List<ApplicationUser> GetUsersByDepartment(int departmentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Where(x => x.Department == departmentId)
                        .ToList();
                return entity;
            }
        }

        // GET users by job title
        public List<ApplicationUser> GetUsersByJobTitle(int jobTitleId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Users
                        .Where(x => x.JobTitle == jobTitleId)
                        .ToList();
                return entity;
            }
        }
    }
}
