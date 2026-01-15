
namespace repairshop_client.Models;

public class Car
{
	public Int16 CarId { get; set; }

	public string Plate { get; set; }

	public string Brand { get; set; }

	public string Model { get; set; }
	
	public Int32 Year { get; set; }

	public Int16 ClientId { get; set; }
}
