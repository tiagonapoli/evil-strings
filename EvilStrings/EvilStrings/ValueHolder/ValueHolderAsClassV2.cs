namespace EvilStrings.ValueHolder
{
    public class ValueHolderAsClassV2
    {
        public int ElementId { get; }
        public int VehicleId { get; }
        public int Term { get; }
        public int Mileage { get; }
        public decimal Value { get; }

        public ValueHolderAsClassV2(int elementId, int vehicleId, int term, int mileage, decimal value)
        {
            ElementId = elementId;
            VehicleId = vehicleId;
            Term = term;
            Mileage = mileage;
            Value = value;
        }
    }
}