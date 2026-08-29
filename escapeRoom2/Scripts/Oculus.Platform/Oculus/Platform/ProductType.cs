using System.ComponentModel;

namespace Oculus.Platform
{
	public enum ProductType
	{
		[Description("UNKNOWN")]
		Unknown = 0,
		[Description("DURABLE")]
		DURABLE = 1,
		[Description("CONSUMABLE")]
		CONSUMABLE = 2,
		[Description("SUBSCRIPTION")]
		SUBSCRIPTION = 3
	}
}
