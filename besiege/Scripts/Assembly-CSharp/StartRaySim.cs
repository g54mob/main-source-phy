using UnityEngine;

public class StartRaySim : MonoBehaviour
{
	private ArcReactor_Launcher launcher;

	private void Start()
	{
		launcher = GetComponent<ArcReactor_Launcher>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.C))
		{
			launcher.Invoke("LaunchRay", 1f);
		}
	}
}
