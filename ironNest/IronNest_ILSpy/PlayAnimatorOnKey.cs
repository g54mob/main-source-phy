using System;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayAnimatorOnKey : MonoBehaviour
{
	public enum BehaviourTypes
	{
		Trigger,
		Bool,
		BoolHold,
		Play
	}

	public Animator animator;

	public BehaviourTypes Behaviour;

	public string variableName;

	public InputActionReference inputAction;

	public bool manageActionEnable;

	private bool boolState;

	private InputAction _action;

	private void Reset()
	{
		if (this.animator == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Animator animator = default(Animator);
			this.animator = animator;
		}
	}

	private void Awake()
	{
		if (this.animator == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Animator animator = default(Animator);
			this.animator = animator;
		}
		if (this.animator != null && !string.IsNullOrEmpty(variableName) && Behaviour == BehaviourTypes.Bool && HasAnimatorParameter(this.animator, variableName, AnimatorControllerParameterType.Bool))
		{
			bool flag = this.animator.GetBool(variableName);
			boolState = flag;
		}
	}

	private void OnEnable()
	{
		if (inputAction != null)
		{
			InputAction action = inputAction.action;
			if (action != null)
			{
				InputAction action2 = inputAction.action;
				_action = action2;
				Action<InputAction.CallbackContext> value = OnActionPerformed;
				_action.performed += value;
				Action<InputAction.CallbackContext> value2 = OnActionCanceled;
				_action.canceled += value2;
				if (manageActionEnable && !_action.enabled)
				{
					_action.Enable();
				}
				return;
			}
		}
		Debug.LogWarning("PlayAnimatorOnKey: No InputActionReference assigned. Please assign an action (Action Type = Button).", this);
	}

	private void OnDisable()
	{
		if (_action != null)
		{
			Action<InputAction.CallbackContext> value = OnActionPerformed;
			_action.performed -= value;
			Action<InputAction.CallbackContext> value2 = OnActionCanceled;
			_action.canceled -= value2;
			if (manageActionEnable && _action.enabled)
			{
				_action.Disable();
			}
		}
	}

	private void OnActionPerformed(InputAction.CallbackContext ctx)
	{
		//IL_006f: Expected O, but got I4
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		if (!(animator != null))
		{
			return;
		}
		if (!string.IsNullOrEmpty(variableName))
		{
			bool flag = Behaviour == BehaviourTypes.Trigger;
			if (!flag)
			{
				object obj = Behaviour - 1;
				if (!flag)
				{
					object obj2 = obj - 1;
					if (!flag)
					{
						if ((nint)obj2 == 1)
						{
							animator.Play(variableName, 0, 0f);
						}
					}
					else if (EnsureParameter(animator, variableName, AnimatorControllerParameterType.Bool))
					{
						animator.SetBool(variableName, value: true);
					}
				}
				else if (EnsureParameter(animator, variableName, AnimatorControllerParameterType.Bool))
				{
					bool value = (boolState = !boolState);
					animator.SetBool(variableName, value);
				}
			}
			else if (EnsureParameter(animator, variableName, AnimatorControllerParameterType.Trigger))
			{
				animator.SetTrigger(variableName);
			}
		}
		else
		{
			Debug.LogWarning("PlayAnimatorOnKey: 'variableName' is empty. Nothing to do.", this);
		}
	}

	private void OnActionCanceled(InputAction.CallbackContext ctx)
	{
		if (animator != null && Behaviour == BehaviourTypes.BoolHold && !string.IsNullOrEmpty(variableName) && EnsureParameter(animator, variableName, AnimatorControllerParameterType.Bool))
		{
			animator.SetBool(variableName, value: false);
		}
	}

	private bool EnsureParameter(Animator anim, string name, AnimatorControllerParameterType expectedType)
	{
		//IL_003f: Expected I, but got O
		//IL_0391: Expected I4, but got O
		//IL_0055: Expected I, but got O
		//IL_009c: Expected O, but got I
		//IL_0085: Expected I, but got O
		//IL_00c1: Expected I, but got O
		//IL_00d1: Expected O, but got I
		//IL_013d: Expected I4, but got O
		//IL_016b: Expected I, but got O
		//IL_017b: Expected O, but got I
		//IL_024b: Expected I, but got O
		//IL_025b: Expected O, but got I
		if (HasAnimatorParameter(anim, name, expectedType))
		{
			return true;
		}
		object[] array = new object[4];
		bool flag = "PlayAnimatorOnKey" == null;
		nint num = unchecked((nint)"PlayAnimatorOnKey");
		if (!flag)
		{
			nint num2 = (nint)array;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
			object obj = default(object);
			if (obj == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
				string text = default(string);
				throw text;
			}
			num = unchecked((nint)"PlayAnimatorOnKey");
		}
		if (array.Length > 0)
		{
			array[0] = num;
			if (name != null)
			{
				nint num3 = (nint)array;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v283 @ rdx_v32 (Il2CppClass<System.Object[]>)+40]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				object obj3 = default(object);
				if (obj3 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					object obj4 = default(object);
					throw obj4;
				}
			}
			if (array.Length > 1)
			{
				array[1] = name;
				object obj6 = default(object);
				object obj5 = (AnimatorControllerParameterType)obj6;
				if (obj5 != null)
				{
					nint num4 = (nint)array;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v343 @ rdx_v30 (Il2CppClass<System.Object[]>)+40]");
					object obj7 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					object obj8 = default(object);
					bool flag2 = obj8 == null;
					object obj9 = obj5;
					if (flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
						string text2 = default(string);
						throw text2;
					}
				}
				if (array.Length > 2)
				{
					array[2] = obj5;
					string text4;
					if ((object)animator != null)
					{
						GameObject gameObject = animator.gameObject;
						string text3 = gameObject.name;
						bool flag3 = text3 == null;
						text4 = text3;
						if (!flag3)
						{
							nint num5 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v515 @ rdx_v28 (Il2CppClass<System.Object[]>)+40]");
							object obj10 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj11 = default(object);
							bool flag4 = obj11 == null;
							string text5 = text3;
							if (flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj12 = default(object);
								throw obj12;
							}
							text4 = text3;
						}
					}
					else
					{
						text4 = null;
					}
					array[3] = text4;
					string message = string.Format("{0}: Animator parameter '{1}' of type {2} was not found on '{3}'.", array);
					Debug.LogWarning(message, this);
					return false;
				}
			}
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
	}

	private static bool HasAnimatorParameter(Animator anim, string name, AnimatorControllerParameterType type)
	{
		//IL_0176: Expected I4, but got O
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Expected O, but got Unknown
		//IL_009b: Expected O, but got I4
		//IL_00a4: Expected O, but got I4
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		if (!(anim != null) || string.IsNullOrEmpty(name))
		{
			goto IL_0155;
		}
		if ((object)anim != null)
		{
			AnimatorControllerParameter[] parameters = anim.parameters;
			if (parameters != null)
			{
				object obj = parameters + 32;
				object obj2 = 0;
				object obj3 = 0;
				object obj4 = default(object);
				string text = default(string);
				while ((nint)obj2 < parameters.Length)
				{
					if (obj != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808CF520");
						if ((nint)obj4 == (nint)type)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808746E0");
							if (!(text != name))
							{
								return true;
							}
						}
						obj3++;
						obj += 8;
						obj2 = obj3;
						continue;
					}
					goto IL_0168;
				}
				goto IL_0155;
			}
		}
		goto IL_0168;
		IL_0168:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0155:
		return false;
	}

	public PlayAnimatorOnKey()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A012]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		variableName = "Shoot";
		manageActionEnable = true;
		base._002Ector();
	}
}
