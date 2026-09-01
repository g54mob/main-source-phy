namespace MoreMountains.Tools
{
	public class MMLoot<T>
	{
		public T Loot;

		public float Weight;

		[MMReadOnly]
		public float ChancePercentage;

		public virtual float RangeFrom { get; set; }

		public virtual float RangeTo { get; set; }
	}
}
