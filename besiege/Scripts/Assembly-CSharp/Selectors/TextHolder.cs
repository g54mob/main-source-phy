using System;
using System.Linq;
using UnityEngine;

namespace Selectors
{
	public class TextHolder : BaseHolder
	{
		public enum Direction
		{
			Left = 0,
			Right = 1,
			Up = 2,
			Down = 3
		}

		private const float DOUBLE_CLICK_TIME = 0.3f;

		private static readonly char[] WORD_BOUNDS = new char[3] { ' ', '.', ';' };

		private string _valueText = string.Empty;

		private bool _isFocused;

		private bool _isSelecting;

		private bool isSettingText;

		public DynamicText text;

		public Transform flash;

		public Transform BG;

		public GameObject noTextDefault;

		[Tooltip("If true, TextChanged will be called whenever the user types anything valid in the field. If false, TextChanged is only called when the text field is deselected.")]
		public bool invokeEventOnEveryChange;

		public string prefix = string.Empty;

		public string suffix = string.Empty;

		[Tooltip("also allows cutting and pasting")]
		public bool allowCopy = true;

		public bool onlyUpperCase;

		public bool hideTextOnLock;

		public int CharLimit = 20;

		public bool useBackgroundWidthAsLimit;

		[Tooltip("how much margin to leave on the left and right side of the text before scaling")]
		public float margin = 0.1f;

		[Tooltip("character to replace spaces with in selection text so it creates the correct mesh with leading and trailing spaces")]
		public char spaceWidthCharacter = 'l';

		public int mask = -1;

		protected bool locked;

		private string _inputText;

		private Camera hudCamera;

		private DynamicText selection;

		private Vector2 flashStartScale;

		private bool inConflict;

		private DynamicText conflictOverlay;

		private int cursorPos;

		private int selectionAnchorPos;

		private bool selectionChanged;

		private bool prefixSuffixShown = true;

		private bool isDragging;

		private Vector3 mouseClickPos = Vector3.zero;

		private float pauseUntilTime = -1f;

		private float mouseFirstClickTime = -1f;

		private float mouseSecondClickTime = -1f;

		public string ValueText
		{
			get
			{
				return _valueText;
			}
			set
			{
				SetText(value);
			}
		}

		public bool IsFocused
		{
			get
			{
				return _isFocused;
			}
			set
			{
				SetFocused(value);
			}
		}

		public bool IsSelecting
		{
			get
			{
				return _isSelecting;
			}
		}

		public bool IsSelectingAll
		{
			get
			{
				return _isSelecting && SelectionStart == 0 && SelectionEnd == InputText.Length;
			}
		}

		public bool IsSettingText
		{
			get
			{
				return isSettingText;
			}
		}

		private float BackgroundWidth
		{
			get
			{
				return BG.localScale.x - margin * 2f;
			}
		}

		public int SelectionStart
		{
			get
			{
				return Mathf.Clamp(_isSelecting ? Mathf.Min(cursorPos, selectionAnchorPos) : cursorPos, 0, InputText.Length);
			}
		}

		public int SelectionEnd
		{
			get
			{
				return Mathf.Clamp(_isSelecting ? Mathf.Max(cursorPos, selectionAnchorPos) : cursorPos, 0, InputText.Length);
			}
		}

		private string InputText
		{
			get
			{
				return _inputText;
			}
			set
			{
				SetInput(value);
			}
		}

		public event TextChangeHandler TextChanged;

		public event Action<string> TextChangedExternal;

		public event Action<string> TextInput;

		public event Action<bool> FocusChange;

		public event Action<Direction> ArrowKeyPressed;

		protected virtual void Start()
		{
			GameObject gameObject = GameObject.Find("HUD Cam");
			if ((bool)gameObject)
			{
				hudCamera = gameObject.GetComponent<Camera>();
			}
			if (!hudCamera)
			{
				hudCamera = Camera.main;
			}
			flashStartScale = flash.localScale;
			flash.gameObject.SetActive(false);
			if (!string.IsNullOrEmpty(_valueText) && !string.IsNullOrEmpty(prefix))
			{
				_valueText = _valueText.TrimStart(prefix.ToCharArray());
			}
			if (!string.IsNullOrEmpty(_valueText) && !string.IsNullOrEmpty(suffix))
			{
				_valueText = _valueText.TrimEnd(suffix.ToCharArray());
			}
			SetInput(_valueText, false);
		}

