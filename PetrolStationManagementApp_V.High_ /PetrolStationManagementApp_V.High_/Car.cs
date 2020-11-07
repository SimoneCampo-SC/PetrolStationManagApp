using System;
namespace PetrolStationManagementApp_V.High_
{
    public class Car : Vehicle
    {
        public const int AMOUNT_TANK = 40;
        public Car() : base("Car", MyCarFuel(), AMOUNT_TANK, CurrentFuel(AMOUNT_TANK))
        {
        }
    }
}