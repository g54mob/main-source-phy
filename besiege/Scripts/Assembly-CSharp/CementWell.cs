using System.Collections.Generic;
using UnityEngine;

public class CementWell : MonoBehaviour
{
	public CrumpleMesh crumple;

	public MeshRenderer waterRender;

	public List<Rigidbody> cementBags = new List<Rigidbody>();

	public int solidRequirement = 2;

	protected List<Rigidbody> r = new List<Rigidbody>();

	protected float startSpeed;

	protected float startAlpha;

	protected Color startColor;

	protected Color startReflect;

	protected float startFoam;

	protected float startHeight;

	protected float startScroll;

	protected void Start()
	{
		startSpeed = crumple.speed;
		startAlpha = waterRender.material.GetFloat("_Alpha");
		startFoam = waterRender.material.GetFloat("_FoamPower");
		startHeight = waterRender.material.GetFloat("_WaveHeight");
		startScroll = waterRender.material.GetFloat("_ScrollSpeed");
		startColor = waterRender.material.GetColor("_Color");
		startReflect = waterRender.material.GetColor("_ReflectColor");
	}

	protected void OnTriggerEnter(Collider col)
	{
		if (cementBags.Contains(col.attachedRigidbody) && !r.Contains(col.attachedRigidbody))
		{
			r.Add(col.attachedRigidbody);
			float t = 1f * (float)r.Count / (1f * (float)solidRequirement);
			crumple.speed = Mathf.Lerp(startSpeed, 0f, t);
			waterRender.material.SetFloat("_Alpha", Mathf.Lerp(startAlpha, 1f, t));
			waterRender.material.SetFloat("_FoamPower", Mathf.Lerp(startFoam, 0f, t));
			waterRender.material.SetFloat("_WaveHeight", Mathf.Lerp(startHeight, 0f, t));
			waterRender.material.SetFloat("_ScrollSpeed", Mathf.Lerp(startScroll, 0f, t));
			waterRender.material.SetColor("_Color", Color.Lerp(startColor, Color.black, t));
			waterRender.material.SetColor("_ReflectColor", Color.Lerp(startReflect, Color.grey, t));
		}
	}
}