		protected virtual void OnEnable()
		{
			StatMaster.Mode.BeforeSelectionChanged = (Action)Delegate.Combine(StatMaster.Mode.BeforeSelectionChanged, new Action(Terminate));
		}

		protected override void OnDisable()
		{
			StatMaster.Mode.BeforeSelectionChanged = (Action)Delegate.Remove(StatMaster.Mode.BeforeSelectionChanged, new Action(Terminate));
			base.OnDisable();
			Terminate();
		}

		protected virtual void Update()
		{
			if (!UIMask.InsideMask(mask, base.transform.position))
			{
				IsFocused = false;
				return;
			}
			LayerMask layerMask = ((!ReferenceMaster.Instance) ? ((LayerMask)(-1)) : ReferenceMaster.Instance.hudMask);
			if (InputManager.LeftMouseButton())
			{
				RaycastHit hitInfo;
				if (Physics.Raycast(hudCamera.ScreenPointToRay(Input.mousePosition), out hitInfo, float.MaxValue, layerMask) && hitInfo.collider.transform == base.transform)
				{
					if (!IsFocused)
					{
						IsFocused = true;
						SetCursorAndSelection(InputText.Length, 0);
					}
					else if (Time.realtimeSinceStartup < mouseSecondClickTime + 0.3f)
					{
						SetCursorAndSelection(InputText.Length, 0);
					}
					else if (Time.realtimeSinceStartup < mouseFirstClickTime + 0.3f)
					{
						int cursorPosFromRaycast = GetCursorPosFromRaycast(hitInfo);
						SetCursorAndSelection(GetCursorPosNextWordBoundary(Direction.Right, cursorPosFromRaycast), GetCursorPosNextWordBoundary(Direction.Left, cursorPosFromRaycast));
						mouseSecondClickTime = Time.realtimeSinceStartup;
					}
					else
					{
						SetCursor(GetCursorPosFromRaycast(hitInfo));
					}
					mouseFirstClickTime = Time.realtimeSinceStartup;
					mouseClickPos = hitInfo.point;
				}
				else if (!StayFocusedOnClickOutside())
				{
					IsFocused = false;
				}
			}
			if (!IsFocused)
			{
				return;
			}
			if (InputManager.LeftMouseButtonHeld())
			{
				RaycastHit hitInfo2;
				if (Physics.Raycast(hudCamera.ScreenPointToRay(Input.mousePosition), out hitInfo2, float.MaxValue, layerMask) && hitInfo2.collider.transform == base.transform)
				{
					if (!isDragging)
					{
						if ((hitInfo2.point - mouseClickPos).sqrMagnitude > 0f)
						{
							isDragging = true;
							SetCursor(GetCursorPosFromRaycast(hitInfo2));
						}
					}
					else
					{
						SetCursorAndSelection(GetCursorPosFromRaycast(hitInfo2), selectionAnchorPos);
					}
				}
			}
			else
			{
				isDragging = false;
			}
			bool flag = InputManager.GetKey(KeyCode.LeftShift) || InputManager.GetKey(KeyCode.RightShift);
			bool flag2 = InputManager.GetKey(KeyCode.LeftControl) || InputManager.GetKey(KeyCode.RightControl);
			if (InputManager.GetKey(KeyCode.Escape))
			{
				IsFocused = false;
				return;
			}
			if (InputManager.GetKey(KeyCode.LeftArrow))
			{
				OnArrowKeyPressed(flag2, flag, Direction.Left);
			}
			else if (InputManager.GetKey(KeyCode.RightArrow))
			{
				OnArrowKeyPressed(flag2, flag, Direction.Right);
			}
			else if (InputManager.GetKey(KeyCode.UpArrow))
			{
				OnArrowKeyPressed(flag2, flag, Direction.Up);
			}
			else if (InputManager.GetKey(KeyCode.DownArrow))
			{
				OnArrowKeyPressed(flag2, flag, Direction.Down);
			}
			else if (InputManager.GetKeyDown(KeyCode.Home))
			{
				SetCursorAndSelection(0, flag ? ((!_isSelecting) ? cursorPos : selectionAnchorPos) : 0);
			}
			else if (InputManager.GetKeyDown(KeyCode.End))
			{
				SetCursorAndSelection(InputText.Length, (!flag) ? InputText.Length : ((!_isSelecting) ? cursorPos : selectionAnchorPos));
			}
			else if (InputManager.SelectAllKeys())
			{
				SetCursorAndSelection(InputText.Length, 0);
			}
			else if (allowCopy && InputManager.CutKeys())
			{
				CopySelection(InputText.Substring(SelectionStart, SelectionEnd - SelectionStart));
				DeleteSelection();
			}
			else if (allowCopy && InputManager.CopyKeys())
			{
				CopySelection(InputText.Substring(SelectionStart, SelectionEnd - SelectionStart));
			}
			else if (allowCopy && InputManager.PasteKeys())
			{
				PasteSelection();
			}
			else if (InputManager.GetKey(KeyCode.Delete))
			{
				if (Time.realtimeSinceStartup > pauseUntilTime)
				{
					PauseKeys();
					if (_isSelecting)
					{
						DeleteSelection();
					}
					else if (flag)
					{
						InputText = string.Empty;
						SetCursor(0);
					}
					else if (cursorPos < InputText.Length)
					{
						if (flag2)
						{
							int cursorPosNextWordBoundary = GetCursorPosNextWordBoundary(Direction.Right, cursorPos);
							InputText = InputText.Substring(0, cursorPos) + InputText.Substring(cursorPosNextWordBoundary);
						}
						else
						{
							InputText = InputText.Substring(0, cursorPos) + InputText.Substring(cursorPos + 1);
						}
						SetCursor(cursorPos);
					}
				}
			}
			else if (Input.inputString.Length > 0)
			{
				string text = Input.inputString;
				if (_isSelecting && !text.StartsWith("\n") && !text.StartsWith("\r"))
				{
					DeleteSelection();
					text = text.TrimStart('\b');
				}
				int num = cursorPos;
				string text2 = InputText;
				bool flag3 = false;
				string text3 = text;
				foreach (char c in text3)
				{
					if (c == '\n' || c == '\r')
					{
						flag3 = true;
						break;
					}
					if (c == '\u007f' || (c == '\b' && InputManager.LeftHotCtrlKey()))
					{
						if (num != 0)
						{
							int cursorPosNextWordBoundary2 = GetCursorPosNextWordBoundary(Direction.Left, num);
							text2 = text2.Substring(0, cursorPosNextWordBoundary2) + text2.Substring(num);
							num = cursorPosNextWordBoundary2;
						}
						continue;
					}
					if (c == '\b')
					{
						if (num != 0)
						{
							text2 = text2.Substring(0, num - 1) + text2.Substring(num);
							num--;
						}
						continue;
					}
					if ((useBackgroundWidthAsLimit && this.text.bounds.size.x > BackgroundWidth) || text2.Length >= CharLimit)
					{
						break;
					}
					if (ValidateInput(c))
					{
						text2 = text2.Substring(0, num) + c + text2.Substring(num);
						num++;
					}
				}
				InputText = text2;
				if (flag3)
				{
					IsFocused = false;
				}
				else
				{
					SetCursor(num);
				}
			}
			else
			{
				pauseUntilTime = -1f;
			}
			if (selectionChanged)
			{
				selectionChanged = false;
				RenderSelection();
			}
		}

