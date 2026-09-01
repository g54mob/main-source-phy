namespace Photon.Bolt.SceneManagement
{
	internal struct Scene
	{
		public readonly int Index;

		public readonly int Sequence;

		public readonly bool Valid;

		public Scene(int index, int sequence)
		{
			Valid = index != -1;
			if (!Valid)
			{
				index = 0;
			}
			Index = index & 0xFF;
			Sequence = sequence & 0xFF;
		}

		public override int GetHashCode()
		{
			return Sequence ^ Index;
		}

		public override bool Equals(object obj)
		{
			return (Scene)obj == this;
		}

		public override string ToString()
		{
			return $"[Scene Index={Index} Sequence={Sequence}]";
		}

		public static bool operator ==(Scene a, Scene b)
		{
			if (a.Index == b.Index)
			{
				return a.Sequence == b.Sequence;
			}
			return false;
		}

		public static bool operator !=(Scene a, Scene b)
		{
			if (a.Index == b.Index)
			{
				return a.Sequence != b.Sequence;
			}
			return true;
		}
	}
}
