using UnityEngine;

namespace Battlehub.RTCommon
{
	[DefaultExecutionOrder(100)]
	public class InputLowForRDP : MonoBehaviour, IInput, ITouchInput
	{
		private Vector3 m_prevMousePosition;

		public bool IsTouchSupported => Input.touchSupported;

		public int TouchCount => Input.touchCount;

		private void Awake()
		{
			IOC.Register((IInput)this);
		}

		private void OnDestroy()
		{
			IOC.Unregister((IInput)this);
		}

		private void Start()
		{
			m_prevMousePosition = Input.mousePosition;
		}

		private void LateUpdate()
		{
			m_prevMousePosition = Input.mousePosition;
			Debug.Log(m_prevMousePosition);
		}

		public virtual bool IsAnyKeyDown()
		{
			return Input.anyKeyDown;
		}

		public virtual bool IsAnyKey()
		{
			return Input.anyKey;
		}

		public virtual bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDown(key);
		}

		public virtual bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUp(key);
		}

		public virtual bool GetKey(KeyCode key)
		{
			return Input.GetKey(key);
		}

		public virtual float GetAxis(InputAxis axis)
		{
			return axis switch
			{
				InputAxis.X => (Input.mousePosition.x - m_prevMousePosition.x) * 0.1f, 
				InputAxis.Y => (Input.mousePosition.y - m_prevMousePosition.y) * 0.1f, 
				InputAxis.Z => Input.GetAxis("Mouse ScrollWheel"), 
				InputAxis.Horizontal => Input.GetAxis("Horizontal"), 
				InputAxis.Vertical => Input.GetAxis("Vertical"), 
				_ => 0f, 
			};
		}

		public virtual Vector3 GetPointerXY(int pointer)
		{
			if (pointer == 0)
			{
				return Input.mousePosition;
			}
			if (Input.touchCount > pointer)
			{
				return Input.GetTouch(pointer).position;
			}
			return Vector3.zero;
		}

		public virtual bool GetPointerDown(int index)
		{
			return Input.GetMouseButtonDown(index);
		}

		public virtual bool GetPointerUp(int index)
		{
			return Input.GetMouseButtonUp(index);
		}

		public virtual bool GetPointer(int index)
		{
			return Input.GetMouseButton(index);
		}

		public Touch GetTouch(int index)
		{
			return Input.GetTouch(index);
		}
	}
}
