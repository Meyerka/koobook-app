using KooBooKMVC.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KooBooKMVC
{
    public interface IUserData
    {
        void LockUser(string userId);
        void UnLockUser(string userId);

        IEnumerable<ApplicationUser> GetAll(
            Expression<Func<ApplicationUser, bool>> filter = null,
            Func<IQueryable<ApplicationUser>, IOrderedQueryable<ApplicationUser>> orderBy = null,
            string includeProperties = null
            );
    }
}