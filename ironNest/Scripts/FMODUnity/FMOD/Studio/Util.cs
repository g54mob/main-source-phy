using System.Runtime.InteropServices;

namespace FMOD.Studio
{
	[StructLayout((LayoutKind)0, Size = 1)]
	public struct Util
	{
		public static RESULT parseID(string idString, out GUID id)
		{
			id = default(GUID);
			return default(RESULT);
		}

		[PreserveSig]
		private static extern RESULT FMOD_Studio_ParseID(byte[] idString, out GUID id);
	}
}
