using System;
namespace PetrolStationManagementApp_V.High_
{
    public abstract class Fuel
    {
        protected string myFuel;

        public string MyFuel
        {
            get
            {
                return myFuel;
            }
            set
            {
                if ((value == "Unleaded") || (value == "Diesel") || (value == "LPG"))
                { 
                    myFuel = value;
                }
            }
        }
        public Fuel(string ftp)
        {
            this.MyFuel = ftp;
        }
    }
}
