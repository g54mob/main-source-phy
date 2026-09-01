using System.Runtime.InteropServices;

namespace Photon.Bolt
{
	[StructLayout(LayoutKind.Explicit)]
	internal struct NetworkTrigger
	{
		[FieldOffset(0)]
		public int Frame;

		[FieldOffset(4)]
		public int History;

		public override bool Equals(object obj)
		{
			if (!(obj is NetworkTrigger))
			{
				return false;
			}
			return (NetworkTrigger)obj == this;
		}

		public override int GetHashCode()
		{
			return (-1414952011 * -1521134295 + Frame.GetHashCode()) * -1521134295 + History.GetHashCode();
		}

		public void Update(int frame, bool set)
		{
			if (frame > Frame)
			{
				int num = frame - Frame;
				History = ((num < 32) ? (History << num) : 0);
				if (set)
				{
					History |= 1;
				}
				Frame = frame;
			}
			else if (frame == Frame && set)
			{
				History |= 1;
			}
		}

		public static bool operator ==(NetworkTrigger a, NetworkTrigger b)
		{
			if (a.Frame == b.Frame)
			{
				return a.History == b.History;
			}
			return false;
		}

		public static bool operator !=(NetworkTrigger a, NetworkTrigger b)
		{
			if (a.Frame == b.Frame)
			{
				return a.History != b.History;
			}
			return true;
		}
	}
}