		private void OnArrowKeyPressed(bool ctrlHeld, bool shiftHeld, Direction direction)
		{
			if (Time.realtimeSinceStartup > pauseUntilTime)
			{
				PauseKeys();
				MoveCursor(direction, ctrlHeld, shiftHeld);
				if (this.ArrowKeyPressed != null)
				{
					this.ArrowKeyPressed(direction);
				}
			}
		}

		private void PauseKeys()
		{
			pauseUntilTime = Time.realtimeSinceStartup + ((pauseUntilTime != -1f) ? 0.02f : 0.5f);
		}

		protected virtual bool StayFocusedOnClickOutside()
		{
			return false;
		}

		protected void SetCursor(int newCursorPos)
		{
			if (IsFocused)
			{
				cursorPos = Mathf.Clamp(newCursorPos, 0, InputText.Length);
				selectionAnchorPos = cursorPos;
				_isSelecting = false;
				selectionChanged = true;
			}
		}

		protected void SetCursorAndSelection(int newCursorPos, int newSelectionAnchorPos)
		{
			if (IsFocused)
			{
				cursorPos = Mathf.Clamp(newCursorPos, 0, InputText.Length);
				selectionAnchorPos = Mathf.Clamp(newSelectionAnchorPos, 0, InputText.Length);
				_isSelecting = newCursorPos != newSelectionAnchorPos;
				selectionChanged = true;
			}
		}

