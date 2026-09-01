using UnityEngine;

public class Spin : MonoBehaviour
{
	public SpinAxis m_SpinAxis;

	public float speed = 10f;

	public bool reverse;

	public bool respectTimeScale;

	private void OnEnable()
	{
		if (GameSettings.m_Instance != null)
		{
			TerrainIsland rightTerrain = TerrainIslands.GetRightTerrain();
			if ((bool)rightTerrain && base.transform.position.x > rightTerrain.transform.position.x)
			{
				reverse = !reverse;
			}
		}
	}

	private void Update()
	{
		if (GameStateManager.GetState() != GameState.SANDBOX && GameStateManager.GetState() != GameState.BUILD)
		{
			float num = (Mathf.Approximately(Time.timeScale, 0f) ? 0f : (respectTimeScale ? Time.deltaTime : Time.unscaledDeltaTime));
			switch (m_SpinAxis)
			{
			case SpinAxis.X:
				base.transform.Rotate(reverse ? (-Vector3.right) : Vector3.right, speed * num, Space.Self);
				break;
			case SpinAxis.Y:
				base.transform.Rotate(reverse ? (-Vector3.up) : Vector3.up, speed * num, Space.Self);
				break;
			case SpinAxis.Z:
				base.transform.Rotate(reverse ? (-Vector3.forward) : Vector3.forward, speed * num, Space.Self);
				break;
			}
		}
	}
}
