using System;
namespace PetrolStationManagementApp_V.High_
{
    public class HGV : Vehicle
    {
        public const int AMOUNT_TANK = 150;
        static Fuel myFuel = new Diesel();
        public HGV() : base("HGV", myFuel, AMOUNT_TANK, CurrentFuel(AMOUNT_TANK))
        {
        }
    }
}