using UnityEngine;
using VInspector;

public class CharacterRaycast : MonoBehaviour
{
	[SerializeField]
	private Camera cam;

	[Header("Raycast")]
	[SerializeField]
	private LayerMask layerMask = -1;

	[SerializeField]
	private float rayDistance = 100f;

	private RaycastHit currentHit;

	[ReadOnly]
	public GameObject currentObject;

	[SerializeField]
	private bool disableRaycast;

	private void Awake()
	{
		EventManager.GamePause += DisableRaycast;
		EventManager.GameResume += EnableRaycast;
		EventManager.MainMenuLoaded += DisableRaycast;
		EventManager.PuzzleUILoaded += DisableRaycast;
		EventManager.CatUILoaded += DisableRaycast;
		EventManager.GameStart += EnableRaycast;
		EventManager.GameEnd += DisableRaycast;
	}

	private void OnDisable()
	{
		EventManager.GamePause -= DisableRaycast;
		EventManager.GameResume -= EnableRaycast;
		EventManager.MainMenuLoaded -= DisableRaycast;
		EventManager.PuzzleUILoaded -= DisableRaycast;
		EventManager.CatUILoaded -= DisableRaycast;
		EventManager.GameStart -= EnableRaycast;
		EventManager.GameEnd -= DisableRaycast;
	}

	private void DisableRaycast()
	{
		if (currentObject != null)
		{
			currentObject.GetComponent<IHighlight>().SetHighlight(flag: false);
		}
		currentObject = null;
		disableRaycast = true;
	}

	private void EnableRaycast()
	{
		if (currentObject != null)
		{
			currentObject.GetComponent<IHighlight>().SetHighlight(flag: false);
		}
		currentObject = null;
		disableRaycast = false;
	}

	private void Update()
	{
		if (!disableRaycast)
		{
			UpdateRaycast();
		}
	}

	private void UpdateRaycast()
	{
		Ray ray = new Ray(cam.transform.position, cam.transform.forward);
		if (Physics.Raycast(ray.origin, ray.direction, out currentHit, rayDistance, layerMask, QueryTriggerInteraction.Ignore))
		{
			if (currentObject != null)
			{
				currentObject.GetComponent<IHighlight>().SetHighlight(flag: false);
				currentObject = null;
			}
			if (currentHit.collider.gameObject.TryGetComponent<IHighlight>(out var component))
			{
				currentObject = currentHit.collider.gameObject;
				component.SetHighlight(flag: true);
			}
		}
		else
		{
			if (currentObject != null)
			{
				currentObject.GetComponent<IHighlight>().SetHighlight(flag: false);
			}
			currentObject = null;
		}
	}
}
