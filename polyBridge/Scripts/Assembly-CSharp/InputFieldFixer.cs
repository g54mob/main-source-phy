using TMPro;
using UnityEngine;

public class InputFieldFixer : MonoBehaviour
{
	private TMP_InputField _input;

	private float m_TimeToEnable;

	private void Start()
	{
		_input = GetComponent<TMP_InputField>();
	}

	private void Update()
	{
		if (Mathf.Approximately(Mathf.Abs(Input.GetAxis("Mouse ScrollWheel")), 0f))
		{
			if (Time.realtimeSinceStartup > m_TimeToEnable)
			{
				_input.enabled = true;
			}
		}
		else
		{
			_input.enabled = false;
			m_TimeToEnable = Time.realtimeSinceStartup + 0.5f;
		}
	}
}