		private void MoveCursor(Direction direction, bool overWord = false, bool select = false)
		{
			if (IsFocused)
			{
				bool flag = select && !_isSelecting;
				bool flag2 = !select && _isSelecting;
				if (flag)
				{
					selectionAnchorPos = Mathf.Clamp(cursorPos, 0, InputText.Length);
					_isSelecting = true;
				}
				else if (flag2)
				{
					cursorPos = ((direction != Direction.Left) ? SelectionEnd : SelectionStart);
					_isSelecting = false;
				}
				if (!flag2)
				{
					cursorPos = (overWord ? GetCursorPosNextWordBoundary(direction, cursorPos) : ((direction != Direction.Left) ? Mathf.Min(cursorPos + 1, InputText.Length) : Mathf.Max(cursorPos - 1, 0)));
				}
				selectionChanged = true;
			}
		}

		private void RenderSelection()
		{
			if (IsFocused)
			{
				float num = 0f;
				if (!string.IsNullOrEmpty(prefix) && prefixSuffixShown)
				{
					ReferenceMaster.SetDynamicText(selection, prefix);
					selection.GenerateMesh();
					num = selection.bounds.max.x - selection.bounds.min.x;
				}
				int selectionStart = SelectionStart;
				string text = PadLeadingTrailingSpaces(InputText.Substring(0, selectionStart));
				ReferenceMaster.SetDynamicText(selection, text);
				selection.GenerateMesh();
				float num2 = num + selection.bounds.max.x - selection.bounds.min.x;
				float x = this.text.bounds.min.x;
				if (!_isSelecting)
				{
					flash.localPosition = flash.transform.localPosition.WithX(x + num2);
					flash.localScale = flash.localScale.WithX(flashStartScale.x);
					return;
				}
				int selectionEnd = SelectionEnd;
				selection.transform.localPosition = selection.transform.localPosition.WithX(x + num2);
				string text2 = PadLeadingTrailingSpaces(InputText.Substring(selectionStart, selectionEnd - selectionStart));
				ReferenceMaster.SetDynamicText(selection, text2);
				selection.GenerateMesh();
				flash.localPosition = flash.localPosition.WithX(selection.transform.localPosition.x + selection.bounds.center.x);
				flash.localScale = flash.localScale.WithX(selection.bounds.size.x);
			}
		}

		private string PadLeadingTrailingSpaces(string str)
		{
			int num = str.Length - str.TrimStart(' ').Length;
			if (num > 0)
			{
				str = str.TrimStart(' ').PadLeft(str.Length, spaceWidthCharacter);
			}
			int num2 = str.Length - str.TrimEnd(' ').Length;
			if (num2 > 0)
			{
				str = str.TrimEnd(' ').PadRight(str.Length, spaceWidthCharacter);
			}
			return str;
		}

		private int GetCursorPosNextWordBoundary(Direction direction, int fromPosition)
		{
			switch (direction)
			{
			case Direction.Left:
			{
				for (int num = fromPosition - 1; num > 0; num--)
				{
					if (WORD_BOUNDS.Contains(InputText[num - 1]))
					{
						return num;
					}
				}
				return 0;
			}
			case Direction.Right:
			{
				for (int i = fromPosition + 1; i < InputText.Length; i++)
				{
					if (WORD_BOUNDS.Contains(InputText[i]))
					{
						return i;
					}
				}
				return InputText.Length;
			}
			default:
				throw new InvalidOperationException(direction.ToString());
			}
		}

