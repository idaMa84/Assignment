using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Assignment.DataAccess.Repository.IRepository;
using Assignment.Models;

namespace Assignment.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        
    }
}
