using UnityEngine;

public class UnluckFPS : MonoBehaviour
{
	public TextMesh _textMesh;

	public float updateInterval = 0.5f;

	private float accum;

	private int frames;

	private float timeleft;

	public void Start()
	{
		timeleft = updateInterval;
		_textMesh = base.transform.GetComponent<TextMesh>();
	}

	public void Update()
	{
		timeleft -= Time.deltaTime;
		accum += Time.timeScale / Time.deltaTime;
		frames++;
		if (timeleft <= 0f)
		{
			_textMesh.text = "FPS " + (accum / (float)frames).ToString("f2");
			timeleft = updateInterval;
			accum = 0f;
			frames = 0;
		}
	}
}
