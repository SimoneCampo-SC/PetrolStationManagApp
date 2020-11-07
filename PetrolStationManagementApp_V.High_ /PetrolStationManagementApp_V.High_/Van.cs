using System;
namespace PetrolStationManagementApp_V.High_
{
    public class Van : Vehicle
    {
        public const int AMOUNT_TANK = 80;
        public Van() : base("Van", MyVanFuel(), AMOUNT_TANK, CurrentFuel(AMOUNT_TANK))
        {
        }
    }
}