namespace repairshop_client.Models;

public class Repair
{
	public Int16 RepairId { get; set; }

	public Int16 CarId { get; set; }

	public Int16 MechanicId { get; set; }

	public Int16 ServiceId { get; set; }

	public DateTime DateStart { get; set; }

	public DateTime? DateEnd { get; set; }
}
