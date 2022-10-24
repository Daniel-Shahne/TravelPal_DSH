using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPal_DSH.Users
{

    internal class UserManager
    {
        private IUser? signedInUser;
        private List<IUser> users = new()
        {
            new Admin("admin", "password", Enums.All_Countries.Sweden),
            new User("Gandalf", "password", Enums.All_Countries.Japan)
        };

        public UserManager() { }


        // ----------------------- METHODS -------------------------------


        /* Adds an user with bool as success status.
         * Does not add user if username is taken */
        public bool addUser(IUser user)
        {
            var takenUserNames = users.Select(user => user.Name).ToList();

            if (!takenUserNames.Contains(user.Name))
            {
                users.Add(user);
                return true;
            }
            else return false;
        }


        /* Used as step BEFORE signInUser to make sure said username
         * exists */
        private bool validateUsername(string userName)
        {
            IUser? userToLogin = (IUser?)users.First(user => user.Name.Equals(userName));

            if (userToLogin is not null) return true;
            else return false;
        }

        /* Should ONLY be called after validateUsername, to attempt
         * a login after an username has been verified to exist in
         * the list of users.*/
        public bool signInUser(string userName, string password)
        {
            IUser userToLogin = (IUser)users.First(user => user.Name.Equals(userName));

            if (userToLogin.Password.Equals(password))
            {
                signedInUser = userToLogin;
                return true;
            }
            else return false;
        }


        // Deletes an user
        public void deleteUser(IUser user)
        {
            users.Remove(user);
        }

        /* Updates an user's name, only if the new name is
         * not equal to old name. Uses bool as success status. */
        public bool updateUsername(IUser user, string newName)
        {
            if (!user.Name.Equals(newName))
            {
                user.Name = newName;
                return true;
            }
            else return false;
        }
    }
}
