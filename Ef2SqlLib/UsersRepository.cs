using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ef2SqlLib {

    public class UsersRepository {

        private static PrsDb7Context context = new PrsDb7Context();

        public static List<Users> GetAll() {
            return context.Users.ToList();
        }
        public static Users GetByPk(int id) {
            return context.Users.Find(id);
        }
        public static bool Insert(Users user) {
            if(user == null) { throw new Exception("User instance must not be null"); }
            user.Id = 0;
            context.Users.Add(user);
            return context.SaveChanges() == 1;
        }
        public static bool Update(Users user) {
            if(user == null) { throw new Exception("User instance must not be null"); }
            var dbuser = context.Users.Find(user.Id);
            if(dbuser == null) { throw new Exception("No user with that id."); }
            dbuser.Username = user.Username;
            dbuser.Password = user.Password;
            // ...
            return context.SaveChanges() == 1;
        }
        public static bool Delete(Users user) {
            if(user == null) { throw new Exception("User instance must not be null"); }
            var dbuser = context.Users.Find(user.Id);
            if(dbuser == null) { throw new Exception("No user with that id."); }
            context.Users.Remove(dbuser);
            return context.SaveChanges() == 1;
        }
        public static bool Delete(int id) {
            var user = context.Users.Find(id);
            if(user == null) { return false; }
            var rc = Delete(user);
            return rc;
        }
    }
}
