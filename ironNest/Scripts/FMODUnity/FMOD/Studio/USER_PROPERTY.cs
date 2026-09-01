namespace FMOD.Studio
{
	public struct USER_PROPERTY
	{
		public StringWrapper name;

		public USER_PROPERTY_TYPE type;

		private Union_IntBoolFloatString value;

		public int intValue()
		{
			return 0;
		}

		public bool boolValue()
		{
			return false;
		}

		public float floatValue()
		{
			return 0f;
		}

		public string stringValue()
		{
			return null;
		}
	}
}
