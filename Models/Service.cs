using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repairshop_client.Models;

[Table("service")]
public class Service
{
	[Key]
	public Int16 SerivceId { get; set; }

	public string Name { get; set; }

	public Int32 Price { get; set; }
}
