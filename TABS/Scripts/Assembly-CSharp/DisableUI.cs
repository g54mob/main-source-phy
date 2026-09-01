using System.Collections;
using Landfall.TABS.GameState;
using UnityEngine;

public class DisableUI : GameStateListener
{
	[SerializeField]
	private BlurBehind m_BlurBehind;

	public override void OnEnterPlacementState()
	{
		StopAllCoroutines();
		Enable();
	}

	public override void OnEnterBattleState()
	{
		StartCoroutine(Disable());
	}

	public IEnumerator Disable()
	{
		yield return new WaitForSeconds(1f);
		if ((bool)m_BlurBehind)
		{
			m_BlurBehind.enabled = false;
		}
		base.gameObject.SetActive(value: false);
	}

	public void Enable()
	{
		if ((bool)m_BlurBehind)
		{
			m_BlurBehind.enabled = true;
		}
		base.gameObject.SetActive(value: true);
	}
}
