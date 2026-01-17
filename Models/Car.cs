using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repairshop_client.Models;

[Table("car")]
public class Car
{
	[Key]
	[Column("car_id")]
	public Int16 CarId { get; set; }

	[Column("plate")]
	public string Plate { get; set; }

	[Column("brand")]
	public string Brand { get; set; }

	[Column("model")]
	public string Model { get; set; }
	
	[Column("year")]
	public Int32? Year { get; set; }

	[Column("client_id")]
	public Int16 ClientId { get; set; }
	public virtual Models.Client Client { get; set; }

	public virtual ICollection<Models.Repair> Repairs { get; private set; }
		= new ObservableCollection<Models.Repair>();

	public override string ToString () => $"{this.Brand} {this.Model} ({this.Plate})";
}
