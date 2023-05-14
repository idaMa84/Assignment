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
        //public T Get(Expression<Func<T, bool>> filter)
        //{
        //    //List<Like> list = new List<Like>();
        //    //list.Add(new Like(1, "About", "Description"));
        //    //list.Add(new Like(2, "About2", "Description2"));
        //    //list.Add(new Like(3, "About3", "Description3"));

        //    ////Like? like = list.FirstOrDefault(filter);
        //    //return list;
        //    throw new NotImplementedException();
        //}

        //public IEnumerable<T> GetAll()
        //{
        //    //List<Like> list = new List<Like>();
        //    //list.Add(new Like(1, "About", "Description"));
        //    //list.Add(new Like(2, "About2", "Description2"));
        //    //list.Add(new Like(3, "About3", "Description3"));

        //    ////Like? like = list.FirstOrDefault(filter);
        //    //return list;

        //    List<Like> list = new List<Like>();
        //    list.Add(new Like(1, "About", "Description"));
        //    list.Add(new Like(2, "About2", "Description2"));
        //    list.Add(new Like(3, "About3", "Description3"));

        //    //Like? like = list.FirstOrDefault(filter);
        //    return <T> list;
        //    //throw new NotImplementedException();
        //}

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
