using System.ComponentModel;

namespace Oculus.Platform
{
	public enum OfferTerm
	{
		[Description("UNKNOWN")]
		Unknown = 0,
		[Description("WEEKLY")]
		WEEKLY = 1,
		[Description("BIWEEKLY")]
		BIWEEKLY = 2,
		[Description("MONTHLY")]
		MONTHLY = 3,
		[Description("QUARTERLY")]
		QUARTERLY = 4,
		[Description("SEMIANNUAL")]
		SEMIANNUAL = 5,
		[Description("ANNUAL")]
		ANNUAL = 6,
		[Description("BIANNUAL")]
		BIANNUAL = 7
	}
}
