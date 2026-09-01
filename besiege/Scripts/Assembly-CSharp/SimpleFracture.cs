using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFracture : MonoBehaviour
{
	public bool fractureToPoint;

	public int totalMaxFractures = 3;

	public float forcePerDivision = 20f;

	public float minBreakingForce;

	public int maxFracturesPerCall = 3;

	public float randomOffset;

	public Vector3 minFractureSize = Vector3.zero;

	public Vector3 grain = Vector3.one;

	public float useCollisionDirection;

	public bool fractureAtCenter;

	public bool smartJoints;

	public float destroyAllAfterTime;

	public float destroySmallAfterTime;

	public GameObject instantiateOnBreak;

	public float totalMassIfStatic = 1f;

	private Joint[] joints;

	private void Start()
	{
		Rigidbody component = GetComponent<Rigidbody>();
		if (!(component != null))
		{
			return;
		}
		List<Joint> list = new List<Joint>();
		Joint[] array = Object.FindObjectsOfType<Joint>();
		foreach (Joint joint in array)
		{
			if (joint.connectedBody == component)
			{
				list.Add(joint);
				joints = list.ToArray();
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		Vector3 point = collision.contacts[0].point;
		Vector3 force = collision.relativeVelocity * UsedMass(collision);
		FractureAtPoint(point, force);
	}

	private void FractureAtPoint(Vector3 hit, Vector3 force)
	{
		if (!(force.magnitude < Mathf.Max(minBreakingForce, forcePerDivision)))
		{
			int num = Mathf.Min(Mathf.RoundToInt(force.magnitude / forcePerDivision), Mathf.Min(maxFracturesPerCall, totalMaxFractures));
			Vector3 point = base.transform.worldToLocalMatrix.MultiplyPoint(hit);
			StartCoroutine(Fracture(point, force, num));
		}
	}

	private IEnumerator Fracture(Vector3 point, Vector3 force, float iterations)
	{
		if ((bool)instantiateOnBreak && force.magnitude >= Mathf.Max(minBreakingForce, forcePerDivision))
		{
			Object.Instantiate(instantiateOnBreak, base.transform.position, base.transform.rotation);
			instantiateOnBreak = null;
		}
		while (iterations > 0f)
		{
			if (totalMaxFractures == 0 || Vector3.Min(base.gameObject.GetComponent<MeshFilter>().mesh.bounds.size, minFractureSize) != minFractureSize)
			{
				if (destroySmallAfterTime >= 1f)
				{
					Object.Destroy(GetComponent<MeshCollider>(), destroySmallAfterTime - 1f);
					Object.Destroy(base.gameObject, destroySmallAfterTime);
				}
				totalMaxFractures = 0;
				yield break;
			}
			totalMaxFractures--;
			iterations -= 1f;
			if (fractureAtCenter)
			{
				point = GetComponent<MeshFilter>().mesh.bounds.center;
			}
			Vector3 vec = Vector3.Scale(grain, Random.insideUnitSphere).normalized;
			Vector3 sub = base.transform.worldToLocalMatrix.MultiplyVector(force.normalized) * useCollisionDirection * Vector3.Dot(base.transform.worldToLocalMatrix.MultiplyVector(force.normalized), vec);
			Plane plane = new Plane(vec - sub, Vector3.Scale(Random.insideUnitSphere, GetComponent<MeshFilter>().mesh.bounds.size) * randomOffset + point);
			GameObject newObject = Object.Instantiate(base.gameObject, base.transform.position, base.transform.rotation) as GameObject;
			if ((bool)GetComponent<Rigidbody>())
			{
				newObject.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;
			}
			Vector3[] vertsA = base.gameObject.GetComponent<MeshFilter>().mesh.vertices;
			Vector3[] vertsB = newObject.GetComponent<MeshFilter>().mesh.vertices;
			Vector3 average = Vector3.zero;
			Vector3[] array = vertsA;
			foreach (Vector3 v in array)
			{
				average += v;
			}
			average /= (float)base.gameObject.GetComponent<MeshFilter>().mesh.vertexCount;
			average -= plane.GetDistanceToPoint(average) * plane.normal;
			int broken = 0;
			if (fractureToPoint)
			{
				for (int j = 0; j < base.gameObject.GetComponent<MeshFilter>().mesh.vertexCount; j++)
				{
					if (plane.GetSide(vertsA[j]))
					{
						vertsA[j] = average;
						broken++;
					}
					else
					{
						vertsB[j] = average;
					}
				}
			}
			else
			{
				for (int k = 0; k < base.gameObject.GetComponent<MeshFilter>().mesh.vertexCount; k++)
				{
					if (plane.GetSide(vertsA[k]))
					{
						vertsA[k] -= plane.GetDistanceToPoint(vertsA[k]) * plane.normal;
						broken++;
					}
					else
					{
						vertsB[k] -= plane.GetDistanceToPoint(vertsB[k]) * plane.normal;
					}
				}
			}
			if (broken == 0 || broken == base.gameObject.GetComponent<MeshFilter>().mesh.vertexCount)
			{
				totalMaxFractures++;
				iterations += 1f;
				Object.Destroy(newObject);
				yield return null;
				continue;
			}
			base.gameObject.GetComponent<MeshFilter>().mesh.vertices = vertsA;
			newObject.GetComponent<MeshFilter>().mesh.vertices = vertsB;
			base.gameObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();
			newObject.GetComponent<MeshFilter>().mesh.RecalculateNormals();
			base.gameObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
			newObject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
			if ((bool)base.gameObject.GetComponent<MeshCollider>())
			{
				base.gameObject.GetComponent<MeshCollider>().sharedMesh = base.gameObject.GetComponent<MeshFilter>().mesh;
				newObject.GetComponent<MeshCollider>().sharedMesh = newObject.GetComponent<MeshFilter>().mesh;
			}
			else
			{
				Object.Destroy(GetComponent<Collider>());
				Object.Destroy(base.gameObject, 1f);
			}
			if (smartJoints)
			{
				Joint[] jointsb = GetComponents<Joint>();
				if (jointsb != null)
				{
					for (int j = 0; j < jointsb.Length; j++)
					{
						if (jointsb[j].connectedBody != null && plane.GetSide(base.transform.worldToLocalMatrix.MultiplyPoint(jointsb[j].connectedBody.transform.position)))
						{
							if (jointsb[j].gameObject.GetComponent<SimpleFracture>().joints != null)
							{
								Joint[] array2 = jointsb[j].gameObject.GetComponent<SimpleFracture>().joints;
								foreach (Joint c in array2)
								{
									if (c == jointsb[j])
									{
									}
								}
							}
							Object.Destroy(jointsb[j]);
						}
						else
						{
							Object.Destroy(newObject.GetComponents<Joint>()[j]);
						}
					}
				}
				if (joints != null)
				{
					for (int j = 0; j < joints.Length; j++)
					{
						if ((bool)joints[j] && plane.GetSide(base.transform.worldToLocalMatrix.MultiplyPoint(joints[j].transform.position)))
						{
							joints[j].connectedBody = newObject.GetComponent<Rigidbody>();
							List<Joint> temp = new List<Joint>(joints);
							temp.RemoveAt(j);
							joints = temp.ToArray();
						}
						else
						{
							List<Joint> temp = new List<Joint>(joints);
							temp.RemoveAt(j);
							newObject.GetComponent<SimpleFracture>().joints = temp.ToArray();
						}
					}
				}
			}
			else
			{
				if ((bool)GetComponent<Joint>())
				{
					for (int j = 0; j < GetComponents<Joint>().Length; j++)
					{
						Object.Destroy(GetComponents<Joint>()[j]);
						Object.Destroy(newObject.GetComponents<Joint>()[j]);
					}
				}
				if (joints != null)
				{
					for (int j = 0; j < joints.Length; j++)
					{
						Object.Destroy(joints[j]);
					}
					joints = null;
				}
			}
			if (!GetComponent<Rigidbody>())
			{
				base.gameObject.AddComponent<Rigidbody>();
				newObject.AddComponent<Rigidbody>();
				GetComponent<Rigidbody>().mass = totalMassIfStatic;
				newObject.GetComponent<Rigidbody>().mass = totalMassIfStatic;
			}
			base.gameObject.GetComponent<Rigidbody>().mass *= 0.5f;
			newObject.GetComponent<Rigidbody>().mass *= 0.5f;
			base.gameObject.GetComponent<Rigidbody>().centerOfMass = base.transform.worldToLocalMatrix.MultiplyPoint3x4(base.gameObject.GetComponent<Collider>().bounds.center);
			newObject.GetComponent<Rigidbody>().centerOfMass = base.transform.worldToLocalMatrix.MultiplyPoint3x4(newObject.GetComponent<Collider>().bounds.center);
			StartCoroutine(newObject.GetComponent<SimpleFracture>().Fracture(point, force, iterations));
			if (destroyAllAfterTime >= 1f)
			{
				Object.Destroy(newObject.GetComponent<MeshCollider>(), destroyAllAfterTime - 1f);
				Object.Destroy(GetComponent<MeshCollider>(), destroyAllAfterTime - 1f);
				Object.Destroy(newObject, destroyAllAfterTime);
				Object.Destroy(base.gameObject, destroyAllAfterTime);
			}
			yield return null;
		}
		if (totalMaxFractures == 0 || Vector3.Min(base.gameObject.GetComponent<MeshFilter>().mesh.bounds.size, minFractureSize) != minFractureSize)
		{
			if (destroySmallAfterTime >= 1f)
			{
				Object.Destroy(GetComponent<MeshCollider>(), destroySmallAfterTime - 1f);
				Object.Destroy(base.gameObject, destroySmallAfterTime);
			}
			totalMaxFractures = 0;
		}
	}

	private float UsedMass(Collision collision)
	{
		Rigidbody component = collision.gameObject.GetComponent<Rigidbody>();
		Rigidbody component2 = GetComponent<Rigidbody>();
		if (component != null)
		{
			if (component2 != null)
			{
				if (component.mass < component2.mass)
				{
					return component.mass;
				}
				return component2.mass;
			}
			return component.mass;
		}
		if (component2 != null)
		{
			return component2.mass;
		}
		return 1f;
	}
}
