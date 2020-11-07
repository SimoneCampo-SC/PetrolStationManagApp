using System;
namespace PetrolStationManagementApp_V.High_
{
    public class FuellingTransaction
    {
        public string vehicleType;
        public double numberOfLitres;
        public int pumpNumber;
        public string fuelType;
        public bool FilledInTheFile { get; set; }
        public FuellingTransaction(string vehicleType, string fuelType, double numberOfLitres, int pumpNumber)
        {
            this.vehicleType = vehicleType;
            this.fuelType = fuelType;
            this.numberOfLitres = numberOfLitres;
            this.pumpNumber = pumpNumber;
            FilledInTheFile = false; // boolean property which helds the value connected to the transaction file
        }
    }
}

