using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XStorePackageUpdate
	{
		public string PackageIdentifier { get; private set; }

		public bool IsMandatory { get; private set; }

		internal XStorePackageUpdate(XGamingRuntime.Interop.XStorePackageUpdate interopStruct)
		{
			PackageIdentifier = interopStruct.GetPackageIdentifier();
			IsMandatory = interopStruct.isMandatory.Value;
		}
	}
}
