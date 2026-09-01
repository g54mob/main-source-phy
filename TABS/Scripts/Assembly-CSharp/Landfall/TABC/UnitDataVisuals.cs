using UnityEngine;

namespace Landfall.TABC
{
	public class UnitDataVisuals : MonoBehaviour
	{
		public ParticleSystem[] excessParts;

		public ParticleSystem[] allianceParts;

		public void SetExcess(bool setExcessive, bool cutOngoingEffect = false)
		{
			for (int i = 0; i < excessParts.Length; i++)
			{
				if (!excessParts[i])
				{
					continue;
				}
				if (setExcessive)
				{
					excessParts[i].Play();
					continue;
				}
				if (cutOngoingEffect)
				{
					excessParts[i].Clear();
				}
				excessParts[i].Stop();
			}
		}

		public void ShowAlliance(Alliance alliance)
		{
			for (int i = 0; i < allianceParts.Length; i++)
			{
				allianceParts[i].startColor = alliance.color;
				allianceParts[i].Play();
			}
		}

		public void HideAlliance()
		{
			for (int i = 0; i < allianceParts.Length; i++)
			{
				allianceParts[i].Stop();
			}
		}
	}
}
