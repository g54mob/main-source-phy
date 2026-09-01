using System;

namespace ModIO
{
	[Flags]
	public enum GameModRevenuePermissions
	{
		None = 0,
		AllowSales = 1,
		AllowDonations = 2,
		AllowModTrading = 4,
		AllowModScarcity = 8
	}
}
