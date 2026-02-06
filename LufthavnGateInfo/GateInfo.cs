namespace LufthavnGateInfo
{
    public class GateInfo
    {
        public GateInfo(int gateNumber, string flightNumber)
        {
            GateNumber = gateNumber;
            FlightNumber = flightNumber;
        }

        public int GateNumber { get; set; }
        public string FlightNumber { get; set; }

    }
}
