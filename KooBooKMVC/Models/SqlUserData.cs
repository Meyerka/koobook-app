using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KooBooKMVC.Models
{
    public class SqlUserData : IUserData
    {
        private readonly KoobookDbContext _db;
        public SqlUserData(KoobookDbContext db)
        {
            _db = db;
        }

        public IEnumerable<ApplicationUser> GetAll(Expression<Func<ApplicationUser, bool>> filter = null, Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> orderBy = null, string includeProperties = null)
        {
            IQueryable<ApplicationUser> query = _db.ApplicationUser;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            //include properties will be comma seperated
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public void LockUser(string userId)
        {
            var userfromdb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            userfromdb.LockoutEnd = DateTime.Now.AddYears(1000);
        }

        public void UnLockUser(string userId)
        {
            var userfromdb = _db.ApplicationUser.FirstOrDefault(u => u.Id == userId);
            userfromdb.LockoutEnd = DateTime.Now;
        }
    }
}