		private int GetCursorPosFromRaycast(RaycastHit hit)
		{
			if (!IsFocused)
			{
				throw new InvalidOperationException("Cannot get cursor position when not focused.");
			}
			Vector3 vector = text.transform.TransformPoint(text.bounds.min);
			Vector3 vector2 = text.transform.TransformPoint(text.bounds.max);
			float num = Mathf.InverseLerp(vector.x, vector2.x, hit.point.x);
			int value = ((!prefixSuffixShown) ? Mathf.RoundToInt(num * (float)InputText.Length) : (Mathf.RoundToInt(num * (float)(InputText.Length + prefix.Length + suffix.Length)) - prefix.Length));
			return Mathf.Clamp(value, 0, InputText.Length);
		}

		private void DeleteSelection()
		{
			if (_isSelecting)
			{
				InputText = InputText.Substring(0, SelectionStart) + InputText.Substring(SelectionEnd);
				SetCursor(SelectionStart);
			}
		}

		protected virtual void CopySelection(string selection)
		{
			if (_isSelecting)
			{
				GUIUtility.systemCopyBuffer = selection + '\u200b';
				ReferenceMaster.Clipboard.valueText = string.Empty;
			}
		}

		protected virtual void PasteSelection(string pastedText = null)
		{
			if (string.IsNullOrEmpty(pastedText))
			{
				pastedText = GUIUtility.systemCopyBuffer;
			}
			pastedText = pastedText.Replace("\u200b", string.Empty);
			string text = InputText.Substring(0, SelectionStart) + pastedText + InputText.Substring(SelectionEnd);
			if (text.Length > CharLimit)
			{
				text = text.Substring(0, CharLimit);
			}
			InputText = text;
			SetCursor(SelectionStart + pastedText.Length);
		}

		[Obsolete("Use ValueText instead.")]
		public string GrabText()
		{
			return ValueText;
		}

		[Obsolete("Use ValueText instead.")]
		public bool TryGetValue(out string value)
		{
			value = ValueText;
			return true;
		}

		public void SetText(string newValue)
		{
			if (ValidateValue(newValue, out newValue, true))
			{
				IsFocused = false;
				SetInput(newValue, false);
				_valueText = newValue;
				if (this.TextChangedExternal != null)
				{
					this.TextChangedExternal(newValue);
				}
			}
		}

		protected void SetInput(string newValue, bool fireTextChangedEvent = true)
		{
			if (onlyUpperCase)
			{
				newValue = newValue.ToUpper(StaticSettings.Culture);
			}
			prefixSuffixShown = ShowPrefixAndSuffix(newValue);
			string text = ((!prefixSuffixShown) ? newValue : (prefix + newValue + suffix));
			ReferenceMaster.SetDynamicText(this.text, text);
			this.text.GenerateMesh();
			if ((bool)noTextDefault)
			{
				noTextDefault.SetActive(!inConflict && string.IsNullOrEmpty(prefix) && string.IsNullOrEmpty(suffix) && string.IsNullOrEmpty(newValue.Trim()));
			}
			if (_inputText != newValue && this.TextInput != null)
			{
				this.TextInput(newValue);
			}
			if (fireTextChangedEvent && invokeEventOnEveryChange && newValue != GetInputTextForEditing() && ValidateValue(newValue, out newValue))
			{
				_valueText = (_inputText = newValue);
				OnTextChanged();
			}
			_inputText = newValue;
		}

		protected virtual bool ValidateInput(char input)
		{
			if (InputText.Length >= CharLimit)
			{
				return false;
			}
			return true;
		}

		protected virtual bool ValidateValue(string text, out string validatedText, bool isExternalSet = false)
		{
			if (isExternalSet)
			{
				validatedText = text;
				return true;
			}
			if (text.Length > CharLimit)
			{
				validatedText = text.Substring(0, CharLimit);
			}
			validatedText = text;
			return true;
		}

