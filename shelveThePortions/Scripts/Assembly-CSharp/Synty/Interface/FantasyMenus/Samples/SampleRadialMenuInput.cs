using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleRadialMenuInput : MonoBehaviour
	{
		[Header("References")]
		public Transform origin;

		public Selectable[] selectables;

		[Header("Paramters")]
		[Range(0f, 1f)]
		public float currentSelectionBias = 0.1f;

		private bool IsRadialMenuElementSelected()
		{
			for (int i = 0; i < selectables.Length; i++)
			{
				if (EventSystem.current.currentSelectedGameObject == selectables[i].gameObject)
				{
					return true;
				}
			}
			return false;
		}

		private float GetInput(string[] inputAxies)
		{
			float num = 0f;
			for (int i = 0; i < inputAxies.Length; i++)
			{
				num += Input.GetAxisRaw(inputAxies[i]);
			}
			return num;
		}

		private float GetHorizontal()
		{
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				return -1f;
			}
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				return 1f;
			}
			return 0f;
		}

		private float GetVerticle()
		{
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				return -1f;
			}
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				return 1f;
			}
			return 0f;
		}

		private void Update()
		{
			if (!IsRadialMenuElementSelected() || EventSystem.current.alreadySelecting)
			{
				return;
			}
			Vector3 vector = new Vector3(GetHorizontal(), GetVerticle(), 0f);
			if (!(vector.magnitude > 0.1f))
			{
				return;
			}
			Vector3 normalized = vector.normalized;
			Debug.DrawLine(origin.position, origin.position + normalized * 100f, Color.red);
			int num = 0;
			float num2 = 0f;
			for (int i = 0; i < selectables.Length; i++)
			{
				Debug.DrawLine(origin.position, selectables[i].transform.position, Color.green);
				bool flag = EventSystem.current.currentSelectedGameObject == selectables[i].gameObject;
				Vector3 normalized2 = (selectables[i].transform.position - origin.position).normalized;
				float num3 = Vector3.Dot(normalized, normalized2) + (flag ? currentSelectionBias : 0f);
				if (i == 0)
				{
					num = 0;
					num2 = num3;
				}
				else if (num3 > num2)
				{
					num2 = num3;
					num = i;
				}
			}
			if (EventSystem.current.currentSelectedGameObject != selectables[num].gameObject)
			{
				selectables[num].Select();
			}
		}
	}
}
