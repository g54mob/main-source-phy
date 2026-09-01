using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStoreCanAcquireLicenseResult
	{
		public string LicensableSku { get; private set; }

		public XStoreCanLicenseStatus Status { get; private set; }

		internal XStoreCanAcquireLicenseResult(XGamingRuntime.Interop.XStoreCanAcquireLicenseResult interopStruct)
		{
			LicensableSku = interopStruct.GetLicensableSku();
			Status = interopStruct.status;
		}
	}
}
