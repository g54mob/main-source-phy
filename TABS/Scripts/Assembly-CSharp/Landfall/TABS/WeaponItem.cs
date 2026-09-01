using UnityEngine;

namespace Landfall.TABS
{
	public class WeaponItem : CharacterItem, IDatabaseEntity
	{
		[Space(10f)]
		[Header("Icon Generation Data")]
		public Vector3 cameraPos;

		public Quaternion cameraRot;

		public GameObject extraObjectToSpawn;

		public void Initialize(Team team)
		{
			m_propData = new PropItemData();
			m_gameObjects = new GameObject[1] { base.gameObject };
			base.gameObject.AddComponent<PropItemProxy>().m_propItemReference = this;
			GetRenderers();
			SetOriginalTeamColors(team);
		}

		public override void Remove()
		{
			base.Remove();
		}
	}
}
