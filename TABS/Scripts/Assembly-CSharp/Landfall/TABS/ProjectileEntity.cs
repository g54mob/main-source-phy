using UnityEngine;

namespace Landfall.TABS
{
	public class ProjectileEntity : MonoBehaviour, IDatabaseEntity
	{
		[SerializeField]
		private DatabaseEntity m_entity;

		public DatabaseEntity Entity
		{
			get
			{
				return m_entity;
			}
			set
			{
				m_entity = value;
			}
		}

		public string DisplayName
		{
			get
			{
				if (Entity == null)
				{
					Debug.LogError(base.gameObject.name + " HAS NOT ENTITY??? WTF??? HELP??? NAAAHHASJSHJ");
				}
				if (!string.IsNullOrEmpty(Entity.Name))
				{
					return Entity.Name;
				}
				return base.name;
			}
		}
	}
}