		private void SetFocused(bool value)
		{
			if (value == _isFocused)
			{
				return;
			}
			_isFocused = value;
			StopHotkeys(value);
			TextFieldSelected(value);
			flash.gameObject.SetActive(_isFocused);
			if (_isFocused)
			{
				if (!selection)
				{
					selection = ((GameObject)UnityEngine.Object.Instantiate(this.text.gameObject, this.text.transform, false)).GetComponent<DynamicText>();
					selection.name = "selectionPlacer";
					selection.alignment = TextAlignment.Left;
					selection.anchor = DynamicTextAnchor.BaselineLeft;
					selection.transform.localPosition = Vector3.zero;
					selection.GetComponent<MeshRenderer>().enabled = false;
				}
				selection.gameObject.SetActive(true);
				if (flash.parent != this.text.transform)
				{
					flash.SetParent(this.text.transform);
				}
				InputText = GetInputTextForEditing();
				SetCursorAndSelection(InputText.Length, 0);
			}
			else
			{
				_isSelecting = false;
				string validatedText;
				if ((InputText == GetInputTextForEditing() && !inConflict) || !ValidateValue(InputText, out validatedText))
				{
					InputText = ValueText;
				}
				else
				{
					string valueText = (InputText = validatedText);
					_valueText = valueText;
					OnTextChanged();
				}
			}
			if (this.FocusChange != null)
			{
				this.FocusChange(_isFocused);
			}
			if ((bool)conflictOverlay)
			{
				conflictOverlay.gameObject.SetActive(inConflict && !_isFocused);
				this.text.gameObject.SetActive(!inConflict || _isFocused);
			}
		}

		public void SetConflict(bool conflict)
		{
			inConflict = conflict;
			if (!conflictOverlay)
			{
				if (!conflict)
				{
					return;
				}
				conflictOverlay = ((GameObject)UnityEngine.Object.Instantiate(text.gameObject, text.transform.parent, false)).GetComponent<DynamicText>();
				conflictOverlay.name = "conflictOverlay";
				ReferenceMaster.SetDynamicText(conflictOverlay, "●●●");
				if (conflictOverlay.anchor == DynamicTextAnchor.BaselineCenter || conflictOverlay.anchor == DynamicTextAnchor.BaselineLeft || conflictOverlay.anchor == DynamicTextAnchor.BaselineRight)
				{
					conflictOverlay.transform.localPosition += Vector3.zero.WithY(0.02f);
				}
			}
			conflictOverlay.gameObject.SetActive(inConflict && !_isFocused);
			text.gameObject.SetActive(!inConflict || _isFocused);
			if ((bool)noTextDefault)
			{
				noTextDefault.SetActive(!inConflict && string.IsNullOrEmpty(prefix) && string.IsNullOrEmpty(suffix) && string.IsNullOrEmpty(_valueText));
			}
		}

		protected virtual string GetInputTextForEditing()
		{
			return ValueText;
		}

		protected virtual bool ShowPrefixAndSuffix(string text)
		{
			return true;
		}

		public void SetPrefixSuffix(string prefix, string suffix)
		{
			this.prefix = prefix;
			this.suffix = suffix;
		}

		public void Terminate()
		{
			IsFocused = false;
		}

		public void Hide(bool hidden)
		{
			if (hidden)
			{
				if (text.gameObject.activeSelf)
				{
					text.gameObject.SetActive(false);
				}
			}
			else if (!text.gameObject.activeSelf)
			{
				text.gameObject.SetActive(true);
			}
		}

		public void Lock(bool locked)
		{
			this.locked = locked;
			if (hideTextOnLock)
			{
				if (locked)
				{
					if (text.gameObject.activeSelf)
					{
						text.gameObject.SetActive(false);
					}
				}
				else if (!text.gameObject.activeSelf)
				{
					text.gameObject.SetActive(true);
				}
			}
			else if (!text.gameObject.activeSelf)
			{
				text.gameObject.SetActive(true);
			}
		}

		protected void OnTextChanged()
		{
			if (this.TextChanged != null && !isSettingText)
			{
				isSettingText = true;
				this.TextChanged(ValueText);
				isSettingText = false;
			}
		}

		public virtual void ResetDelegate()
		{
			this.TextChanged = null;
		}
	}
}
