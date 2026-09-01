using System.Collections;
using Landfall.TABS;
using Landfall.TABS.GameState;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitEditorTestScene : GameStateListener
{
	[SerializeField]
	private GameObject[] m_disableWhenTesting;

	[SerializeField]
	private Transform[] m_spawns;

	[SerializeField]
	private CanvasGroup m_fade;

	private Weapon[] m_hiddenWeapons;

	public void StartTestScene(UnitBlueprint unitBlueprint)
	{
		CampaignPlayerDataHolder.StartedPlayingTest();
		StartCoroutine(LoadScenes(unitBlueprint));
	}

	private IEnumerator LoadScenes(UnitBlueprint unitBlueprint)
	{
		yield return FadeScreen(fadeToBlack: true, 10f);
		for (int i = 0; i < m_disableWhenTesting.Length; i++)
		{
			m_disableWhenTesting[i].SetActive(value: false);
		}
		m_hiddenWeapons = Object.FindObjectsOfType<Weapon>();
		for (int j = 0; j < m_hiddenWeapons.Length; j++)
		{
			m_hiddenWeapons[j].gameObject.SetActive(value: false);
		}
		yield return SceneManager.LoadSceneAsync("EditorTestScene", LoadSceneMode.Additive);
		yield return SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
		SceneManager.SetActiveScene(SceneManager.GetSceneAt(2));
		yield return null;
		yield return null;
		ServiceLocator.GetService<GameStateManager>().EnterBattleState();
		yield return FadeScreen(fadeToBlack: false, 10f);
	}

	public IEnumerator StopTestScene()
	{
		SetScreen(1f);
		Scene[] array = new Scene[2]
		{
			SceneManager.GetSceneAt(1),
			SceneManager.GetSceneAt(2)
		};
		for (int i = 0; i < array.Length; i++)
		{
			GameObject[] rootGameObjects = array[i].GetRootGameObjects();
			for (int j = 0; j < rootGameObjects.Length; j++)
			{
				rootGameObjects[j].SetActive(value: false);
			}
			SceneManager.UnloadSceneAsync(array[i]);
		}
		for (int k = 0; k < m_disableWhenTesting.Length; k++)
		{
			m_disableWhenTesting[k].SetActive(value: true);
		}
		if (m_hiddenWeapons != null)
		{
			for (int l = 0; l < m_hiddenWeapons.Length; l++)
			{
				m_hiddenWeapons[l].gameObject.SetActive(value: true);
			}
		}
		yield return FadeScreen(fadeToBlack: false, 10f);
	}

	private IEnumerator FadeScreen(bool fadeToBlack, float speed = 1f)
	{
		float target = (fadeToBlack ? 1f : 0f);
		while (true)
		{
			m_fade.alpha = Mathf.MoveTowards(m_fade.alpha, target, Time.deltaTime * speed);
			if (m_fade.alpha == target)
			{
				break;
			}
			yield return null;
		}
	}

	private void SetScreen(float alpha)
	{
		m_fade.alpha = alpha;
	}

	public override void OnEnterPlacementState()
	{
	}

	public override void OnEnterBattleState()
	{
	}

	public override void OnExitBattleState()
	{
		StartCoroutine(StopTestScene());
	}
}
