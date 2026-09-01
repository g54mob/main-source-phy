using System.Collections.Generic;
using Modding;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/FreeWheel")]
public class FreeWheel : BlockBehaviour
{
	public MeshCollider altCollider;

	public Vector3 inertia = Vector3.zero;

	public bool hasAltCollider;

	protected MToggle optimiseCollider;

	private MSlider contactSlider;

	protected override void Awake()
	{
		base.Awake();
		if ((!isSimulating || SimPhysics) && hasAltCollider)
		{
			optimiseCollider = AddToggle("OPTIMISE COLLIDER", "opt-collider", true);
			optimiseCollider.Toggled += ToggleColliders;
			contactSlider = AddSlider(4430, "contact", 0.1f, 0.1f, 0.5f, string.Empty);
			contactSlider.DisplayInMapper = CogMotorControllerHinge.ShowContactToggle;
			ToggleColliders(true);
			optimiseCollider.DisplayInMapper = false;
		}
	}

	protected virtual void ToggleColliders(bool val)
	{
		if (!isSimulating && !stripped)
		{
			for (int i = 0; i < myBounds.childColliders.Count; i++)
			{
				myBounds.childColliders[i].enabled = !val;
			}
			if ((bool)altCollider)
			{
				altCollider.enabled = val;
			}
			SingleInstance<Events>.Instance.CollidersChanged(this);
		}
	}

	private static Vector3 ScaleInertia(Vector3 scale, Vector3 inertia)
	{
		Vector3 vector = scale;
		Vector3 vector2 = new Vector3(Mathf.Max(vector.y, vector.z), Mathf.Max(vector.x, vector.z), Mathf.Max(vector.x, vector.y));
		Vector3 vector3 = inertia;
		return new Vector3(vector3.x * vector2.x * vector2.x, vector3.y * vector2.y * vector2.y, vector3.z * vector2.z * vector2.z);
	}

	protected override void Start()
	{
		base.Start();
		if (!isSimulating || !SimPhysics)
		{
			return;
		}
		if (hasAltCollider)
		{
			if (altCollider.enabled)
			{
				for (int i = 0; i < myBounds.childColliders.Count - 1; i++)
				{
					Object.Destroy(myBounds.childColliders[i].gameObject);
				}
				myBounds.childColliders = new List<Collider> { altCollider };
				if (inertia != Vector3.zero)
				{
					Rigidbody.inertiaTensor = ScaleInertia(base.transform.localScale, inertia);
				}
				if (Rigidbody.drag > 0f)
				{
					Rigidbody.drag = (originalDrag = 0.1f);
				}
				Rigidbody.angularDrag = (originalADrag = 0.05f);
			}
			else
			{
				myBounds.childColliders.Remove(altCollider);
				Object.Destroy(altCollider);
			}
		}
		if (stripped)
		{
			return;
		}
		float num = Mathf.Clamp(contactSlider.Value * 0.1f, 0.01f, 0.05f);
		if (float.IsNaN(num))
		{
			num = 0.01f;
		}
		foreach (Collider childCollider in myBounds.childColliders)
		{
			childCollider.contactOffset = num;
		}
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (hasAltCollider && !isSimulating && data.WasLoadedFromFile && !data.HasKey("bmt-opt-collider"))
		{
			optimiseCollider.SetValue(false);
			optimiseCollider.ApplyValue();
		}
	}
}
