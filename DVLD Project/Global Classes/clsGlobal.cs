using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using System.Xml.Linq;

namespace DVLD_Project_Wla.Global_Classes
{
    public class clsGlobal
    {
        public static clsUser CurrentUser;
        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
          
                string KeyPath = @"HKEY_CURRENT_USER\Software\DVLD";

                try
                {
                string Data = Username + "," + Password;
                    Registry.SetValue(KeyPath, "UserNameandPassowrd", Data);

                    Console.WriteLine($"Valu {"UserNameandPassowrd"} successfull to the Registry");
                return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error in occurred {ex.Message}");
                return false;
                }
          
            
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {

            string KeyPath = @"HKEY_CURRENT_USER\Software\DVLD";

            string Name = "UserNameandPassowrd";

            try
            {
                string value = Registry.GetValue(KeyPath, Name, null) as string;
                if (value != null)
                {
                    Console.WriteLine($"the Value of {Name} is: {value}");
                    
                    string[] result = value.Split(new string[] { "," }, StringSplitOptions.None);

                    Username = result[0];
                    Password = result[1];
                }
                else
                {
                    Console.WriteLine($"Value {Name} is not found in the Registry");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error in occurred {ex.Message}");
                return false;

            }
            return true;
        }
    }
}
