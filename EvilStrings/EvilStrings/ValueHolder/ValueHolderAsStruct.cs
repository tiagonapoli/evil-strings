namespace EvilStrings.ValueHolder
{
    public struct ValueHolderAsStruct
    {
        public int ElementId { get; }
        public int VehicleId { get; }
        public int Term { get; }
        public int Mileage { get; }
        public decimal Value { get; }

        public ValueHolderAsStruct(int elementId, int vehicleId, int term, int mileage, decimal value)
        {
            ElementId = elementId;
            VehicleId = vehicleId;
            Term = term;
            Mileage = mileage;
            Value = value;
        }
    }
}