using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TFBGames
{
	public class DebugHighlightSelectedUIItem : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Don't destroy this game object when the scene loads. Only works if it doesn't have a parent.")]
		protected bool dontDestroyOnLoad = true;

		private GameObject currentObject;

		private Color currentColor;

		private Selectable currentSelectable;

		[SerializeField]
		private Color highlightColor = Color.magenta;

		private DebugHighlightSelectedUIItem instance;

		public void Awake()
		{
			if (!(instance != null) || !(this != instance))
			{
				instance = this;
				if (dontDestroyOnLoad && base.transform.parent == null)
				{
					Object.DontDestroyOnLoad(base.gameObject);
				}
			}
		}

		private void OnDisable()
		{
			DeselectCurrentObject();
		}

		private void Update()
		{
			EventSystem current = EventSystem.current;
			if (!(current == null))
			{
				GameObject currentSelectedGameObject = current.currentSelectedGameObject;
				if (currentSelectedGameObject != currentObject)
				{
					DeselectCurrentObject();
					SelectCurrentObject(currentSelectedGameObject);
				}
				UpdateCurrentObject();
			}
		}

		private void DeselectCurrentObject()
		{
			if (currentSelectable == null || currentSelectable.targetGraphic == null)
			{
				currentSelectable = null;
				return;
			}
			currentSelectable.targetGraphic.color = currentColor;
			currentSelectable = null;
		}

		private void SelectCurrentObject(GameObject current)
		{
			currentSelectable = current.GetComponent<Selectable>();
			if (currentSelectable != null && currentSelectable.targetGraphic != null)
			{
				currentColor = currentSelectable.targetGraphic.color;
			}
		}

		private void UpdateCurrentObject()
		{
			if (!(currentSelectable == null) && !(currentSelectable.targetGraphic == null))
			{
				currentSelectable.targetGraphic.color = highlightColor;
			}
		}

		private void OnGUI()
		{
			float num = 20f;
			Vector2 vector = new Vector2(250f, 40f);
			Rect position = new Rect(num, num, vector.x, vector.y);
			Color color = GUI.color;
			GUI.color = Color.red;
			GUI.Box(position, "");
			GUI.Box(position, "Remove " + GetType().Name + "\nfrom release builds!");
			GUI.color = color;
		}
	}
}
