using UnityEngine;

public class DisableComponent : MonoBehaviour
{
	public Component _ComponentToDisable;

	public float timeBeforeDisabling = 4f;

	private float timer;

	private void Start()
	{
		_ComponentToDisable.gameObject.SetActive(true);
	}

	private void Update()
	{
		if (timer < timeBeforeDisabling)
		{
			timer += Time.deltaTime;
		}
		else
		{
			_ComponentToDisable.gameObject.SetActive(false);
		}
	}
}
