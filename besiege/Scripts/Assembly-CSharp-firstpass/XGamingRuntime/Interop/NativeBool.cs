namespace XGamingRuntime.Interop
{
	internal struct NativeBool
	{
		private byte value;

		internal bool Value
		{
			get
			{
				return value != 0;
			}
		}

		internal NativeBool(bool value)
		{
			this.value = (byte)(value ? 1u : 0u);
		}
	}
}
