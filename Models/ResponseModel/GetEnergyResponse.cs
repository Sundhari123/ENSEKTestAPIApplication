namespace ENSEKTestAPIApplication.Models.ResponseModel
{
    class GetEnergyResponse
    {
        public FuelTypeDetails electric { get; set; }
        public FuelTypeDetails gas { get; set; }
        public FuelTypeDetails nuclear { get; set; }
        public FuelTypeDetails oil { get; set; }
    }

    public class FuelTypeDetails
    {
        public int energy_id { get; set; }
        public double price_per_unit { get; set; }
        public int quantity_of_units { get; set; }
        public string unit_type { get; set; }
    }
}
