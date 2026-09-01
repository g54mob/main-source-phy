using Cpp2ILInjected;
using Localisation;
using TMPro;
using UnityEngine;

public class HoverTooltip : MonoBehaviour
{
	private Camera raycastCamera;

	private TextMeshProUGUI tmpText;

	private TextIdentifier textIdentifier;

	private Vector2 tooltipScreenOffset;

	private bool debugLogs;

	private RectTransform _rectTransform;

	private Transform _worldAnchor;

	private bool _visible;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		RectTransform rectTransform = default(RectTransform);
		_rectTransform = rectTransform;
		if (!raycastCamera)
		{
			Camera main = Camera.main;
			raycastCamera = main;
		}
		GameObject gameObject = base.gameObject;
		gameObject.SetActive(value: false);
		_visible = false;
	}

	private void LateUpdate()
	{
		if (_visible && _worldAnchor != null)
		{
			UpdateScreenPosition();
		}
	}

	public void Show(Transform worldAnchor)
	{
		if ((bool)worldAnchor)
		{
			_worldAnchor = worldAnchor;
			_visible = true;
			UpdateScreenPosition();
			GameObject gameObject = base.gameObject;
			gameObject.SetActive(value: true);
			if (debugLogs)
			{
				string text = worldAnchor.name;
				string message = "[HoverTooltip] Show — anchor: '" + text + "'.";
				Debug.Log(message, this);
			}
		}
	}

	public void Show(Transform worldAnchor, string addition)
	{
		if ((bool)worldAnchor)
		{
			_worldAnchor = worldAnchor;
			_visible = true;
			if (tmpText != null)
			{
				string text = textIdentifier.Get();
				string text2 = text + " " + addition;
				tmpText.text = text2;
			}
			UpdateScreenPosition();
			GameObject gameObject = base.gameObject;
			gameObject.SetActive(value: true);
			if (debugLogs)
			{
				string text3 = worldAnchor.name;
				string message = "[HoverTooltip] Show — anchor: '" + text3 + "'.";
				Debug.Log(message, this);
			}
		}
	}

	public void Show(string addition)
	{
		_visible = true;
		if (tmpText != null)
		{
			string text = textIdentifier.Get();
			string text2 = text + " " + addition;
			tmpText.text = text2;
		}
		GameObject gameObject = base.gameObject;
		gameObject.SetActive(value: true);
		if (debugLogs)
		{
			Debug.Log("[HoverTooltip] Show'.", this);
		}
	}

	public void Hide()
	{
		if (_visible)
		{
			_visible = false;
			_worldAnchor = null;
			GameObject gameObject = base.gameObject;
			gameObject.SetActive(value: false);
			if (debugLogs)
			{
				Debug.Log("[HoverTooltip] Hide.", this);
			}
		}
	}

	private unsafe void UpdateScreenPosition()
	{
		//IL_00a0: Expected O, but got Ref
		//IL_00b3: Invalid comparison between I4 and F4
		//IL_00d6: Expected O, but got Ref
		if ((bool)raycastCamera && (bool)_rectTransform && _worldAnchor != null)
		{
			Vector3 position = _worldAnchor.position;
			float num = default(float);
			if (!(0f > raycastCamera.WorldToScreenPoint((Vector3)(&num)).z))
			{
			}
			_rectTransform.position = (Vector3)(&num);
		}
	}

	public HoverTooltip()
	{
		//IL_000b: Expected O, but got I4
		tooltipScreenOffset = (Vector2)0;
		_ = 1114636288;
		base._002Ector();
	}
}
