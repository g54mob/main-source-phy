namespace Interop
{
	public struct ColliderProperties
	{
		public byte __bits;

		public byte SupportsMaterial
		{
			readonly get
			{
				return BitHelper.ExtractRange(__bits, 0, 1);
			}
			set
			{
				BitHelper.SetRange(ref __bits, 0, 1, value);
			}
		}

		public byte SupportsTrigger
		{
			readonly get
			{
				return BitHelper.ExtractRange(__bits, 1, 1);
			}
			set
			{
				BitHelper.SetRange(ref __bits, 1, 1, value);
			}
		}

		public byte IsListeningTransform
		{
			readonly get
			{
				return BitHelper.ExtractRange(__bits, 2, 1);
			}
			set
			{
				BitHelper.SetRange(ref __bits, 2, 1, value);
			}
		}

		public byte HasModifiableContacts
		{
			readonly get
			{
				return BitHelper.ExtractRange(__bits, 3, 1);
			}
			set
			{
				BitHelper.SetRange(ref __bits, 3, 1, value);
			}
		}

		public byte Padding
		{
			readonly get
			{
				return BitHelper.ExtractRange(__bits, 4, 4);
			}
			set
			{
				BitHelper.SetRange(ref __bits, 4, 4, value);
			}
		}
	}
}
