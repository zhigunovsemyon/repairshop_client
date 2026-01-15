namespace repairshop_client.Models;

public class Mechanic
{
	public Int16 MechanicId { get; set; }

	public string Name { get; set; }

	public string Surname { get; set; }

	public string? Patronim { get; set; }

	public Int16 Qualification { get; set; }
}
