namespace Lektion4SoenderhoejTaxi.Models
{
	public class Bestilling
	{
		public string Kunde { get; set; }
		public string Adresse { get; set; }
		public string Tidspunkt { get; set; }

		public string Print()
		{
			return $"Taxa til {Kunde} på {Adresse} kl. {Tidspunkt}";
		}
	}
}