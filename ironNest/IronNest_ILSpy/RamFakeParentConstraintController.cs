using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

public class RamFakeParentConstraintController : MonoBehaviour
{
	private List<FakeParentConstraint> constraints;

	private List<Collider> chargeColliders;

	private bool activateOnContact;

	private bool deactivateOnExit;

	private bool debugLog;

	private readonly HashSet<Collider> activeOverlaps;

	private bool hasFired;

	public void ActivateAllConstraints()
	{
		SetAllConstraintsActive(active: true);
	}

	public void DeactivateAllConstraints()
	{
		hasFired = false;
		activeOverlaps.Clear();
		SetAllConstraintsActive(active: false);
	}

	public void SetAllConstraintsActive(bool active)
	{
		//IL_00e6: Expected O, but got I4
		//IL_00ef: Expected O, but got I4
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Expected O, but got Unknown
		List<FakeParentConstraint> list = constraints;
		object obj = 0;
		object obj2 = 0;
		UnityEngine.Object obj3 = default(UnityEngine.Object);
		while ((nint)obj2 < list._size)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			if (!(obj3 != null))
			{
			}
			list = constraints;
			obj++;
			obj2 = obj;
		}
		if (debugLog)
		{
			string arg = base.name;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg2 = default(object);
			string message = string.Format("{0} '{1}': SetAllConstraintsActive({2})", "RamFakeParentConstraintController", arg, arg2);
			Debug.Log(message, this);
		}
	}

	public bool NotifyRamTriggerEnter(Collider other, Collider ramTrigger)
	{
		//IL_0334: Expected I4, but got O
		if (activateOnContact)
		{
			if (IsChargeMatch(other))
			{
				activeOverlaps.Add(other);
				if (debugLog)
				{
					string[] array = new string[6];
					if (array.Length > 0)
					{
						array[0] = "RamFakeParentConstraintController '";
						string text = base.name;
						if (array.Length > 1)
						{
							array[1] = text;
							if (array.Length > 2)
							{
								array[2] = "': Enter CHARGE match via trigger '";
								bool flag = (object)ramTrigger == null;
								string text2 = null;
								if (!flag)
								{
									string text3 = ramTrigger.name;
									text2 = text3;
								}
								if (array.Length > 3)
								{
									array[3] = text2;
									if (array.Length > 4)
									{
										array[4] = "': ";
										string text4 = other.name;
										if (array.Length > 5)
										{
											array[5] = text4;
											string message = string.Concat(array);
											Debug.Log(message, other);
											goto IL_020d;
										}
									}
								}
							}
						}
					}
					IndexOutOfRangeException ex = new IndexOutOfRangeException();
					return (byte)(int)ex != 0;
				}
				goto IL_020d;
			}
			if (debugLog)
			{
				string text5 = base.name;
				string text6 = other.name;
				string message2 = "RamFakeParentConstraintController '" + text5 + "': Enter ignored (not a charge match): " + text6;
				Debug.Log(message2, other);
			}
		}
		goto IL_02f9;
		IL_02f9:
		return false;
		IL_020d:
		if (!hasFired)
		{
			HashSet<Collider> hashSet = activeOverlaps;
			if (hashSet._count == 1)
			{
				hasFired = true;
				SetAllConstraintsActive(active: true);
				return true;
			}
		}
		goto IL_02f9;
	}

	public bool NotifyRamTriggerExit(Collider other, Collider ramTrigger)
	{
		//IL_0268: Expected I4, but got O
		if (IsChargeMatch(other))
		{
			if (activeOverlaps != null)
			{
				bool flag = activeOverlaps.Remove(other);
				if (!debugLog)
				{
					goto IL_019a;
				}
				string[] array = new string[6];
				if (array != null)
				{
					array[0] = "RamFakeParentConstraintController '";
					string text = base.name;
					array[1] = text;
					array[2] = "': Exit CHARGE match via trigger '";
					string text2 = ramTrigger?.name;
					array[3] = text2;
					array[4] = "': ";
					if ((object)other != null)
					{
						string text3 = other.name;
						array[5] = text3;
						string message = string.Concat(array);
						Debug.Log(message, other);
						goto IL_019a;
					}
				}
			}
			goto IL_025a;
		}
		return false;
		IL_019a:
		if (deactivateOnExit)
		{
			HashSet<Collider> hashSet = activeOverlaps;
			if (activeOverlaps == null)
			{
				goto IL_025a;
			}
			if (hashSet._count == 0)
			{
				hasFired = false;
				if (activeOverlaps == null)
				{
					goto IL_025a;
				}
				activeOverlaps.Clear();
				SetAllConstraintsActive(active: false);
			}
		}
		return true;
		IL_025a:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private bool IsChargeMatch(Collider other)
	{
		//IL_0243: Expected I4, but got O
		//IL_00fa: Expected O, but got I4
		//IL_0103: Expected O, but got I4
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Expected O, but got Unknown
		if (other != null && chargeColliders != null)
		{
			List<Collider> list = chargeColliders;
			if (list._size != 0)
			{
				if (list.Contains(other))
				{
					goto IL_0219;
				}
				if ((object)other == null)
				{
					goto IL_0235;
				}
				Rigidbody attachedRigidbody = other.attachedRigidbody;
				if (attachedRigidbody != null)
				{
					List<Collider> list2 = chargeColliders;
					bool flag = chargeColliders == null;
					object obj = 0;
					object obj2 = 0;
					if (flag)
					{
						goto IL_0235;
					}
					UnityEngine.Object obj3 = default(UnityEngine.Object);
					while ((nint)obj < list2._size)
					{
						if (chargeColliders != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							if (obj3 != null)
							{
								if ((object)obj3 == null)
								{
									goto IL_0235;
								}
								Rigidbody attachedRigidbody2 = ((Collider)obj3).attachedRigidbody;
								if (attachedRigidbody2 != null && !(attachedRigidbody2 != attachedRigidbody))
								{
									goto IL_0219;
								}
							}
							list2 = chargeColliders;
							obj2++;
							if (chargeColliders != null)
							{
								obj = obj2;
								continue;
							}
						}
						goto IL_0235;
					}
				}
			}
		}
		return false;
		IL_0235:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0219:
		return true;
	}

	public RamFakeParentConstraintController()
	{
		List<FakeParentConstraint> list = new List<FakeParentConstraint>();
		constraints = list;
		chargeColliders = new List<Collider>();
		activateOnContact = true;
		activeOverlaps = new HashSet<Collider>();
		base._002Ector();
	}
}
