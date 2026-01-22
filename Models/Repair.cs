using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repairshop_client.Models;

[Table("repair")]
public class Repair
{
	private DateOnly? _dateEnd;

	[Key]
	[Column("repair_id")]
	public Int16 RepairId { get; set; }

	[Column("car_id")]
	public Int16 CarId { get; set; }
	public virtual Models.Car Car { get; set; }

	[Column("mechanic_id")]
	public Int16 MechanicId { get; set; }
	public virtual Models.Mechanic Mechanic { get; set; }

	[Column("service_id")]
	public Int16 ServiceId { get; set; }
	public virtual Models.Service Service { get; set; }

	[Column("date_start")]
	public DateOnly DateStart { get; set; }

	[Column("date_end")]
	public DateOnly? DateEnd 
	{
		get => this._dateEnd; 
		set => this._dateEnd = (DateOnly.MinValue == value) 
			? null
			: value;
	}
}
