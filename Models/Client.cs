using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repairshop_client.Models;

[Table("clients")]
public class Client
{
	[Key]
	[Column("client_id")]
	public Int16 ClientId { get; set; }

	[Column("name")]
	public string Name { get; set; }
	
	[Column("surname")]
	public string Surname { get; set; }

	[Column("patronim")]
	public string? Patronim { get; set; }

	[Column("email")]
	public string? Email { get; set; }

	[Column("phone")]
	public Int64 Phone {  get; set; }
}
