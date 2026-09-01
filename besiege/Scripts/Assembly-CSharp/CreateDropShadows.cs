using System.Collections.Generic;
using UnityEngine;

public class CreateDropShadows : MonoBehaviour
{
	[SerializeField]
	private DynamicText text;

	[SerializeField]
	private MeshRenderer mesh;

	[SerializeField]
	private bool createOnAwake = true;

	private Vector3 startpos;

	private float posToAdd;

	private string textFrom;

	private MeshRenderer shadowMesh;

	private float letterSize;

	public GameObject shadow;

	public float size = 10f;

	public Vector3 offset;

	private List<GameObject> shadowObjects;

	protected void Awake()
	{
		if (text == null)
		{
			text = GetComponent<DynamicText>();
		}
		if (mesh == null)
		{
			mesh = GetComponent<MeshRenderer>();
		}
		shadowObjects = new List<GameObject>();
		shadowMesh = shadow.GetComponent<MeshRenderer>();
	}

	protected void Start()
	{
		if (createOnAwake)
		{
			Create();
		}
	}

	public void Clear()
	{
		if (shadowObjects != null)
		{
			for (int i = 0; i < shadowObjects.Count; i++)
			{
				Object.Destroy(shadowObjects[i]);
			}
			shadowObjects.Clear();
		}
	}

	public void Create()
	{
		if (shadowObjects.Count > 0)
		{
			return;
		}
		textFrom = this.text.textSB.ToString();
		posToAdd = mesh.bounds.size.x / (float)textFrom.Length;
		letterSize = posToAdd * (1f - this.text.letterSpacing / (float)(textFrom.Length - 1) * (float)textFrom.Length);
		startpos = new Vector3(mesh.bounds.min.x + letterSize / 2f, mesh.bounds.center.y, mesh.bounds.center.z) + offset;
		string text = textFrom;
		foreach (char c in text)
		{
			if (!char.IsWhiteSpace(c))
			{
				GameObject gameObject = Object.Instantiate(shadow, startpos, Quaternion.Euler(90f, 0f, 0f)) as GameObject;
				gameObject.transform.localScale *= letterSize / shadowMesh.bounds.size.x * size;
				gameObject.transform.SetParent(base.transform, true);
				shadowObjects.Add(gameObject);
			}
			startpos.x += posToAdd;
		}
	}
}
