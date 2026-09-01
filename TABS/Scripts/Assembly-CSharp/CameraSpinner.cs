using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraSpinner : MonoBehaviour
{
	public float m_rotationDrag = 5f;

	public float m_rotationSpring = 100f;

	public float m_zoomSpring = 10f;

	public float m_zoomSpringController = 10f;

	public float m_zoomDrag = 5f;

	public float m_zoomFieldOfViewFactor = 2f;

	public float m_zoomMaxSpring = 10f;

	public float m_zoomMinSpring = 10f;

	public float m_UpDownSpringZoomedIn = 2f;

	public float m_UpDownSpringZoomedOut = 2f;

	public float m_UpDownDrag = 5f;

	public float m_UpDownMaxSpring = 10f;

	public float m_UpDownMinSpring = 10f;

	public float m_zoomedInMaxY = 0.5f;

	public float m_zoomedInMinY = -1f;

	public float m_SpinForce = 10f;

	public float m_ControllerDamping = 0.5f;

	public Camera renderingCamera;

	private float rotation;

	private float velocity;

	private float upDownVelocity;

	private float distance = 5.1f;

	private float yPos;

	private float defaultYValue;

	[SerializeField]
	private float minDistance = 0.85f;

	[SerializeField]
	private float maxDistance = 50f;

	private float distanceVelocity;

	private RigidbodyHolder rigidbodyHolder;

	private PlayerActions m_playerActions;

	private bool m_useController;

	private InputService inputService;

	[SerializeField]
	private Slider m_zoomSlider;

	public bool UseController
	{
		get
		{
			return m_useController;
		}
		set
		{
			m_useController = value;
		}
	}

	private void Awake()
	{
		m_playerActions = PlayerActions.Instance;
		yPos = renderingCamera.transform.localPosition.y;
		inputService = ServiceLocator.GetService<InputService>();
	}

	private void Start()
	{
		m_useController = inputService.CurrentInputType == InputType.Controller;
	}

	private void OnSliderValueChanged(float value)
	{
		distance = value;
	}

	private void Update()
	{
		float t = Mathf.InverseLerp(minDistance, maxDistance, distance);
		if (!EventSystem.current.IsPointerOverGameObject())
		{
			if (Input.GetMouseButton(0) || m_useController)
			{
				velocity += m_playerActions.m_aim.X * m_rotationSpring;
				upDownVelocity -= m_playerActions.m_aim.Y * Mathf.Lerp(m_UpDownSpringZoomedIn, m_UpDownSpringZoomedOut, t);
			}
			float num = (float)m_playerActions.m_placementZoom * (m_useController ? m_zoomSpringController : m_zoomSpring) * Time.deltaTime;
			distanceVelocity += num;
		}
		float num2 = (m_useController ? m_ControllerDamping : 1f);
		velocity -= velocity * m_rotationDrag * num2 * Time.deltaTime;
		upDownVelocity -= upDownVelocity * m_UpDownDrag * Time.deltaTime;
		if (distance > maxDistance)
		{
			float num3 = maxDistance - distance;
			distanceVelocity += num3 * Time.deltaTime * m_zoomMaxSpring;
		}
		if (distance < minDistance)
		{
			float num4 = minDistance - distance;
			distanceVelocity += num4 * Time.deltaTime * m_zoomMinSpring;
		}
		distanceVelocity -= distanceVelocity * m_zoomDrag * Time.deltaTime;
		rotation += velocity * Time.deltaTime;
		distance += distanceVelocity * Time.deltaTime;
		if (yPos > m_zoomedInMaxY)
		{
			float num5 = m_zoomedInMaxY - yPos;
			upDownVelocity += num5 * Time.deltaTime * m_UpDownMaxSpring;
		}
		if (yPos < m_zoomedInMinY)
		{
			float num6 = m_zoomedInMinY - yPos;
			upDownVelocity += num6 * Time.deltaTime * m_UpDownMinSpring;
		}
		yPos += upDownVelocity * Time.deltaTime;
		renderingCamera.transform.localPosition = new Vector3(0f, Mathf.Lerp(yPos, defaultYValue, t), 0f - distance);
		renderingCamera.fieldOfView += distanceVelocity * Time.deltaTime * m_zoomFieldOfViewFactor;
		base.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
	}

	private void FixedUpdate()
	{
		if ((bool)rigidbodyHolder)
		{
			Rigidbody[] allRigs = rigidbodyHolder.AllRigs;
			foreach (Rigidbody obj in allRigs)
			{
				Vector3 vector = obj.transform.position - base.transform.position;
				vector.y = 0f;
				obj.AddForce(vector * Mathf.Abs(velocity) * m_SpinForce * Time.fixedDeltaTime, ForceMode.Acceleration);
			}
		}
	}

	public void SetRigidbodyHolder(RigidbodyHolder rigidbodyHolder)
	{
		this.rigidbodyHolder = rigidbodyHolder;
	}
}
