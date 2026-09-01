namespace XGamingRuntime.Interop
{
	internal struct XStoreConsumableResult
	{
		internal readonly uint quantity;

		internal XStoreConsumableResult(XGamingRuntime.XStoreConsumableResult publicObject)
		{
			quantity = publicObject.Quantity;
		}
	}
}
