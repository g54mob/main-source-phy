using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

public class ShowProjectile : MonoBehaviour
{
	private RangeWeapon spawner;

	public Vector3 offset;

	public Vector3 arrowOffset;

	public float disableObjectFor = 3f;

	public bool useCooldownOfWeaponInstead = true;

	public bool lerpIn;

	private GameObject spawned;

	public Transform objectReference;

	public CodeAnimation enableAnim;

	private float size;

	private IdleBowParticle idleParticle;

	private List<ParticleSystem> idleParticles;

	public bool IsInBlindGame { get; set; }

	private void Start()
	{
		if ((bool)base.transform.parent.GetComponent<HandLeft>() && (bool)objectReference)
		{
			objectReference.transform.localEulerAngles = new Vector3(objectReference.transform.localEulerAngles.x, objectReference.transform.localEulerAngles.y * -1f, objectReference.transform.localEulerAngles.z * -1f);
			objectReference.transform.localPosition = new Vector3(objectReference.transform.localPosition.x * -1f, objectReference.transform.localPosition.y, objectReference.transform.localPosition.z);
		}
		spawner = GetComponent<RangeWeapon>();
		spawner.AddShootAction(Shoot);
		Transform transform = base.transform;
		if ((bool)objectReference)
		{
			transform = objectReference;
		}
		spawned = Object.Instantiate(spawner.ObjectToSpawn, transform.position, transform.rotation);
		if (spawned != null && spawner != null && spawner.connectedData != null && spawner.connectedData.unit != null)
		{
			Renderer[] componentsInChildren = spawned.GetComponentsInChildren<Renderer>();
			spawner.connectedData.unit.AddRenderersToShowHide(componentsInChildren, IsInBlindGame);
		}
		Rigidbody[] componentsInChildren2 = spawned.GetComponentsInChildren<Rigidbody>();
		for (int i = 0; i < componentsInChildren2.Length; i++)
		{
			componentsInChildren2[i].isKinematic = true;
			Joint component = componentsInChildren2[i].transform.GetComponent<Joint>();
			if ((bool)component)
			{
				Object.Destroy(component);
			}
			Object.Destroy(componentsInChildren2[i]);
		}
		MonoBehaviour[] componentsInChildren3 = spawned.GetComponentsInChildren<MonoBehaviour>();
		for (int j = 0; j < componentsInChildren3.Length; j++)
		{
			if (!(componentsInChildren3[j] is SetTeamColorOnStart) && !(componentsInChildren3[j] is TeamColor))
			{
				if (componentsInChildren3[j] is ArrowIdlePosition)
				{
					arrowOffset = -componentsInChildren3[j].transform.localPosition;
				}
				else
				{
					Object.Destroy(componentsInChildren3[j]);
				}
			}
		}
		spawned.transform.SetParent(transform, worldPositionStays: true);
		spawned.transform.localScale = objectReference.transform.localScale;
		spawned.transform.localPosition = offset;
		spawned.transform.localPosition = arrowOffset;
		ParticleSystem[] componentsInChildren4 = spawned.GetComponentsInChildren<ParticleSystem>();
		idleParticles = new List<ParticleSystem>();
		for (int k = 0; k < componentsInChildren4.Length; k++)
		{
			idleParticle = componentsInChildren4[k].transform.GetComponent<IdleBowParticle>();
			if ((bool)idleParticle)
			{
				componentsInChildren4[k].Play();
				idleParticles.Add(componentsInChildren4[k]);
				idleParticle = null;
			}
			else
			{
				Object.Destroy(componentsInChildren4[k]);
			}
		}
	}

	public void Shoot()
	{
		StartCoroutine(DisableObject());
	}

	private IEnumerator DisableObject()
	{
		if (useCooldownOfWeaponInstead && (bool)spawner)
		{
			disableObjectFor = spawner.internalCooldown / spawner.attackSpeedM * 0.8f;
		}
		spawned.SetActive(value: false);
		yield return new WaitForSeconds(disableObjectFor);
		spawned.SetActive(value: true);
		if ((bool)enableAnim)
		{
			enableAnim.animationSpeed *= spawner.attackSpeedM;
			enableAnim.PlayIn();
		}
		for (int i = 0; i < idleParticles.Count; i++)
		{
			idleParticles[i].Play();
		}
	}
}
