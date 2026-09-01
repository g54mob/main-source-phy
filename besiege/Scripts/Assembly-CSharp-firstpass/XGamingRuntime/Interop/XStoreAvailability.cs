namespace XGamingRuntime.Interop
{
	internal struct XStoreAvailability
	{
		internal readonly UTF8StringPtr availabilityId;

		internal readonly XStorePrice price;

		internal readonly TimeT endDate;

		internal XStoreAvailability(XGamingRuntime.XStoreAvailability publicObject, DisposableCollection disposableCollection)
		{
			availabilityId = new UTF8StringPtr(publicObject.AvailabilityId, disposableCollection);
			price = new XStorePrice(publicObject.Price, disposableCollection);
			endDate = new TimeT(publicObject.EndDate);
		}
	}
}
