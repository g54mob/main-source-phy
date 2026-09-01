using UnityEngine;

public class BlurControl : MonoBehaviour
{
	public float val;

	public Renderer myRenderer;

	private void Awake()
	{
		myRenderer = base.gameObject.GetComponent<Renderer>();
	}

	private void Start()
	{
		val = 0f;
		myRenderer.material.SetFloat("_blurSizeXY", val);
	}

	private void Update()
	{
		if (Input.GetButton("Up"))
		{
			val += Time.deltaTime;
			if (val > 20f)
			{
				val = 20f;
			}
			myRenderer.material.SetFloat("_blurSizeXY", val);
		}
		else if (Input.GetButton("Down"))
		{
			val = (val - Time.deltaTime) % 20f;
			if (val < 0f)
			{
				val = 0f;
			}
			myRenderer.material.SetFloat("_blurSizeXY", val);
		}
	}

	private void OnGUI()
	{
		GUI.TextArea(new Rect(10f, 10f, 200f, 50f), "Press the 'Up' and 'Down' arrows \nto interact with the blur plane\nCurrent value: " + val);
	}
}
