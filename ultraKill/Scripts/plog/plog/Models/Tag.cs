using plog.Helpers;

namespace plog.Models
{
	public class Tag
	{
		public string Name { get; }

		public int Hash { get; }

		public UniversalColor Color { get; private set; }

		public Tag(string name)
		{
			Name = name;
			Hash = HashingHelper.Fnv1A(name);
			Color = ColorHelper.GetColorForHash(Hash);
		}

		public Tag(object obj)
			: this(obj.GetType().Name)
		{
		}

		public override string ToString()
		{
			return Name;
		}

		public override bool Equals(object? obj)
		{
			if (!(obj is Tag tag))
			{
				if (obj is string text)
				{
					return text == Name;
				}
				return false;
			}
			return tag.Hash == Hash;
		}

		public override int GetHashCode()
		{
			return Hash;
		}
	}
}
