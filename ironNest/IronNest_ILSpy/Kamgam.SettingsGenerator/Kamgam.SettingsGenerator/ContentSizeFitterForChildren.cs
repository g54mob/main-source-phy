using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class ContentSizeFitterForChildren : MonoBehaviour
{
	public bool FitWidth = true;

	public bool FitHeight;

	public float AdditionalWidth;

	public float AdditionalHeight;

	public bool AutoRefresh = true;

	public bool AlwaysRefresh;

	public RectTransform[] IgnoreList;

	public int ForceUpdateFirstNFrames;

	protected int framesInUpdate;

	protected RectTransform rectTransform;

	protected bool isDirty;

	protected int lastChildCount;

	protected Vector3[] corners = new Vector3[4];

	public RectTransform RectTransform
	{
		get
		{
			if (this.rectTransform == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				RectTransform rectTransform = default(RectTransform);
				this.rectTransform = rectTransform;
			}
			return this.rectTransform;
		}
	}

	private void Awake()
	{
		//IL_001a: Expected I4, but got I8
		isDirty = true;
		lastChildCount = -1;
	}

	private void OnEnable()
	{
		isDirty = false;
		updateSize();
	}

	public void Update()
	{
		if (AlwaysRefresh)
		{
			goto IL_00d7;
		}
		if (ForceUpdateFirstNFrames > 0)
		{
			int num = framesInUpdate + 1;
			framesInUpdate = num;
			if (framesInUpdate < ForceUpdateFirstNFrames)
			{
				goto IL_00d7;
			}
		}
		if (AutoRefresh)
		{
			Transform transform = base.transform;
			int childCount = transform.childCount;
			if (lastChildCount != childCount)
			{
				goto IL_00d7;
			}
		}
		goto IL_00fe;
		IL_00fe:
		if (isDirty)
		{
			isDirty = false;
			updateSize();
		}
		return;
		IL_00d7:
		isDirty = true;
		goto IL_00fe;
	}

	public void Refresh()
	{
		updateSize();
	}

	protected void updateSize()
	{
		//IL_017b: Expected O, but got I
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Expected O, but got Unknown
		if (FitWidth || FitHeight)
		{
			Transform transform = base.transform;
			int childCount = transform.childCount;
			lastChildCount = childCount;
			RectTransform rectTransform = RectTransform;
			Transform parent = rectTransform.parent;
			RectTransform child = RectTransform;
			Bounds bounds = calculateShallowBounds(parent, child);
			if (FitWidth)
			{
				RectTransform rectTransform2 = RectTransform;
				object obj2 = default(object);
				object obj = obj2 + obj2;
				RectTransform rectTransform3 = RectTransform;
				object obj3 = obj / rectTransform3.localScale.x;
				float size = (float)obj3 + AdditionalWidth;
				rectTransform2.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size);
			}
			if (FitHeight)
			{
				RectTransform rectTransform4 = RectTransform;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rax_v9 (UnityEngine.Bounds)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rax_v9 (UnityEngine.Bounds)+10]");
				object obj4 = num + 0;
				RectTransform rectTransform5 = RectTransform;
				object obj5 = obj4 / rectTransform5.localScale.y;
				float size2 = (float)obj5 + AdditionalHeight;
				rectTransform4.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size2);
			}
			RectTransform rectTransform6 = RectTransform;
			rectTransform6.ForceUpdateRectTransforms();
		}
	}

	protected unsafe Bounds calculateShallowBounds(Transform root, Transform child)
	{
		//IL_0008: Expected O, but got Ref
		//IL_071a: Expected native int or pointer, but got O
		//IL_00ce: Expected O, but got I4
		//IL_052e: Expected O, but got I
		//IL_0628: Expected I, but got O
		//IL_0737: Expected O, but got I4
		//IL_0740: Expected O, but got I4
		//IL_06fb: Expected O, but got I
		//IL_0703: Expected native int or pointer, but got O
		//IL_01e3: Expected O, but got I
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Expected O, but got Unknown
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Expected O, but got Unknown
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Expected O, but got Unknown
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Expected O, but got Unknown
		//IL_02fb: Expected O, but got I4
		//IL_03d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Expected O, but got Unknown
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Expected O, but got Unknown
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a0: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Bounds bounds;
		float num2;
		Vector3 vector = default(Vector3);
		float num38 = default(float);
		if ((object)child != null)
		{
			int childCount = child.childCount;
			bool flag = childCount <= 0;
			Bounds bounds2 = default(Bounds);
			bounds = bounds2;
			if (flag)
			{
				goto IL_0712;
			}
			if ((object)root != null)
			{
				Matrix4x4 worldToLocalMatrix = root.worldToLocalMatrix;
				float m = worldToLocalMatrix.m03;
				_ = worldToLocalMatrix.m03;
				Transform transform = base.transform;
				if ((object)transform != null)
				{
					float m2 = worldToLocalMatrix.m02;
					int num = 0;
					object obj3 = 0;
					num2 = -3.4028235E+38f;
					float num3 = -3.4028235E+38f;
					float num4 = 3.4028235E+38f;
					float num5 = 3.4028235E+38f;
					float num6 = -3.4028235E+38f;
					float num7 = 3.4028235E+38f;
					int num8 = 0;
					object obj9 = default(object);
					object obj11 = default(object);
					object obj12 = default(object);
					object obj14 = default(object);
					object obj15 = default(object);
					object obj17 = default(object);
					while (true)
					{
						int childCount2 = transform.childCount;
						if (num8 >= childCount2)
						{
							break;
						}
						Transform transform2 = base.transform;
						if ((object)transform2 != null)
						{
							Transform child2 = transform2.GetChild(num);
							bool flag2 = (object)child2 == null;
							Component component = null;
							if (!flag2)
							{
								bool flag3 = (object)child2.GetType() != typeof(RectTransform);
								component = null;
								if (!flag3)
								{
									component = child2;
								}
							}
							object obj4 = 32;
							object obj5 = 0;
							while (true)
							{
								RectTransform[] ignoreList = IgnoreList;
								if (IgnoreList == null)
								{
									break;
								}
								if ((nint)obj5 < ignoreList.Length)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v240 @ rdi_v8+v216 @ rax_v26 (UnityEngine.RectTransform[])]");
									if ((UnityEngine.Object)0 != component)
									{
										obj5++;
										obj4 += 8;
										continue;
									}
								}
								else
								{
									if ((object)component == null)
									{
										break;
									}
									GameObject gameObject = component.gameObject;
									if ((object)gameObject == null)
									{
										break;
									}
									if (gameObject.activeSelf)
									{
										obj3++;
										((RectTransform)component).GetWorldCorners(corners);
										object obj6 = corners + 32;
										bool flag4 = corners == null;
										float num9 = num2;
										float num10 = num3;
										float num11 = num4;
										float num12 = num5;
										float num13 = num6;
										float num14 = num7;
										object obj7 = 0;
										if (flag4)
										{
											break;
										}
										bool flag5;
										do
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v950 @ rdx_v22+8]");
											float num15 = 0f * worldToLocalMatrix.m02;
											float num16 = (float)obj6 * worldToLocalMatrix.m00;
											object obj8 = obj6 * obj9;
											object obj10 = obj6 * obj11;
											float num17 = (float)vector * worldToLocalMatrix.m01;
											float num18 = num17 + num16;
											m2 = (float)vector * (float)obj12;
											object obj13 = (object)vector * obj14;
											float num19 = num18 + num15;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v950 @ rdx_v22+8]");
											m = 0f * (float)obj15;
											float num20 = (float)obj8 + m2;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v950 @ rdx_v22+8]");
											object obj16 = 0 * obj17;
											object obj18 = obj10 + obj13;
											float num21 = num19;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-80]");
											float num22 = num21 + 0f;
											float num23 = num20 + m;
											object obj19 = obj18 + obj16;
											float num24 = num23;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-7C]");
											float num25 = num24 + 0f;
											float num26 = (float)obj19;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-78]");
											float num27 = num26 + 0f;
											if (num14 > num22)
											{
												num14 = num22;
											}
											if (num12 > num25)
											{
												num12 = num25;
											}
											if (num11 > num27)
											{
												num11 = num27;
											}
											if (num22 > num13)
											{
												num13 = num22;
											}
											if (num25 > num10)
											{
												num10 = num25;
											}
											if (num27 > num9)
											{
												num9 = num27;
											}
											obj7++;
											obj6 += 12;
											flag5 = (nint)obj7 < 4;
											num2 = num9;
											num3 = num10;
											num4 = num11;
											num5 = num12;
											num6 = num13;
											num7 = num14;
										}
										while (flag5);
									}
								}
								num++;
								transform = base.transform;
								if ((object)transform == null)
								{
									break;
								}
								goto IL_04ec;
							}
						}
						goto IL_0555;
						IL_04ec:
						num8 = num;
					}
					if ((nint)obj3 <= 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+A0]");
						bounds = (Bounds)0;
						goto IL_0712;
					}
					nint num28 = (nint)typeof(Vector3);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v639 @ rax_v19 (Il2CppClass<UnityEngine.Vector3>)+B8]");
					nint num29 = 0;
					float num30 = (float)Vector3.zeroVector * 0.5f;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v640 @ rcx_v17 (Il2CppStaticFields<UnityEngine.Vector3>)+4]");
					float num31 = 0f * 0.5f;
					float num32 = num7 - num30;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v640 @ rcx_v17 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
					float num33 = 0f * 0.5f;
					float num34 = num5 - num31;
					float num35 = num4 - num33;
					float num37 = default(float);
					if ((num6 > num32 && !(num3 > num34)) || num2 > num35)
					{
						float num36 = num7 + num30;
						num37 = num5 + num31;
						num38 = num4 + num33;
						if (!(num36 > num6))
						{
							goto IL_0848;
						}
					}
					if (!(num37 > num3))
					{
						goto IL_06eb;
					}
					goto IL_0848;
				}
			}
		}
		goto IL_0555;
		IL_085f:
		return bounds;
		IL_0848:
		if (num38 > num2)
		{
			goto IL_06eb;
		}
		goto IL_085f;
		IL_0712:
		((Bounds*)(nint)bounds)->m_Center = vector;
		goto IL_085f;
		IL_06eb:
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+A0]");
		bounds = (Bounds)0;
		((Bounds*)(nint)bounds)->m_Center = vector;
		goto IL_085f;
		IL_0555:
		return (Bounds)new NullReferenceException();
	}

	protected bool isIgnored(RectTransform t)
	{
		//IL_000e: Expected O, but got I4
		//IL_0017: Expected O, but got I4
		//IL_0020: Expected O, but got I4
		//IL_00f2: Expected I4, but got O
		//IL_006f: Expected O, but got I
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Expected O, but got Unknown
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		RectTransform[] ignoreList = IgnoreList;
		object obj = 0;
		object obj2 = 0;
		object obj3 = 32;
		while (true)
		{
			if ((nint)obj2 < ignoreList.Length)
			{
				RectTransform[] ignoreList2 = IgnoreList;
				if ((nint)obj >= ignoreList2.Length)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rdi_v4+v66 @ rax_v10 (UnityEngine.RectTransform[])]");
				if ((UnityEngine.Object)0 != t)
				{
					ignoreList = IgnoreList;
					obj++;
					obj3 += 8;
					obj2 = obj;
					continue;
				}
				return true;
			}
			return false;
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
	}
}
