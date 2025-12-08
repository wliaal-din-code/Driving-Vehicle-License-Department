using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Buisness
{
    public class clsPerson
    {
        public enum enMode { AddNew, Update }

        public enMode Mode;

        public int PersonID { get; set; }
        public string NotionalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        private string _ImagePath { get; set; }
       public clsCountry CountryInfo { get; set; }

        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }
        }

        public string ImagePath
        { 
            get { return _ImagePath; }
            set { _ImagePath = value; }
                
        }

       public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.DateOfBirth = DateTime.Now;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
            this.Mode = enMode.AddNew;
        }

       public clsPerson( int personID, string notionalNo, string firstName, string secondName, string thirdName, string lastName, DateTime dateOfBirth, short gendor, string address, string phone, string email, int nationalityCountryID, string imagePath)
        {
            
            this.PersonID = personID;
            this.NotionalNo = notionalNo;
            this.FirstName = firstName;
            this.SecondName = secondName;
            this.ThirdName = thirdName;
            this.LastName = lastName;
            this.DateOfBirth = dateOfBirth;
            this.Gendor = gendor;
            this.Address = address;
            this.Phone = phone;
            this.Email = email;
            this.NationalityCountryID = nationalityCountryID;
            this.ImagePath = imagePath;
            CountryInfo = clsCountry.Find(this.NationalityCountryID);
            this.Mode = enMode.Update;

        }

        public static clsPerson Find(int PresonID)
        {
            string notionalNo = "", firstName = "", secondName = "", thirdName = "", lastName = "";
            DateTime dateOfBirth = DateTime.Now;
            short gendor = 0;
            string address = "", phone = "", email = "";
            int nationalityCountryID = -1;
            string imagePath = "";


            bool IsFound = clsPersonData.GetPersonInfoByID
                (

                PresonID, ref notionalNo, ref firstName, ref secondName,
                ref thirdName, ref lastName, ref dateOfBirth,
                ref gendor, ref address, ref email,
                ref nationalityCountryID, ref phone, ref imagePath

                );

            if (IsFound)
                return new clsPerson(PresonID, notionalNo, firstName, secondName, thirdName, lastName, 
                    dateOfBirth, gendor, address, phone, email, nationalityCountryID, imagePath);
            else
                return null;
                
        }

        public static clsPerson Find(string notionalNo)
        {
            int PresonID = -1;
             string firstName = "", secondName = "", thirdName = "", lastName = "";
            DateTime dateOfBirth = DateTime.Now;
            short gendor = 0;
            string address = "", phone = "", email = "";
            int nationalityCountryID = -1;
            string imagePath = "";


            bool IsFound = clsPersonData.GetPersonInfoByNationalNo
                (

                  notionalNo,ref PresonID, ref firstName, ref secondName,
                ref thirdName, ref lastName, ref dateOfBirth,
                ref gendor, ref address, ref email,
                ref nationalityCountryID, ref phone, ref imagePath

                );

            if (IsFound)
                return new clsPerson(PresonID, notionalNo, firstName, secondName, thirdName, lastName,
                    dateOfBirth, gendor, address, phone, email, nationalityCountryID, imagePath);
            else
                return null;

        }

        private bool _AddNewPerson()
        {
            this.PersonID = clsPersonData.AddNewPerson(this.NotionalNo, this.FirstName, this.SecondName,
                this.ThirdName, this.LastName,
                this.DateOfBirth, this.Gendor, this.Address, this.Phone, this.Email,
                this.NationalityCountryID, this.ImagePath);
            return (this.PersonID != -1);
        }

        private bool _UpdatePerson()
        {
           return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName,
        this.ThirdName, this.LastName, this.NotionalNo, this.DateOfBirth,
        this.Gendor, this.Address, this.Phone, this.Email,
         this.NationalityCountryID, this.ImagePath);
           
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                    case enMode.Update:
                   return _UpdatePerson();
                    

            }
            return false;

        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }

        public static bool DeletePerson(int PersonID)
        {
            return clsPersonData.DeletePerson(PersonID);
        }


        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }

        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }

    }
}
