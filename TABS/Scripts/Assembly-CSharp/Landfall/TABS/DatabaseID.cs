using System;

namespace Landfall.TABS
{
	[Serializable]
	public struct DatabaseID : IEquatable<DatabaseID>
	{
		public static DatabaseID DefaultUnassignedID = new DatabaseID(0, 0);

		public static readonly int _LANDFALL_CONTENT = -1;

		public static readonly int _LOCAL_CONTENT = -2;

		public int m_modID;

		public int m_ID;

		private static Random r = new Random();

		public DatabaseID(int modID, int ID)
		{
			m_modID = modID;
			m_ID = ID;
		}

		public DatabaseID(int ID)
			: this(_LOCAL_CONTENT, ID)
		{
		}

		public bool IsDefaultID()
		{
			if (m_modID != 0)
			{
				return m_ID == 0;
			}
			return true;
		}

		public override bool Equals(object o)
		{
			if (o.GetType() == typeof(DatabaseID))
			{
				DatabaseID databaseID = (DatabaseID)o;
				if (databaseID.m_modID == m_modID)
				{
					return databaseID.m_ID == m_ID;
				}
				return false;
			}
			return false;
		}

		public bool Equals(DatabaseID other)
		{
			if (other.m_modID == m_modID)
			{
				return other.m_ID == m_ID;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (252008734 * -1521134295 + m_modID.GetHashCode()) * -1521134295 + m_ID.GetHashCode();
		}

		public static bool operator ==(DatabaseID a, DatabaseID b)
		{
			if (a.m_modID == b.m_modID)
			{
				return a.m_ID == b.m_ID;
			}
			return false;
		}

		public static bool operator !=(DatabaseID a, DatabaseID b)
		{
			if (a.m_modID == b.m_modID)
			{
				return a.m_ID != b.m_ID;
			}
			return true;
		}

		public override string ToString()
		{
			return m_modID.ToString() + m_ID;
		}

		public static DatabaseID NewID()
		{
			return new DatabaseID(r.Next());
		}

		public static DatabaseID NewID(int modID)
		{
			return new DatabaseID(modID, r.Next());
		}
	}
}
