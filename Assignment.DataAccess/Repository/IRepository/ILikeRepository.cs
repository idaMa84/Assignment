using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Assignment.Models;

namespace Assignment.DataAccess.Repository.IRepository
{
    public interface ILikeRepository : IRepository<Like>
    {
        void Update(Like obj);
        void Save(Like obj);
        public List<Like> GetAll();
        public Like Get(string? id);
    }
}
