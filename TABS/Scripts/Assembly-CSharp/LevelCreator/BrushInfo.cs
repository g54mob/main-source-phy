namespace LevelCreator
{
	public struct BrushInfo
	{
		public StrengthSetting mScaleSetting;

		public StrengthSetting mRoughnessSetting;

		public float mYawAngle;

		public float mRollAngle;

		public float CalculateCurrentScale()
		{
			return 0.75f + 1.25f * Utility.FromStrengthValue(mScaleSetting);
		}

		public static bool AlmostEqual(BrushInfo a, BrushInfo b)
		{
			if (a.mScaleSetting == b.mScaleSetting && a.mRoughnessSetting == b.mRoughnessSetting && Utility.PositiveModulo((int)(a.mYawAngle - b.mYawAngle), 360) <= 0)
			{
				return Utility.PositiveModulo((int)(a.mRollAngle - b.mRollAngle), 360) <= 0;
			}
			return false;
		}
	}
}
