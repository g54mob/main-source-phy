using UnityEngine;
using UnityEngine.SceneManagement;

public class RemoveWhenSkinsLoaded : SingleInstanceFindOnly<RemoveWhenSkinsLoaded>
{
	protected float timer = 5f;

	public override string Name
	{
		get
		{
			return "RemoveWhenSkinsLoaded";
		}
	}

	protected override void Awake()
	{
		base.Awake();
		SceneManager.sceneLoaded += OnSceneLoad;
	}

	private void Update()
	{
		timer -= Time.unscaledDeltaTime;
		if (timer < 0f)
		{
			Object.Destroy(base.gameObject);
		}
	}

	private void OnSceneLoad(Scene scene, LoadSceneMode m)
	{
		if (scene.name != "TITLE SCREEN" && this != null)
		{
			Object.Destroy(base.gameObject);
		}
	}
}
