using Landfall.TABS;
using UnityEngine;

[CreateAssetMenu(fileName = "New VoiceBundle", menuName = "TABS/VoiceBundle", order = 99)]
public class VoiceBundle : ScriptableObject, IDatabaseEntity
{
	public string VocalRef = "";

	public string DeathRef = "";

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
}
