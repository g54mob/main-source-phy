using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MoreMountains.Tools
{
	public class MMDebugOnScreenConsole : MonoBehaviour
	{
		[Header("Bindings")]
		public RectTransform Container;

		public Image BackgroundImage;

		public Text ConsoleText;

		[Header("Label")]
		public Color LabelColor;

		[Header("Value")]
		public string ValueColor;

		public float ValueSizeRatio;

		protected RectTransform _rectTransform;

		protected int _numberOfMessages;

		protected bool _messageStackHasBeenDisplayed;

		protected bool _newMessageThisFrame;

		protected int _largestMessageLength;

		protected StringBuilder _stringBuilder;

		protected string _valueTagStart;

		protected string _valueTagEnd;

		protected const string space = " ";

		protected Vector2 _closedSize;

		protected Vector2 _openBackgroundWidth;

		protected int _last_append_at_frame;

		public virtual void Toggle()
		{
		}

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		protected virtual void SetFontSize(int fontSize)
		{
		}

		protected virtual void LateUpdate()
		{
		}

		public virtual void SetScreenOffset(int top = 10, int left = 10)
		{
		}

		public virtual void SetMessage(string newMessage)
		{
		}

		public virtual void AddMessage(string label, object value, int fontSize)
		{
		}
	}
}
