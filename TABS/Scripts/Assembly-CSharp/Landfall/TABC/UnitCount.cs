namespace Landfall.TABC
{
	public class UnitCount
	{
		public UnitData unit;

		public int numberOfUnits;

		public UnitCount(UnitData unit, int numberOfUnits = 1)
		{
			this.unit = unit;
			this.numberOfUnits = numberOfUnits;
		}
	}
}
