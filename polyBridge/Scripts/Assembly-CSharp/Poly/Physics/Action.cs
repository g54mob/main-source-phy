using Poly.Base;

namespace Poly.Physics
{
	public class Action : WorldObject
	{
		internal short worldIdx;

		public World world { get; private set; }

		public bool isAddedToWorld => worldIdx >= 0;

		public virtual void SetWorldAndIndex(World world, int index)
		{
			this.world = world;
			worldIdx = (short)index;
		}

		protected new void OnValidate()
		{
			base.OnValidate();
		}

		protected new void Awake()
		{
			base.Awake();
			worldIdx = -1;
		}

		protected new void OnDestroy()
		{
			base.OnDestroy();
		}

		protected new void OnEnable()
		{
			base.OnEnable();
			Registry<Action>.Add(this);
		}

		protected new void OnDisable()
		{
			base.OnDisable();
			Registry<Action>.Remove(this);
		}

		public virtual void OnAddedToWorld()
		{
		}

		public virtual void Execute()
		{
		}

		public virtual void LateExecute()
		{
		}
	}
}
