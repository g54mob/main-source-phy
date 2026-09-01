using UnityEngine;

public class ClickableQuadButton : MonoBehaviour
{
	public Camera cam;

	public string messageName;

	public Transform target;

	public Material hiliteMaterial;

	private Material orgMaterial;

	private void Start()
	{
		orgMaterial = GetComponent<Renderer>().material;
		if (cam == null)
		{
			cam = Camera.main;
		}
	}

	private void Update()
	{
		GetComponent<Renderer>().material = orgMaterial;
		if (cam == null)
		{
			Debug.LogError("No camera", this);
			return;
		}
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
		bool flag = Input.GetMouseButtonDown(0);
		if (Input.touchCount > 0)
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
			{
				ray = cam.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0f));
				flag = true;
			}
		}
		RaycastHit hitInfo;
		if (!Physics.Raycast(ray, out hitInfo) || !(hitInfo.collider != null))
		{
			return;
		}
		ClickableQuadButton component = hitInfo.collider.GetComponent<ClickableQuadButton>();
		if ((bool)component && component == this)
		{
			GetComponent<Renderer>().material = hiliteMaterial;
			if (flag)
			{
				target.SendMessage(messageName);
			}
		}
	}
}
