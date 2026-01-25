using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repairshop_client.Models;

[Table("repair")]
public class Repair
{
	private DateTime? _dateEnd;
	private DateTime _dateStart;

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
	public DateTime DateStart
	{
		get => this._dateStart;
		set => this._dateStart = DateTime.SpecifyKind(value, DateTimeKind.Utc);
	}

	[Column("date_end")]
	public DateTime? DateEnd
	{
		get => this._dateEnd;
		set => this._dateEnd = DateEndsetter(value);
	}

	private static DateTime? DateEndsetter(DateTime? val)
	{
		if (val is null) {
			return null;
		}
		DateTime notNull = (DateTime)val;
		return (DateTime.MinValue == val)
			? null
			: DateTime.SpecifyKind(notNull, DateTimeKind.Utc);
	}
}
