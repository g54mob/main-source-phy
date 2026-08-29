using System.ComponentModel;

namespace Oculus.Platform
{
	public enum OfferType
	{
		[Description("UNKNOWN")]
		Unknown = 0,
		[Description("INTRO_OFFER")]
		INTROOFFER = 1,
		[Description("FREE_TRIAL")]
		FREETRIAL = 2
	}
}
