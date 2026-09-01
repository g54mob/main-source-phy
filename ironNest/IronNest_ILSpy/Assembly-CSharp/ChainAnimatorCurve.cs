using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cpp2ILInjected;
using UnityEngine;

public class ChainAnimatorCurve : MonoBehaviour
{
	public enum ChainMode
	{
		LoopRunning,
		OpenExtendRetract
	}

	public ChainMode mode;

	public Mesh meshA;

	public Mesh meshB;

	public Material chainLinkMaterial;

	public float linkSpacing;

	public int meshBInterval;

	public int visibleLinkCount;

	public Vector3 linkRotationOffset;

	public float chainMovement;

	public int maxLinks;

	public bool updateOnTransformChange;

	public bool includeParentTransformChanges;

	public AnimationCurve chainCurve;

	public float key0Time;

	public float key0Value;

	public float key1Time;

	public float key1Value;

	public float key2Time;

	public float key2Value;

	public float key3Time;

	public float key3Value;

	public float key4Time;

	public float key4Value;

	public float key5Time;

	public float key5Value;

	public float tangentSampleStep;

	private readonly float[] cachedInTangents;

	private readonly float[] cachedOutTangents;

	private readonly float[] cachedInWeights;

	private readonly float[] cachedOutWeights;

	private readonly WeightedMode[] cachedWeightedModes;

	private int lastCurveHash;

	private readonly List<Matrix4x4> matricesA;

	private readonly List<Matrix4x4> matricesB;

	private int linkCount;

	private float lastChainMovement;

	private float lastLinkSpacing;

	private Vector3 lastRotationOffset;

	private int lastMeshBInterval;

	private ChainMode lastMode;

	private int lastVisibleLinkCount;

	private float lastTangentSampleStep;

	private Transform _transform;

	private readonly float[] lastKeyTimes;

	private readonly float[] lastKeyValues;

	private const int curveKeyCount = 6;

	private Transform Transform
	{
		get
		{
			Transform result = _transform;
			if ((object)_transform == null)
			{
				result = (_transform = base.transform);
			}
			return result;
		}
	}

	private void Awake()
	{
		CacheCurveTangents();
		SyncCurveWithKeys(force: true);
		bool flag = !updateOnTransformChange;
		lastChainMovement = 0f / 0f;
		if (!flag)
		{
			MarkTransformChainAsChanged();
		}
	}

	private void OnValidate()
	{
		int num = CurveHash();
		if (num != lastCurveHash)
		{
			CacheCurveTangents();
			int num2 = CurveHash();
			lastCurveHash = num2;
		}
		bool flag = maxLinks < 0;
		int num3 = 0;
		if (!flag)
		{
			num3 = maxLinks;
		}
		int num4 = meshBInterval;
		maxLinks = num3;
		if (meshBInterval < 1)
		{
			num4 = 1;
		}
		meshBInterval = num4;
		int num5 = visibleLinkCount;
		if (visibleLinkCount >= 0)
		{
			if (num5 > num3)
			{
				num5 = num3;
			}
		}
		else
		{
			num5 = 0;
		}
		visibleLinkCount = num5;
		bool flag2 = !(0.0001f < tangentSampleStep);
		float num6 = 0.0001f;
		if (!flag2)
		{
			num6 = tangentSampleStep;
		}
		tangentSampleStep = num6;
		SyncCurveWithKeys(force: false);
		bool flag3 = !updateOnTransformChange;
		lastChainMovement = 0f / 0f;
		if (!flag3)
		{
			MarkTransformChainAsChanged();
		}
	}

