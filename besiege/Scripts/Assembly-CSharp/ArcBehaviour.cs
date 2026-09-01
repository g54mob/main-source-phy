using UnityEngine;

public class ArcBehaviour : MonoBehaviour
{
	public Transform start;

	public Transform middle;

	public Transform end;

	public float maxHeight = 200f;

	private Bezier bezier;

	private void Start()
	{
		bezier = GetComponent<Bezier>();
	}

	private void Update()
	{
		Vector3 vector = InputManager.CursorPosition();
		float num = Mathf.Min(Screen.height, Mathf.Max(vector.y, 0f));
		middle.position = new Vector3(middle.position.x, num / (float)Screen.height * maxHeight, middle.position.z);
		Debug.DrawLine(start.position, middle.position, Color.red);
		Debug.DrawLine(middle.position, end.position, Color.red);
		Debug.DrawLine(start.position, end.position, Color.red);
		bezier.Plot(start.position, middle.position, end.position);
	}
}
