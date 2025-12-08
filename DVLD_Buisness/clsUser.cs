using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsUser
    {
        public  enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { set; get; }
        public int PersonID { set; get; }
        public clsPerson PersonInfo;
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }

        public clsUser() 
        {
            UserID = -1;
            UserName = "";
            Password = "";
            IsActive = true;
            Mode = enMode.AddNew;
        }

        public clsUser(int userID, int personID, string userName, string password, bool isActive)
        {
            
            UserID = userID;
            PersonID = personID;
            PersonInfo = clsPerson.Find(personID);
            UserName = userName;
            Password = password;
            IsActive = isActive;
        }

        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
                string UserName = "", Password = "";
            bool isActive = false;

            bool isFound = clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref isActive);

            if (isFound)
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            else
                return null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "", Password = "";
            bool isActive = false;

            bool isFound = clsUserData.GetUserInfoByPersonID(PersonID,ref UserID, ref UserName, ref Password, ref isActive);

            if (isFound)
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            else
                return null;
        }

        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool isActive = false;

            bool isFound = clsUserData.GetUserInfoByUsernameAndPassword( UserName,  Password,ref PersonID, ref UserID,  ref isActive);

            if (isFound)
                return new clsUser(UserID, PersonID, UserName, Password, isActive);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateUser();

            }

            return false;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(this.UserID, this.UserName, this.Password, this.IsActive);
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool isUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool isUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool isUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }
    }
}
