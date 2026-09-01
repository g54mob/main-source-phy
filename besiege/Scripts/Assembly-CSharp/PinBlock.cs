using System.Collections;
using UnityEngine;

public class PinBlock : MonoBehaviour
{
	[HideInInspector]
	public bool hideVisuals;

	[HideInInspector]
	public BlockVisualController VisualController;

	public ConfigurableJoint myJoint;

	public bool isPinned;

	public Renderer vis;

	public Transform pinTransform;

	public Vector3 startScale;

	public float wobbleAmount = 0.5f;

	public float wobbleDuration = 0.5f;

	public Rigidbody parentRigidbody;

	public float fadeDuration = 0.5f;

	public float fadeTransparency = 0.3f;

	private float timedOut = 0.1f;

	private float lastCheck;

	private float checkInterval = 0.1f;

	private Machine machine;

	private bool foundMachine;

	private void Start()
	{
		machine = GetComponentInParent<Machine>();
		foundMachine = machine != null;
		if (foundMachine && !machine.isSimulating)
		{
			vis.transform.parent.up = Vector3.up;
			startScale = pinTransform.localScale;
			StartCoroutine(WobblePinVis());
		}
		else
		{
			pinTransform.localScale = startScale;
		}
	}

	private void Update()
	{
		timedOut -= Time.deltaTime;
		if (foundMachine && machine.isSimulating)
		{
			lastCheck += Time.deltaTime;
			if (lastCheck > checkInterval)
			{
				CheckNullJoint();
				lastCheck = 0f;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!foundMachine || !machine.isSimulating || isPinned || !(timedOut > 0f))
		{
			return;
		}
		if (!machine.SimPhysics)
		{
			Pin();
			return;
		}
		Rigidbody attachedRigidbody = other.attachedRigidbody;
		if ((bool)myJoint && !myJoint.connectedBody && (bool)attachedRigidbody && attachedRigidbody != parentRigidbody && (bool)attachedRigidbody.GetComponent<BlockBehaviour>() && attachedRigidbody.gameObject.layer != 16)
		{
			Pin();
			myJoint.connectedBody = attachedRigidbody;
		}
	}

	private void Pin()
	{
		if (!isPinned)
		{
			isPinned = true;
			StartCoroutine(LerpTransparency());
		}
	}

	public void Release()
	{
		if (myJoint != null && myJoint.connectedBody != null)
		{
			myJoint.connectedBody.WakeUp();
			myJoint.connectedBody = null;
		}
	}

	private void CheckNullJoint()
	{
		if (!myJoint || !myJoint.connectedBody)
		{
			vis.enabled = false;
		}
	}

	private IEnumerator WobblePinVis()
	{
		float i = 0f;
		float rate = 1f / wobbleDuration;
		while (i < 1f)
		{
			i += Time.deltaTime * rate;
			pinTransform.localScale = startScale * (1f + Mathfx.SmoothBounce(i) * wobbleAmount);
			yield return null;
		}
	}

	private IEnumerator LerpTransparency()
	{
		float i = 0f;
		float rate = 1f / fadeDuration;
		if (hideVisuals)
		{
			fadeTransparency = 0f;
		}
		while (i < 1f)
		{
			i += Time.deltaTime * rate;
			if (VisualController != null)
			{
				VisualController.SetTransparency(Mathf.Lerp(1f, fadeTransparency, i));
			}
			yield return null;
		}
	}
}
