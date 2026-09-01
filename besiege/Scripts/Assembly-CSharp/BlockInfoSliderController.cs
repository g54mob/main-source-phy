using UnityEngine;

public class BlockInfoSliderController : ClickBehaviour
{
	public Transform handleObj;

	public Camera hudCam;

	public TextMesh scalerTextMesh;

	public float scalerAmount;

	public Transform minPoint;

	public Transform maxPoint;

	public Vector3 currentPoint;

	public float minValue;

	public float maxValue = 2f;

	public float myValue = 1f;

	public SimpleNumberField textField;

	private bool isClicked;

	private Vector3 newMousePos;

	private void Start()
	{
		releaseOnlyOver = true;
		hudCam = GameObject.Find("HUD Cam").GetComponent<Camera>();
	}

	public override void OnClicked()
	{
		isClicked = true;
	}

	public override void OnClickReleased()
	{
		isClicked = false;
	}

	private void Update()
	{
		if (isClicked)
		{
			currentPoint = new Vector3(Mathf.Clamp(hudCam.ScreenToWorldPoint(new Vector3(InputManager.CursorPosition().x, InputManager.CursorPosition().y, 0f)).x, minPoint.position.x, maxPoint.position.x), minPoint.position.y, minPoint.position.z);
			float num = Vector3.Distance(minPoint.position, currentPoint) / Vector3.Distance(minPoint.position, maxPoint.position);
			myValue = Mathf.Clamp((maxValue - minValue) * num + minValue, minValue, maxValue);
			textField.SetCustomNumber(myValue);
		}
		myValue = Mathf.Clamp(myValue, minValue, maxValue);
		handleObj.position = Vector3.Lerp(minPoint.position, maxPoint.position, (myValue - minValue) / (maxValue - minValue));
	}

	private void SetValue(float valuey)
	{
		valuey = Mathf.Clamp(valuey, minValue, maxValue);
		myValue = valuey;
		textField.SetCustomNumber(myValue);
	}
}
