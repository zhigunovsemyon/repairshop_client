namespace repairshop_client.Models;

public class Client
{
	public Int16 ClientId { get; set; }

	public string Name { get; set; }
	
	public string Surname { get; set; }

	public string? Patronim { get; set; }

	public string? Email { get; set; }

	public Int64 Phone {  get; set; }
}
