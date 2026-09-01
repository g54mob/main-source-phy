public static class MedalTierExtensions
{
	public static bool AtLeast(MedalTier a, MedalTier b)
	{
		//IL_000d: Expected O, but got I4
		//IL_001a: Expected O, but got I4
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		object obj = a - b;
		object obj2 = a ^ b;
		object obj3 = a ^ obj;
		object obj4 = obj2 & obj3;
		bool flag = (nint)obj4 < 0;
		bool flag2 = (nint)obj < 0;
		return flag2 == flag;
	}

	public static MedalTier Max(MedalTier a, MedalTier b)
	{
		MedalTier medalTier = default(MedalTier);
		if (medalTier <= b)
		{
		}
		return b;
	}

	public static MedalTier Clamp(int value)
	{
		if (value >= 0)
		{
			if (value > 3)
			{
				return MedalTier.Gold;
			}
			return (MedalTier)value;
		}
		return MedalTier.Unearned;
	}
}
