using UnityEngine;

public class AudioVisualiser : MonoBehaviour
{
	public AudioSource aSource;

	public float amplitude = 1f;

	public float sizeBetweenPoints = 0.1f;

	public TextMesh nameText;

	private float[] samples = new float[128];

	private LineRenderer lRenderer;

	private Transform myTransform;

	private void Awake()
	{
		lRenderer = GetComponent<LineRenderer>();
		myTransform = base.transform;
		aSource = GameObject.Find("MUSIC").GetComponent<AudioSource>();
	}

	private void Start()
	{
		lRenderer.SetVertexCount(samples.Length);
		SetName();
	}

	private void Update()
	{
		aSource.GetOutputData(samples, 0);
		for (int i = 0; i < samples.Length; i++)
		{
			Vector3 position = new Vector3(myTransform.position.x + (float)i * sizeBetweenPoints, myTransform.position.y + samples[i] * amplitude, myTransform.position.z);
			lRenderer.SetPosition(i, position);
		}
	}

	private void SetName()
	{
		nameText.text = aSource.clip.name;
	}
}