	private unsafe void Update()
	{
		//IL_00cc: Expected F4, but got I4
		//IL_0149: Invalid comparison between I4 and F4
		//IL_006d: Expected O, but got Ref
		//IL_016b: Invalid comparison between I4 and F4
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected F4, but got Unknown
		SyncCurveWithKeys(force: false);
		float num;
		if (chainCurve != null)
		{
			int length = chainCurve.length;
			if (length >= 2)
			{
				int length2 = chainCurve.length;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF2F0");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF690");
				object obj = default(object);
				chainCurve.GetKeys((Span<Keyframe>)(&obj));
				int length3 = chainCurve.length;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				object obj3 = default(object);
				object obj4 = default(object);
				object obj2 = obj3 - obj4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				num = obj2 & 0;
				goto IL_00d1;
			}
		}
		num = 0f;
		goto IL_00d1;
		IL_00d1:
		if ((meshA == null && !(meshB != null)) || !(chainLinkMaterial != null) || !(0f < num) || !(0f < linkSpacing))
		{
			return;
		}
		if (mode == ChainMode.OpenExtendRetract)
		{
			int num2 = visibleLinkCount;
			if (visibleLinkCount >= 0)
			{
				if (num2 > maxLinks)
				{
					num2 = maxLinks;
				}
			}
			else
			{
				num2 = 0;
			}
			visibleLinkCount = num2;
		}
		if (NeedsUpdate())
		{
			UpdateMatrices(num);
			lastChainMovement = chainMovement;
			lastLinkSpacing = linkSpacing;
			lastRotationOffset = linkRotationOffset;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ChainAnimatorCurve)+54]");
			_ = 0;
			lastMeshBInterval = meshBInterval;
			lastMode = mode;
			lastVisibleLinkCount = visibleLinkCount;
			lastTangentSampleStep = tangentSampleStep;
			CacheKeyState();
			if (updateOnTransformChange)
			{
				Transform transform = _transform;
				if ((object)_transform == null)
				{
					transform = (_transform = base.transform);
				}
				transform.hasChanged = false;
				if (includeParentTransformChanges)
				{
					Transform transform2 = _transform;
					if ((object)_transform == null)
					{
						transform2 = (_transform = base.transform);
					}
					while (true)
					{
						Transform parent = transform2.parent;
						if (parent != null)
						{
							parent.hasChanged = false;
							transform2 = parent;
							continue;
						}
						break;
					}
				}
			}
		}
		List<Matrix4x4> list = matricesA;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v649 @ rax_v18 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+18]");
		if ((nint)0 > (nint)0 && meshA != null && chainLinkMaterial != null)
		{
			Graphics.DrawMeshInstanced(meshA, 0, chainLinkMaterial, matricesA);
		}
		List<Matrix4x4> list2 = matricesB;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v688 @ rax_v20 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+18]");
		if ((nint)0 > (nint)0 && meshB != null && chainLinkMaterial != null)
		{
			Graphics.DrawMeshInstanced(meshB, 0, chainLinkMaterial, matricesB);
		}
	}

	private void CacheCurveTangents()
	{
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Expected O, but got Unknown
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Expected O, but got Unknown
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Expected O, but got Unknown
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Expected O, but got Unknown
		//IL_0053: Expected O, but got I4
		//IL_0331: Expected O, but got I4
		//IL_033a: Expected O, but got I4
		//IL_0344: Expected O, but got I4
		//IL_034e: Expected O, but got I4
		//IL_0071: Expected O, but got I
		//IL_00e0: Expected O, but got I
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Expected O, but got Unknown
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Expected O, but got Unknown
		//IL_0147: Expected O, but got I
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Expected O, but got Unknown
		//IL_01aa: Expected O, but got I
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Expected O, but got Unknown
		//IL_020d: Expected O, but got I
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Expected O, but got Unknown
		//IL_0270: Expected O, but got I
		//IL_02be: Expected O, but got I4
		//IL_02c7: Expected O, but got I4
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Expected O, but got Unknown
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Expected O, but got Unknown
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Expected O, but got Unknown
		_ = 0;
		object obj2 = default(object);
		object obj = obj2 - 72;
		_ = 0;
		_ = 0;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF2F0");
		object obj3 = obj2 - 72;
		object obj4 = obj2 - 56;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF690");
		Span<Keyframe> keys = (Span<Keyframe>)(obj2 - 56);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-38]");
		_ = 0;
		chainCurve.GetKeys(keys);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-40]");
		bool flag = (nint)0 >= (nint)6;
		object obj5 = 6;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-40]");
			obj5 = 0;
		}
		object obj6 = 0;
		object obj7 = 0;
		object obj8 = 32;
		object obj9 = 0;
		do
		{
			float[] array = cachedInTangents;
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) >= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5))
			{
				float[] array2 = cachedOutTangents;
				float[] array3 = cachedInWeights;
				float[] array4 = cachedOutWeights;
				WeightedMode[] array5 = cachedWeightedModes;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-48]");
				object obj10 = 0;
				object obj11 = obj2 - 40;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+v441 @ rax_v13]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+10+v441 @ rax_v13]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+18+v441 @ rax_v13]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B70");
				object obj12 = obj2 - 40;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-48]");
				object obj13 = 0;
				float[] array6 = cachedOutTangents;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+v476 @ rax_v15]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+10+v476 @ rax_v15]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+18+v476 @ rax_v15]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D4F790");
				object obj14 = obj2 - 40;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-48]");
				object obj15 = 0;
				float[] array7 = cachedInWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+v478 @ rax_v17]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+10+v478 @ rax_v17]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+18+v478 @ rax_v17]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D250A0");
				object obj16 = obj2 - 40;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-48]");
				object obj17 = 0;
				float[] array8 = cachedOutWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+v480 @ rax_v19]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+10+v480 @ rax_v19]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+18+v480 @ rax_v19]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D25090");
				object obj18 = obj2 - 40;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v9 @ rsp-48]");
				object obj19 = 0;
				WeightedMode[] array9 = cachedWeightedModes;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+v495 @ rax_v21]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+10+v495 @ rax_v21]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ r14_v4+18+v495 @ rax_v21]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B90");
				keys = (Span<Keyframe>)0;
				obj9 = 0;
			}
			obj6++;
			obj8 += 4;
			obj7 += 28;
		}
		while ((nint)obj8 < 56);
	}

	private unsafe void SyncCurveWithKeys(bool force)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0037: Expected O, but got I4
		//IL_0040: Expected O, but got I4
		//IL_03ca: Expected O, but got Ref
		//IL_03e1: Expected O, but got I8
		//IL_0741: Expected O, but got I4
		//IL_075b: Expected O, but got I8
		//IL_0057: Expected O, but got I4
		//IL_0060: Expected O, but got I4
		//IL_007c: Expected O, but got Ref
		//IL_0094: Expected O, but got Ref
		//IL_00a2: Expected O, but got Ref
		//IL_00bf: Expected O, but got Ref
		//IL_00eb: Expected O, but got I
		//IL_00f9: Expected O, but got Ref
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_0154: Invalid comparison between O and F4
		//IL_0180: Expected O, but got I
		//IL_018e: Expected O, but got Ref
		//IL_042c: Expected O, but got I
		//IL_043a: Expected O, but got Ref
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Expected O, but got Unknown
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Expected O, but got Unknown
		//IL_0498: Invalid comparison between O and F4
		//IL_01d1: Expected O, but got I
		//IL_01df: Expected O, but got Ref
		//IL_04bd: Expected O, but got Ref
		//IL_04cd: Expected O, but got I
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Expected O, but got Unknown
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Expected O, but got Unknown
		//IL_052b: Invalid comparison between O and F4
		//IL_022a: Expected O, but got I
		//IL_0238: Expected O, but got Ref
		//IL_0552: Expected O, but got I
		//IL_0560: Expected O, but got Ref
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Expected O, but got Unknown
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b5: Expected O, but got Unknown
		//IL_05be: Invalid comparison between O and F4
		//IL_0283: Expected O, but got I
		//IL_0291: Expected O, but got Ref
		//IL_05e3: Expected O, but got Ref
		//IL_05f3: Expected O, but got I
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Expected O, but got Unknown
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Expected O, but got Unknown
		//IL_0651: Invalid comparison between O and F4
		//IL_02dc: Expected O, but got I
		//IL_02ea: Expected O, but got Ref
		//IL_0678: Expected O, but got I
		//IL_0686: Expected O, but got Ref
		//IL_06a3: Expected O, but got I
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Expected O, but got Unknown
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Expected O, but got Unknown
		//IL_0704: Invalid comparison between F4 and I4
		//IL_0713: Invalid comparison between O and F4
		//IL_0335: Expected O, but got I
		//IL_0343: Expected O, but got Ref
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Expected O, but got Unknown
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Expected O, but got Unknown
		//IL_03b1: Invalid comparison between F4 and I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		int length = chainCurve.length;
		bool flag = length != 6;
		object obj3 = 0;
		object obj4 = 0;
		if (!flag)
		{
			obj3 = 0;
			obj4 = 0;
			if (!force)
			{
				object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF2F0");
				object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 73));
				object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF690");
				Span<Keyframe> keys = (Span<Keyframe>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
				_ = 0;
				chainCurve.GetKeys(keys);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
				object obj8 = 0;
				object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ rax_v15+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v228 @ rax_v15+18]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				object obj11 = default(object);
				object obj10 = obj11 - key0Time;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj12 = obj10 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj12) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
					object obj13 = 0;
					object obj14 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v253 @ rax_v55+10]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v253 @ rax_v55+18]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
				object obj15 = 0;
				object obj16 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rax_v18+1C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rax_v18+2C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rax_v18+34]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				object obj18 = default(object);
				object obj17 = obj18 - key1Time;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj19 = obj17 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj19) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
					object obj20 = 0;
					object obj21 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rax_v49+1C]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rax_v49+2C]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v316 @ rax_v49+34]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
				}
				object obj22 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
				object obj23 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rax_v21+38]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rax_v21+48]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ rax_v21+50]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				object obj25 = default(object);
				object obj24 = obj25 - key2Time;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj26 = obj24 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj26) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
					object obj27 = 0;
					object obj28 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rax_v47+38]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rax_v47+48]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v379 @ rax_v47+50]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
				object obj29 = 0;
				object obj30 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ rax_v24+54]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ rax_v24+64]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ rax_v24+6C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				object obj32 = default(object);
				object obj31 = obj32 - key3Time;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj33 = obj31 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj33) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
					object obj34 = 0;
					object obj35 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v442 @ rax_v41+54]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v442 @ rax_v41+64]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v442 @ rax_v41+6C]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
				}
				object obj36 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
				object obj37 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v480 @ rax_v27+70]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v480 @ rax_v27+80]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v480 @ rax_v27+88]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				object obj39 = default(object);
				object obj38 = obj39 - key4Time;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				object obj40 = obj38 & 0;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj40) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
					object obj41 = 0;
					object obj42 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v506 @ rax_v39+70]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v506 @ rax_v39+80]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v506 @ rax_v39+88]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
				object obj43 = 0;
				object obj44 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v542 @ rax_v30+8C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v542 @ rax_v30+9C]");
				obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v542 @ rax_v30+9C]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v542 @ rax_v30+A4]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				object obj46 = default(object);
				object obj45 = obj46 - key5Time;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
				obj4 = obj45 & 0;
				float num = (float)obj4 - 0.0001f;
				bool flag2 = num == 0f;
				if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)0.0001f))
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
					object obj47 = 0;
					object obj48 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 25));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v568 @ rax_v33+8C]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v568 @ rax_v33+9C]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v568 @ rax_v33+A4]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
					object obj50 = default(object);
					object obj49 = obj50 - key5Value;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
					obj4 = obj49 & 0;
					float num2 = (float)obj4 - 0.0001f;
					flag2 = num2 == 0f;
				}
				if (flag2)
				{
					return;
				}
			}
		}
		object obj51 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 57));
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF2F0");
		object obj52 = 6442450944L;
		object obj53 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v184 @ r15_v1+3E6294+v187 @ rax_v10*4]");
		object obj54 = 0 + 6442450944L;
		Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v189 @ rcx_v7 (should have been resolved before IL gen)");
		/*Error: End of method reached without returning.*/;
	}

	private int CurveHash()
	{
		//IL_0027: Expected O, but got I4
		//IL_03c7: Expected I4, but got O
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_00a0: Expected O, but got I4
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Expected O, but got Unknown
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Expected O, but got Unknown
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Expected O, but got Unknown
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Expected O, but got Unknown
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Expected O, but got Unknown
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Expected O, but got Unknown
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Expected O, but got Unknown
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Expected O, but got Unknown
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Expected O, but got Unknown
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Expected O, but got Unknown
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Expected O, but got Unknown
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Expected O, but got Unknown
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Expected O, but got Unknown
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Expected O, but got Unknown
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Expected O, but got Unknown
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Expected O, but got Unknown
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Expected O, but got Unknown
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Expected O, but got Unknown
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Expected O, but got Unknown
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Expected O, but got Unknown
		//IL_03a1: Expected I4, but got O
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Expected O, but got Unknown
		Keyframe[] keys = chainCurve.keys;
		int num = keys.Length;
		object obj = 0;
		float num2 = default(float);
		object obj36 = default(object);
		while (true)
		{
			if ((nint)obj < keys.Length)
			{
				if ((nint)obj >= keys.Length)
				{
					break;
				}
				object obj2 = obj * 28;
				object obj3 = obj2 + 32;
				object obj4 = obj3 + (object)keys;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
				int hashCode = num2.GetHashCode();
				object obj5 = num * 31;
				object obj6 = obj5 + hashCode;
				if ((nint)obj >= keys.Length)
				{
					break;
				}
				object obj7 = obj * 28;
				object obj8 = obj7 + 32;
				object obj9 = obj8 + (object)keys;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D38490");
				int hashCode2 = num2.GetHashCode();
				object obj10 = obj6 * 31;
				object obj11 = obj10 + hashCode2;
				if ((nint)obj >= keys.Length)
				{
					break;
				}
				object obj12 = obj * 28;
				object obj13 = obj12 + 32;
				object obj14 = obj13 + (object)keys;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B70");
				int hashCode3 = num2.GetHashCode();
				object obj15 = obj11 * 31;
				object obj16 = obj15 + hashCode3;
				if ((nint)obj >= keys.Length)
				{
					break;
				}
				object obj17 = obj * 28;
				object obj18 = obj17 + 32;
				object obj19 = obj18 + (object)keys;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D4F790");
				int hashCode4 = num2.GetHashCode();
				object obj20 = obj16 * 31;
				object obj21 = obj20 + hashCode4;
				if ((nint)obj >= keys.Length)
				{
					break;
				}
				object obj22 = obj * 28;
				object obj23 = obj22 + 32;
				object obj24 = obj23 + (object)keys;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D250A0");
				int hashCode5 = num2.GetHashCode();
				object obj25 = obj21 * 31;
				object obj26 = obj25 + hashCode5;
				if ((nint)obj >= keys.Length)
				{
					break;
				}
				object obj27 = obj * 28;
				object obj28 = obj27 + 32;
				object obj29 = obj28 + (object)keys;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D25090");
				int hashCode6 = num2.GetHashCode();
				object obj30 = obj26 * 31;
				object obj31 = obj30 + hashCode6;
				if ((nint)obj >= keys.Length)
				{
					break;
				}
				object obj32 = obj * 28;
				object obj33 = obj32 + 32;
				object obj34 = obj33 + (object)keys;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B90");
				object obj35 = obj31 * 31;
				num = (int)(obj36 + obj35);
				obj++;
				continue;
			}
			return num;
		}
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (int)ex;
	}

	private void CacheKeyState()
	{
		float[] array = lastKeyTimes;
		array[0] = key0Time;
		float[] array2 = lastKeyValues;
		array2[0] = key0Value;
		float[] array3 = lastKeyTimes;
		array3[1] = key1Time;
		float[] array4 = lastKeyValues;
		array4[1] = key1Value;
		float[] array5 = lastKeyTimes;
		array5[2] = key2Time;
		float[] array6 = lastKeyValues;
		array6[2] = key2Value;
		float[] array7 = lastKeyTimes;
		array7[3] = key3Time;
		float[] array8 = lastKeyValues;
		array8[3] = key3Value;
		float[] array9 = lastKeyTimes;
		array9[4] = key4Time;
		float[] array10 = lastKeyValues;
		array10[4] = key4Value;
		float[] array11 = lastKeyTimes;
		array11[5] = key5Time;
		float[] array12 = lastKeyValues;
		array12[5] = key5Value;
	}

	private unsafe float GetCurveLength()
	{
		//IL_0136: Expected F4, but got I4
		//IL_004b: Expected F4, but got I4
		//IL_009b: Expected F4, but got I4
		//IL_00b8: Expected O, but got Ref
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Expected F4, but got Unknown
		if (chainCurve != null)
		{
			int length = chainCurve.length;
			if (length >= 2)
			{
				bool flag = chainCurve == null;
				float num = 0f;
				if (!flag)
				{
					int length2 = chainCurve.length;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF2F0");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF690");
					bool flag2 = chainCurve == null;
					num = 0f;
					if (!flag2)
					{
						float num2 = default(float);
						chainCurve.GetKeys((Span<Keyframe>)(&num2));
						bool flag3 = chainCurve == null;
						num = num2;
						if (!flag3)
						{
							int length3 = chainCurve.length;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
							object obj2 = default(object);
							object obj3 = default(object);
							object obj = obj2 - obj3;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
							return obj & 0;
						}
					}
				}
				throw new NullReferenceException();
			}
		}
		return 0f;
	}

	private void MarkDirty()
	{
		lastChainMovement = 0f / 0f;
	}

	private bool NeedsUpdate()
	{
		//IL_07ce: Expected I4, but got O
		//IL_01b5: Expected O, but got I4
		//IL_0814: Expected O, but got I4
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Expected O, but got Unknown
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Expected O, but got Unknown
		//IL_084d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0852: Expected O, but got Unknown
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_085f: Expected O, but got Unknown
		//IL_0963: Expected O, but got I4
		//IL_0694: Expected O, but got I
		//IL_06eb: Invalid comparison between F4 and O
		//IL_070a: Invalid comparison between F4 and I4
		//IL_0733: Expected O, but got I4
		float[] array = lastKeyTimes;
		if (array.Length <= 0)
		{
			goto IL_07c0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E57E5h\"");
		bool flag2;
		bool flag3;
		if (array[0] == key0Time)
		{
			float[] array2 = lastKeyValues;
			if (array2.Length <= 0)
			{
				goto IL_07c0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E57EAh\"");
			bool flag = array2[0] != key0Value;
			flag2 = true;
			if (!flag)
			{
				flag3 = false;
				flag2 = true;
				goto IL_07db;
			}
		}
		else
		{
			flag2 = true;
		}
		flag3 = flag2;
		goto IL_07db;
		IL_07c0:
		IndexOutOfRangeException ex = new IndexOutOfRangeException();
		return (byte)(int)ex != 0;
		IL_089a:
		object obj;
		bool flag4;
		if (obj != null)
		{
			flag4 = flag2;
		}
		bool flag5;
		object obj2 = flag4 | flag5;
		if (updateOnTransformChange)
		{
			Transform transform = _transform;
			if ((object)_transform == null)
			{
				transform = (_transform = base.transform);
			}
			if (transform.hasChanged)
			{
				goto IL_07a6;
			}
			if (includeParentTransformChanges)
			{
				Transform transform2 = _transform;
				if ((object)_transform == null)
				{
					transform2 = (_transform = base.transform);
				}
				while (true)
				{
					Transform parent = transform2.parent;
					if (!(parent != null))
					{
						break;
					}
					if (!parent.hasChanged)
					{
						transform2 = parent;
						continue;
					}
					goto IL_07a6;
				}
			}
		}
		bool flag11;
		if (lastMode == mode && lastVisibleLinkCount == visibleLinkCount)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp near ptr 00000001803E5B51h\"");
			if (lastChainMovement == chainMovement)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E5B51h\"");
				if (lastLinkSpacing == linkSpacing)
				{
					object obj3 = lastRotationOffset - linkRotationOffset;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ChainAnimatorCurve)+FC]");
					nint num = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (ChainAnimatorCurve)+54]");
					object obj4 = num - 0;
					object obj6 = default(object);
					object obj5 = obj6 - obj6;
					object obj7 = obj5 * obj5;
					object obj8 = obj3 * obj3;
					object obj9 = obj4 * obj4;
					object obj10 = obj7 + obj8;
					object obj11 = obj10 + obj9;
					bool flag6 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj11);
					float num2 = 9.9999994E-11f - (float)obj11;
					bool flag7 = num2 == 0f;
					bool flag8 = !flag6;
					bool flag9 = !flag7;
					object obj12 = flag9 & flag8;
					if (obj12 != null && lastMeshBInterval == meshBInterval)
					{
						bool flag10 = lastTangentSampleStep == tangentSampleStep;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E5B51h\"");
						flag11 = false;
						if (flag10)
						{
							goto IL_08e3;
						}
					}
				}
			}
		}
		goto IL_07a6;
		IL_08e3:
		bool flag12 = obj2 == null;
		bool result = flag11;
		if (!flag12)
		{
			result = flag2;
		}
		return result;
		IL_07a6:
		flag11 = flag2;
		goto IL_08e3;
		IL_086e:
		float[] array3 = lastKeyTimes;
		if (array3.Length <= 5)
		{
			goto IL_07c0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E5986h\"");
		if (array3[5] == key5Time)
		{
			float[] array4 = lastKeyValues;
			if (array4.Length <= 5)
			{
				goto IL_07c0;
			}
			bool flag13 = array4[5] == key5Value;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E5986h\"");
			flag5 = false;
			if (flag13)
			{
				goto IL_089a;
			}
		}
		flag5 = flag2;
		goto IL_089a;
		IL_0819:
		float[] array5 = lastKeyTimes;
		object obj14;
		bool flag16;
		if (array5.Length > 3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E58E8h\"");
			float[] array7;
			if (array5[3] == key3Time)
			{
				float[] array6 = lastKeyValues;
				if (array6.Length <= 3)
				{
					goto IL_07c0;
				}
				bool flag14 = array6[3] == key3Value;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E58D9h\"");
				bool flag15 = false;
				if (!flag14)
				{
					flag15 = flag2;
				}
				object obj13 = obj14 | flag15;
				obj = obj13 | flag16;
				array7 = lastKeyTimes;
			}
			else
			{
				object obj15 = obj14 | flag16;
				obj = obj15 | flag2;
				array7 = array5;
			}
			if (array7.Length > 4)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E5936h\"");
				if (array7[4] == key4Time)
				{
					float[] array8 = lastKeyValues;
					if (array8.Length <= 4)
					{
						goto IL_07c0;
					}
					bool flag17 = array8[4] == key4Value;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E5936h\"");
					flag4 = false;
					if (flag17)
					{
						goto IL_086e;
					}
				}
				flag4 = flag2;
				goto IL_086e;
			}
		}
		goto IL_07c0;
		IL_07db:
		float[] array9 = lastKeyTimes;
		if (array9.Length > 1)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E5839h\"");
			if (array9[1] == key1Time)
			{
				float[] array10 = lastKeyValues;
				if (array10.Length <= 1)
				{
					goto IL_07c0;
				}
				bool flag18 = array10[1] == key1Value;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E5833h\"");
				bool flag19 = false;
				if (!flag18)
				{
					flag19 = flag2;
				}
				obj14 = flag19 | flag3;
			}
			else
			{
				object obj16 = flag3 | flag2;
				obj14 = obj16;
			}
			float[] array11 = lastKeyTimes;
			if (array11.Length > 2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E588Ah\"");
				if (array11[2] == key2Time)
				{
					float[] array12 = lastKeyValues;
					if (array12.Length <= 2)
					{
						goto IL_07c0;
					}
					bool flag20 = array12[2] == key2Value;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803E588Ah\"");
					flag16 = false;
					if (flag20)
					{
						goto IL_0819;
					}
				}
				flag16 = flag2;
				goto IL_0819;
			}
		}
		goto IL_07c0;
	}

	private unsafe void UpdateMatrices(float curveLength)
	{
		//IL_0008: Expected O, but got Ref
		//IL_00a5: Expected O, but got I
		//IL_013e: Expected O, but got I
		//IL_017f: Expected O, but got Ref
		//IL_018d: Expected O, but got Ref
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Expected O, but got Unknown
		//IL_01f1: Expected O, but got Ref
		//IL_02a2: Invalid comparison between F8 and I4
		//IL_05cd: Expected I4, but got F8
		//IL_05d6: Invalid comparison between I4 and F4
		//IL_036c: Expected F4, but got I4
		//IL_02c8: Expected F8, but got I4
		//IL_07e5: Invalid comparison between I4 and F4
		//IL_05f6: Invalid comparison between F8 and I4
		//IL_0323: Invalid comparison between I4 and F4
		//IL_050e: Expected F4, but got I4
		//IL_075c: Invalid comparison between I4 and F4
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Expected O, but got Unknown
		//IL_04c3: Invalid comparison between I4 and F4
		//IL_054a: Expected F4, but got I4
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Expected O, but got Unknown
		//IL_0715: Invalid comparison between I4 and F4
		//IL_03c4: Expected F4, but got I4
		//IL_0616: Invalid comparison between I4 and F4
		//IL_0633: Invalid comparison between I4 and F4
		//IL_03d2: Expected F4, but got I4
		//IL_0422: Expected F4, but got I4
		object obj2 = default(object);
		object obj = (object)(&obj2);
		List<Matrix4x4> list = matricesA;
		_ = 0;
		_ = 0;
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rdi_v1 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+1C]");
		_ = (nint)0 + (nint)1;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
		object obj3 = default(object);
		if (obj3 == null)
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rdi_v1 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rdi_v1 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+10]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v38 @ rdi_v1 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+18]");
				Array.Clear((Array)num, 0, 0);
			}
		}
		List<Matrix4x4> list2 = matricesB;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rdi_v3 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+1C]");
		_ = (nint)0 + (nint)1;
		if (!RuntimeHelpers.IsReferenceOrContainsReferences<Matrix4x4>())
		{
			_ = 0;
		}
		else
		{
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rdi_v3 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+18]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rdi_v3 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+10]");
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v111 @ rdi_v3 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+18]");
				Array.Clear((Array)num2, 0, 0);
			}
		}
		int length = chainCurve.length;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF2F0");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808BF690");
		object obj4 = default(object);
		chainCurve.GetKeys((Span<Keyframe>)(&obj4));
		object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v396 @ stack_-D8_v3+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v396 @ stack_-D8_v3+18]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
		object obj7 = default(object);
		object obj6 = obj7 * 28;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v415 @ rcx_v17+FFFFFFE4+v396 @ stack_-D8_v3]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v415 @ rcx_v17+FFFFFFF4+v396 @ stack_-D8_v3]");
		_ = 0;
		object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v415 @ rcx_v17+FFFFFFFC+v396 @ stack_-D8_v3]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181D72B80");
		Transform transform;
		if (meshBInterval <= 1)
		{
			Vector3 euler = default(Vector3);
			Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
			transform = _transform;
			if ((object)_transform != null)
			{
				goto IL_024b;
			}
		}
		transform = (_transform = base.transform);
		goto IL_024b;
		IL_05ed:
		double num3;
		if (!(num3 > 0.0))
		{
			return;
		}
		object obj9 = default(object);
		float num5 = default(float);
		float num4 = (float)obj9 - num5;
		int num6 = 0;
		float num8;
		float endT = default(float);
		int interval = default(int);
		ref Quaternion offsetRot = default(ref Quaternion);
		ref Quaternion currentRot = default(ref Quaternion);
		do
		{
			object obj10 = num6 * linkSpacing;
			float num7 = (float)obj10 + num8;
			float x = num7 / curveLength;
			float num9 = MathF.Floor(x);
			float num10 = num9 * curveLength;
			float num11 = num7 - num10;
			if (!(0f > num11))
			{
				if (num11 > curveLength)
				{
					num11 = curveLength;
				}
			}
			else
			{
				num11 = 0f;
			}
			float num12 = ((0f < curveLength) ? (num11 / curveLength) : 0f);
			if (!(0f > num12))
			{
				if (num12 > 1f)
				{
					num12 = 1f;
				}
			}
			else
			{
				num12 = 0f;
			}
			float num13 = num12 * num4;
			float curveT = num13 + num5;
			AddLinkMatrix(num6, curveT, num5, endT, interval, ref offsetRot, ref currentRot);
			num6++;
		}
		while (num6 < linkCount);
		return;
		IL_024b:
		Quaternion rotation = transform.rotation;
		if (mode != ChainMode.OpenExtendRetract)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm0,xmm6\"");
			double num14 = Math.Ceiling(0.0);
			bool flag = num14 < (double)maxLinks;
			num3 = num14;
			if (!flag)
			{
				num3 = maxLinks;
			}
			linkCount = (int)num3;
			if (0f < curveLength)
			{
				float num15 = chainMovement * linkSpacing;
				float x2 = num15 / curveLength;
				float num16 = MathF.Floor(x2);
				float num17 = num16 * curveLength;
				num8 = num15 - num17;
				if (!(0f > num8))
				{
					if (num8 > curveLength)
					{
						num8 = curveLength;
					}
					goto IL_05ed;
				}
			}
			num8 = 0f;
			goto IL_05ed;
		}
		if (visibleLinkCount >= 0)
		{
			int num18 = maxLinks;
			if (visibleLinkCount <= maxLinks)
			{
				num18 = visibleLinkCount;
			}
			linkCount = num18;
			if (num18 <= 0)
			{
				return;
			}
			float num19 = (float)obj9 - num5;
			int num20 = 0;
			do
			{
				float num21;
				if (0f < curveLength)
				{
					object obj11 = num20 * linkSpacing;
					num21 = (float)obj11 / curveLength;
					if (!(0f > num21))
					{
						if (num21 > 1f)
						{
							num21 = 1f;
						}
						goto IL_0753;
					}
				}
				num21 = 0f;
				goto IL_0753;
				IL_0753:
				if (!(0f > num21))
				{
					if (num21 > 1f)
					{
						num21 = 1f;
					}
				}
				else
				{
					num21 = 0f;
				}
				float num22 = num21 * num19;
				float curveT2 = num22 + num5;
				AddLinkMatrix(num20, curveT2, num5, endT, interval, ref offsetRot, ref currentRot);
				num20++;
			}
			while (num20 < linkCount);
		}
		else
		{
			linkCount = 0;
		}
	}

	private unsafe void AddLinkMatrix(int i, float curveT, float startT, float endT, int interval, [In] ref Quaternion offsetRot, [In] ref Quaternion currentRot)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0077: Invalid comparison between F4 and I
		//IL_0248: Expected I, but got O
		//IL_0251: Invalid comparison between O and F4
		//IL_010b: Expected O, but got I
		//IL_009e: Expected F4, but got I
		//IL_0276: Expected I, but got O
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Expected O, but got Unknown
		//IL_032a: Invalid comparison between F4 and O
		//IL_0151: Expected O, but got Ref
		//IL_022a: Expected O, but got Ref
		object obj2 = default(object);
		object obj = obj2 - 72;
		float num = chainCurve.Evaluate(curveT);
		float num2 = curveT + tangentSampleStep;
		if (!(startT > num2))
		{
			float num3 = num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+70]");
			if (num3 > 0f)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+70]");
				num2 = 0f;
			}
		}
		else
		{
			num2 = startT;
		}
		float num4 = chainCurve.Evaluate(num2);
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		nint num5 = (nint)typeof(Vector3);
		Vector3 vector = default(Vector3);
		object obj4;
		Vector3 upwards;
		if (System.Runtime.CompilerServices.Unsafe.As<Vector3, UIntPtr>(ref vector) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			object obj3 = 0 / vector;
			obj4 = obj3;
			upwards = vector;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v516 @ rcx_v9 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v533 @ rax_v48 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			obj4 = 0;
			upwards = Vector3.zeroVector;
		}
		nint num7 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v563 @ rcx_v10 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num8 = 0;
		object obj5 = upwards - Vector3.zeroVector;
		object obj7 = default(object);
		object obj6 = obj7 - (object)vector;
		object obj8 = obj4;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v566 @ rax_v13 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		object obj9 = obj8 - 0;
		object obj10 = obj6 * obj6;
		object obj11 = obj5 * obj5;
		object obj12 = obj9 * obj9;
		object obj13 = obj10 + obj11;
		object obj14 = obj13 + obj12;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj14))
		{
		}
		Vector3 forward = default(Vector3);
		Quaternion quaternion = Quaternion.Internal_LookRotation(ref forward, ref upwards);
		Transform transform = _transform;
		if ((object)_transform == null)
		{
			transform = (_transform = base.transform);
		}
		Vector3 vector2 = transform.TransformPoint((Vector3)(&forward));
		Quaternion q = default(Quaternion);
		Matrix4x4 matrix4x = Matrix4x4.Internal_TRS(ref upwards, ref q, ref forward);
		List<Matrix4x4> list;
		if (meshB != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+78]");
			if ((int)((nint)i % (nint)0) == 0)
			{
				list = matricesB;
				goto IL_0209;
			}
		}
		if (meshA != null)
		{
			list = matricesA;
			goto IL_0209;
		}
		return;
		IL_0209:
		_ = matrix4x.m02;
		_ = matrix4x.m03;
		object obj15 = default(object);
		list.Add((Matrix4x4)(&obj15));
	}

	private void DrawInstances()
	{
		List<Matrix4x4> list = matricesA;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+18]");
		if ((nint)0 > (nint)0 && meshA != null && chainLinkMaterial != null)
		{
			Graphics.DrawMeshInstanced(meshA, 0, chainLinkMaterial, matricesA);
		}
		List<Matrix4x4> list2 = matricesB;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v79 @ rax_v6 (System.Collections.Generic.List`1<UnityEngine.Matrix4x4>)+18]");
		if ((nint)0 > (nint)0 && meshB != null && chainLinkMaterial != null)
		{
			Graphics.DrawMeshInstanced(meshB, 0, chainLinkMaterial, matricesB);
		}
	}

	private bool IsAnyRelevantTransformChanged()
	{
		//IL_0179: Expected I4, but got O
		Transform transform = _transform;
		if ((object)_transform == null)
		{
			Transform transform2 = (_transform = base.transform);
			bool flag = (object)transform2 == null;
			transform = transform2;
			if (flag)
			{
				goto IL_016b;
			}
		}
		bool hasChanged = transform.hasChanged;
		if (!hasChanged)
		{
			if (includeParentTransformChanges != hasChanged)
			{
				Transform transform3 = _transform;
				if ((object)_transform == null)
				{
					Transform transform4 = (_transform = base.transform);
					bool flag2 = (object)transform4 == null;
					transform3 = transform4;
					if (flag2)
					{
						goto IL_016b;
					}
				}
				Transform transform5 = transform3;
				while (true)
				{
					Transform parent = transform5.parent;
					if (!(parent != null))
					{
						break;
					}
					if ((object)parent != null)
					{
						if (!parent.hasChanged)
						{
							transform5 = parent;
							continue;
						}
						goto IL_0165;
					}
					goto IL_016b;
				}
			}
			return false;
		}
		goto IL_0165;
		IL_016b:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_0165:
		return true;
	}

	private void ClearTransformChangedFlags()
	{
		Transform transform = _transform;
		if ((object)_transform == null)
		{
			transform = (_transform = base.transform);
		}
		transform.hasChanged = false;
		if (!includeParentTransformChanges)
		{
			return;
		}
		Transform transform2 = _transform;
		if ((object)_transform == null)
		{
			transform2 = (_transform = base.transform);
		}
		Transform transform3 = transform2;
		while (true)
		{
			Transform parent = transform3.parent;
			if (parent != null)
			{
				parent.hasChanged = false;
				transform3 = parent;
				continue;
			}
			break;
		}
	}

	private void MarkTransformChainAsChanged()
	{
		Transform transform = _transform;
		if ((object)_transform == null)
		{
			transform = (_transform = base.transform);
		}
		transform.hasChanged = true;
		if (!includeParentTransformChanges)
		{
			return;
		}
		Transform transform2 = _transform;
		if ((object)_transform == null)
		{
			transform2 = (_transform = base.transform);
		}
		Transform transform3 = transform2;
		while (true)
		{
			Transform parent = transform3.parent;
			if (parent != null)
			{
				parent.hasChanged = true;
				transform3 = parent;
				continue;
			}
			break;
		}
	}

	private float GetKeyTime(int i)
	{
		//IL_0054: Expected F4, but got I4
		//IL_002a: Expected O, but got I8
		//IL_0044: Expected O, but got I8
		if (i <= 5)
		{
			object obj = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ r8_v1+3E54A0+i @ rdx (System.Int32)*4]");
			object obj2 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v17 @ rdx_v2 (should have been resolved before IL gen)");
		}
		return 0f;
	}

	private float GetKeyValue(int i)
	{
		//IL_0054: Expected F4, but got I4
		//IL_002a: Expected O, but got I8
		//IL_0044: Expected O, but got I8
		if (i <= 5)
		{
			object obj = 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v15 @ r8_v1+3E5510+i @ rdx (System.Int32)*4]");
			object obj2 = 0 + 6442450944L;
			Cpp2ILHelpers.NoteDecompilerIssue("Indirect jump: v17 @ rdx_v2 (should have been resolved before IL gen)");
		}
		return 0f;
	}

	public ChainAnimatorCurve()
	{
		//IL_01a4: Expected I, but got O
		//IL_011e: Expected I, but got O
		//IL_0160: Expected I4, but got I8
		linkSpacing = 0.5f;
		meshBInterval = 8;
		nint num = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v48 @ rax_v3 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num2 = 0;
		linkRotationOffset = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v53 @ rcx_v3 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		maxLinks = 512;
		includeParentTransformChanges = true;
		AnimationCurve animationCurve = AnimationCurve.Linear(0f, 0f, 1f, 0f);
		chainCurve = animationCurve;
		key1Time = 1f;
		key2Time = 2f;
		key2Value = 1f;
		key3Time = 3f;
		key4Time = 4f;
		key4Value = -1f;
		key5Time = 5f;
		tangentSampleStep = 0.01f;
		float[] array = new float[6];
		cachedInTangents = array;
		cachedOutTangents = new float[6];
		cachedInWeights = new float[6];
		cachedOutWeights = new float[6];
		cachedWeightedModes = new WeightedMode[6];
		matricesA = new List<Matrix4x4>(512);
		matricesB = new List<Matrix4x4>(128);
		lastChainMovement = 0f / 0f;
		lastLinkSpacing = 0f / 0f;
		nint num3 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rax_v24 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num4 = 0;
		lastRotationOffset = Vector3.zeroVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rcx_v22 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
		_ = 0;
		lastMeshBInterval = -1;
		lastVisibleLinkCount = -2147483648;
		lastTangentSampleStep = 0f / 0f;
		float[] array2 = new float[6];
		lastKeyTimes = array2;
		float[] array3 = new float[6];
		lastKeyValues = array3;
		base._002Ector();
	}
}
