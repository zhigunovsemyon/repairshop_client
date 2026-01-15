using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repairshop_client.Models;

[Table("repair")]
public class Repair
{
	[Key]
	[Column("repair_id")]
	public Int16 RepairId { get; set; }

	[Column("car_id")]
	public Int16 CarId { get; set; }

	[Column("mechanic_id")]
	public Int16 MechanicId { get; set; }

	[Column("service_id")]
	public Int16 ServiceId { get; set; }

	[Column("date_start")]
	public DateTime DateStart { get; set; }

	[Column("date_end")]
	public DateTime? DateEnd { get; set; }
}
