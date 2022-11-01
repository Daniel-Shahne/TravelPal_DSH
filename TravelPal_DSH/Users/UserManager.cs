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
            new User("Gandalf", "password", Enums.All_Countries.Sweden),
            new User("Daniel", "12345", Enums.All_Countries.Iran)
        };

        internal IUser? SignedInUser { get => signedInUser; }
        internal List<IUser> Users { get => users; }

        public UserManager() { }


        // ----------------------- METHODS -------------------------------


        /* Adds an user with bool as success status.
         * Does not add user if username is taken */
        public bool addUser(IUser user)
        {
            // Checks if username is taken
            if (validateUsername(user.Name)) return false;
            else
            {
                users.Add(user);
                return true;
            }
        }


        /* Used as step BEFORE signInUser to make sure said username
         * exists. Also used in addUser to check if username is taken */
        public bool validateUsername(string userName)
        {
            IUser? userToLogin = (IUser?)users.FirstOrDefault(user => user.Name.Equals(userName), null);

            if (userToLogin is not null) return true;
            else return false;
        }

        /* Should ONLY be called after validateUsername, to attempt
         * a login after an username has been verified to exist in
         * the list of users. Bool as success status, and sets
         * signedInUser upon success */
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
         * not taken. Uses bool as success status. */
        public void updateUsername(IUser user, string newName)
        {
            user.Name = newName;
        }

        // Checks if username is taken. Bool as status for taken or not
        public bool isUsernameTaken(string newName)
        {
            foreach (IUser iUser in users)
            {
                if (iUser.Name.Equals(newName)) return true;
            }
            return false;
        }
    }
}
