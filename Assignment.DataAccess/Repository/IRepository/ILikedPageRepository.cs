using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Assignment.Models;

namespace Assignment.DataAccess.Repository.IRepository
{
    public interface ILikedPageRepository : IRepository<LikedPage>
    {
        void Update(LikedPage obj);    
        public List<LikedPage> GetAll(string AccessToken);
        public LikedPage Get(string? id, string accessToken);
        public string GetFacebookAccessToken(string? urlWithFacebookCode);
    }
}
