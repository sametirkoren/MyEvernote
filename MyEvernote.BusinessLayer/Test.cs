using MyEvernote.DataAccessLayer.EntityFramework;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer
{
    public class Test
    {
        private Repository<EvernoteUser> repo_user = new Repository<EvernoteUser>();
        private Repository<Category> repo = new Repository<Category>();
        public Test()
        {
           
            List<Category> categories = repo.List();
        }

        public void InsertTest()
        {
           
            int result = repo_user.Insert(new EvernoteUser() { 
                Name="aaa",
                Surname="bbb",
                Email="deneme@gmail.com",
                ActivateGuid = Guid.NewGuid(),
                IsActive = true , 
                IsAdmin = true ,
                Username = "aabb",
                Password = "111",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUsername = "aabb"
            });
        }

        public void UpdateTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "aabb");

            if(user != null)
            {
                user.Username = "xxx";
                int result = repo_user.Update(user);
            }
        }

        public void DeleteTest()
        {
            EvernoteUser user = repo_user.Find(x => x.Username == "xxx");
            if(user != null)
            {
                int result = repo_user.Delete(user);
            }
        }
    }
}
