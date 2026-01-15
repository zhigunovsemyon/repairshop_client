using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repairshop_client.Models;

[Table("mechanics")]
public class Mechanic
{
	[Key]
	[Column("mechanic_id")]
	public Int16 MechanicId { get; set; }

	[Column("name")]
	public string Name { get; set; }

	[Column("surname")]
	public string Surname { get; set; }

	[Column("patronim")]
	public string? Patronim { get; set; }

	[Column("qualification")]
	public Int16 Qualification { get; set; }
}
