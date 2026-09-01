using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreConsumableResult
	{
		public uint Quantity { get; private set; }

		internal XStoreConsumableResult(XGamingRuntime.Interop.XStoreConsumableResult interopStruct)
		{
			Quantity = interopStruct.quantity;
		}
	}
}
