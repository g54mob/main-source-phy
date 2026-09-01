using Landfall.TABS_Input;
using UnityEngine;

public class UnitEditorCameraSpinner : MonoBehaviour
{
	public GameObject cameraSpinner;

	public float drag = 1f;

	public float pullForce = 1f;

	public float spinForceAmount;

	private Vector2 velocity;

	public RigidbodyHolder allRigs;

	private UnitEditorRayCaster rayCaster;

	private PlayerActions m_playerActions;

	private bool isHoldingUI;

	public bool IsSpinning { get; private set; }

	private void Start()
	{
		rayCaster = GetComponent<UnitEditorRayCaster>();
		m_playerActions = PlayerActions.Instance;
	}

	private void Update()
	{
		IsSpinning = false;
		Vector2 zero = Vector2.zero;
		if (Input.GetKeyDown(KeyCode.Mouse0) && rayCaster.RaycastUI())
		{
			isHoldingUI = true;
		}
		if (Input.GetKey(KeyCode.Mouse0))
		{
			if (!isHoldingUI)
			{
				IsSpinning = true;
				if (Mathf.Abs(m_playerActions.m_aim.X) > Mathf.Abs(m_playerActions.m_aim.Y))
				{
					zero.x = m_playerActions.m_aim.X * 1f;
				}
				else
				{
					zero.y = m_playerActions.m_aim.Y * -0.2f;
				}
				velocity -= Time.deltaTime * velocity * (drag / Mathf.Clamp(zero.magnitude, 0.1f, zero.magnitude));
				velocity -= Time.deltaTime * velocity * drag * 5f;
			}
		}
		else
		{
			isHoldingUI = false;
		}
		velocity += pullForce * zero;
		velocity -= Time.deltaTime * velocity * drag;
		velocity.y -= Time.deltaTime * velocity.y * drag * 5f;
		cameraSpinner.transform.Rotate(Vector3.up * velocity.x * Time.deltaTime, Space.World);
		cameraSpinner.transform.Rotate(base.transform.right * velocity.y * Time.deltaTime, Space.Self);
		if ((bool)allRigs)
		{
			allRigs.data.mainRig.constraints = (RigidbodyConstraints)10;
			Vector3 zero2 = Vector3.zero;
			for (int i = 0; i < allRigs.AllRigs.Length; i++)
			{
				zero2 = allRigs.AllRigs[i].transform.position - cameraSpinner.transform.position;
				zero2.y = 0f;
				allRigs.AllRigs[i].AddForce(zero2 * spinForceAmount * Mathf.Abs(velocity.x) * Time.deltaTime, ForceMode.Acceleration);
			}
		}
	}
}
