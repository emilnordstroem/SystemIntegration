namespace Lektion4SoenderhoejTaxi.Models
{
	public class Bestilling
	{
		public int Id { get; set; }
		public string Kunde { get; set; }
		public bool PriorityPickUp { get; set; }
		public string Destination { get; set; }
		public string Tidspunkt { get; set; }

		public override string ToString()
		{
			return PriorityPickUp ? $"PRIORITY PICKUP: Taxa til {Kunde} på {Destination} kl. {Tidspunkt}" : $"Taxa til {Kunde} på {Destination} kl. {Tidspunkt}";
		}
	}
}