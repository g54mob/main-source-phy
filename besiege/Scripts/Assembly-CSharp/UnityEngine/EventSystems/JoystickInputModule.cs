namespace UnityEngine.EventSystems
{
	[AddComponentMenu("Event/Joystick Input Module")]
	public class JoystickInputModule : PointerInputModule
	{
		public GameObject cursorObject;

		private Vector2 auxVec2;

		[SerializeField]
		private string m_SubmitButton = "Submit";

		public int playerNumber;

		private PointerEventData pointer;

		protected override void Start()
		{
			base.Start();
			if (cursorObject == null || base.eventSystem == null)
			{
				Debug.LogError("Set the game objects in the cursor module.");
				Object.Destroy(base.gameObject);
			}
			pointer = new PointerEventData(base.eventSystem);
			pointer.pointerId = playerNumber;
		}

		public override void Process()
		{
			Camera main = Camera.main;
			if (!main)
			{
				return;
			}
			Vector3 vector = main.WorldToScreenPoint(cursorObject.transform.position);
			auxVec2.x = vector.x;
			auxVec2.y = vector.y;
			pointer.position = auxVec2;
			base.eventSystem.RaycastAll(pointer, m_RaycastResultCache);
			RaycastResult raycastResult = BaseInputModule.FindFirstRaycast(m_RaycastResultCache);
			pointer.pointerCurrentRaycast = raycastResult;
			ProcessMove(pointer);
			pointer.clickCount = 0;
			if (Input.GetButtonDown(m_SubmitButton))
			{
				pointer.pressPosition = auxVec2;
				pointer.clickTime = Time.unscaledTime;
				pointer.pointerPressRaycast = raycastResult;
				pointer.clickCount = 1;
				pointer.eligibleForClick = true;
				if (m_RaycastResultCache.Count > 0)
				{
					pointer.selectedObject = raycastResult.gameObject;
					pointer.pointerPress = ExecuteEvents.ExecuteHierarchy(raycastResult.gameObject, pointer, ExecuteEvents.submitHandler);
					pointer.rawPointerPress = raycastResult.gameObject;
				}
				else
				{
					pointer.rawPointerPress = null;
				}
			}
			else
			{
				pointer.clickCount = 0;
				pointer.eligibleForClick = false;
				pointer.pointerPress = null;
				pointer.rawPointerPress = null;
			}
		}
	}
}
