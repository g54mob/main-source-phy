using UnityEngine;

public class TemporaryUpdate : MonoBehaviour
{
	public delegate bool CustomUpdate();

	private CustomUpdate update;

	private float killTimer = -1f;

	private void Update()
	{
		if (killTimer > 0f)
		{
			killTimer -= Time.deltaTime;
			if (killTimer <= 0f)
			{
				Object.Destroy(this);
			}
		}
		else if (update == null || (killTimer < 0f && update()))
		{
			killTimer = 1f;
		}
	}

	public static void temporaryUpdate(GameObject gameObject, CustomUpdate update)
	{
		TemporaryUpdate temporaryUpdate = gameObject.GetComponent<TemporaryUpdate>();
		if (temporaryUpdate == null)
		{
			temporaryUpdate = gameObject.AddComponent<TemporaryUpdate>();
		}
		temporaryUpdate.update = update;
		temporaryUpdate.killTimer = -1f;
	}
}
