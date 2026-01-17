using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace repairshop_client.Models;

[Table("service")]
public class Service
{
	[Key]
	[Column("service_id")]
	public Int16 SerivceId { get; set; }

	[Column("name")]
	public string Name { get; set; }

	[Column("price")]
	public Int32 Price { get; set; }

	public virtual ICollection<Models.Repair> Repairs { get; private set; }
		= new ObservableCollection<Models.Repair>();

	public override string ToString () => this.Name;
}
