namespace FMOD
{
	public struct DSP_PARAMETER_DESC
	{
		public DSP_PARAMETER_TYPE type;

		public byte[] name;

		public byte[] label;

		public string description;

		public DSP_PARAMETER_DESC_UNION desc;
	}
}
