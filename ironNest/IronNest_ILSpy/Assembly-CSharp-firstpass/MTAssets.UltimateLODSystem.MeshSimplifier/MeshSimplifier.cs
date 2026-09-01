using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using MTAssets.UltimateLODSystem.MeshSimplifier.Internal;
using UnityEngine;

namespace MTAssets.UltimateLODSystem.MeshSimplifier;

public sealed class MeshSimplifier
{
	private const int TriangleEdgeCount = 3;

	private const int TriangleVertexCount = 3;

	private const double DoubleEpsilon = 0.001;

	private const double DenomEpilson = 1E-08;

	private static readonly int UVChannelCount = MeshUtils.UVChannelCount;

	private SimplificationOptions simplificationOptions;

	private bool verbose;

	private int subMeshCount;

	private int[] subMeshOffsets;

	private ResizableArray<Triangle> triangles;

	private ResizableArray<Vertex> vertices;

	private ResizableArray<Ref> refs;

	private ResizableArray<Vector3> vertNormals;

	private ResizableArray<Vector4> vertTangents;

	private UVChannels<Vector2> vertUV2D;

	private UVChannels<Vector3> vertUV3D;

	private UVChannels<Vector4> vertUV4D;

	private ResizableArray<Color> vertColors;

	private ResizableArray<BoneWeight> vertBoneWeights;

	private ResizableArray<BlendShapeContainer> blendShapes;

	private Matrix4x4[] bindposes;

	private readonly double[] errArr;

	private readonly int[] attributeIndexArr;

	private readonly HashSet<Triangle> triangleHashSet1;

	private readonly HashSet<Triangle> triangleHashSet2;

	public unsafe SimplificationOptions SimplificationOptions
	{
		get
		{
			//IL_000f: Expected I4, but got O
			//IL_000a: Expected native int or pointer, but got O
			//IL_001f: Expected native int or pointer, but got O
			//IL_0034: Expected native int or pointer, but got O
			SimplificationOptions simplificationOptions = default(SimplificationOptions);
			((SimplificationOptions*)(nint)simplificationOptions)->PreserveBorderEdges = (byte)(int)this.simplificationOptions != 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			((SimplificationOptions*)(nint)simplificationOptions)->MaxIterationCount = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
			((SimplificationOptions*)(nint)simplificationOptions)->ManualUVComponentCount = false;
			return simplificationOptions;
		}
		set
		{
			//IL_000e: Expected O, but got Ref
			//IL_001d: Expected O, but got I4
			object obj = default(object);
			ValidateOptions((SimplificationOptions)(&obj));
			simplificationOptions = (SimplificationOptions)value.PreserveBorderEdges;
			_ = value.MaxIterationCount;
			_ = value.ManualUVComponentCount;
		}
	}

	public unsafe bool PreserveBorderEdges
	{
		get
		{
			//IL_0007: Expected I4, but got O
			return (byte)(int)simplificationOptions != 0;
		}
		set
		{
			//IL_0034: Expected O, but got Ref
			_ = this.simplificationOptions;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
			_ = 0;
			object obj = default(object);
			ValidateOptions((SimplificationOptions)(&obj));
			SimplificationOptions simplificationOptions = default(SimplificationOptions);
			this.simplificationOptions = simplificationOptions;
		}
	}

	public unsafe bool PreserveUVSeamEdges
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+11]");
			return false;
		}
		set
		{
			//IL_0034: Expected O, but got Ref
			_ = this.simplificationOptions;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
			_ = 0;
			object obj = default(object);
			ValidateOptions((SimplificationOptions)(&obj));
			SimplificationOptions simplificationOptions = default(SimplificationOptions);
			this.simplificationOptions = simplificationOptions;
		}
	}

	public unsafe bool PreserveUVFoldoverEdges
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+12]");
			return false;
		}
		set
		{
			//IL_0034: Expected O, but got Ref
			_ = this.simplificationOptions;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
			_ = 0;
			object obj = default(object);
			ValidateOptions((SimplificationOptions)(&obj));
			SimplificationOptions simplificationOptions = default(SimplificationOptions);
			this.simplificationOptions = simplificationOptions;
		}
	}

	public unsafe bool PreserveSurfaceCurvature
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+13]");
			return false;
		}
		set
		{
			//IL_0034: Expected O, but got Ref
			_ = this.simplificationOptions;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
			_ = 0;
			object obj = default(object);
			ValidateOptions((SimplificationOptions)(&obj));
			SimplificationOptions simplificationOptions = default(SimplificationOptions);
			this.simplificationOptions = simplificationOptions;
		}
	}

	public unsafe bool EnableSmartLink
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+14]");
			return false;
		}
		set
		{
			//IL_0034: Expected O, but got Ref
			_ = this.simplificationOptions;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
			_ = 0;
			object obj = default(object);
			ValidateOptions((SimplificationOptions)(&obj));
			SimplificationOptions simplificationOptions = default(SimplificationOptions);
			this.simplificationOptions = simplificationOptions;
		}
	}

	public unsafe int MaxIterationCount
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			return 0;
		}
		set
		{
			//IL_0034: Expected O, but got Ref
			_ = this.simplificationOptions;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
			_ = 0;
			object obj = default(object);
			ValidateOptions((SimplificationOptions)(&obj));
			SimplificationOptions simplificationOptions = default(SimplificationOptions);
			this.simplificationOptions = simplificationOptions;
		}
	}

	public unsafe double Agressiveness
	{
		get
		{
			//IL_000d: Expected F8, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+28]");
			return 0.0;
		}
		set
		{
			//IL_002f: Expected O, but got Ref
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			_ = 0;
			object obj = default(object);
			ValidateOptions((SimplificationOptions)(&obj));
			simplificationOptions = simplificationOptions;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
			_ = 0;
		}
	}

	public bool Verbose
	{
		get
		{
			return verbose;
		}
		set
		{
			verbose = value;
		}
	}

	public unsafe double VertexLinkDistance
	{
		get
		{
			//IL_000d: Expected F8, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+18]");
			return 0.0;
		}
		set
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Expected O, but got Unknown
			//IL_0040: Expected O, but got Ref
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,xmm0\"");
			object obj = default(object);
			object obj2 = default(object);
			bool flag = obj == obj2;
			object obj4 = default(object);
			object obj3 = ~obj4;
			object obj5 = flag & obj3;
			if (obj5 == null)
			{
			}
			SimplificationOptions simplificationOptions = default(SimplificationOptions);
			ValidateOptions((SimplificationOptions)(&simplificationOptions));
			this.simplificationOptions = this.simplificationOptions;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
			_ = 0;
		}
	}

	public unsafe double VertexLinkDistanceSqr
	{
		get
		{
			//IL_0017: Expected F8, but got I
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+18]");
			return 0.0;
		}
		set
		{
			//IL_0098: Expected I, but got O
			//IL_0063: Expected O, but got Ref
			nint num = (nint)typeof(Math);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v34 @ rcx_v2 (Il2CppClass<System.Math>)+E4]");
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtsd xmm0,xmm6\"");
			}
			else
			{
				double num2 = Math.Sqrt(value);
			}
			SimplificationOptions simplificationOptions = default(SimplificationOptions);
			ValidateOptions((SimplificationOptions)(&simplificationOptions));
			this.simplificationOptions = this.simplificationOptions;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
			_ = 0;
		}
	}

	public unsafe Vector3[] Vertices
	{
		get
		{
			//IL_0039: Expected O, but got I
			//IL_006a: Unknown result type (might be due to invalid IL or missing references)
			//IL_006f: Expected O, but got Unknown
			//IL_0085: Expected O, but got I
			//IL_008e: Expected O, but got I4
			//IL_0097: Expected O, but got I4
			//IL_0115: Unknown result type (might be due to invalid IL or missing references)
			//IL_011a: Expected O, but got Unknown
			//IL_012b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Expected O, but got Unknown
			//IL_0146: Unknown result type (might be due to invalid IL or missing references)
			//IL_014b: Expected O, but got Unknown
			//IL_0154: Unknown result type (might be due to invalid IL or missing references)
			//IL_0159: Expected O, but got Unknown
			ResizableArray<Vertex> resizableArray = vertices;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rsi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
			Vector3[] array = new Vector3[0];
			ResizableArray<Vertex> resizableArray2 = vertices;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rdi_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rsi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
			if ((nint)0 > (nint)0)
			{
				object obj2 = array + 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v97 @ rdi_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
				object obj3 = (nint)0 + (nint)40;
				object obj4 = 0;
				object obj5 = 0;
				object obj7 = default(object);
				while (true)
				{
					object obj6 = obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ rdi_v4+18]");
					if ((nint)obj6 < 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm6\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm6,xmm6\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm6\"");
						if ((nint)obj5 < array.Length)
						{
							obj5++;
							obj2 = obj7;
							obj4++;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v101 @ rsi_v8+10]");
							_ = 0;
							obj3 -= -128;
							obj2 += 12;
							object obj8 = obj4;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v31 @ rsi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
							if ((nint)obj8 >= 0)
							{
								break;
							}
							continue;
						}
					}
					return (Vector3[])(object)new IndexOutOfRangeException();
				}
			}
			return array;
		}
		set
		{
			//IL_0008: Expected O, but got Ref
			//IL_0042: Expected O, but got I4
			//IL_006c: Expected I, but got O
			//IL_0075: Expected O, but got I4
			//IL_0093: Expected O, but got I
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a1: Expected O, but got Unknown
			//IL_00b7: Expected O, but got I
			//IL_0184: Expected O, but got I
			//IL_0194: Expected O, but got I
			//IL_019d: Expected O, but got I4
			//IL_01a2: Expected I, but got O
			//IL_01ab: Expected O, but got I4
			//IL_01e2: Expected O, but got I
			//IL_01f2: Expected O, but got I
			//IL_01fb: Expected O, but got I4
			//IL_0212: Unknown result type (might be due to invalid IL or missing references)
			//IL_0217: Expected O, but got Unknown
			//IL_0227: Expected O, but got I
			//IL_0230: Unknown result type (might be due to invalid IL or missing references)
			//IL_0235: Expected O, but got Unknown
			//IL_0283: Expected O, but got I
			//IL_02a0: Expected O, but got I
			//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02c8: Expected O, but got Unknown
			//IL_02d8: Expected O, but got I
			object obj2 = default(object);
			object obj = (object)(&obj2);
			Vector3[] array = default(Vector3[]);
			if (array != null)
			{
				bindposes = null;
				bool flag = vertices == null;
				Vector3[] array2 = null;
				if (!flag)
				{
					array2 = (Vector3[])array.Length;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
					ResizableArray<Vertex> resizableArray = vertices;
					bool flag2 = vertices == null;
					nint num = unchecked((nint)null);
					object obj3 = 0;
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ r15_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
						object obj4 = 0;
						object obj5 = array + 32;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ r15_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
						object obj6 = (nint)0 + (nint)32;
						Matrix4x4[] array3 = null;
						Matrix4x4[] array4 = null;
						while (true)
						{
							if ((nint)array3 >= array.Length)
							{
								return;
							}
							if ((nint)array4 < array.Length)
							{
								_ = 0;
								_ = 1;
								_ = 0;
								_ = 0;
								_ = 0;
								_ = 0;
								_ = 0;
								_ = 0;
								_ = 0;
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-59]");
								_ = 0;
								_ = 0;
								_ = 0;
								_ = 0;
								_ = 0;
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v159 @ r15_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
								bool flag3 = (nint)0 == 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r14_v4+8]");
								object obj7 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r14_v4+8]");
								object obj8 = 0;
								object obj9 = 0;
								num = unchecked((nint)null);
								obj3 = 0;
								if (flag3)
								{
									break;
								}
								Matrix4x4[] array5 = array4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v158 @ r15_v4+18]");
								bool flag4 = (nint)array5 >= 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r14_v4+8]");
								obj7 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r14_v4+8]");
								obj8 = 0;
								obj9 = 0;
								if (!flag4)
								{
									array4 = (Matrix4x4[])(array4 + 1);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-49]");
									obj6 = 0;
									obj5 += 12;
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm3\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-55]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-29]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-19]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1-9]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+17]");
									obj9 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+7]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+27]");
									obj8 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+17]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+27]");
									_ = 0;
									obj6 -= -128;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v157 @ r14_v4+8]");
									obj7 = 0;
									array3 = array4;
									continue;
								}
							}
							throw new IndexOutOfRangeException();
						}
					}
				}
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentNullException ex = new ArgumentNullException("value");
			ex._002Ector("value");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public int SubMeshCount => subMeshCount;

	public int BlendShapeCount
	{
		get
		{
			if (blendShapes != null)
			{
				ResizableArray<BlendShapeContainer> resizableArray = blendShapes;
				return resizableArray.length;
			}
			return 0;
		}
	}

	public Vector3[] Normals
	{
		get
		{
			//IL_001c: Expected O, but got I
			if (vertNormals != null)
			{
				ResizableArray<Vector3> resizableArray = vertNormals;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector3>)+10]");
				return (Vector3[])0;
			}
			return null;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			object obj = this + 96;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807267B0");
		}
	}

	public Vector4[] Tangents
	{
		get
		{
			//IL_001c: Expected O, but got I
			if (vertTangents != null)
			{
				ResizableArray<Vector4> resizableArray = vertTangents;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector4>)+10]");
				return (Vector4[])0;
			}
			return null;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			object obj = this + 104;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807267B0");
		}
	}

	public Vector2[] UV1
	{
		get
		{
			return GetUVs2D(0);
		}
		set
		{
			SetUVs(0, value);
		}
	}

	public Vector2[] UV2
	{
		get
		{
			return GetUVs2D(1);
		}
		set
		{
			SetUVs(1, value);
		}
	}

	public Vector2[] UV3
	{
		get
		{
			return GetUVs2D(2);
		}
		set
		{
			SetUVs(2, value);
		}
	}

	public Vector2[] UV4
	{
		get
		{
			return GetUVs2D(3);
		}
		set
		{
			SetUVs(3, value);
		}
	}

	public Vector2[] UV5
	{
		get
		{
			return GetUVs2D(4);
		}
		set
		{
			SetUVs(4, value);
		}
	}

	public Vector2[] UV6
	{
		get
		{
			return GetUVs2D(5);
		}
		set
		{
			SetUVs(5, value);
		}
	}

	public Vector2[] UV7
	{
		get
		{
			return GetUVs2D(6);
		}
		set
		{
			SetUVs(6, value);
		}
	}

	public Vector2[] UV8
	{
		get
		{
			return GetUVs2D(7);
		}
		set
		{
			SetUVs(7, value);
		}
	}

	public Color[] Colors
	{
		get
		{
			//IL_001c: Expected O, but got I
			if (vertColors != null)
			{
				ResizableArray<Color> resizableArray = vertColors;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Color>)+10]");
				return (Color[])0;
			}
			return null;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			object obj = this + 136;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807267B0");
		}
	}

	public BoneWeight[] BoneWeights
	{
		get
		{
			//IL_001c: Expected O, but got I
			if (vertBoneWeights != null)
			{
				ResizableArray<BoneWeight> resizableArray = vertBoneWeights;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v37 @ rax_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.BoneWeight>)+10]");
				return (BoneWeight[])0;
			}
			return null;
		}
		set
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Expected O, but got Unknown
			object obj = this + 144;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807267B0");
		}
	}

	public MeshSimplifier()
	{
		//IL_00c3: Expected I, but got O
		nint num = (nint)typeof(SimplificationOptions);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rax_v3 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.SimplificationOptions>)+B8]");
		nint num2 = 0;
		simplificationOptions = SimplificationOptions.Default;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v4 (Il2CppStaticFields<MTAssets.UltimateLODSystem.MeshSimplifier.SimplificationOptions>)+10]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rax_v4 (Il2CppStaticFields<MTAssets.UltimateLODSystem.MeshSimplifier.SimplificationOptions>)+20]");
		_ = 0;
		double[] array = new double[3];
		errArr = array;
		int[] array2 = new int[3];
		attributeIndexArr = array2;
		HashSet<Triangle> hashSet = new HashSet<Triangle>();
		triangleHashSet1 = hashSet;
		HashSet<Triangle> hashSet2 = new HashSet<Triangle>();
		triangleHashSet2 = hashSet2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		ResizableArray<Triangle> resizableArray = new ResizableArray<Triangle>(0);
		triangles = resizableArray;
		ResizableArray<Vertex> resizableArray2 = new ResizableArray<Vertex>(0);
		vertices = resizableArray2;
		ResizableArray<Ref> resizableArray3 = new ResizableArray<Ref>(0);
		refs = resizableArray3;
	}

	public MeshSimplifier(Mesh mesh)
		: this()
	{
		if (mesh != null)
		{
			Initialize(mesh);
		}
	}

	private unsafe void InitializeVertexAttribute<T>(T[] attributeValues, ref ResizableArray<T> attributeArray, string attributeName)
	{
		//IL_0335: Expected O, but got I
		//IL_02f1: Expected O, but got I4
		//IL_035c: Expected O, but got Ref
		//IL_03ab: Expected O, but got I
		//IL_013a: Expected I, but got O
		//IL_014a: Expected O, but got I
		//IL_01bf: Expected I, but got O
		//IL_01cf: Expected O, but got I
		//IL_0244: Expected I, but got O
		//IL_0254: Expected O, but got I
		MeshSimplifier meshSimplifier = default(MeshSimplifier);
		bool flag = meshSimplifier.verbose;
		MeshSimplifier meshSimplifier2 = this;
		if (!flag)
		{
			bool flag2 = meshSimplifier.verbose;
			meshSimplifier2 = (MeshSimplifier)(object)"Failed to set vertex attribute '{0}' with {1} length of array, when {2} was needed.";
			if (!flag2)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4B90");
				meshSimplifier2 = meshSimplifier;
			}
		}
		ref ResizableArray<T> reference = default(ref ResizableArray<T>);
		if (attributeValues != null)
		{
			ResizableArray<Vertex> resizableArray = vertices;
			bool flag3 = vertices == null;
			T[] array = attributeValues;
			if (!flag3)
			{
				int num = attributeValues.Length;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v80 @ rax_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
				if ((nint)num != 0)
				{
					if (attributeValues.Length != 0)
					{
						object[] array2 = new object[3];
						string text = default(string);
						if (text != null)
						{
							nint num2 = (nint)array2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v424 @ rdx_v32 (Il2CppClass<System.Object[]>)+40]");
							array = (T[])0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj = default(object);
							bool flag4 = obj == null;
							meshSimplifier2 = (MeshSimplifier)(object)text;
							if (flag4)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj2 = default(object);
								throw obj2;
							}
						}
						array2[0] = text;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object obj3 = default(object);
						if (obj3 != null)
						{
							nint num3 = (nint)array2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v572 @ rdx_v30 (Il2CppClass<System.Object[]>)+40]");
							object obj4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj5 = default(object);
							bool flag5 = obj5 == null;
							object obj6 = obj3;
							if (flag5)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj7 = default(object);
								throw obj7;
							}
						}
						array2[1] = obj3;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						object obj8 = default(object);
						if (obj8 != null)
						{
							nint num4 = (nint)array2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v599 @ rdx_v28 (Il2CppClass<System.Object[]>)+40]");
							object obj9 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
							object obj10 = default(object);
							bool flag6 = obj10 == null;
							object obj11 = obj8;
							if (flag6)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
								object obj12 = default(object);
								throw obj12;
							}
						}
						array2[2] = obj8;
						Debug.LogErrorFormat("Failed to set vertex attribute '{0}' with {1} length of array, when {2} was needed.", array2);
					}
					goto IL_02ad;
				}
				bool flag7 = reference == null;
				bool flag8 = meshSimplifier.verbose;
				if (!flag7)
				{
					array = (T[])attributeValues.Length;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
					string text = null;
					meshSimplifier2 = (MeshSimplifier)(object)reference;
				}
				else
				{
					T[] array3 = null;
					bool flag9 = meshSimplifier.verbose;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v421 @ r9_v10 (System.Boolean)+18]");
					string text = (string)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8540");
					reference = ref *(ResizableArray<T>*)array3;
					array = array3;
					meshSimplifier2 = (MeshSimplifier)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference);
				}
				if (*(object*)reference != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v491 @ r8_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<T>&)+10]");
					int length = default(int);
					Array.Copy(attributeValues, 0, (Array)0, 0, length);
					return;
				}
			}
			throw new NullReferenceException();
		}
		goto IL_02ad;
		IL_02ad:
		reference = ref *(ResizableArray<T>*)null;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static double VertexError(ref SymmetricMatrix q, double x, double y, double z)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm7,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm8,xmm8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm9\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm7,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm8,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm5,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+20h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm4\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm2\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rcx+38h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm9\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [rcx+48h]\"");
		return z;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe double CurvatureError(ref Vertex vert0, ref Vertex vert1)
	{
		//IL_026a: Expected F8, but got I
		//IL_0091: Expected O, but got Ref
		//IL_010f: Expected F8, but got I4
		//IL_0289: Expected O, but got I4
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Expected O, but got Unknown
		//IL_02f0: Expected O, but got I4
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Expected O, but got Unknown
		//IL_015e: Expected F8, but got I
		//IL_016c: Expected I, but got O
		//IL_022d: Expected F8, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+8]");
		double num = 0.0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,qword ptr [rsp+68h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm1,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm6,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAF40");
		bool flag = triangleHashSet1 == null;
		object obj = default(object);
		MeshSimplifier meshSimplifier = (MeshSimplifier)(&obj);
		if (!flag)
		{
			triangleHashSet1.Clear();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA250");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA250");
			if (triangleHashSet2 != null)
			{
				triangleHashSet2.Clear();
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA070");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18087B860");
				double result = 0.0;
				HashSet<Triangle>.Enumerator enumerator = default(HashSet<Triangle>.Enumerator);
				HashSet<Triangle>.Enumerator enumerator2 = default(HashSet<Triangle>.Enumerator);
				object obj6 = default(object);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18087B860");
					bool flag2 = false;
					bool flag4;
					bool flag5;
					bool flag6;
					while (true)
					{
						bool flag3 = enumerator2.MoveNext();
						object obj2 = flag3 ^ flag3;
						object obj3 = flag3 & obj2;
						flag4 = (nint)obj3 < 0;
						flag5 = (flag3 ? 1 : 0) < (false ? 1 : 0);
						flag6 = !flag3;
						if (flag6)
						{
							break;
						}
						Triangle current = enumerator2.Current;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v467 @ rax_v24 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle)+50]");
						num = 0.0;
						nint num2 = (nint)typeof(Vector3d);
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm6,xmm6\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm12\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm11\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm0\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm7,xmm7\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm7,xmm10\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm7\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm8\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v438 @ rcx_v20 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
						bool flag7 = (nint)0 <= (nint)0;
						int index = current.index;
						if (!flag7)
						{
							index = current.index;
							flag2 = current.deleted;
						}
					}
					enumerator2.Dispose();
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm8,xmm9\"");
					bool flag8 = flag5 == flag4;
					object obj4 = !flag8;
					object obj5 = obj4 | flag6;
					obj = obj6;
					if (obj5 == null)
					{
						result = (flag2 ? 1 : 0);
						obj = obj6;
					}
				}
				enumerator.Dispose();
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,qword ptr [rsp+70h]\"");
				return result;
			}
		}
		throw new NullReferenceException();
	}

	private unsafe double CalculateError(ref Vertex vert0, ref Vertex vert1, out Vector3d result)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_006d: Expected O, but got I
		//IL_0058: Expected O, but got I4
		//IL_03de: Expected I, but got O
		//IL_0925: Expected O, but got I
		//IL_0945: Expected F8, but got I
		//IL_0910: Expected F8, but got I
		//IL_0885: Expected O, but got I
		//IL_08a5: Expected F8, but got I
		//IL_0350: Expected F8, but got I
		//IL_0350: Expected F8, but got I
		//IL_0350: Expected F8, but got O
		//IL_0860: Expected O, but got I
		//IL_0870: Expected F8, but got I
		object obj2 = default(object);
		object obj = obj2 - 232;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+28]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+38]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+48]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,qword ptr [rsi+30h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm10,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm14,xmm9\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,qword ptr [rsi+40h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+68]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm11\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+58]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-80]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+78]");
		bool flag = (nint)0 == 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm15,xmm5\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm13,qword ptr [rsi+50h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm7,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm8,xmm3\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm7,qword ptr [rsi+60h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm1,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,qword ptr [rsi+70h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-60]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+68]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+78]");
		object obj3;
		if ((nint)0 == 0)
		{
			obj3 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+78]");
			obj3 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm12\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm11\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm12\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm13\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm14\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm14\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm14\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm11\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm12\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm13\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm7\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm6,qword ptr [182206E50h]\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803C209Dh\"");
		double result2;
		if (!flag && obj3 == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm5,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm5,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm13\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm13\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm5,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm10,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm5,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm10,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm5,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm5,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm4,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm15,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm15,xmm13\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm12,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm13\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm5,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-50]");
			ref Vector3d reference = ref *(Vector3d*)null;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm12,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm10\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm15\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-50]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+13]");
			if (0 != (nint)obj3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B9AD0");
			}
			Vector3d vector3d = result;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [result @ r9 (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+8]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [result @ r9 (MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d&)+10]");
			SymmetricMatrix q = default(SymmetricMatrix);
			result2 = VertexError(ref q, (double)vector3d, num, 0.0);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm6\"");
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+8]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+18]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+18]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+8]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+18]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+18]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+18]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+18]");
			_ = 0;
			nint num2 = (nint)typeof(MeshSimplifier);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm7,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm11,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm6,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm11,qword ptr [rbp+10h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm7,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm11,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-20]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rbp-80h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm3,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm5,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm5,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm10\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm13\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rbp-60h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm15\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rbp-80h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,qword ptr [rbp-50h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm3,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rbp-70h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm10\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm13\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm15\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm12,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm14,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm13,xmm13\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm12,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm15,xmm15\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm14,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm8,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm13,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rbp-80h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm14,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm13,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm4,xmm5\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm15,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm8,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm10\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm13\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm15\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v309 @ rcx_v4 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier>)+E4]");
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm12,xmm4\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v309 @ rcx_v4 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier>)+E4]");
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm6\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+8]");
					ref Vector3d reference = ref *(Vector3d*)null;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-20]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+30]");
					result2 = 0.0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+F8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-70]");
					ref Vector3d reference = ref *(Vector3d*)null;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+30]");
					result2 = 0.0;
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm12,xmm5\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v309 @ rcx_v4 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier>)+E4]");
				if ((nint)0 <= (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm6\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+8]");
					ref Vector3d reference = ref *(Vector3d*)null;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-20]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+30]");
					result2 = 0.0;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+100]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-40]");
					ref Vector3d reference = ref *(Vector3d*)null;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert1 @ r8 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+30]");
					result2 = 0.0;
				}
			}
		}
		return result2;
	}

	private static void CalculateBarycentricCoords(ref Vector3d point, ref Vector3d a, ref Vector3d b, ref Vector3d c, out Vector3 result)
	{
		//IL_0045: Expected I, but got O
		do
		{
			nint num = (nint)typeof(Math);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm8,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm7,qword ptr [rsp+50h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm6,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm10,xmm10\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm4,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm5,xmm5\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm0,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm5,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm12,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm12,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm0,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm10,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm11,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm13,xmm5\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm11,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm10,xmm5\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm11,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm12,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm13,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm8,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm13,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm10,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm8,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm7,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm10,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm8,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm13\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v68 @ rcx_v3 (Il2CppClass<System.Math>)+E4]");
		}
		while ((nint)0 > (nint)0);
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm11\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm13\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm8,xmm10\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm0\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,xmm11\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm1,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm8,xmm9\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm1\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm8,xmm6\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm8\"");
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm2,xmm8\"");
		object obj2 = default(object);
		object obj = obj2;
		_ = 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static Vector4 NormalizeTangent(Vector4 tangent)
	{
		//IL_0013: Invalid comparison between O and F4
		//IL_0095: Expected native int or pointer, but got O
		//IL_00a2: Expected native int or pointer, but got O
		//IL_00af: Expected native int or pointer, but got O
		//IL_00bc: Expected native int or pointer, but got O
		//IL_0030: Expected F4, but got I4
		//IL_0039: Expected F4, but got I4
		//IL_0042: Expected F4, but got I4
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		object obj = default(object);
		float z;
		float y;
		float x;
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			z = 0f;
			y = 0f;
			x = 0f;
		}
		else
		{
			x = tangent.x / (float)obj;
			y = tangent.y / (float)obj;
			z = tangent.z / (float)obj;
		}
		Vector4 vector = default(Vector4);
		((Vector4*)(nint)vector)->w = tangent.w;
		((Vector4*)(nint)vector)->x = x;
		((Vector4*)(nint)vector)->y = y;
		((Vector4*)(nint)vector)->z = z;
		return vector;
	}

	private unsafe bool Flipped(ref Vector3d p, int i0, int i1, ref Vertex v0, bool[] deleted)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0043: Expected O, but got I
		//IL_0053: Expected O, but got I
		//IL_008b: Expected O, but got I
		//IL_009b: Expected O, but got I
		//IL_00ab: Expected O, but got I
		//IL_00c1: Expected O, but got I
		//IL_00e4: Expected O, but got I
		//IL_00f2: Expected O, but got I4
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Expected O, but got Unknown
		//IL_0717: Expected I4, but got O
		//IL_013c: Expected O, but got I
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Expected O, but got Unknown
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Expected O, but got Unknown
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Expected O, but got Unknown
		//IL_01c2: Expected O, but got I
		//IL_01d8: Expected O, but got I
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Expected O, but got Unknown
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Expected O, but got Unknown
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Expected O, but got Unknown
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected O, but got Unknown
		//IL_077d: Expected O, but got I
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Expected O, but got Unknown
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Expected O, but got Unknown
		//IL_07f8: Expected O, but got I4
		//IL_0362: Expected O, but got I
		//IL_0318: Expected O, but got I
		//IL_034d: Expected O, but got I
		//IL_06d2: Expected O, but got I4
		//IL_0832: Expected O, but got I
		//IL_0842: Expected O, but got I
		//IL_0447: Expected O, but got I
		//IL_04a9: Expected I, but got O
		//IL_0523: Expected O, but got Ref
		//IL_05d8: Expected O, but got I4
		//IL_0676: Expected O, but got I
		//IL_0686: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		int num2 = default(int);
		int num = num2;
		ResizableArray<Ref> resizableArray = refs;
		ResizableArray<Triangle> resizableArray2 = triangles;
		ResizableArray<Vertex> resizableArray3 = vertices;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+B0]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v190 @ r12_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ r8_v7+24]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+10]");
			object obj5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+B8]");
			object obj6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v86 @ rdi_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
			object obj7 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+B8]");
			object obj8 = (nint)(-32) - (nint)0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v41 @ rax_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+10]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+B8]");
			object obj9 = (nint)0 + (nint)32;
			object obj10 = 0;
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ r8_v7+20]");
				object obj11 = 0 + obj10;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+18]");
				if ((nint)obj11 < 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+20+v201 @ rcx_v9*8]");
					nint num3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ rdi_v7+18]");
					if (num3 < 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+20+v201 @ rcx_v9*8]");
						object obj12 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+20+v201 @ rcx_v9*8]");
						object obj13 = 0 + obj12;
						object obj14 = obj13 + 3;
						object obj15 = obj14 << 5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v669 @ rax_v16+v193 @ rdi_v7]");
						bool flag = (nint)0 == 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v669 @ rax_v16+v193 @ rdi_v7]");
						if ((nint)0 != 0)
						{
							goto IL_0717;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+20+v201 @ rcx_v9*8]");
						object obj16 = (nint)0 >> 32;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+20+v201 @ rcx_v9*8]");
						object obj17 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+20+v201 @ rcx_v9*8]");
						object obj18 = 0 + obj17;
						object obj19 = obj16 + 1;
						object obj20 = obj18 << 5;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r9d\"");
						object obj21 = obj16 + 2;
						object obj22 = obj5 >> 31;
						object obj23 = obj5 + obj22;
						object obj24 = obj23 * 2;
						object obj25 = obj23 + obj24;
						object obj26 = obj19 - obj25;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v669 @ rax_v16+v193 @ rdi_v7]");
						if ((nint)0 == 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v688 @ r8_v9+24+v193 @ rdi_v7]");
							num2 = 0;
						}
						else
						{
							object obj27 = obj26 - 1;
							flag = obj27 == null;
							if ((nint)obj26 == 1)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v688 @ r8_v9+28+v193 @ rdi_v7]");
								num2 = 0;
							}
							else
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v688 @ r8_v9+2C+v193 @ rdi_v7]");
								num2 = 0;
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+20+v201 @ rcx_v9*8]");
						object obj28 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+20+v201 @ rcx_v9*8]");
						object obj29 = 0 + obj28;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
						object obj30 = obj29 << 5;
						object obj31 = obj23 >> 31;
						object obj32 = obj23 + obj31;
						object obj33 = obj32 * 2;
						object obj34 = obj32 + obj33;
						object obj35 = obj21 - obj34;
						object obj36 = !flag;
						object obj37;
						if (obj36 == null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r8_v11+24+v193 @ rdi_v7]");
							obj37 = 0;
						}
						else if ((nint)obj35 == 1)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r8_v11+28+v193 @ rdi_v7]");
							obj37 = 0;
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v188 @ r8_v11+2C+v193 @ rdi_v7]");
							obj37 = 0;
						}
						if (num2 != num && (nint)obj37 != num)
						{
							int num4 = num2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v191 @ r12_v4+18]");
							if ((nint)num4 < (nint)0)
							{
								int num5 = num2 << 7;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v831 @ rax_v32 (System.Int32)+38+v191 @ r12_v4]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,qword ptr [rsp+30h]\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm7\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm6,xmm6\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm7,xmm7\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm7\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm6\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BADE0");
								object obj38 = obj37;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v191 @ r12_v4+18]");
								if ((nint)obj38 < 0)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+98]");
									object obj39 = 0;
									object obj40 = obj37 << 7;
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm1\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm2,xmm2\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm4,xmm3\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm1,xmm1\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm1\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm2\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BADE0");
									nint num6 = (nint)typeof(Math);
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm7,xmm10\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm9\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm12\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm7,xmm0\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm7,xmm1\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm7,xmm14\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v869 @ rcx_v19 (Il2CppClass<System.Math>)+E4]");
									if ((nint)0 > (nint)0)
									{
										goto IL_0694;
									}
									object obj41 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 128));
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm8\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm10\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm8,xmm9\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm11\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm0\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,xmm10\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm12\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm9\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm8,xmm0\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm8\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v858 @ rax_v37+38+v191 @ r12_v4]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BADE0");
									object obj42 = obj10;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r13_v6+18]");
									if ((nint)obj42 < 0)
									{
										obj9 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+20+v201 @ rcx_v9*8]");
										nint num7 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ rdi_v7+18]");
										if (num7 < 0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,qword ptr [rax+rdi+70h]\"");
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rax+rdi+68h]\"");
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,qword ptr [rax+rdi+78h]\"");
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm15,xmm1\"");
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v487 @ rdx_v7+20+v201 @ rcx_v9*8]");
											nint num8 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ rdi_v7+18]");
											bool flag2 = num8 <= 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v858 @ rax_v37+28+v191 @ r12_v4]");
											object obj43 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v831 @ rax_v32 (System.Int32)+28+v191 @ r12_v4]");
											object obj44 = 0;
											if (!flag2)
											{
												goto IL_0694;
											}
											goto IL_0822;
										}
									}
								}
							}
						}
						else
						{
							object obj45 = obj10;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v179 @ r13_v6+18]");
							if ((nint)obj45 < 0)
							{
								obj9 = 1;
								goto IL_0822;
							}
						}
					}
				}
				IndexOutOfRangeException ex = new IndexOutOfRangeException();
				return (byte)(int)ex != 0;
				IL_0717:
				obj9++;
				object obj46 = obj8 + obj9;
				obj10++;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v489 @ r8_v7+24]");
				if ((nint)obj46 >= 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+A8]");
				num = 0;
				continue;
				IL_0694:
				return true;
				IL_0822:
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+B0]");
				obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+90]");
				obj5 = 0;
				goto IL_0717;
			}
		}
		return false;
	}

	private unsafe void UpdateTriangles(int i0, int ia0, ref Vertex v, ResizableArray<bool> deleted, ref int deletedTriangles)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0027: Expected O, but got I
		//IL_003c: Expected O, but got I
		//IL_0074: Expected O, but got I
		//IL_007d: Expected O, but got I4
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Expected O, but got Unknown
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Expected O, but got Unknown
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Expected O, but got Unknown
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Expected O, but got Unknown
		//IL_051e: Expected O, but got I
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Expected O, but got Unknown
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Expected O, but got Unknown
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Expected Ref, but got Unknown
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Expected O, but got Unknown
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Expected Ref, but got Unknown
		//IL_0231: Expected O, but got I
		//IL_0258: Expected O, but got I
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Expected O, but got Unknown
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected Ref, but got Unknown
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Expected Ref, but got Unknown
		//IL_02d7: Expected O, but got I
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Expected O, but got Unknown
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Expected O, but got Unknown
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Expected Ref, but got Unknown
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Expected Ref, but got Unknown
		//IL_0380: Expected O, but got I
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Expected O, but got Unknown
		//IL_04ab: Expected O, but got Ref
		//IL_04bb: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		ResizableArray<Triangle> resizableArray = triangles;
		ResizableArray<Vertex> resizableArray2 = vertices;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v44 @ rdi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rsi_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ r9_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+24]");
		if ((nint)0 <= (nint)0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+B0]");
		object obj5 = 0;
		object obj6 = 0;
		MeshSimplifier meshSimplifier = this;
		int num = ia0;
		object obj9 = default(object);
		object obj12 = default(object);
		Triangle triangle = default(Triangle);
		int index = default(int);
		object obj15 = default(object);
		object obj33 = default(object);
		while (true)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ r9_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+20]");
			object obj7 = 0 + obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			object obj8 = obj9 * 2;
			object obj10 = obj9 + obj8;
			object obj11 = obj10 << 5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rcx_v8+60+v74 @ rdi_v3]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rcx_v8+70+v74 @ rdi_v3]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rcx_v8+60+v74 @ rdi_v3]");
			if ((nint)0 == 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				if (obj12 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+98]");
					triangle.set_Item(index, 0);
					if (num != -1)
					{
						triangle.SetAttributeIndex(index, num);
					}
					_ = 1;
					object obj13 = (object)triangle >> 32;
					object obj14 = obj15 << 7;
					object obj16 = obj13 << 7;
					object obj17 = obj16 + 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rsi_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
					ref Vertex vert = ref *(Vertex*)(obj17 + 0);
					object obj18 = obj14 + 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rsi_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
					ref Vertex vert2 = ref *(Vertex*)(obj18 + 0);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+90]");
					double num2 = ((MeshSimplifier)0).CalculateError(ref vert, ref vert2, out var result);
					object obj19 = obj15 >> 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+90]");
					meshSimplifier = (MeshSimplifier)0;
					object obj20 = obj19 << 7;
					object obj21 = obj15 << 7;
					object obj22 = obj20 + 32;
					object obj23 = obj21 + 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rsi_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
					ref Vertex vert3 = ref *(Vertex*)(obj22 + 0);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rsi_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
					ref Vertex vert4 = ref *(Vertex*)(obj23 + 0);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+90]");
					double num3 = ((MeshSimplifier)0).CalculateError(ref vert4, ref vert3, out result);
					object obj24 = (object)triangle >> 32;
					object obj25 = obj15 >> 32;
					object obj26 = obj24 << 7;
					object obj27 = obj25 << 7;
					object obj28 = obj26 + 32;
					object obj29 = obj27 + 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rsi_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
					ref Vertex vert5 = ref *(Vertex*)(obj28 + 0);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v73 @ rsi_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
					ref Vertex vert6 = ref *(Vertex*)(obj29 + 0);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+90]");
					double num4 = ((MeshSimplifier)0).CalculateError(ref vert6, ref vert5, out result);
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm7\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rsi_v3+18]");
					if ((nint)obj25 > 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm7\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rsi_v3+18]");
						if ((nint)obj25 <= 0)
						{
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm2,xmm6\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rsi_v3+18]");
						if ((nint)obj25 <= 0)
						{
						}
					}
					object obj30 = obj9 * 2;
					object obj31 = obj9 + obj30;
					object obj32 = obj31 << 5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v183 @ rcx_v8+30+v74 @ rdi_v3]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm6\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm3\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-60]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-50]");
					_ = 0;
					meshSimplifier.refs.Add((Ref)(&obj33));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+B0]");
					obj5 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+A0]");
					num = 0;
				}
				else
				{
					object obj34 = obj9 + 1;
					object obj35 = obj34 * 2;
					object obj36 = obj34 + obj35;
					object obj37 = obj36 << 5;
					_ = 1;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+B8]");
					object obj38 = 0;
					obj38++;
				}
			}
			obj6++;
			object obj39 = obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ r9_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+24]");
			if ((nint)obj39 < 0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+A8]");
				ref Vertex reference = ref *(Vertex*)null;
				continue;
			}
			break;
		}
	}

	private unsafe void InterpolateVertexAttributes(int dst, int i0, int i1, int i2, ref Vector3 barycentricCoord)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0ddf: Expected O, but got I
		//IL_025b: Expected O, but got I
		//IL_0e75: Expected O, but got I4
		//IL_001b: Expected O, but got Ref
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0077: Expected O, but got Ref
		//IL_009e: Expected O, but got I
		//IL_00bb: Expected O, but got I
		//IL_00e5: Expected O, but got I
		//IL_0276: Expected O, but got Ref
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Expected O, but got Unknown
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Expected O, but got Unknown
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Expected O, but got Unknown
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Expected O, but got Unknown
		//IL_0114: Expected O, but got I
		//IL_0149: Expected O, but got I
		//IL_0173: Expected O, but got I
		//IL_019d: Expected O, but got I
		//IL_0544: Expected O, but got I4
		//IL_054d: Expected O, but got I4
		//IL_02e7: Expected O, but got Ref
		//IL_030e: Expected O, but got I
		//IL_032b: Expected O, but got I
		//IL_0355: Expected O, but got I
		//IL_038c: Expected O, but got I
		//IL_0e04: Invalid comparison between O and F4
		//IL_06d6: Expected O, but got I4
		//IL_06df: Expected O, but got I4
		//IL_03d3: Expected O, but got I
		//IL_03fd: Expected O, but got I
		//IL_0427: Expected O, but got I
		//IL_0451: Expected O, but got I
		//IL_0210: Expected I, but got O
		//IL_0236: Expected O, but got I
		//IL_08e5: Expected O, but got I4
		//IL_08ee: Expected O, but got I4
		//IL_0496: Invalid comparison between I and F4
		//IL_0e29: Expected O, but got Ref
		//IL_0e57: Expected I4, but got O
		//IL_0afc: Expected O, but got Ref
		//IL_0b16: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1b: Expected O, but got Unknown
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b30: Expected O, but got Unknown
		//IL_0b40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b45: Expected O, but got Unknown
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Expected O, but got Unknown
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Expected O, but got Unknown
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Expected O, but got Unknown
		//IL_0ced: Expected O, but got I4
		//IL_0cf6: Expected O, but got I4
		//IL_0b58: Expected O, but got Ref
		//IL_0b7f: Expected O, but got I
		//IL_0ba9: Expected O, but got I
		//IL_0bd3: Expected O, but got I
		//IL_0e93: Expected O, but got I
		//IL_0ea1: Expected O, but got Ref
		//IL_0bf3: Expected O, but got Ref
		//IL_0c0d: Expected O, but got I
		//IL_0c1b: Expected O, but got Ref
		//IL_0c38: Expected O, but got I
		//IL_0c55: Expected O, but got I
		//IL_0c7f: Expected O, but got I
		//IL_0591: Expected O, but got I
		//IL_0723: Expected O, but got I
		//IL_0d09: Expected O, but got Ref
		//IL_0d23: Expected O, but got I
		//IL_0932: Expected O, but got I
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Expected O, but got Unknown
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Expected O, but got Unknown
		//IL_0d31: Expected O, but got I4
		//IL_0d3a: Expected O, but got I4
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Expected O, but got Unknown
		//IL_08b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Expected O, but got Unknown
		//IL_05c9: Expected O, but got Ref
		//IL_0f05: Expected O, but got I
		//IL_0ab7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0abc: Expected O, but got Unknown
		//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aca: Expected O, but got Unknown
		//IL_0768: Expected O, but got Ref
		//IL_05fa: Expected O, but got I
		//IL_0612: Expected O, but got Ref
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Expected O, but got Unknown
		//IL_0637: Unknown result type (might be due to invalid IL or missing references)
		//IL_063c: Expected O, but got Unknown
		//IL_0659: Expected O, but got I
		//IL_0691: Expected O, but got I
		//IL_0977: Expected O, but got Ref
		//IL_098f: Expected O, but got Ref
		//IL_0799: Expected O, but got I
		//IL_07b1: Expected O, but got Ref
		//IL_07c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Expected O, but got Unknown
		//IL_07d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Expected O, but got Unknown
		//IL_07f8: Expected O, but got I
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_080d: Expected O, but got Unknown
		//IL_0837: Expected O, but got I
		//IL_0861: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Expected O, but got Unknown
		//IL_0896: Expected O, but got I
		//IL_0da6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dab: Expected O, but got Unknown
		//IL_09ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bf: Expected O, but got Unknown
		//IL_09de: Expected O, but got I
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f4: Expected O, but got Unknown
		//IL_0a1e: Expected O, but got I
		//IL_0a3b: Expected O, but got I
		//IL_0a72: Expected O, but got I
		//IL_0a98: Expected O, but got Ref
		//IL_0a98: Expected O, but got I
		//IL_0aa9: Expected O, but got Ref
		//IL_0d7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7f: Expected O, but got Unknown
		//IL_0d88: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8d: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		bool flag = vertNormals == null;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+77]");
		object obj3 = 0;
		object obj22 = default(object);
		int index2 = default(int);
		int num9;
		if (!flag)
		{
			object obj4 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-71]");
			object obj5 = 0 * obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-6D]");
			object obj6 = 0 * obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-69]");
			object obj7 = 0 * obj3;
			object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-6D]");
			nint num = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
			object obj9 = num * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-71]");
			nint num2 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
			object obj10 = num2 * 0;
			object obj11 = obj9 + obj6;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-69]");
			nint num3 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
			object obj12 = num3 * 0;
			object obj13 = obj10 + obj5;
			object obj14 = obj12 + obj7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+6F]");
			object obj15 = 0;
			int index = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-71]");
			nint num4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj16 = num4 * 0;
			object obj17 = obj16 + obj13;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-6D]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj18 = num5 * 0;
			object obj19 = obj18 + obj11;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-69]");
			nint num6 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj20 = num6 * 0;
			object obj21 = obj20 + obj14;
			Vector3 vector = ((ResizableArray<Vector3>)null).get_Item(index);
			if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj22) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
			{
				obj21 /= obj22;
				obj17 /= obj22;
				obj19 /= obj22;
				object obj23 = obj21;
				object obj24 = obj22;
				object obj25 = obj22;
			}
			else
			{
				nint num7 = (nint)typeof(Vector3);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1299 @ rax_v74 (Il2CppClass<UnityEngine.Vector3>)+B8]");
				nint num8 = 0;
				_ = Vector3.zeroVector;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1300 @ rcx_v58 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
				object obj23 = 0;
				object obj24 = obj22;
				object obj25 = obj22;
			}
			Vector3 vector2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
			vertNormals.set_Item(index2, vector2);
			object obj26 = obj22;
			num9 = (int)vector2;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+6F]");
			object obj15 = 0;
			num9 = i0;
		}
		bool flag2 = vertTangents == null;
		Vector4 vector3 = (Vector4)num9;
		if (!flag2)
		{
			object obj27 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-61]");
			object obj28 = 0 * obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-5D]");
			object obj29 = 0 * obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-59]");
			object obj30 = 0 * obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-55]");
			object obj31 = 0 * obj3;
			object obj32 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-5D]");
			nint num10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
			object obj33 = num10 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-61]");
			nint num11 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
			object obj34 = num11 * 0;
			object obj35 = obj33 + obj29;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-59]");
			nint num12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
			object obj36 = num12 * 0;
			object obj37 = obj34 + obj28;
			object obj38 = obj36 + obj30;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-55]");
			nint num13 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
			object obj39 = num13 * 0;
			object obj40 = obj39 + obj31;
			int index3 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-61]");
			nint num14 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj41 = num14 * 0;
			object obj17 = obj41 + obj37;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-5D]");
			nint num15 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj42 = num15 * 0;
			object obj19 = obj42 + obj35;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-59]");
			nint num16 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj43 = num16 * 0;
			object obj21 = obj43 + obj38;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-55]");
			nint num17 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj44 = num17 * 0;
			object obj45 = obj44 + obj40;
			Vector4 vector4 = ((ResizableArray<Vector4>)null).get_Item(index3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			if (!(0f > 1E-05f))
			{
				_ = 0;
				_ = 0;
				_ = 0;
			}
			else
			{
				object obj46 = obj17;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
				obj17 = obj46 / 0;
				object obj47 = obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
				obj19 = obj47 / 0;
				object obj48 = obj21;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
				obj21 = obj48 / 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-61]");
			object obj25 = 0;
			vector3 = (Vector4)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-61]");
			_ = 0;
			vertTangents.set_Item(index2, vector3);
		}
		bool flag3 = vertUV2D == null;
		int num18 = i1;
		object obj54 = default(object);
		if (!flag3)
		{
			object obj49 = 0;
			object obj50 = 32;
			while ((nint)obj49 < UVChannelCount)
			{
				UVChannels<Vector2> uVChannels = vertUV2D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v361 @ rax_v50 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
				object obj51 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ r12_v19+v362 @ rax_v51]");
				if ((nint)0 != 0)
				{
					object obj52 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 79));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					object obj19 = obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
					object obj17 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					Vector2 value = (Vector2)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+53]");
					object obj53 = 0 * obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
					object obj25 = obj54 * 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-7D]");
					nint num19 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
					object obj23 = num19 * 0;
					object obj55 = obj53 + obj25;
					object obj24 = obj55 + obj23;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v352 @ r12_v19+v362 @ rax_v51]");
					((ResizableArray<Vector2>)0).set_Item(index2, value);
				}
				obj49++;
				obj50 += 8;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+67]");
			num18 = 0;
		}
		if (vertUV3D != null)
		{
			object obj56 = 0;
			object obj57 = 32;
			object obj66 = default(object);
			while ((nint)obj56 < UVChannelCount)
			{
				UVChannels<Vector3> uVChannels2 = vertUV3D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v363 @ rax_v38 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
				object obj58 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v353 @ r12_v16+v364 @ rax_v39]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v353 @ r12_v16+v364 @ rax_v39]");
				if ((nint)0 != 0)
				{
					object obj59 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					object obj19 = obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
					object obj17 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					Vector3 value2 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-6D]");
					object obj60 = 0 * obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-69]");
					object obj61 = 0 * obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-7D]");
					nint num20 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
					object obj62 = num20 * 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
					object obj63 = obj54 * 0;
					object obj64 = obj60 + obj62;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-79]");
					nint num21 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
					object obj25 = num21 * 0;
					object obj26 = obj64 + obj63;
					object obj65 = obj61 + obj25;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
					object obj23 = obj66 * 0;
					object obj24 = obj65 + obj23;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+4F]");
					((ResizableArray<Vector3>)0).set_Item(index2, value2);
				}
				obj56++;
				obj57 += 8;
			}
		}
		if (vertUV4D != null)
		{
			object obj67 = 0;
			object obj68 = 32;
			object obj78 = default(object);
			while ((nint)obj67 < UVChannelCount)
			{
				UVChannels<Vector4> uVChannels3 = vertUV4D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v365 @ rax_v26 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
				object obj69 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ r12_v13+v366 @ rax_v27]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v354 @ r12_v13+v366 @ rax_v27]");
				if ((nint)0 != 0)
				{
					object obj70 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					object obj71 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 113));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-59]");
					object obj72 = 0 * obj3;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-69]");
					nint num22 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
					object obj73 = num22 * 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-55]");
					object obj74 = 0 * obj3;
					object obj75 = obj72 + obj73;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-79]");
					nint num23 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
					object obj76 = num23 * 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-65]");
					nint num24 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
					object obj25 = num24 * 0;
					object obj26 = obj75 + obj76;
					object obj77 = obj74 + obj25;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-75]");
					nint num25 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
					object obj23 = num25 * 0;
					object obj24 = obj77 + obj23;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+4F]");
					((ResizableArray<Vector4>)0).set_Item(index2, (Vector4)(&obj78));
					obj78 = obj22;
					vector3 = (Vector4)(&obj78);
				}
				obj67++;
				obj68 += 8;
			}
		}
		if (vertColors != null)
		{
			object obj79 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-5D]");
			object obj80 = 0 * obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-59]");
			object obj81 = 0 * obj3;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-55]");
			object obj82 = 0 * obj3;
			object obj83 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-5D]");
			nint num26 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
			object obj84 = num26 * 0;
			object obj85 = obj84 + obj80;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-59]");
			nint num27 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
			object obj86 = num27 * 0;
			object obj87 = obj86 + obj81;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-55]");
			nint num28 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+4]");
			object obj88 = num28 * 0;
			object obj89 = obj88 + obj82;
			object obj90 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj25 = 0;
			Color value3 = (Color)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 97));
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-5D]");
			nint num29 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj91 = num29 * 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-59]");
			nint num30 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj92 = num30 * 0;
			object obj23 = obj91 + obj85;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1-55]");
			nint num31 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ r15_v1+8]");
			object obj93 = num31 * 0;
			object obj24 = obj92 + obj87;
			object obj26 = obj93 + obj89;
			vertColors.set_Item(index2, value3);
		}
		if (blendShapes == null)
		{
			return;
		}
		ResizableArray<BlendShapeContainer> resizableArray = blendShapes;
		object obj94 = 0;
		object obj95 = 0;
		while ((nint)obj95 < resizableArray.length)
		{
			object obj96 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 79));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v16 @ rbp_v1+4F]");
			object obj97 = 0;
			object obj98 = 0;
			object obj99 = 32;
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v397 @ rsi_v6+18]");
				object obj100 = 0;
				object obj101 = obj98;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v371 @ rax_v15+18]");
				if ((nint)obj101 >= 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803B8CA0");
				obj98++;
				obj99 += 8;
			}
			resizableArray = blendShapes;
			obj94++;
			obj95 = obj94;
		}
	}

	private bool AreUVsTheSame(int channel, int indexA, int indexB)
	{
		//IL_001f: Expected O, but got I
		//IL_018e: Expected O, but got I
		//IL_0513: Expected I4, but got O
		//IL_0334: Expected O, but got I
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected O, but got Unknown
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Expected O, but got Unknown
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Expected O, but got Unknown
		//IL_00bb: Expected O, but got I
		//IL_00d8: Expected O, but got I
		//IL_0108: Invalid comparison between F4 and O
		//IL_0127: Invalid comparison between F4 and I4
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Expected O, but got Unknown
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Expected O, but got Unknown
		//IL_022a: Expected O, but got I
		//IL_0247: Expected O, but got I
		//IL_0264: Expected O, but got I
		//IL_02ae: Invalid comparison between F4 and O
		//IL_02cd: Invalid comparison between F4 and I4
		//IL_03d0: Expected O, but got I
		//IL_03ed: Expected O, but got I
		//IL_040a: Expected O, but got I
		//IL_0427: Expected O, but got I
		//IL_048b: Invalid comparison between F4 and O
		//IL_04aa: Invalid comparison between F4 and I4
		object obj3 = default(object);
		if (vertUV2D != null)
		{
			UVChannels<Vector2> uVChannels = vertUV2D;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rax_v21 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
			object obj = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rax_v21 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
			if ((nint)0 == 0)
			{
				goto IL_0505;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v57 @ rcx_v14+20+channel @ rdx (System.Int32)*8]");
			if ((nint)0 != 0)
			{
				object obj2 = obj3 + 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				object obj4 = obj3 - 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+20]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-20]");
				object obj5 = num - 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp+24]");
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-1C]");
				object obj6 = num2 - 0;
				object obj7 = obj5 * obj5;
				object obj8 = obj6 * obj6;
				object obj9 = obj8 + obj7;
				bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj9);
				float num3 = 9.9999994E-11f - (float)obj9;
				bool flag2 = num3 == 0f;
				bool flag3 = !flag;
				bool flag4 = !flag2;
				return flag4 & flag3;
			}
		}
		if (vertUV3D != null)
		{
			UVChannels<Vector3> uVChannels2 = vertUV3D;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v16 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
			object obj10 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v121 @ rax_v16 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
			if ((nint)0 == 0)
			{
				goto IL_0505;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v125 @ rcx_v11+20+channel @ rdx (System.Int32)*8]");
			if ((nint)0 != 0)
			{
				object obj11 = obj3 - 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				object obj12 = obj3 - 16;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-20]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-10]");
				object obj13 = num4 - 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-1C]");
				nint num5 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-C]");
				object obj14 = num5 - 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-18]");
				nint num6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-8]");
				object obj15 = num6 - 0;
				object obj16 = obj14 * obj14;
				object obj17 = obj15 * obj15;
				object obj18 = obj13 * obj13;
				object obj19 = obj16 + obj18;
				object obj20 = obj19 + obj17;
				bool flag5 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj20);
				float num7 = 9.9999994E-11f - (float)obj20;
				bool flag6 = num7 == 0f;
				bool flag7 = !flag5;
				bool flag8 = !flag6;
				return flag8 & flag7;
			}
		}
		if (vertUV4D != null)
		{
			UVChannels<Vector4> uVChannels3 = vertUV4D;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rax_v11 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
			object obj21 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v122 @ rax_v11 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
			if ((nint)0 == 0)
			{
				goto IL_0505;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v126 @ rcx_v8+20+channel @ rdx (System.Int32)*8]");
			if ((nint)0 != 0)
			{
				object obj22 = obj3 - 16;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				object obj23 = obj3 - 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-10]");
				nint num8 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-20]");
				object obj24 = num8 - 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-C]");
				nint num9 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-1C]");
				object obj25 = num9 - 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-8]");
				nint num10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-18]");
				object obj26 = num10 - 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-4]");
				nint num11 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rsp-14]");
				object obj27 = num11 - 0;
				object obj28 = obj25 * obj25;
				object obj29 = obj26 * obj26;
				object obj30 = obj24 * obj24;
				object obj31 = obj27 * obj27;
				object obj32 = obj28 + obj30;
				object obj33 = obj32 + obj29;
				object obj34 = obj33 + obj31;
				bool flag9 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)9.9999994E-11f) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj34);
				float num12 = 9.9999994E-11f - (float)obj34;
				bool flag10 = num12 == 0f;
				bool flag11 = !flag9;
				bool flag12 = !flag10;
				return flag12 & flag11;
			}
		}
		return false;
		IL_0505:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private unsafe void RemoveVertexPass(int startTrisCount, int targetTrisCount, double threshold, ResizableArray<bool> deleted0, ResizableArray<bool> deleted1, ref int deletedTris)
	{
		//IL_0008: Expected O, but got Ref
		//IL_002c: Expected O, but got I
		//IL_0064: Expected O, but got I
		//IL_0087: Expected O, but got I4
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		//IL_0d6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d71: Expected O, but got Unknown
		//IL_0d7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d7f: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Expected O, but got Unknown
		//IL_01c6: Expected F8, but got I
		//IL_01e5: Expected F8, but got I
		//IL_0204: Expected F8, but got I
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Expected O, but got Unknown
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Expected O, but got Unknown
		//IL_02c2: Expected I4, but got O
		//IL_0317: Expected I4, but got O
		//IL_0e01: Expected O, but got I4
		//IL_034a: Expected I4, but got O
		//IL_0358: Expected O, but got I4
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Expected O, but got Unknown
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Expected O, but got Unknown
		//IL_0e3f: Expected O, but got I
		//IL_0e4f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e54: Expected O, but got Unknown
		//IL_0d20: Expected O, but got I
		//IL_0d30: Expected O, but got I
		//IL_0476: Expected O, but got I4
		//IL_04c1: Expected O, but got I4
		//IL_050c: Expected O, but got I4
		//IL_0670: Expected O, but got I4
		//IL_0699: Expected O, but got I4
		//IL_079e: Expected O, but got I
		//IL_07d4: Expected O, but got I4
		//IL_07ec: Expected O, but got I
		//IL_07f9: Expected O, but got Ref
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Expected O, but got Unknown
		//IL_0821: Expected I4, but got O
		//IL_083c: Expected O, but got I
		//IL_084c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0851: Expected O, but got Unknown
		//IL_0ea5: Expected O, but got I4
		//IL_0ece: Expected O, but got I4
		//IL_0ef7: Expected O, but got I4
		//IL_09c5: Expected O, but got I
		//IL_0a68: Expected O, but got I
		//IL_0b29: Expected O, but got I4
		//IL_0b50: Expected O, but got Ref
		//IL_0af8: Expected I4, but got I8
		//IL_0b73: Expected O, but got I4
		//IL_0b9a: Expected O, but got Ref
		//IL_0cd1: Expected O, but got I
		//IL_0cd1: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		ResizableArray<Triangle> resizableArray = triangles;
		_ = 0;
		_ = 0;
		_ = 0;
		_ = 0;
		ResizableArray<Vertex> resizableArray2 = vertices;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ rbx_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r15_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
		if ((nint)0 <= (nint)0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r15_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ r15_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
		_ = 0;
		_ = 0;
		_ = 0;
		object obj5 = 0;
		MeshSimplifier meshSimplifier = null;
		ref Vertex reference2 = default(ref Vertex);
		bool[] array7 = default(bool[]);
		MeshSimplifier meshSimplifier2;
		do
		{
			object obj6 = meshSimplifier * 2;
			object obj7 = (object)meshSimplifier + obj6;
			object obj8 = obj7 << 5;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v654 @ rax_v11+61+v247 @ r15_v5]");
			if ((nint)0 == 0)
			{
				object obj9 = meshSimplifier + 1;
				object obj10 = obj9 * 2;
				object obj11 = obj9 + obj10;
				object obj12 = obj11 << 5;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1050 @ rax_v15+v247 @ r15_v5]");
				if ((nint)0 == 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm1\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1050 @ rax_v15+v247 @ r15_v5]");
					if ((nint)0 <= (nint)0)
					{
						double[] array = errArr;
						object obj13 = meshSimplifier * 2;
						object obj14 = (object)meshSimplifier + obj13;
						object obj15 = obj14 << 5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r8_v7+40+v247 @ r15_v5]");
						array[0] = 0.0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r8_v7+48+v247 @ r15_v5]");
						array[1] = 0.0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v214 @ r8_v7+50+v247 @ r15_v5]");
						array[2] = 0.0;
						int[] array2 = attributeIndexArr;
						object obj16 = meshSimplifier * 2;
						object obj17 = (object)meshSimplifier + obj16;
						object obj18 = obj17 << 5;
						int[] array3 = array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r8_v9+34+v247 @ r15_v5]");
						array3[0] = 0;
						int[] array4 = array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r8_v9+38+v247 @ r15_v5]");
						array4[1] = 0;
						object obj19 = meshSimplifier * 2;
						object obj20 = (object)meshSimplifier + obj19;
						int[] array5 = array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ r8_v9+3C+v247 @ r15_v5]");
						array5[2] = 0;
						int num = obj20 << 5;
						int num2 = 1;
						int num3 = 1;
						int num4 = 0;
						int num5 = 0;
						int num6 = 0;
						bool flag4;
						do
						{
							double[] array6 = errArr;
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm1\"");
							bool flag = num6 > array6.Length;
							int num7 = (int)array2;
							if (!flag)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r9d\"");
								object obj21 = (object)array2 >> 31;
								num7 = (int)((object)array2 + obj21);
								object obj22 = num7 * 2;
								object obj23 = num7 + obj22;
								object obj24 = num3 - obj23;
								int num8;
								switch (num5)
								{
								case 0:
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v501 @ r12_v6 (System.Int32)+24+v247 @ r15_v5]");
									num8 = 0;
									break;
								case 1:
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v501 @ r12_v6 (System.Int32)+28+v247 @ r15_v5]");
									num8 = 0;
									break;
								default:
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v501 @ r12_v6 (System.Int32)+2C+v247 @ r15_v5]");
									num8 = 0;
									break;
								}
								int num9;
								if (obj24 == null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v501 @ r12_v6 (System.Int32)+24+v247 @ r15_v5]");
									num9 = 0;
								}
								else if ((nint)obj24 == 1)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v501 @ r12_v6 (System.Int32)+28+v247 @ r15_v5]");
									num9 = 0;
								}
								else
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v501 @ r12_v6 (System.Int32)+2C+v247 @ r15_v5]");
									num9 = 0;
								}
								int num10 = num9 << 7;
								meshSimplifier = (MeshSimplifier)(num8 << 7);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1119 @ rcx_v13 (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+98+v249 @ rbx_v3]");
								nint num11 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1349 @ rax_v35 (System.Int32)+98+v249 @ rbx_v3]");
								if (num11 == 0)
								{
									int num12 = num9 << 7;
									meshSimplifier = (MeshSimplifier)(num8 << 7);
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1119 @ rcx_v13 (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+99+v249 @ rbx_v3]");
									nint num13 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1352 @ rax_v38 (System.Int32)+99+v249 @ rbx_v3]");
									if (num13 == 0)
									{
										int num14 = num9 << 7;
										meshSimplifier = (MeshSimplifier)(num8 << 7);
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1119 @ rcx_v13 (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+9A+v249 @ rbx_v3]");
										nint num15 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1355 @ rax_v41 (System.Int32)+9A+v249 @ rbx_v3]");
										if (num15 == 0)
										{
											if ((object)simplificationOptions != null)
											{
												int num16 = num8 << 7;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1163 @ rax_v101 (System.Int32)+98+v249 @ rbx_v3]");
												if ((nint)0 != 0)
												{
													goto IL_0db0;
												}
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1166 @ rcx_v30 (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+11]");
											if ((nint)0 != 0)
											{
												int num17 = num8 << 7;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1164 @ rax_v99 (System.Int32)+99+v249 @ rbx_v3]");
												if ((nint)0 != 0)
												{
													goto IL_0db0;
												}
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1166 @ rcx_v30 (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+12]");
											if ((nint)0 != 0)
											{
												int num18 = num8 << 7;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1165 @ rax_v97 (System.Int32)+9A+v249 @ rbx_v3]");
												if ((nint)0 != 0)
												{
													goto IL_0db0;
												}
											}
											ref Vector3d result = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3d>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
											int num19 = num9 << 7;
											object obj25 = num19 + 32;
											int num20 = num8 << 7;
											ref Vertex vert = ref *(Vertex*)(obj25 + obj3);
											object obj26 = num20 + 32;
											double num21 = CalculateError(ref *(Vertex*)(obj26 + obj3), ref vert, out result);
											int num22 = num8 << 7;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
											int num23 = num9 << 7;
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
											ref Vector3d reference = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3d>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
											bool flag2 = Flipped(ref reference, num8, num9, ref reference2, array7);
											ref Vector3d reference3 = ref reference;
											if (!flag2)
											{
												reference3 = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3d>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
												if (!Flipped(ref reference3, num9, num8, ref reference2, array7))
												{
													object obj27 = num4 + 2;
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r8d\"");
													object obj28 = (nint)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference3) >> 31;
													object obj29 = (ref System.Runtime.CompilerServices.Unsafe.As<Vector3d, _003F>(ref reference3)) + (ref *(_003F*)obj28);
													object obj30 = obj29 * 2;
													object obj31 = obj29 + obj30;
													int num24 = obj27 - obj31;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-58]");
													object obj32 = (nint)0 * (nint)2;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-58]");
													object obj33 = 0 + obj32;
													object obj34 = obj33 << 5;
													int num25;
													switch (num24)
													{
													case 0:
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v714 @ rcx_v37+24+v247 @ r15_v5]");
														num25 = 0;
														break;
													case 1:
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v714 @ rcx_v37+28+v247 @ r15_v5]");
														num25 = 0;
														break;
													default:
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v714 @ rcx_v37+2C+v247 @ r15_v5]");
														num25 = 0;
														break;
													}
													ref Vector3d point = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3d>(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
													int num26 = num25 << 7;
													object obj35 = num26 + 40;
													int num27 = num9 << 7;
													ref Vector3d c = ref *(Vector3d*)(obj35 + obj3);
													object obj36 = num27 + 40;
													int num28 = num8 << 7;
													ref Vector3d b = ref *(Vector3d*)(obj36 + obj3);
													object obj37 = num28 + 40;
													CalculateBarycentricCoords(ref point, ref *(Vector3d*)(obj37 + obj3), ref b, ref c, out System.Runtime.CompilerServices.Unsafe.As<Vertex, Vector3>(ref reference2));
													int num29 = num8 << 7;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-40]");
													_ = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-30]");
													_ = 0;
													int num30 = num8 << 7;
													int num31 = num9 << 7;
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm15,xmm10\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm11,xmm7\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm13,qword ptr [rcx+rbx+70h]\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm3\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm8,qword ptr [rcx+rbx+80h]\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,qword ptr [rcx+rbx+90h]\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ rax_v70 (System.Int32)+58+v249 @ rbx_v3]");
													_ = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ rax_v70 (System.Int32)+58+v249 @ rbx_v3]");
													_ = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm15,xmm13\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm0\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm11,xmm8\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm6,xmm4\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ rax_v70 (System.Int32)+48+v249 @ rbx_v3]");
													obj = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm1,xmm1\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,qword ptr [rcx+rbx+50h]\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ rax_v70 (System.Int32)+48+v249 @ rbx_v3]");
													_ = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm14\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm14,qword ptr [rcx+rbx+60h]\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm2\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm14\"");
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-20]");
													_ = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ rax_v70 (System.Int32)+68+v249 @ rbx_v3]");
													_ = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ rax_v70 (System.Int32)+78+v249 @ rbx_v3]");
													_ = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v258 @ rax_v70 (System.Int32)+88+v249 @ rbx_v3]");
													_ = 0;
													int[] array8 = attributeIndexArr;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-60]");
													object obj38 = 0;
													num = array8[num5];
													InterpolateVertexAttributes(array8[num5], array8[num5], array8[obj38], (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference2), ref *(Vector3*)array7);
													int num32 = num8 << 7;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1559 @ rax_v75 (System.Int32)+99+v249 @ rbx_v3]");
													if ((nint)0 != 0)
													{
														num = -1;
													}
													ResizableArray<Ref> resizableArray3 = refs;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r13_v11 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+18]");
													num5 = 0;
													int num33 = num8 << 7;
													object obj39 = num33 + 32;
													UpdateTriangles(num8, num, ref *(Vertex*)(obj39 + obj3), (ResizableArray<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference2), ref *(int*)array7);
													int num34 = num9 << 7;
													object obj40 = num34 + 32;
													ref Vertex reference4 = ref *(Vertex*)(obj40 + obj3);
													UpdateTriangles(num8, num, ref reference4, (ResizableArray<bool>)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference2), ref *(int*)array7);
													ResizableArray<Ref> resizableArray4 = refs;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v172 @ rsi_v11 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+18]");
													nint num35 = 0;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r13_v11 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+18]");
													num9 = (int)(num35 - 0);
													int num36 = num8 << 7;
													int num37 = num9;
													Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v260 @ rax_v84 (System.Int32)+44+v249 @ rbx_v3]");
													if ((nint)num37 > (nint)0)
													{
														int num38 = num8 << 7;
														Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r13_v11 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+18]");
														_ = 0;
														num3 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference4);
														num6 = num;
														num7 = num8;
													}
													else
													{
														bool flag3 = num9 <= 0;
														num3 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference4);
														num6 = num;
														num7 = num8;
														if (!flag3)
														{
															ResizableArray<Ref> resizableArray5 = refs;
															int num39 = num8 << 7;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1630 @ r9_v34 (System.Int32)+40+v249 @ rbx_v3]");
															num3 = 0;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rcx_v48 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+10]");
															nint num40 = 0;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r13_v11 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+18]");
															nint num41 = 0;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rcx_v48 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+10]");
															nint num42 = 0;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1630 @ r9_v34 (System.Int32)+40+v249 @ rbx_v3]");
															Array.Copy((Array)num40, (int)num41, (Array)num42, 0, (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference2));
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v272 @ rcx_v48 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+10]");
															num6 = 0;
															Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ r13_v11 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+18]");
															num7 = 0;
														}
													}
													int num43 = num8 << 7;
													break;
												}
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+110]");
											obj5 = 0;
											num3 = num2;
											num6 = num4;
											num7 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref reference3);
											meshSimplifier = this;
										}
									}
								}
							}
							goto IL_0db0;
							IL_0db0:
							num6++;
							num3++;
							num5++;
							flag4 = num5 < 3;
							num2 = num3;
							num4 = num6;
							array2 = (int[])num7;
						}
						while (flag4);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+140]");
						object obj41 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+118]");
						object obj42 = 0 - obj41;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+120]");
						if ((nint)obj42 <= 0)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-58]");
						meshSimplifier = (MeshSimplifier)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+110]");
						obj5 = 0;
					}
				}
			}
			obj5++;
			meshSimplifier = (MeshSimplifier)(meshSimplifier + 1);
			meshSimplifier2 = meshSimplifier;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-28]");
		}
		while ((nint)meshSimplifier2 < 0);
	}

	private unsafe void UpdateMesh(int iteration)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0027: Expected O, but got I
		//IL_0046: Expected O, but got I
		//IL_00ad: Expected O, but got I4
		//IL_0210: Expected O, but got I
		//IL_00d1: Expected O, but got I
		//IL_00e7: Expected O, but got I
		//IL_00f1: Expected O, but got I4
		//IL_00fa: Expected O, but got I4
		//IL_0104: Expected O, but got I4
		//IL_1b99: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b9e: Expected O, but got Unknown
		//IL_1ba8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bad: Expected O, but got Unknown
		//IL_0966: Expected F8, but got I
		//IL_09a0: Expected O, but got I4
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Expected O, but got Unknown
		//IL_1bea: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bef: Expected O, but got Unknown
		//IL_1bf8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bfd: Expected O, but got Unknown
		//IL_1c06: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c0b: Expected O, but got Unknown
		//IL_029e: Expected O, but got I4
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Expected O, but got Unknown
		//IL_1275: Expected O, but got I
		//IL_1dfd: Expected I, but got O
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a63: Expected O, but got Unknown
		//IL_0a6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a71: Expected O, but got Unknown
		//IL_1290: Expected O, but got I
		//IL_02fa: Expected F8, but got I
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Expected O, but got Unknown
		//IL_035a: Expected O, but got I4
		//IL_12b8: Expected O, but got I
		//IL_0ba9: Expected I4, but got F8
		//IL_0bc8: Expected I, but got O
		//IL_0b70: Expected F8, but got I4
		//IL_1d8b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d90: Expected O, but got Unknown
		//IL_1e13: Expected O, but got I
		//IL_1fa8: Expected I, but got O
		//IL_0be8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bed: Expected I4, but got Unknown
		//IL_0b16: Expected O, but got I4
		//IL_0b1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b24: Expected O, but got Unknown
		//IL_13f9: Expected O, but got Ref
		//IL_03fd: Expected O, but got I
		//IL_0402: Expected I, but got O
		//IL_1462: Expected O, but got I
		//IL_1472: Expected O, but got I
		//IL_14a0: Expected O, but got I
		//IL_143b: Expected O, but got I4
		//IL_1444: Expected O, but got I4
		//IL_144d: Expected O, but got I4
		//IL_049a: Expected O, but got I
		//IL_049f: Expected I, but got O
		//IL_1521: Unknown result type (might be due to invalid IL or missing references)
		//IL_1526: Expected O, but got Unknown
		//IL_0caa: Expected F8, but got I
		//IL_0cba: Expected F8, but got I
		//IL_0900: Expected O, but got I
		//IL_04ef: Expected O, but got I
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Expected O, but got Unknown
		//IL_159b: Expected O, but got I
		//IL_09ec: Expected O, but got I
		//IL_2018: Expected O, but got Ref
		//IL_167d: Expected O, but got I
		//IL_0d11: Expected F8, but got I
		//IL_0d2f: Expected F8, but got I
		//IL_0d3f: Expected F8, but got I
		//IL_0d5a: Expected O, but got I4
		//IL_0941: Expected O, but got I
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Expected O, but got Unknown
		//IL_177f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1784: Expected O, but got Unknown
		//IL_081e: Expected O, but got I
		//IL_05ec: Expected O, but got I
		//IL_05a2: Expected O, but got I
		//IL_1895: Expected O, but got I
		//IL_05d7: Expected O, but got I
		//IL_18d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d9: Expected O, but got Unknown
		//IL_18ef: Expected O, but got I
		//IL_1906: Unknown result type (might be due to invalid IL or missing references)
		//IL_190b: Expected O, but got Unknown
		//IL_1942: Expected O, but got I
		//IL_0e5f: Expected F8, but got I
		//IL_0e6f: Expected O, but got I
		//IL_1d26: Expected O, but got I
		//IL_1c46: Expected O, but got Ref
		//IL_0898: Expected F8, but got I
		//IL_0653: Expected O, but got Ref
		//IL_0681: Expected O, but got I
		//IL_1984: Unknown result type (might be due to invalid IL or missing references)
		//IL_1989: Expected O, but got Unknown
		//IL_19a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_19a5: Expected O, but got Unknown
		//IL_19ea: Expected O, but got I
		//IL_0f1b: Expected I4, but got I8
		//IL_08bb: Expected F8, but got I
		//IL_0f74: Expected O, but got I
		//IL_0727: Expected O, but got I
		//IL_073d: Expected O, but got I
		//IL_1a35: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a3a: Expected O, but got Unknown
		//IL_1a51: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a56: Expected O, but got Unknown
		//IL_1a9b: Expected O, but got I
		//IL_1abe: Expected F8, but got I
		//IL_1f78: Expected O, but got I
		//IL_1f7d: Expected I, but got O
		//IL_1fe1: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fe6: Expected O, but got Unknown
		//IL_1b70: Expected F8, but got I
		//IL_1045: Expected O, but got I
		//IL_105b: Expected O, but got I
		//IL_106d: Expected I, but got O
		//IL_107b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1080: Expected O, but got Unknown
		//IL_109b: Expected I4, but got O
		//IL_10b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_10b7: Expected O, but got Unknown
		//IL_10c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cc: Expected O, but got Unknown
		//IL_10fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_10ff: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		ResizableArray<Triangle> resizableArray = triangles;
		_ = 0;
		_ = 0;
		ResizableArray<Vertex> resizableArray2 = vertices;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r15_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
		object obj3 = 0;
		ResizableArray<Triangle> resizableArray3 = triangles;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v82 @ rdi_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
		object obj4 = 0;
		ResizableArray<Vertex> resizableArray4 = vertices;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v584 @ rax_v7 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v585 @ rax_v8 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
		_ = 0;
		if (iteration > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v584 @ rax_v7 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
			bool flag = (nint)0 <= (nint)0;
			object obj5 = 0;
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r15_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
				object obj6 = (nint)0 + (nint)32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r15_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
				object obj7 = (nint)0 + (nint)32;
				object obj8 = 0;
				object obj9 = 0;
				object obj10 = 0;
				object obj11 = obj6;
				bool flag2;
				do
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v746 @ r10_v17+40]");
					if ((nint)0 == 0)
					{
						if (obj8 != obj10)
						{
							obj7 = obj11;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v806 @ rcx_v116+10]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v806 @ rcx_v116+20]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v806 @ rcx_v116+30]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v806 @ rcx_v116+40]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v806 @ rcx_v116+50]");
							_ = 0;
							obj7 = obj9;
						}
						obj9++;
						obj8++;
						obj7 += 96;
					}
					obj11 += 96;
					obj10++;
					object obj12 = obj10;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v584 @ rax_v7 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
					flag2 = (nint)obj12 < 0;
					obj6 = obj11;
					obj5 = obj9;
				}
				while (flag2);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
			UpdateReferences();
			return;
		}
		UpdateReferences();
		if (iteration != 0)
		{
			return;
		}
		ResizableArray<Ref> resizableArray5 = refs;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ r12_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+10]");
		object obj13 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v289 @ r12_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+10]");
		_ = 0;
		List<int> list = new List<int>(8);
		List<int> list2 = new List<int>(8);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-28]");
		double d;
		int num25;
		int num26;
		int num7;
		if ((nint)0 > (nint)0)
		{
			object obj14 = obj4 + 153;
			int num = 0;
			int num2;
			do
			{
				_ = 0;
				obj14 = 0;
				_ = 0;
				obj14 -= -128;
				num++;
				num2 = num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-28]");
			}
			while ((nint)num2 < (nint)0);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+18]");
			d = 0.0;
			object obj15 = obj4 + 68;
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,xmm9\"");
			int num3 = 0;
			int num4 = 0;
			List<int> list3 = list;
			double num5 = -1.7976931348623157E+308;
			double num6 = 1.7976931348623157E+308;
			object obj16 = 0;
			object obj17 = obj15;
			num7 = 0;
			nint num9 = default(nint);
			object obj33 = default(object);
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v660 @ r14_v32 (System.Collections.Generic.List`1<System.Int32>)+1C]");
				_ = (nint)0 + (nint)1;
				if (RuntimeHelpers.IsReferenceOrContainsReferences<int>())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v660 @ r14_v32 (System.Collections.Generic.List`1<System.Int32>)+18]");
					if ((nint)0 > (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v660 @ r14_v32 (System.Collections.Generic.List`1<System.Int32>)+10]");
						nint num8 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v660 @ r14_v32 (System.Collections.Generic.List`1<System.Int32>)+18]");
						Array.Clear((Array)num8, 0, 0);
						num9 = unchecked((nint)null);
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1626 @ rax_v12 (System.Collections.Generic.List`1<System.Int32>)+1C]");
				_ = (nint)0 + (nint)1;
				nint num10;
				if (!RuntimeHelpers.IsReferenceOrContainsReferences<int>())
				{
					num10 = num9;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1626 @ rax_v12 (System.Collections.Generic.List`1<System.Int32>)+18]");
					bool flag3 = (nint)0 <= (nint)0;
					num10 = num9;
					if (!flag3)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1626 @ rax_v12 (System.Collections.Generic.List`1<System.Int32>)+10]");
						nint num11 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1626 @ rax_v12 (System.Collections.Generic.List`1<System.Int32>)+18]");
						Array.Clear((Array)num11, 0, 0);
						num10 = unchecked((nint)null);
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-24]");
				bool flag4 = (nint)0 <= (nint)0;
				int num12 = num7;
				if (!flag4)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v780 @ rax_v108-4]");
					object obj18 = (nint)0 + (nint)4;
					object obj19 = obj18 * 8;
					object obj20 = obj13 + obj19;
					nint num13 = num10;
					int num14 = num7;
					int num15 = num7;
					bool flag9;
					do
					{
						obj = obj20;
						int num16 = num15;
						object obj21 = obj20;
						bool flag8;
						do
						{
							object obj22 = obj21 * 2;
							object obj23 = obj21 + obj22;
							object obj24 = obj23 << 5;
							object obj25;
							switch (num16)
							{
							case 0:
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2682 @ rax_v143+24+v83 @ r15_v4]");
								obj25 = 0;
								break;
							case 1:
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2682 @ rax_v143+28+v83 @ r15_v4]");
								obj25 = 0;
								break;
							default:
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2682 @ rax_v143+2C+v83 @ r15_v4]");
								obj25 = 0;
								break;
							}
							bool flag5 = num14 <= 0;
							int num17 = num15;
							if (!flag5)
							{
								bool flag7;
								do
								{
									object obj26 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 32));
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+20]");
									bool flag6 = 0 == (nint)obj25;
									num13 = 0;
									num15 = num17;
									if (flag6)
									{
										break;
									}
									num15 = num17 + 1;
									flag7 = num15 < num14;
									num13 = 0;
									num17 = num15;
								}
								while (flag7);
							}
							if (num15 != num14)
							{
								object obj27 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 36));
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
								int value = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+24]");
								object obj28 = (nint)0 + (nint)1;
								list.set_Item(num15, value);
								list3 = list;
								num13 = 0;
							}
							else
							{
								int item = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
								_ = 1;
								list.Add(item);
								int item2 = (int)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 64));
								list2.Add(item2);
								num14++;
								list3 = list;
							}
							obj21 = obj;
							num16++;
							flag8 = num16 < 3;
							num15 = 0;
						}
						while (flag8);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+28]");
						object obj29 = (nint)0 + (nint)1;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-8]");
						obj20 = (nint)0 + (nint)8;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-24]");
						flag9 = (nint)obj29 < 0;
						num10 = num13;
						num12 = num14;
						num7 = 0;
						num15 = 0;
					}
					while (flag9);
				}
				bool flag10 = num12 <= 0;
				num9 = num10;
				int num18 = num4;
				double num19 = num5;
				double num20 = num6;
				object obj30 = obj16;
				int num21 = num7;
				if (!flag10)
				{
					bool flag14;
					do
					{
						object obj31 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 56));
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-38]");
						bool flag11 = (nint)0 != 1;
						num9 = 0;
						if (!flag11)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
							num18++;
							object obj32 = obj33 << 7;
							_ = 1;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+190]");
							object obj34 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v783 @ rax_v125+14]");
							bool flag12 = (nint)0 == 0;
							num9 = 0;
							if (!flag12)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm8,qword ptr [rax+rdi+28h]\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ rdi_v4+18]");
								if ((nint)obj33 > 0)
								{
									object obj35 = obj33 << 7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2844 @ rax_v134+28+v318 @ rdi_v4]");
									num20 = 0.0;
								}
								object obj36 = obj33 << 7;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2601 @ rax_v130+28+v318 @ rdi_v4]");
								obj30 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm7\"");
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ rdi_v4+18]");
								bool flag13 = (nint)obj33 <= 0;
								num9 = 0;
								if (!flag13)
								{
									object obj37 = obj33 << 7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2600 @ rax_v132+28+v318 @ rdi_v4]");
									num19 = 0.0;
									num9 = 0;
								}
							}
						}
						num21++;
						flag14 = num21 < num12;
						num4 = num18;
						num5 = num19;
						num6 = num20;
						obj16 = obj30;
					}
					while (flag14);
				}
				int num22 = num3 + 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-18]");
				int num23 = (int)((nint)0 + (nint)1);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-10]");
				obj17 = --128;
				int num24 = num23;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-28]");
				if ((nint)num24 >= (nint)0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-30]");
				obj13 = 0;
				num3 = num22;
				obj15 = obj17;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+190]");
			MeshSimplifier meshSimplifier = (MeshSimplifier)0;
			num25 = num4;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-28]");
			num26 = 0;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+18]");
			d = 0.0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm9,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1A8]");
			_ = 0;
			double num5 = -1.7976931348623157E+308;
			double num6 = 1.7976931348623157E+308;
			object obj16 = 0;
			num25 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-28]");
			num26 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-28]");
			int num23 = 0;
			MeshSimplifier meshSimplifier = this;
			num7 = 0;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1709 @ rsi_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+14]");
		if ((nint)0 != 0)
		{
			BorderVertex[] array = new BorderVertex[num25];
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm7,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-28]");
			bool flag15 = (nint)0 <= (nint)0;
			int num27 = num7;
			if (!flag15)
			{
				object obj38 = obj4 + 40;
				object obj39 = array + 32;
				int num28 = num7;
				int num29 = num7;
				object obj40 = obj38;
				int num30 = num7;
				bool flag16;
				do
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v784 @ rax_v98+70]");
					if ((nint)0 != 0)
					{
						object obj16 = obj40;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm8\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,xmm7\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,xmm0\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,xmm1\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm6\"");
						num28++;
						obj39 = num30;
						obj39 += 8;
					}
					obj40 -= -128;
					num30++;
					num29++;
					flag16 = num29 < num26;
					num27 = num28;
					double num31 = 1.0;
					obj38 = obj40;
				}
				while (flag16);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180689FD0");
			nint num32 = (nint)typeof(Math);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1974 @ rcx_v44 (Il2CppClass<System.Math>)+E4]");
			double num33;
			if ((nint)0 <= (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtsd xmm0,xmm9\"");
				num33 = 0.0;
			}
			else
			{
				num33 = Math.Sqrt(d);
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm0,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm6\"");
			int num34 = Math.Max((int)num33, 1);
			bool flag17 = num27 <= 0;
			nint num9 = (nint)BorderVertexComparer.instance;
			int num23 = 0;
			if (!flag17)
			{
				num23 = array + 32;
				int num35 = num7;
				nint num36 = num7;
				int num37 = num27;
				double num38 = 2147483647.0;
				nint num39 = 0;
				num9 = num7;
				int num40 = num34;
				int num41 = num7;
				int num42 = num34;
				int num43 = num27;
				object obj41 = array;
				object obj43 = default(object);
				double num53 = default(double);
				double num55 = default(double);
				bool flag20;
				do
				{
					if (((int*)num23)->m_value != -1)
					{
						int num44 = num41 + 1;
						int num45 = ((int*)num23)->m_value << 7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ rax_v63 (System.Int32)+28+v318 @ rdi_v4]");
						num33 = 0.0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ rax_v63 (System.Int32)+38+v318 @ rdi_v4]");
						double num31 = 0.0;
						num42 = (int)(num9 + 1);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ rax_v63 (System.Int32)+28+v318 @ rdi_v4]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2379 @ rax_v63 (System.Int32)+38+v318 @ rdi_v4]");
						_ = 0;
						if (num42 < num27)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+18]");
							num38 = 0.0;
							int num46 = num23 + 8;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+10]");
							double num5 = 0.0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+8]");
							double num6 = 0.0;
							int num47 = ((int*)num23)->m_value << 7;
							obj = num47;
							int num48 = num44;
							int num49 = num42;
							int num50 = num27;
							object obj42;
							double num52;
							double num54;
							IntPtr intPtr;
							double num56;
							double num57;
							int num58;
							int num59;
							int num60;
							object obj44;
							bool flag19;
							do
							{
								num7 = ((int*)num46)->m_value;
								if (((int*)num46)->m_value != -1)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2850 @ r14_v23 (System.Int32)+4]");
									nint num51 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2646 @ r8_v30 (System.Int32)+4]");
									num42 = (int)(num51 - 0);
									bool flag18 = num42 > num40;
									obj42 = obj43;
									num52 = num53;
									num54 = num55;
									num37 = num46;
									intPtr = num39;
									num56 = num31;
									num57 = num33;
									num58 = num40;
									num59 = num41;
									num60 = num23;
									obj44 = obj41;
									if (flag18)
									{
										break;
									}
									int num61 = ((int*)num46)->m_value << 7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v787 @ rax_v75 (System.Int32)+28+v318 @ rdi_v4]");
									num33 = 0.0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v787 @ rax_v75 (System.Int32)+38+v318 @ rdi_v4]");
									obj43 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm4,xmm0\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v787 @ rax_v75 (System.Int32)+38+v318 @ rdi_v4]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm0,xmm0\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm2\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,xmm0\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm4,xmm4\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm3\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm1\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm3\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,xmm1\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm9,xmm4\"");
									int value2 = ((int*)num46)->m_value;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v318 @ rdi_v4+18]");
									if ((nint)value2 >= (nint)0)
									{
										((int*)num46)->m_value = -1;
										object obj45 = obj;
										_ = 0;
										int num62 = ((int*)num46)->m_value << 7;
										_ = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+190]");
										if (!((MeshSimplifier)0).AreUVsTheSame(0, ((int*)num23)->m_value, ((int*)num46)->m_value))
										{
											_ = 1;
											int num63 = ((int*)num46)->m_value << 7;
											_ = 1;
										}
										else
										{
											_ = 1;
											int num64 = ((int*)num46)->m_value << 7;
											_ = 1;
										}
										int num65 = num7 << 7;
										int num66 = num7 << 7;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2943 @ rax_v85 (System.Int32)+44+v318 @ rdi_v4]");
										_ = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2943 @ rax_v85 (System.Int32)+44+v318 @ rdi_v4]");
										if ((nint)0 > (nint)0)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-30]");
											object obj46 = (nint)0 + (nint)32;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2941 @ rax_v83 (System.Int32)+40+v318 @ rdi_v4]");
											object obj47 = (nint)0 * (nint)8;
											object obj48 = obj46 + obj47;
											nint num67 = unchecked((nint)null);
											nint num68;
											do
											{
												object obj49 = obj48 * 2;
												object obj50 = obj48 + obj49;
												int index = obj48 >> 32;
												object obj51 = obj50 << 5;
												object obj52 = obj51 + 32;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r15_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
												Triangle triangle = (Triangle)(obj52 + 0);
												((Triangle*)triangle)->set_Item(index, ((int*)num23)->m_value);
												num67++;
												obj48 += 8;
												num68 = num67;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-8]");
											}
											while (num68 < 0);
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-18]");
											num46 = 0;
										}
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-38]");
										num23 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-10]");
										obj41 = 0;
										num39 = unchecked((nint)null);
										num40 = num34;
										num41 = num35;
										num44 = num48;
									}
									num53 = num5;
									num55 = num6;
									num31 = num38;
									num50 = num27;
								}
								num44++;
								num7 = num49 + 1;
								num37 = num46 + 8;
								flag19 = num7 < num50;
								obj42 = obj43;
								num52 = num53;
								num54 = num55;
								intPtr = num39;
								num56 = num31;
								num57 = num33;
								num58 = num40;
								num59 = num41;
								num60 = num23;
								num42 = num50;
								obj44 = obj41;
								num48 = num44;
								num46 = num37;
							}
							while (flag19);
							obj43 = obj42;
							num53 = num52;
							num55 = num54;
							num39 = intPtr;
							num31 = num56;
							num33 = num57;
							num9 = num36;
							num40 = num58;
							num41 = num59;
							num23 = num60;
							obj41 = obj44;
						}
						num43 = num27;
					}
					num41++;
					num9++;
					num23 += 8;
					flag20 = num9 < num43;
					num35 = num41;
					num36 = num9;
				}
				while (flag20);
				num7 = 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+190]");
			((MeshSimplifier)0).UpdateReferences();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-28]");
		if ((nint)0 > (nint)0)
		{
			int num69 = num7;
			int num71;
			do
			{
				int num70 = num69 << 7;
				num69++;
				_ = 0;
				_ = 0;
				_ = 0;
				_ = 0;
				_ = 0;
				num71 = num69;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-28]");
			}
			while ((nint)num71 < (nint)0);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1A8]");
		if ((nint)0 <= (nint)0)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r15_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
		object obj53 = (nint)0 + (nint)44;
		int num73;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rsi_v7-8]");
			object obj54 = (nint)0 << 7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v812 @ rcx_v17+38+v318 @ rdi_v4]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rsi_v7-4]");
			object obj55 = (nint)0 << 7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v791 @ rax_v20+38+v318 @ rdi_v4]");
			_ = 0;
			object obj56 = obj53 << 7;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2121 @ rax_v22+38+v318 @ rdi_v4]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v812 @ rcx_v17+38+v318 @ rdi_v4]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm3,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm5,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm4,qword ptr [rbp+58h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm7,xmm7\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm2,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm6,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm8,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm8,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm10,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm7,xmm10\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm2,xmm5\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm3,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm1,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm7,xmm5\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm7,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm1\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+A0]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2121 @ rax_v22+28+v318 @ rdi_v4]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2121 @ rax_v22+28+v318 @ rdi_v4]");
			_ = 0;
			nint num72 = (nint)typeof(Vector3d);
			object obj57 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 8));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BAF40");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,qword ptr [182206DA0h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2189 @ rcx_v20 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.Vector3d>)+E4]");
			object obj60;
			if ((nint)0 <= (nint)0)
			{
				object obj58 = 0;
				object obj59 = 0;
				obj60 = 0;
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+10]");
				object obj59 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+18]");
				object obj58 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm3,xmm0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm2,xmm0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"divsd xmm6,xmm0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+A0]");
				obj60 = 0;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+18]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm12,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm10\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm1,xmm11\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm13,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm14,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm15,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm12,xmm1\"");
			object obj61 = obj60;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206EE0]");
			object obj62 = obj61 ^ 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm3\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm0,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm12,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rsi_v7-8]");
			object obj63 = (nint)0 << 7;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,qword ptr [rax+rdi+90h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm10,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,qword ptr [rax+rdi+50h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm8,qword ptr [rax+rdi+60h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm7,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,qword ptr [rax+rdi+70h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm10,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,qword ptr [rax+rdi+80h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm7,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm5,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-38]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm3,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rsi_v7-4]");
			object obj64 = (nint)0 << 7;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,qword ptr [rax+rdi+90h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm10,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,qword ptr [rax+rdi+50h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm1,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm8,qword ptr [rax+rdi+60h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm7,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,qword ptr [rax+rdi+70h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm10,xmm9\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,xmm0\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,qword ptr [rax+rdi+80h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm7,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-30]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm5,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-38]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm3,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-40]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1A8]");
			int num23 = 0;
			num73 = num7 + 1;
			object obj65 = obj53 << 7;
			obj53 += 96;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm10,xmm14\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm2,xmm12\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm11,xmm13\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm9,xmm15\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm11,xmm10\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm8,qword ptr [rsp+48h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm7,qword ptr [rbp-30h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,qword ptr [rsp+40h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm5,qword ptr [rbp-38h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm4,qword ptr [rbp-20h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm9,xmm8\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm3,qword ptr [rbp-40h]\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm7,xmm6\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1932 @ rax_v32+48+v318 @ rdi_v4]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm5,xmm4\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm3,xmm2\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1932 @ rax_v32+58+v318 @ rdi_v4]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1932 @ rax_v32+68+v318 @ rdi_v4]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1932 @ rax_v32+78+v318 @ rdi_v4]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1932 @ rax_v32+88+v318 @ rdi_v4]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1A8]");
		}
		while ((nint)num73 < (nint)0);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v39 @ r15_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
		object obj66 = (nint)0 + (nint)32;
		int num74 = num7;
		int num75 = num7;
		int num83;
		do
		{
			object obj67 = obj66 >> 32;
			object obj68 = obj67 << 7;
			object obj69 = obj4 + 32;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v869 @ rsi_v10+8]");
			object obj70 = (nint)0 << 7;
			ref Vector3d result = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3d>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
			object obj71 = obj68 + 32;
			ref Vertex vert = ref *(Vertex*)(obj69 + obj70);
			ref Vertex vert2 = ref *(Vertex*)(obj71 + obj4);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+190]");
			double num76 = ((MeshSimplifier)0).CalculateError(ref vert2, ref vert, out result);
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
			object obj72 = obj66 >> 32;
			object obj73 = obj72 << 7;
			object obj74 = obj4 + 32;
			object obj75 = obj66 << 7;
			object obj76 = obj4 + 32;
			ref Vertex vert3 = ref *(Vertex*)(obj74 + obj73);
			ref Vector3d result2 = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3d>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
			ref Vertex vert4 = ref *(Vertex*)(obj76 + obj75);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+190]");
			double num77 = ((MeshSimplifier)0).CalculateError(ref vert4, ref vert3, out result2);
			object obj77 = obj66 >> 32;
			object obj78 = obj66 >> 32;
			object obj79 = obj77 << 7;
			object obj80 = obj4 + 32;
			object obj81 = obj78 << 7;
			object obj82 = obj4 + 32;
			ref Vertex vert5 = ref *(Vertex*)(obj80 + obj79);
			ref Vector3d result3 = ref System.Runtime.CompilerServices.Unsafe.As<object, Vector3d>(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 48));
			ref Vertex vert6 = ref *(Vertex*)(obj82 + obj81);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+190]");
			double num78 = ((MeshSimplifier)0).CalculateError(ref vert6, ref vert5, out result3);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v869 @ rsi_v10+20]");
			double num79 = 0.0;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm2,xmm1\"");
			int num80 = num75;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ r15_v4+18]");
			if ((nint)num80 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm1\"");
				int num81 = num75;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ r15_v4+18]");
				if ((nint)num81 <= (nint)0)
				{
					goto IL_1b1d;
				}
			}
			else
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm0,xmm2\"");
				int num82 = num75;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ r15_v4+18]");
				if ((nint)num82 <= (nint)0)
				{
					goto IL_1b1d;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v869 @ rsi_v10+28]");
				num79 = 0.0;
			}
			goto IL_1fb7;
			IL_1fb7:
			num75++;
			num74++;
			obj66 += 96;
			num83 = num74;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1A8]");
			continue;
			IL_1b1d:
			num79 = num78;
			goto IL_1fb7;
		}
		while ((nint)num83 < (nint)0);
	}

	private void UpdateReferences()
	{
		//IL_0024: Expected O, but got I
		//IL_023e: Expected O, but got I4
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		//IL_0063: Expected O, but got I4
		//IL_00fc: Expected O, but got I
		//IL_0105: Expected O, but got I4
		//IL_0304: Expected O, but got I
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Expected O, but got Unknown
		//IL_0263: Expected O, but got I4
		//IL_026c: Expected O, but got I4
		//IL_0121: Expected O, but got I
		//IL_0137: Expected O, but got I
		//IL_014d: Expected O, but got I
		//IL_007d: Expected O, but got I4
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_0189: Expected O, but got I
		//IL_0342: Expected O, but got I
		//IL_034b: Expected O, but got I4
		//IL_0354: Expected O, but got I4
		//IL_0293: Expected O, but got I4
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Expected O, but got Unknown
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Expected O, but got Unknown
		//IL_01aa: Expected O, but got I
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Expected O, but got Unknown
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Expected O, but got Unknown
		//IL_01dc: Expected O, but got I
		//IL_01f2: Expected O, but got I
		//IL_036f: Expected O, but got I
		//IL_0385: Expected O, but got I
		//IL_039b: Expected O, but got I
		//IL_03b1: Expected O, but got I
		//IL_03d1: Expected O, but got I
		//IL_03e7: Expected O, but got I
		//IL_03fd: Expected O, but got I
		//IL_0413: Expected O, but got I
		//IL_045d: Expected O, but got I
		//IL_0484: Expected O, but got I
		//IL_04b1: Expected O, but got I
		//IL_04de: Expected O, but got I
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Expected O, but got Unknown
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Expected O, but got Unknown
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Expected O, but got Unknown
		ResizableArray<Triangle> resizableArray = triangles;
		ResizableArray<Vertex> resizableArray2 = vertices;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rdx_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rdx_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
		if ((nint)0 > (nint)0)
		{
			object obj2 = obj + 68;
			object obj3 = 0;
			object obj4;
			do
			{
				_ = 0;
				obj2 = 0;
				obj2 -= -128;
				obj3++;
				obj4 = obj3;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rdx_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
			}
			while ((nint)obj4 < 0);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
			object obj5 = (nint)0 + (nint)40;
			object obj6 = 0;
			object obj16;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ rdx_v15-4]");
				object obj7 = (nint)0 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ rdx_v15-4]");
				object obj8 = (nint)0 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v722 @ rax_v43+44+v103 @ rbx_v3]");
				object obj9 = (nint)0 + (nint)1;
				object obj10 = obj5 << 7;
				object obj11 = obj5 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v761 @ rax_v47+44+v103 @ rbx_v3]");
				object obj12 = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ rdx_v15+4]");
				object obj13 = (nint)0 << 7;
				obj6++;
				obj5 += 96;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v118 @ rdx_v15+4]");
				object obj14 = (nint)0 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v765 @ rax_v51+44+v103 @ rbx_v3]");
				object obj15 = (nint)0 + (nint)1;
				obj16 = obj6;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
			}
			while ((nint)obj16 < 0);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rdx_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
		bool flag = (nint)0 <= (nint)0;
		object obj17 = 0;
		if (!flag)
		{
			object obj18 = obj + 68;
			object obj19 = 0;
			object obj20 = 0;
			bool flag2;
			do
			{
				obj17 = obj20 + obj18;
				obj18 = 0;
				obj19++;
				obj18 -= -128;
				object obj21 = obj19;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v56 @ rdx_v3 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
				flag2 = (nint)obj21 < 0;
				obj20 = obj17;
			}
			while (flag2);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
		ResizableArray<Ref> resizableArray3 = refs;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v85 @ r9_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Ref>)+10]");
		object obj22 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
		if ((nint)0 > (nint)0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
			object obj23 = (nint)0 + (nint)44;
			object obj24 = 0;
			object obj25 = 0;
			object obj41;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r8_v9-8]");
				object obj26 = (nint)0 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r8_v9-8]");
				object obj27 = (nint)0 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r8_v9-8]");
				object obj28 = (nint)0 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v772 @ rax_v17+44+v103 @ rbx_v3]");
				object obj29 = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r8_v9-4]");
				object obj30 = (nint)0 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r8_v9-4]");
				object obj31 = (nint)0 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v98 @ r8_v9-4]");
				object obj32 = (nint)0 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v777 @ rax_v23+44+v103 @ rbx_v3]");
				object obj33 = (nint)0 + (nint)1;
				object obj34 = obj23 << 7;
				object obj35 = obj23 << 7;
				object obj36 = obj23 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v782 @ rax_v29+44+v103 @ rbx_v3]");
				object obj37 = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v772 @ rax_v17+44+v103 @ rbx_v3]");
				nint num = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v770 @ rax_v15+40+v103 @ rbx_v3]");
				object obj38 = num + 0;
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v777 @ rax_v23+44+v103 @ rbx_v3]");
				nint num2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v775 @ rax_v21+40+v103 @ rbx_v3]");
				object obj39 = num2 + 0;
				_ = 1;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v782 @ rax_v29+44+v103 @ rbx_v3]");
				nint num3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v780 @ rax_v27+40+v103 @ rbx_v3]");
				object obj40 = num3 + 0;
				obj24++;
				obj23 += 96;
				obj25++;
				_ = 2;
				obj41 = obj24;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v29 @ rax_v2 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
			}
			while ((nint)obj41 < 0);
		}
	}

	private unsafe void CompactMesh()
	{
		//IL_0008: Expected O, but got Ref
		//IL_0027: Expected O, but got I
		//IL_0037: Expected O, but got I
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Expected O, but got Unknown
		//IL_009b: Expected O, but got I4
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Expected O, but got Unknown
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Expected O, but got Unknown
		//IL_0134: Expected O, but got I
		//IL_01cd: Expected O, but got I
		//IL_01f6: Expected O, but got I
		//IL_025c: Expected O, but got I8
		//IL_1397: Unknown result type (might be due to invalid IL or missing references)
		//IL_139c: Expected O, but got Unknown
		//IL_0292: Expected O, but got I
		//IL_02a4: Expected O, but got I8
		//IL_02ae: Expected O, but got I4
		//IL_0904: Expected O, but got I
		//IL_087e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0883: Expected O, but got Unknown
		//IL_088c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Expected O, but got Unknown
		//IL_02cb: Expected O, but got I
		//IL_121c: Expected I4, but got O
		//IL_13d0: Expected O, but got I
		//IL_13da: Unknown result type (might be due to invalid IL or missing references)
		//IL_13df: Expected O, but got Unknown
		//IL_13e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13ed: Expected O, but got Unknown
		//IL_093f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Expected O, but got Unknown
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Expected O, but got Unknown
		//IL_0960: Unknown result type (might be due to invalid IL or missing references)
		//IL_0965: Expected O, but got Unknown
		//IL_0978: Unknown result type (might be due to invalid IL or missing references)
		//IL_097d: Expected O, but got Unknown
		//IL_098b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0990: Expected O, but got Unknown
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_099e: Expected O, but got Unknown
		//IL_09b9: Unsupported input type for neg.
		//IL_09b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09be: Expected O, but got Unknown
		//IL_09cc: Expected O, but got I4
		//IL_09d5: Expected O, but got I4
		//IL_09ed: Expected I4, but got O
		//IL_09fe: Expected O, but got I4
		//IL_0a0f: Expected O, but got I4
		//IL_0a17: Expected I4, but got O
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a9: Expected O, but got Unknown
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b7: Expected O, but got Unknown
		//IL_0333: Expected O, but got I
		//IL_085b: Expected O, but got I
		//IL_086b: Expected O, but got I
		//IL_0e44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e49: Expected O, but got Unknown
		//IL_0e53: Expected O, but got I4
		//IL_1558: Unknown result type (might be due to invalid IL or missing references)
		//IL_155d: Expected O, but got Unknown
		//IL_1586: Expected O, but got I
		//IL_158f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1594: Expected O, but got Unknown
		//IL_15a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_15a7: Expected O, but got Unknown
		//IL_15b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_15ba: Expected O, but got Unknown
		//IL_0372: Expected O, but got I
		//IL_1473: Expected O, but got I
		//IL_149f: Expected O, but got I
		//IL_0e2e: Expected O, but got I
		//IL_160d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1612: Expected O, but got Unknown
		//IL_161b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1620: Expected O, but got Unknown
		//IL_1629: Unknown result type (might be due to invalid IL or missing references)
		//IL_162e: Expected O, but got Unknown
		//IL_163c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1641: Expected O, but got Unknown
		//IL_1888: Expected O, but got I
		//IL_1891: Unknown result type (might be due to invalid IL or missing references)
		//IL_1896: Expected O, but got Unknown
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Expected O, but got Unknown
		//IL_0542: Expected O, but got I
		//IL_0482: Expected O, but got I
		//IL_1427: Expected O, but got I
		//IL_143c: Expected O, but got I
		//IL_0eab: Expected O, but got I
		//IL_0ece: Expected O, but got I
		//IL_0aa9: Expected O, but got I
		//IL_0ac3: Expected O, but got I
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Expected O, but got Unknown
		//IL_14d3: Expected O, but got I
		//IL_14e3: Expected O, but got I
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Expected O, but got Unknown
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Expected O, but got Unknown
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Expected O, but got Unknown
		//IL_0f04: Expected O, but got I
		//IL_0f0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f12: Expected O, but got Unknown
		//IL_0f5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f61: Expected O, but got Unknown
		//IL_14fe: Expected O, but got I
		//IL_1513: Expected O, but got I
		//IL_1523: Expected O, but got I
		//IL_04f0: Expected O, but got I
		//IL_0b25: Expected O, but got I
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Expected O, but got Unknown
		//IL_0b41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b46: Expected O, but got Unknown
		//IL_0b4f: Expected O, but got I4
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Expected O, but got Unknown
		//IL_1147: Unknown result type (might be due to invalid IL or missing references)
		//IL_114c: Expected O, but got Unknown
		//IL_1155: Expected O, but got I4
		//IL_115e: Expected O, but got I4
		//IL_0bda: Expected O, but got I
		//IL_0be3: Expected O, but got I4
		//IL_0d47: Expected O, but got I
		//IL_0c63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c68: Expected O, but got Unknown
		//IL_0c71: Expected O, but got I4
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Expected O, but got Unknown
		//IL_0d22: Expected O, but got I
		//IL_080c: Expected O, but got I
		//IL_081c: Expected O, but got I
		//IL_083e: Expected O, but got I
		//IL_078e: Expected O, but got I
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Expected O, but got Unknown
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Expected O, but got Unknown
		//IL_1179: Expected O, but got I4
		//IL_1182: Expected O, but got I4
		//IL_1789: Expected O, but got I
		//IL_1799: Expected O, but got I
		//IL_0d64: Expected O, but got I
		//IL_0d74: Expected O, but got I
		//IL_16c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16c7: Expected O, but got Unknown
		//IL_16d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d5: Expected O, but got Unknown
		//IL_1538: Expected O, but got I
		//IL_17e0: Expected O, but got I
		//IL_0d98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9d: Expected O, but got Unknown
		//IL_0da6: Expected O, but got I4
		//IL_1703: Unknown result type (might be due to invalid IL or missing references)
		//IL_1708: Expected O, but got Unknown
		//IL_1711: Unknown result type (might be due to invalid IL or missing references)
		//IL_1716: Expected O, but got Unknown
		//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Expected O, but got Unknown
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Expected O, but got Unknown
		//IL_1744: Unknown result type (might be due to invalid IL or missing references)
		//IL_1749: Expected O, but got Unknown
		//IL_1752: Unknown result type (might be due to invalid IL or missing references)
		//IL_1757: Expected O, but got Unknown
		//IL_1239: Expected O, but got I
		//IL_1242: Unknown result type (might be due to invalid IL or missing references)
		//IL_1247: Expected O, but got Unknown
		//IL_1250: Unknown result type (might be due to invalid IL or missing references)
		//IL_1255: Expected O, but got Unknown
		//IL_1265: Expected O, but got I
		//IL_0cab: Expected O, but got I
		//IL_0cbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc0: Expected O, but got Unknown
		//IL_11be: Expected O, but got I
		//IL_0e11: Expected O, but got I
		//IL_0ce3: Expected O, but got I
		//IL_0de9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dee: Expected O, but got Unknown
		//IL_0df7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfc: Expected O, but got Unknown
		//IL_11f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_11fe: Expected O, but got Unknown
		//IL_1207: Unknown result type (might be due to invalid IL or missing references)
		//IL_120c: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = (object)(&obj2);
		ResizableArray<Vertex> resizableArray = vertices;
		_ = 0;
		ResizableArray<Vertex> resizableArray2 = vertices;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v40 @ rsi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
		object obj4 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
		_ = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v69 @ rax_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
		if ((nint)0 > (nint)0)
		{
			object obj5 = obj3 + 68;
			Vector2[][] array = null;
			do
			{
				obj5 = 0;
				obj5 -= -128;
				array = (Vector2[][])(array + 1);
			}
			while (System.Runtime.CompilerServices.Unsafe.As<Vector2[][], UIntPtr>(ref array) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4));
		}
		if (vertNormals != null)
		{
			ResizableArray<Vector3> resizableArray3 = vertNormals;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v879 @ rax_v186 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector3>)+10]");
			_ = 0;
		}
		else
		{
			_ = 0;
		}
		Vector2[][] array2;
		if (vertTangents != null)
		{
			ResizableArray<Vector4> resizableArray4 = vertTangents;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1604 @ rax_v184 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector4>)+10]");
			array2 = (Vector2[][])0;
		}
		else
		{
			array2 = null;
		}
		Vector2[][] array3;
		if (vertUV2D != null)
		{
			Vector2[][] data = vertUV2D.Data;
			array3 = data;
		}
		else
		{
			array3 = null;
		}
		if (vertUV3D != null)
		{
			Vector3[][] data2 = vertUV3D.Data;
		}
		else
		{
			_ = 0;
		}
		Vector4[][] array4;
		if (vertUV4D != null)
		{
			Vector4[][] data3 = vertUV4D.Data;
			array4 = data3;
		}
		else
		{
			array4 = null;
		}
		Vector2[][] array5;
		if (vertColors != null)
		{
			ResizableArray<Color> resizableArray5 = vertColors;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1687 @ rax_v179 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Color>)+10]");
			array5 = (Vector2[][])0;
		}
		else
		{
			array5 = null;
		}
		Vector2[][] array6;
		if (vertBoneWeights != null)
		{
			ResizableArray<BoneWeight> resizableArray6 = vertBoneWeights;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1706 @ rax_v177 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.BoneWeight>)+10]");
			array6 = (Vector2[][])0;
		}
		else
		{
			array6 = null;
		}
		Vector2[][] array7;
		if (blendShapes != null)
		{
			ResizableArray<BlendShapeContainer> resizableArray7 = blendShapes;
			array7 = (Vector2[][])(object)resizableArray7.items;
		}
		else
		{
			array7 = null;
		}
		int[] array8 = new int[subMeshCount];
		subMeshOffsets = array8;
		ResizableArray<Triangle> resizableArray8 = triangles;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ rdx_v85 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
		bool flag = (nint)0 <= (nint)0;
		Vector2[][] array9 = null;
		Vector2[][] array10 = null;
		object obj6 = 4294967295L;
		MeshSimplifier meshSimplifier = this;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ rdx_v85 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v335 @ rdx_v85 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
			object obj7 = (nint)0 + (nint)32;
			Vector2[][] array11 = null;
			obj6 = 4294967295L;
			object obj8 = 0;
			Vector2[][] array13;
			do
			{
				object obj9 = obj7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
				object obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+40]");
				if ((nint)0 == 0)
				{
					object obj11 = obj7 >> 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
					object obj12 = (nint)0 >> 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
					_ = 0;
					Vector2[][] array12;
					if (obj12 != obj11)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
						object obj13 = (nint)0 >> 32;
						object obj14 = obj7 >> 32;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
						_ = 0;
						object obj15 = obj14 << 7;
						object obj16 = obj13 << 7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1011 @ rax_v167+28+v70 @ rsi_v3]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1011 @ rax_v167+38+v70 @ rsi_v3]");
						_ = 0;
						if (array6 != null)
						{
							object obj17 = obj14 + 1;
							object obj18 = obj17 << 5;
							object obj19 = obj13 + 1;
							object obj20 = obj19 << 5;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1034 @ rcx_v101+v306 @ stack_-228_v3 (UnityEngine.Vector2[][])]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1034 @ rcx_v101+10+v306 @ stack_-228_v3 (UnityEngine.Vector2[][])]");
							_ = 0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
						object obj21 = (nint)0 >> 32;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+60]");
						obj9 = 0;
						array12 = array6;
					}
					else
					{
						array12 = array6;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm1,8\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
					bool flag2 = 0 == (nint)obj9;
					object obj22 = obj9;
					if (!flag2)
					{
						object obj23 = obj9 << 7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
						object obj24 = (nint)0 << 7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1013 @ rax_v159+28+v70 @ rsi_v3]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1013 @ rax_v159+38+v70 @ rsi_v3]");
						_ = 0;
						if (array12 != null)
						{
							object obj25 = obj9 + 1;
							object obj26 = obj25 << 5;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
							object obj27 = (nint)0 + (nint)1;
							object obj28 = obj27 << 5;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v993 @ rdx_v98+v186 @ r10_v19 (UnityEngine.Vector2[][])]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v993 @ rdx_v98+10+v186 @ r10_v19 (UnityEngine.Vector2[][])]");
							_ = 0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+60]");
						obj9 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
						obj22 = 0;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm0,8\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
					object obj29 = (nint)0 >> 32;
					object obj30 = obj9 >> 32;
					bool flag3 = obj29 == obj30;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
					object obj31 = 0;
					object obj32 = obj9;
					if (!flag3)
					{
						object obj33 = obj9 >> 32;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
						object obj34 = (nint)0 >> 32;
						object obj35 = obj33 << 7;
						object obj36 = obj34 << 7;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1015 @ rax_v146+28+v70 @ rsi_v3]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1015 @ rax_v146+38+v70 @ rsi_v3]");
						_ = 0;
						if (array6 != null)
						{
							object obj37 = obj33 + 1;
							object obj38 = obj37 << 5;
							object obj39 = (object)array6 + obj38;
							object obj40 = obj34 + 1;
							object obj41 = obj40 << 5;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v994 @ rdx_v94+10]");
							_ = 0;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
						obj31 = (nint)0 >> 32;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+68]");
						obj32 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+60]");
						obj9 = 0;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+170]");
					object obj42 = (nint)0 + (nint)1;
					object obj43 = obj8 + 1;
					object obj44 = obj8 * 2;
					object obj45 = obj8 + obj44;
					object obj46 = obj45 << 5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+20]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+30]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+40]");
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+50]");
					_ = 0;
					object obj47 = obj8 * 2;
					object obj48 = obj8 + obj47;
					object obj49 = obj48 << 5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+170]");
					_ = 0;
					object obj50 = obj9 >> 32;
					object obj51 = obj50 << 7;
					_ = 1;
					object obj52 = obj22 << 7;
					resizableArray8 = (ResizableArray<Triangle>)(obj32 >> 32);
					_ = 1;
					object obj53 = (object)resizableArray8 << 7;
					_ = 1;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
					bool flag4 = 0 <= (nint)obj6;
					obj8 = obj43;
					if (!flag4)
					{
						object obj54 = obj6 + 1;
						object obj55 = obj54;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
						if ((nint)obj55 < 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+160]");
							object obj56 = 0;
							object obj57 = obj54 * 4;
							ResizableArray<Triangle> resizableArray9 = (ResizableArray<Triangle>)(obj57 + 32);
							object obj59;
							do
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v149 @ rbx_v38+40]");
								object obj58 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+170]");
								_ = 0;
								obj54++;
								resizableArray8 = (ResizableArray<Triangle>)(resizableArray9 + 4);
								obj59 = obj54;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
							}
							while ((nint)obj59 < 0);
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+160]");
						object obj60 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2616 @ rax_v140+40]");
						obj53 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+170]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v265 @ r9_v45+10]");
						obj6 = 0;
						obj8 = obj43;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+170]");
				array9 = (Vector2[][])0;
				array11 = (Vector2[][])(array11 + 1);
				obj7 += 96;
				array13 = array11;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-78]");
			}
			while ((nint)array13 < 0);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+178]");
			obj4 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+160]");
			meshSimplifier = (MeshSimplifier)0;
			array10 = null;
		}
		object obj61 = obj6 + 1;
		if ((nint)obj61 < meshSimplifier.subMeshCount)
		{
			object obj62 = obj61 * 4;
			object obj63 = obj62 + 32;
			do
			{
				int[] array14 = meshSimplifier.subMeshOffsets;
				obj61++;
				obj63 += 4;
			}
			while ((nint)obj61 < meshSimplifier.subMeshCount);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
		ResizableArray<Triangle> resizableArray10 = meshSimplifier.triangles;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rax_v22 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
		object obj64 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v381 @ rax_v22 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
		_ = 0;
		int num;
		Vector2[][] array19;
		if ((nint)obj4 > 0)
		{
			object obj65 = obj3 + 40;
			obj6 = obj3 + 32;
			object obj66 = array6 + 32;
			object obj67 = array6 + 32;
			object obj68 = array5 + 32;
			object obj69 = array5 + 32;
			object obj70 = (object)array2 - (object)array5;
			object obj71 = 0 - array5;
			object obj72 = 32;
			object obj73 = 32;
			Vector2[][] array15 = array10;
			Vector2[][] array16 = array10;
			num = (int)array10;
			Vector2[][] array17 = array10;
			object obj74 = 32;
			Vector2[][] array18 = array10;
			object obj75 = 32;
			int num2 = (int)array10;
			object obj76 = obj69;
			bool flag5;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm2,4\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ rbx_v9+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ rbx_v9+20]");
				if ((nint)0 > (nint)0)
				{
					if (num != num2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-50]");
						object obj77 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm3,xmm3\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-38]");
						obj65 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v156 @ rbx_v9+10]");
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-50]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v960 @ rbx_v16+20+v2890 @ r8_v47 (UnityEngine.Vector2[][])]");
							_ = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v960 @ rbx_v16+28+v2890 @ r8_v47 (UnityEngine.Vector2[][])]");
							_ = 0;
						}
						if (array2 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+40]");
							object obj78 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2539 @ rax_v114+v2236 @ rcx_v51]");
							_ = 0;
						}
						if (array3 != null)
						{
							object obj79 = array3 + 32;
							object obj80 = 0;
							while ((nint)obj80 < UVChannelCount)
							{
								object obj81 = obj79;
								if (obj79 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v991 @ r11_v15+v1025 @ rax_v112]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v991 @ r11_v15+4+v1025 @ rax_v112]");
									_ = 0;
								}
								obj80++;
								obj79 += 8;
							}
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-80]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-80]");
							object obj82 = (nint)0 + (nint)32;
							object obj83 = 0;
							while ((nint)obj83 < UVChannelCount)
							{
								object obj84 = obj82;
								if (obj82 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_-1F0_v5 (UnityEngine.Vector2[][])+20+v1040 @ rcx_v69]");
									_ = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v110 @ stack_-1F0_v5 (UnityEngine.Vector2[][])+28+v1040 @ rcx_v69]");
									_ = 0;
									array17 = array15;
									array18 = array16;
								}
								obj83++;
								obj82 += 8;
							}
						}
						if (array4 != null)
						{
							object obj85 = array4 + 32;
							object obj86 = 0;
							while ((nint)obj86 < UVChannelCount)
							{
								object obj87 = obj85;
								if (obj85 != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+48]");
									array17 = (Vector2[][])0;
									object obj88 = obj76;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+48]");
									object obj89 = obj88 + 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+178]");
									nint num3 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+48]");
									object obj90 = num3 + 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v999 @ rdx_v65+v1042 @ rcx_v65]");
									_ = 0;
								}
								obj86++;
								obj85 += 8;
							}
						}
						if (array5 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+178]");
							obj68 = 0;
							obj68 = obj76;
							obj69 = obj76;
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+178]");
							obj68 = 0;
							obj69 = obj76;
						}
						if (array6 != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-58]");
							object obj91 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-68]");
							object obj92 = 0;
							obj92 = obj91;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2994 @ r8_v45+10]");
							_ = 0;
						}
						if (array7 != null)
						{
							object obj93 = array7 + 32;
							object obj94 = 0;
							while (true)
							{
								ResizableArray<BlendShapeContainer> resizableArray11 = meshSimplifier.blendShapes;
								if ((nint)obj94 >= resizableArray11.length)
								{
									break;
								}
								((BlendShapeContainer)obj93).MoveVertexElement(num, num2);
								obj94++;
								obj93 += 8;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+178]");
							obj68 = 0;
							obj69 = obj76;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-70]");
						obj65 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-60]");
						obj6 = 0;
						array17 = array15;
						obj74 = obj73;
						array18 = array16;
						obj75 = obj72;
					}
					obj65 -= -128;
					obj74 += 8;
					array18 = (Vector2[][])(array18 + 12);
					obj68 += 16;
					num++;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-68]");
					_ = (nint)0 + (nint)32;
					obj73 = obj74;
					array16 = array18;
				}
				obj6 -= -128;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-58]");
				_ = (nint)0 + (nint)32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-78]");
				object obj95 = (nint)0 + (nint)1;
				obj75 += 8;
				array17 = (Vector2[][])(array17 + 12);
				obj69 += 16;
				num2++;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+50]");
				flag5 = (nint)obj95 < 0;
				obj72 = obj75;
				array15 = array17;
				obj76 = obj69;
			}
			while (flag5);
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+58]");
			obj64 = 0;
			array19 = array6;
		}
		else
		{
			num = (int)array10;
			array19 = array6;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+170]");
		if ((nint)0 > (nint)0)
		{
			object obj96 = obj64 + 32;
			object obj97 = 0;
			object obj103;
			do
			{
				object obj98 = obj96 >> 32;
				object obj99 = obj98 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1003 @ rdx_v40+40+v70 @ rsi_v3]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+68]");
				object obj100 = (nint)0 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v2443 @ rax_v66+40+v70 @ rsi_v3]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+68]");
				object obj101 = (nint)0 >> 32;
				object obj102 = obj101 << 7;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1005 @ rdx_v42+40+v70 @ rsi_v3]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+60]");
				obj96 = 0;
				obj97++;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ rcx_v44+10]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ rcx_v44+20]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ rcx_v44+30]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ rcx_v44+40]");
				_ = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v416 @ rcx_v44+50]");
				_ = 0;
				obj96 += 96;
				obj103 = obj97;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+170]");
			}
			while ((nint)obj103 < 0);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-50]");
		if ((nint)0 != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
		}
		if (array2 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
		}
		if (array3 != null)
		{
			meshSimplifier.vertUV2D.Resize(num, trimExess: true);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1-80]");
		if ((nint)0 != 0)
		{
			meshSimplifier.vertUV3D.Resize(num, trimExess: true);
		}
		if (array4 != null)
		{
			meshSimplifier.vertUV4D.Resize(num, trimExess: true);
		}
		if (array5 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
		}
		if (array19 != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
		}
		if (array7 == null)
		{
			return;
		}
		ResizableArray<BlendShapeContainer> resizableArray12 = meshSimplifier.blendShapes;
		object obj104 = array7 + 32;
		object obj105 = 0;
		object obj106 = 0;
		while ((nint)obj106 < resizableArray12.length)
		{
			object obj107 = obj104;
			object obj108 = 0;
			object obj109 = 32;
			while (true)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v298 @ r15_v10+18]");
				object obj110 = 0;
				object obj111 = obj108;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v395 @ rax_v39+18]");
				if ((nint)obj111 >= 0)
				{
					break;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v367 @ rsi_v9+v395 @ rax_v39]");
				obj6 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
				obj108++;
				obj109 += 8;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v12 @ rbp_v1+160]");
			object obj112 = 0;
			obj105++;
			obj104 += 8;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v3096 @ rax_v40+98]");
			resizableArray12 = (ResizableArray<BlendShapeContainer>)0;
			obj106 = obj105;
		}
	}

	private void CalculateSubMeshOffsets()
	{
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Expected O, but got Unknown
		//IL_002c: Expected O, but got I8
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Expected O, but got Unknown
		//IL_0050: Expected O, but got I
		//IL_005d: Expected O, but got I8
		//IL_0066: Expected O, but got I4
		//IL_006f: Expected O, but got I4
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Expected O, but got Unknown
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Expected O, but got Unknown
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Expected O, but got Unknown
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Expected O, but got Unknown
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Expected O, but got Unknown
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Expected O, but got Unknown
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		//IL_0149: Expected I4, but got O
		//IL_0159: Expected O, but got I
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Expected O, but got Unknown
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Expected O, but got Unknown
		int[] array = new int[subMeshCount];
		int[] array2 = (int[])(this + 64);
		subMeshOffsets = array;
		ResizableArray<Triangle> resizableArray = triangles;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r11_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
		bool flag = (nint)0 <= (nint)0;
		object obj = 4294967295L;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r11_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
			object obj2 = (nint)0 + (nint)32;
			object obj3 = 4294967295L;
			object obj4 = 0;
			object obj5 = 0;
			object obj10 = default(object);
			bool flag2;
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ r9_v5+10]");
				if (0 > (nint)obj3)
				{
					object obj6 = obj3 + 1;
					object obj7 = obj6;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ r9_v5+10]");
					if ((nint)obj7 < 0)
					{
						object obj8 = obj6 * 4;
						int[] array3 = (int[])(obj8 + 32);
						object obj9;
						do
						{
							array2 = subMeshOffsets;
							obj6++;
							obj9 = obj6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ r9_v5+10]");
						}
						while ((nint)obj9 < 0);
					}
					int[] array4 = subMeshOffsets;
					array4[obj10] = (int)obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v83 @ r9_v5+10]");
					obj3 = 0;
				}
				obj5++;
				obj4++;
				obj2 += 96;
				object obj11 = obj4;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r11_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
				flag2 = (nint)obj11 < 0;
				obj = obj3;
			}
			while (flag2);
		}
		object obj12 = obj + 1;
		if ((nint)obj12 < subMeshCount)
		{
			object obj13 = obj12 * 4;
			object obj14 = obj13 + 32;
			do
			{
				int[] array5 = subMeshOffsets;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v43 @ r11_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
				_ = 0;
				obj12++;
				obj14 += 4;
			}
			while ((nint)obj12 < subMeshCount);
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe void GetTrianglesContainingVertex(ref Vertex vert, HashSet<Triangle> tris)
	{
		//IL_006c: Expected O, but got I
		//IL_0089: Expected O, but got I
		//IL_0030: Expected O, but got Ref
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Expected O, but got Unknown
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+20]");
		object obj = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+24]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+20]");
		object obj2 = num + 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+20]");
		if (0 < (nint)obj2)
		{
			object obj3 = default(object);
			do
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				tris.Add((Triangle)(&obj3));
				obj++;
			}
			while (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj2));
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe void GetTrianglesContainingBothVertices(ref Vertex vert0, ref Vertex vert1, HashSet<Triangle> tris)
	{
		//IL_0008: Expected O, but got Ref
		//IL_01a3: Expected O, but got I
		//IL_01c0: Expected O, but got I
		//IL_002a: Expected O, but got I
		//IL_003d: Expected O, but got Ref
		//IL_005a: Expected O, but got Ref
		//IL_007f: Expected O, but got I
		//IL_0153: Expected O, but got Ref
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_00ba: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+20]");
		object obj3 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+20]");
		nint num = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+24]");
		object obj4 = num + 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+20]");
		if (0 >= (nint)obj4)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+20]");
		nint num2 = 0;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [vert0 @ rdx (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex&)+24]");
		object obj5 = num2 + 0;
		object obj9 = default(object);
		object obj10 = default(object);
		bool flag2;
		do
		{
			object obj6 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 103));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			object obj7 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 49));
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-31]");
			object obj8 = (nint)0 >> 32;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
			object obj11;
			if (obj9 != (object)vert1)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1-29]");
				obj10 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				if (obj9 != (object)vert1)
				{
					obj10 >>= 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					bool flag = obj9 != (object)vert1;
					obj11 = obj10;
					if (flag)
					{
						goto IL_0161;
					}
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+F]");
			_ = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+1F]");
			_ = 0;
			tris.Add((Triangle)(&obj9));
			obj11 = obj10;
			goto IL_0161;
			IL_0161:
			obj3++;
			flag2 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj3) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj5);
			obj10 = obj11;
		}
		while (flag2);
	}

	public int[][] GetAllSubMeshTriangles()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Expected O, but got Unknown
		int[][] array = new int[subMeshCount][];
		if (subMeshCount > 0)
		{
			object obj = array + 32;
			int num = 0;
			do
			{
				int[] subMeshTriangles = GetSubMeshTriangles(num);
				if (num < array.Length)
				{
					obj = subMeshTriangles;
					num++;
					obj += 8;
					continue;
				}
				return (int[][])(object)new IndexOutOfRangeException();
			}
			while (num < subMeshCount);
		}
		return array;
	}

	public unsafe int[] GetSubMeshTriangles(int subMeshIndex)
	{
		//IL_00f3: Expected O, but got I
		//IL_0162: Expected O, but got I4
		//IL_0262: Expected O, but got I
		//IL_026b: Expected O, but got I4
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Expected O, but got Unknown
		//IL_01a1: Expected O, but got I4
		//IL_05a7: Expected O, but got I4
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Expected O, but got Unknown
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Expected I4, but got Unknown
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Expected O, but got Unknown
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Expected I, but got Unknown
		//IL_0616: Expected O, but got I4
		//IL_0202: Expected O, but got I4
		//IL_020b: Expected O, but got I4
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Expected O, but got Unknown
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Expected O, but got Unknown
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Expected O, but got Unknown
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Expected O, but got Unknown
		int[] array3;
		if (subMeshIndex >= 0)
		{
			bool flag = subMeshOffsets != null;
			int num = subMeshIndex;
			if (!flag)
			{
				CalculateSubMeshOffsets();
				num = 0;
			}
			int[] array = subMeshOffsets;
			if (subMeshOffsets != null)
			{
				if (subMeshIndex >= array.Length)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("subMeshIndex", "The sub-mesh index is greater than or equals to the sub mesh count.");
					ex._002Ector("subMeshIndex", "The sub-mesh index is greater than or equals to the sub mesh count.");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex;
				}
				if (array.Length != subMeshCount)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					InvalidOperationException ex2 = new InvalidOperationException("The sub-mesh triangle offsets array is not the same size as the count of sub-meshes. This should not be possible to happen.");
					ex2._002Ector("The sub-mesh triangle offsets array is not the same size as the count of sub-meshes. This should not be possible to happen.");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
					throw ex2;
				}
				ResizableArray<Triangle> resizableArray = triangles;
				if (triangles != null)
				{
					ResizableArray<Triangle> resizableArray2 = triangles;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rsi_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
					object obj = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v342 @ rax_v31 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
					int num2 = 0;
					if (subMeshIndex >= array.Length)
					{
						goto IL_052c;
					}
					int num3 = array[subMeshIndex];
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v342 @ rax_v31 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
					if ((nint)num3 >= (nint)0)
					{
						return new int[0];
					}
					object obj2 = subMeshIndex + 1;
					bool flag2 = (nint)obj2 < subMeshCount;
					int num4;
					if ((nint)obj2 < subMeshCount)
					{
						object obj3 = subMeshIndex + 1;
						bool flag3 = (nint)obj3 < array.Length;
						if ((nint)obj3 >= array.Length)
						{
							goto IL_052c;
						}
						num2 = array[obj3];
						object obj4 = array[obj3] - array[subMeshIndex];
						object obj5 = 0;
						if (!flag3)
						{
							obj5 = obj4;
						}
						object obj6 = obj5 * 2;
						object obj7 = obj5 + obj6;
						int[] array2 = new int[obj7];
						bool flag4 = array[subMeshIndex] >= num2;
						array3 = array2;
						if (flag4)
						{
							goto IL_03e8;
						}
						num4 = array[subMeshIndex];
						array3 = array2;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v342 @ rax_v31 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
						object obj8 = -array[subMeshIndex];
						object obj9 = 0;
						if (!flag2)
						{
							obj9 = obj8;
						}
						object obj10 = obj9 * 2;
						object obj11 = obj9 + obj10;
						int[] array4 = new int[obj11];
						num4 = array[subMeshIndex];
						array3 = array4;
					}
					object obj12 = array[subMeshIndex] * 2;
					object obj13 = obj12 + array[subMeshIndex];
					num = array3 + 36;
					object obj14 = obj13 << 5;
					object obj15 = obj14 + 32;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rsi_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
					nint num5 = (nint)(obj15 + 0);
					int num6 = array[subMeshIndex];
					object obj16 = 2;
					while (true)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ rsi_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
						if ((nint)0 == 0)
						{
							break;
						}
						int num7 = num4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v120 @ rsi_v6+18]");
						if ((nint)num7 < (nint)0)
						{
							if (array3 == null)
							{
								break;
							}
							object obj17 = obj16 - 2;
							if ((nint)obj17 < array3.Length)
							{
								int num8 = (int)(num5 >> 32);
								object obj18 = obj16 - 1;
								if ((nint)obj18 < array3.Length)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"psrldq xmm2,8\"");
									((int*)num)->m_value = (int)num5;
									if ((nint)obj16 < array3.Length)
									{
										int num9 = (int)(num5 >> 32);
										num4++;
										obj16 += 3;
										num += 12;
										num6++;
										num5 += 96;
										if (num6 < num2)
										{
											continue;
										}
										goto IL_03e8;
									}
								}
							}
						}
						goto IL_052c;
					}
				}
			}
			return (int[])(object)new NullReferenceException();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex3 = new ArgumentOutOfRangeException("subMeshIndex", "The sub-mesh index is negative.");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex3;
		IL_03e8:
		return array3;
		IL_052c:
		throw new IndexOutOfRangeException();
	}

	public void ClearSubMeshes()
	{
		subMeshCount = 0;
		subMeshOffsets = null;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
	}

	public void AddSubMeshTriangles(int[] triangles)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_00cd: Expected O, but got I
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_0127: Expected I, but got O
		//IL_0130: Expected O, but got I4
		//IL_014e: Expected O, but got I
		//IL_0184: Expected O, but got I
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Expected O, but got Unknown
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Expected O, but got Unknown
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Expected O, but got Unknown
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Expected O, but got Unknown
		//IL_01ea: Expected O, but got I4
		//IL_045b: Expected O, but got I
		//IL_0477: Expected O, but got I4
		//IL_01fd: Expected O, but got I
		//IL_0219: Expected O, but got I4
		//IL_0243: Expected O, but got I4
		//IL_0261: Expected O, but got I
		//IL_0281: Expected O, but got I4
		//IL_028a: Expected O, but got I4
		//IL_02ba: Expected O, but got I4
		//IL_02c3: Expected O, but got I4
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Expected O, but got Unknown
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Expected O, but got Unknown
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Expected O, but got Unknown
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Expected O, but got Unknown
		//IL_0368: Expected O, but got I4
		if (triangles != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul dword ptr [rbx+18h]\"");
			object obj = (object)triangles >> 31;
			object obj2 = (object)triangles + obj;
			object obj3 = obj2 * 2;
			object obj4 = obj2 + obj3;
			if (triangles.Length == (nint)obj4)
			{
				ResizableArray<Triangle> resizableArray = this.triangles;
				int num = subMeshCount + 1;
				subMeshCount = num;
				if (this.triangles != null)
				{
					ResizableArray<Triangle> resizableArray2 = this.triangles;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul dword ptr [rbx+18h]\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rdi_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
					object obj5 = 0;
					object obj6 = obj2 >> 31;
					object obj7 = obj6 + obj2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ rcx_v19 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
					obj2 = 0 + obj7;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
					ResizableArray<Triangle> resizableArray3 = this.triangles;
					bool flag = this.triangles == null;
					nint num2 = unchecked((nint)null);
					object obj8 = 0;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v247 @ r11_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
						object obj9 = 0;
						if ((nint)obj7 <= 0)
						{
							return;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rdi_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
						object obj10 = (nint)0 * (nint)2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v72 @ rdi_v5 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
						object obj11 = 0 + obj10;
						object obj12 = obj11 << 5;
						object obj13 = triangles + 36;
						object obj14 = obj12 + 32;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v247 @ r11_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
						obj8 = obj14 + 0;
						num2 = 2;
						object obj15 = 0;
						while (true)
						{
							object obj16 = num2 - 2;
							bool flag2 = (nint)obj16 >= triangles.Length;
							object obj17 = 0;
							if (!flag2)
							{
								object obj18 = num2 - 1;
								bool flag3 = (nint)obj18 >= triangles.Length;
								obj17 = 0;
								if (!flag3)
								{
									bool flag4 = num2 >= triangles.Length;
									obj17 = 0;
									if (!flag4)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ r10_v6+4]");
										obj2 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v247 @ r11_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
										bool flag5 = (nint)0 == 0;
										object obj19 = 0;
										obj17 = 0;
										if (flag5)
										{
											break;
										}
										object obj20 = obj5;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v248 @ r11_v5+18]");
										bool flag6 = (nint)obj20 >= 0;
										obj19 = 0;
										obj17 = 0;
										if (!flag6)
										{
											num2 += 3;
											object obj21 = obj5 + 1;
											obj8 = obj5;
											obj15++;
											obj13 += 12;
											_ = subMeshCount;
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm2\"");
											_ = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm2\"");
											_ = 0;
											_ = 0;
											obj8 += 96;
											bool flag7 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj15) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj7);
											obj19 = 0;
											obj5 = obj21;
											if (!flag7)
											{
												return;
											}
											continue;
										}
									}
								}
							}
							throw new IndexOutOfRangeException();
						}
					}
				}
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentException ex = new ArgumentException("The index array length must be a multiple of 3 in order to represent triangles.", "triangles");
			ex._002Ector("The index array length must be a multiple of 3 in order to represent triangles.", "triangles");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex2 = new ArgumentNullException("triangles");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex2;
	}

	public unsafe void AddSubMeshTriangles(int[][] triangles)
	{
		//IL_0008: Expected O, but got Ref
		//IL_0017: Expected O, but got I4
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_0032: Expected I, but got O
		//IL_003b: Expected O, but got I4
		//IL_0040: Expected I, but got O
		//IL_0136: Expected O, but got I
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Expected O, but got Unknown
		//IL_0175: Expected I, but got O
		//IL_017e: Expected O, but got I4
		//IL_007b: Expected O, but got I
		//IL_019c: Expected O, but got I
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Expected O, but got Unknown
		//IL_01b3: Expected O, but got I4
		//IL_01b8: Expected I, but got O
		//IL_01c1: Expected O, but got I4
		//IL_01ca: Expected O, but got I4
		//IL_0681: Expected O, but got Ref
		//IL_00b9: Expected O, but got I4
		//IL_056a: Expected O, but got Ref
		//IL_0210: Expected O, but got I4
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_0226: Expected I, but got O
		//IL_023e: Expected O, but got I4
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Expected O, but got Unknown
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Expected O, but got Unknown
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Expected O, but got Unknown
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Expected O, but got Unknown
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Expected O, but got Unknown
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Expected O, but got Unknown
		//IL_0302: Expected O, but got I4
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Expected O, but got Unknown
		//IL_0738: Expected O, but got I4
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Expected O, but got Unknown
		//IL_0337: Expected O, but got I4
		//IL_0367: Expected O, but got I4
		//IL_0385: Expected O, but got I
		//IL_0410: Expected O, but got I4
		//IL_0419: Expected O, but got I4
		//IL_0449: Expected O, but got I4
		//IL_0452: Expected O, but got I4
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Expected O, but got Unknown
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Expected O, but got Unknown
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Expected O, but got Unknown
		//IL_049a: Expected O, but got I
		//IL_04b0: Expected O, but got I
		//IL_04e2: Expected O, but got I
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Expected O, but got Unknown
		//IL_0546: Expected O, but got I
		//IL_0556: Expected O, but got I
		object obj2 = default(object);
		object obj = (object)(&obj2);
		int[][] array = default(int[][]);
		if (array != null)
		{
			object obj3 = array.Length;
			object obj4 = array + 32;
			int[][] array2 = array;
			nint num = unchecked((nint)null);
			object obj5 = 0;
			nint num2 = unchecked((nint)null);
			object arg = default(object);
			object arg2 = default(object);
			while (true)
			{
				if (num2 < (nint)obj3)
				{
					if (num < (nint)obj3)
					{
						bool flag = obj4 == null;
						string text = (string)num;
						if (!flag)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul ecx\"");
							object obj6 = (object)array2 >> 31;
							array2 = (int[][])(object)((object)array2 + obj6);
							object obj7 = !flag;
							if (obj7 == null)
							{
								num++;
								obj5 += (object)array2;
								obj4 += 8;
								num2 = num;
								continue;
							}
							object obj8 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							string message = $"The index array length at index {arg} must be a multiple of 3 in order to represent triangles.";
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
							ArgumentException ex = new ArgumentException(message, "triangles");
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
							throw ex;
						}
						object obj9 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 111));
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						string message2 = $"The index array at index {arg2} is null.";
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						ArgumentException ex2 = new ArgumentException(message2);
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
						throw ex2;
					}
				}
				else
				{
					ResizableArray<Triangle> resizableArray = this.triangles;
					if (this.triangles == null)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ rax_v15 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
					object obj10 = 0;
					object obj11 = obj5;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v193 @ rax_v15 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
					array2 = (int[][])(obj11 + 0);
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
					ResizableArray<Triangle> resizableArray2 = this.triangles;
					bool flag2 = this.triangles == null;
					num = unchecked((nint)null);
					obj4 = 0;
					if (flag2)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ rdi_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
					object obj12 = 0;
					object obj13 = array + 32;
					object obj14 = 0;
					num = unchecked((nint)null);
					obj4 = 0;
					object obj15 = 0;
					MeshSimplifier meshSimplifier = this;
					while (true)
					{
						if ((nint)obj15 >= array.Length)
						{
							return;
						}
						int num3 = meshSimplifier.subMeshCount + 1;
						meshSimplifier.subMeshCount = num3;
						bool flag3 = (nint)obj14 >= array.Length;
						object obj16 = 0;
						if (flag3)
						{
							break;
						}
						num = (nint)obj13;
						bool flag4 = obj13 == null;
						obj16 = 0;
						if (flag4)
						{
							goto end_IL_06a7;
						}
						_ = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul dword ptr [r8+18h]\"");
						object obj17 = (object)array2 >> 31;
						object obj18 = obj17 + (object)array2;
						if ((nint)obj18 > 0)
						{
							obj3 = obj13 + 36;
							object obj19 = obj10 * 2;
							object obj20 = obj10 + obj19;
							object obj21 = obj20 << 5;
							object obj22 = obj21 + 32;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ rdi_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
							object obj23 = obj22 + 0;
							obj5 = obj10;
							obj4 = 2;
							while (true)
							{
								object obj24 = obj4 - 2;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r8_v1 (Il2CppMethodInfo)+18]");
								bool flag5 = (nint)obj24 >= 0;
								obj16 = 0;
								if (flag5)
								{
									break;
								}
								object obj25 = obj4 - 1;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r8_v1 (Il2CppMethodInfo)+18]");
								bool flag6 = (nint)obj25 >= 0;
								obj16 = 0;
								if (flag6)
								{
									break;
								}
								object obj26 = obj4;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v91 @ r8_v1 (Il2CppMethodInfo)+18]");
								bool flag7 = (nint)obj26 >= 0;
								obj16 = 0;
								if (flag7)
								{
									break;
								}
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v427 @ r11_v6+4]");
								array2 = (int[][])0;
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v427 @ r11_v6-4]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v427 @ r11_v6-4]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v427 @ r11_v6+4]");
								_ = 0;
								_ = meshSimplifier.subMeshCount;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v427 @ r11_v6+4]");
								_ = 0;
								_ = 0;
								_ = 0;
								_ = 0;
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ rdi_v4 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
								bool flag8 = (nint)0 == 0;
								object obj27 = 0;
								obj16 = 0;
								if (flag8)
								{
									goto end_IL_06a7;
								}
								object obj28 = obj5;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v225 @ rdi_v5+18]");
								bool flag9 = (nint)obj28 >= 0;
								obj27 = 0;
								obj16 = 0;
								if (flag9)
								{
									break;
								}
								obj4 += 3;
								obj5++;
								obj3 += 12;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-39]");
								obj23 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+6F]");
								array2 = (int[][])((nint)0 + (nint)1);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1-29]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm0,xmm2\"");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+7]");
								obj27 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpcklpd xmm1,xmm2\"");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+7]");
								_ = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+17]");
								_ = 0;
								obj23 += 96;
								if (System.Runtime.CompilerServices.Unsafe.As<int[][], UIntPtr>(ref array2) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj18))
								{
									continue;
								}
								goto IL_0536;
							}
							break;
						}
						goto IL_06d2;
						IL_06d2:
						obj14++;
						obj10 += obj18;
						obj13 += 8;
						obj15 = obj14;
						continue;
						IL_0536:
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+7F]");
						obj13 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v8 @ rbp_v1+67]");
						meshSimplifier = (MeshSimplifier)0;
						goto IL_06d2;
					}
				}
				throw new IndexOutOfRangeException();
				continue;
				end_IL_06a7:
				break;
			}
			throw new NullReferenceException();
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex3 = new ArgumentNullException("triangles");
		ex3._002Ector("triangles");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex3;
	}

	public Vector2[] GetUVs2D(int channel)
	{
		//IL_0043: Expected O, but got I
		//IL_009d: Expected O, but got I
		//IL_00ad: Expected O, but got I
		//IL_00df: Expected O, but got I
		if (channel >= 0 && channel < UVChannelCount)
		{
			if (vertUV2D != null)
			{
				UVChannels<Vector2> uVChannels = vertUV2D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v18 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v18 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rcx_v13+20+channel @ rdx (System.Int32)*8]");
					if ((nint)0 == 0)
					{
						goto IL_00df;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v18 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rcx_v14+20+channel @ rdx (System.Int32)*8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rcx_v14+20+channel @ rdx (System.Int32)*8]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rax_v19+10]");
						return (Vector2[])0;
					}
				}
				return (Vector2[])(object)new NullReferenceException();
			}
			goto IL_00df;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
		IL_00df:
		return null;
	}

	public Vector3[] GetUVs3D(int channel)
	{
		//IL_0043: Expected O, but got I
		//IL_009d: Expected O, but got I
		//IL_00ad: Expected O, but got I
		//IL_00df: Expected O, but got I
		if (channel >= 0 && channel < UVChannelCount)
		{
			if (vertUV3D != null)
			{
				UVChannels<Vector3> uVChannels = vertUV3D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v18 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v18 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rcx_v13+20+channel @ rdx (System.Int32)*8]");
					if ((nint)0 == 0)
					{
						goto IL_00df;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v18 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rcx_v14+20+channel @ rdx (System.Int32)*8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rcx_v14+20+channel @ rdx (System.Int32)*8]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rax_v19+10]");
						return (Vector3[])0;
					}
				}
				return (Vector3[])(object)new NullReferenceException();
			}
			goto IL_00df;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
		IL_00df:
		return null;
	}

	public Vector4[] GetUVs4D(int channel)
	{
		//IL_0043: Expected O, but got I
		//IL_009d: Expected O, but got I
		//IL_00ad: Expected O, but got I
		//IL_00df: Expected O, but got I
		if (channel >= 0 && channel < UVChannelCount)
		{
			if (vertUV4D != null)
			{
				UVChannels<Vector4> uVChannels = vertUV4D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v18 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v18 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
				if ((nint)0 != 0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v104 @ rcx_v13+20+channel @ rdx (System.Int32)*8]");
					if ((nint)0 == 0)
					{
						goto IL_00df;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v103 @ rax_v18 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rcx_v14+20+channel @ rdx (System.Int32)*8]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v164 @ rcx_v14+20+channel @ rdx (System.Int32)*8]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v161 @ rax_v19+10]");
						return (Vector4[])0;
					}
				}
				return (Vector4[])(object)new NullReferenceException();
			}
			goto IL_00df;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
		IL_00df:
		return null;
	}

	public void GetUVs(int channel, List<Vector2> uvs)
	{
		//IL_007a: Expected I4, but got O
		//IL_00e7: Expected O, but got I
		//IL_012e: Expected O, but got I
		//IL_014d: Expected O, but got I4
		//IL_017c: Expected O, but got I4
		//IL_01bf: Expected O, but got I
		//IL_01ee: Expected O, but got I4
		//IL_0236: Expected O, but got I
		int num = default(int);
		if (num >= 0 && num < UVChannelCount)
		{
			List<Vector2> list = default(List<Vector2>);
			if (list != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+1C]");
				_ = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
				object obj = default(object);
				int num3;
				if (obj == null)
				{
					_ = 0;
					int num2 = num;
					num3 = (int)list;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
					num3 = 0;
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
					bool flag = (nint)0 <= (nint)0;
					int num2 = num;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+10]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector2>)+18]");
						Array.Clear((Array)num4, 0, 0);
						num2 = 0;
					}
				}
				if (vertUV2D == null)
				{
					return;
				}
				UVChannels<Vector2> uVChannels = vertUV2D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v27 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v27 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
				bool flag2 = (nint)0 == 0;
				List<Vector2> list2 = (List<Vector2>)num3;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rcx_v24+18]");
					bool flag3 = (nint)num >= (nint)0;
					list2 = (List<Vector2>)num3;
					if (flag3)
					{
						throw new IndexOutOfRangeException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rcx_v24+20+v115 @ rdx_v3 (System.Int32)*8]");
					if ((nint)0 == 0)
					{
						return;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v27 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rcx_v25+20+v115 @ rdx_v3 (System.Int32)*8]");
					int num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rcx_v25+20+v115 @ rdx_v3 (System.Int32)*8]");
					bool flag4 = (nint)0 == 0;
					list2 = (List<Vector2>)num3;
					if (!flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rdx_v2 (System.Int32)+10]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rdx_v2 (System.Int32)+10]");
							list.AddRange((IEnumerable<Vector2>)0);
						}
						return;
					}
				}
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentNullException ex = new ArgumentNullException("uvs");
			ex._002Ector("uvs");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex2;
	}

	public void GetUVs(int channel, List<Vector3> uvs)
	{
		//IL_007a: Expected I4, but got O
		//IL_00e7: Expected O, but got I
		//IL_012e: Expected O, but got I
		//IL_014d: Expected O, but got I4
		//IL_017c: Expected O, but got I4
		//IL_01bf: Expected O, but got I
		//IL_01ee: Expected O, but got I4
		//IL_0236: Expected O, but got I
		int num = default(int);
		if (num >= 0 && num < UVChannelCount)
		{
			List<Vector3> list = default(List<Vector3>);
			if (list != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+1C]");
				_ = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
				object obj = default(object);
				int num3;
				if (obj == null)
				{
					_ = 0;
					int num2 = num;
					num3 = (int)list;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
					num3 = 0;
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
					bool flag = (nint)0 <= (nint)0;
					int num2 = num;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+10]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector3>)+18]");
						Array.Clear((Array)num4, 0, 0);
						num2 = 0;
					}
				}
				if (vertUV3D == null)
				{
					return;
				}
				UVChannels<Vector3> uVChannels = vertUV3D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v27 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v27 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
				bool flag2 = (nint)0 == 0;
				List<Vector3> list2 = (List<Vector3>)num3;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rcx_v24+18]");
					bool flag3 = (nint)num >= (nint)0;
					list2 = (List<Vector3>)num3;
					if (flag3)
					{
						throw new IndexOutOfRangeException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rcx_v24+20+v115 @ rdx_v3 (System.Int32)*8]");
					if ((nint)0 == 0)
					{
						return;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v27 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rcx_v25+20+v115 @ rdx_v3 (System.Int32)*8]");
					int num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rcx_v25+20+v115 @ rdx_v3 (System.Int32)*8]");
					bool flag4 = (nint)0 == 0;
					list2 = (List<Vector3>)num3;
					if (!flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rdx_v2 (System.Int32)+10]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rdx_v2 (System.Int32)+10]");
							list.AddRange((IEnumerable<Vector3>)0);
						}
						return;
					}
				}
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentNullException ex = new ArgumentNullException("uvs");
			ex._002Ector("uvs");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex2;
	}

	public void GetUVs(int channel, List<Vector4> uvs)
	{
		//IL_007a: Expected I4, but got O
		//IL_00e7: Expected O, but got I
		//IL_012e: Expected O, but got I
		//IL_014d: Expected O, but got I4
		//IL_017c: Expected O, but got I4
		//IL_01bf: Expected O, but got I
		//IL_01ee: Expected O, but got I4
		//IL_0236: Expected O, but got I
		int num = default(int);
		if (num >= 0 && num < UVChannelCount)
		{
			List<Vector4> list = default(List<Vector4>);
			if (list != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector4>)+1C]");
				_ = (nint)0 + (nint)1;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
				object obj = default(object);
				int num3;
				if (obj == null)
				{
					_ = 0;
					int num2 = num;
					num3 = (int)list;
				}
				else
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector4>)+18]");
					num3 = 0;
					_ = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector4>)+18]");
					bool flag = (nint)0 <= (nint)0;
					int num2 = num;
					if (!flag)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector4>)+10]");
						nint num4 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v119 @ r8_v3 (System.Collections.Generic.List`1<UnityEngine.Vector4>)+18]");
						Array.Clear((Array)num4, 0, 0);
						num2 = 0;
					}
				}
				if (vertUV4D == null)
				{
					return;
				}
				UVChannels<Vector4> uVChannels = vertUV4D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v27 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
				object obj2 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v27 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
				bool flag2 = (nint)0 == 0;
				List<Vector4> list2 = (List<Vector4>)num3;
				if (!flag2)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rcx_v24+18]");
					bool flag3 = (nint)num >= (nint)0;
					list2 = (List<Vector4>)num3;
					if (flag3)
					{
						throw new IndexOutOfRangeException();
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v223 @ rcx_v24+20+v115 @ rdx_v3 (System.Int32)*8]");
					if ((nint)0 == 0)
					{
						return;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v222 @ rax_v27 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
					object obj3 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rcx_v25+20+v115 @ rdx_v3 (System.Int32)*8]");
					int num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v242 @ rcx_v25+20+v115 @ rdx_v3 (System.Int32)*8]");
					bool flag4 = (nint)0 == 0;
					list2 = (List<Vector4>)num3;
					if (!flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rdx_v2 (System.Int32)+10]");
						if ((nint)0 != 0)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v116 @ rdx_v2 (System.Int32)+10]");
							list.AddRange((IEnumerable<Vector4>)0);
						}
						return;
					}
				}
				throw new NullReferenceException();
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentNullException ex = new ArgumentNullException("uvs");
			ex._002Ector("uvs");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex2;
	}

	public void SetUVs(int channel, IList<Vector2> uvs)
	{
		//IL_019f: Expected O, but got I
		//IL_01e8: Expected O, but got I
		//IL_0231: Expected O, but got I
		//IL_00ba: Expected O, but got I
		//IL_00cf: Expected O, but got I
		//IL_0161: Expected O, but got I
		if (channel >= 0 && channel < UVChannelCount)
		{
			if (uvs != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				if ((nint)obj > 0)
				{
					if (vertUV2D == null)
					{
						UVChannels<Vector2> uVChannels = new UVChannels<Vector2>();
						vertUV2D = uVChannels;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					UVChannels<Vector2> uVChannels2 = vertUV2D;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v267 @ rbx_v7 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rbx_v8+20+channel @ rdx (System.Int32)*8]");
					ResizableArray<Vector2> resizableArray = (ResizableArray<Vector2>)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rbx_v8+20+channel @ rdx (System.Int32)*8]");
					if ((nint)0 == 0)
					{
						int num = default(int);
						ResizableArray<Vector2> resizableArray2 = new ResizableArray<Vector2>(num, num);
						vertUV2D.set_Item(channel, resizableArray2);
						resizableArray = resizableArray2;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v550 @ rbx_v10 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector2>)+10]");
					uvs.CopyTo((Vector2[])0, 0);
					goto IL_01af;
				}
			}
			if (vertUV2D != null)
			{
				UVChannels<Vector2> uVChannels3 = vertUV2D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v24 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
				object obj3 = 0;
				_ = 0;
			}
			goto IL_01af;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
		IL_01af:
		if (vertUV3D != null)
		{
			UVChannels<Vector3> uVChannels4 = vertUV3D;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rax_v21 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
			object obj4 = 0;
			_ = 0;
		}
		if (vertUV4D != null)
		{
			UVChannels<Vector4> uVChannels5 = vertUV4D;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rax_v19 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
			object obj5 = 0;
			_ = 0;
		}
	}

	public void SetUVs(int channel, IList<Vector3> uvs)
	{
		//IL_019f: Expected O, but got I
		//IL_01e8: Expected O, but got I
		//IL_0231: Expected O, but got I
		//IL_00ba: Expected O, but got I
		//IL_00cf: Expected O, but got I
		//IL_0161: Expected O, but got I
		if (channel >= 0 && channel < UVChannelCount)
		{
			if (uvs != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				if ((nint)obj > 0)
				{
					if (vertUV3D == null)
					{
						UVChannels<Vector3> uVChannels = new UVChannels<Vector3>();
						vertUV3D = uVChannels;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					UVChannels<Vector3> uVChannels2 = vertUV3D;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v267 @ rbx_v7 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rbx_v8+20+channel @ rdx (System.Int32)*8]");
					ResizableArray<Vector3> resizableArray = (ResizableArray<Vector3>)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rbx_v8+20+channel @ rdx (System.Int32)*8]");
					if ((nint)0 == 0)
					{
						int num = default(int);
						ResizableArray<Vector3> resizableArray2 = new ResizableArray<Vector3>(num, num);
						vertUV3D.set_Item(channel, resizableArray2);
						resizableArray = resizableArray2;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v550 @ rbx_v10 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector3>)+10]");
					uvs.CopyTo((Vector3[])0, 0);
					goto IL_01af;
				}
			}
			if (vertUV3D != null)
			{
				UVChannels<Vector3> uVChannels3 = vertUV3D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v24 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
				object obj3 = 0;
				_ = 0;
			}
			goto IL_01af;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
		IL_01af:
		if (vertUV2D != null)
		{
			UVChannels<Vector2> uVChannels4 = vertUV2D;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rax_v21 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
			object obj4 = 0;
			_ = 0;
		}
		if (vertUV4D != null)
		{
			UVChannels<Vector4> uVChannels5 = vertUV4D;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rax_v19 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
			object obj5 = 0;
			_ = 0;
		}
	}

	public void SetUVs(int channel, IList<Vector4> uvs)
	{
		//IL_019f: Expected O, but got I
		//IL_01e8: Expected O, but got I
		//IL_0231: Expected O, but got I
		//IL_00ba: Expected O, but got I
		//IL_00cf: Expected O, but got I
		//IL_0161: Expected O, but got I
		if (channel >= 0 && channel < UVChannelCount)
		{
			if (uvs != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
				object obj = default(object);
				if ((nint)obj > 0)
				{
					if (vertUV4D == null)
					{
						UVChannels<Vector4> uVChannels = new UVChannels<Vector4>();
						vertUV4D = uVChannels;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					UVChannels<Vector4> uVChannels2 = vertUV4D;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v267 @ rbx_v7 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rbx_v8+20+channel @ rdx (System.Int32)*8]");
					ResizableArray<Vector4> resizableArray = (ResizableArray<Vector4>)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v268 @ rbx_v8+20+channel @ rdx (System.Int32)*8]");
					if ((nint)0 == 0)
					{
						int num = default(int);
						ResizableArray<Vector4> resizableArray2 = new ResizableArray<Vector4>(num, num);
						vertUV4D.set_Item(channel, resizableArray2);
						resizableArray = resizableArray2;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v550 @ rbx_v10 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector4>)+10]");
					uvs.CopyTo((Vector4[])0, 0);
					goto IL_01af;
				}
			}
			if (vertUV4D != null)
			{
				UVChannels<Vector4> uVChannels3 = vertUV4D;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v166 @ rax_v24 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
				object obj3 = 0;
				_ = 0;
			}
			goto IL_01af;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
		IL_01af:
		if (vertUV2D != null)
		{
			UVChannels<Vector2> uVChannels4 = vertUV2D;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v290 @ rax_v21 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
			object obj4 = 0;
			_ = 0;
		}
		if (vertUV3D != null)
		{
			UVChannels<Vector3> uVChannels5 = vertUV3D;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v291 @ rax_v19 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
			object obj5 = 0;
			_ = 0;
		}
	}

	public void SetUVs(int channel, IList<Vector4> uvs, int uvComponentCount)
	{
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Expected O, but got Unknown
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_02e6: Expected I, but got O
		//IL_031e: Expected O, but got I
		//IL_0115: Expected I, but got O
		//IL_06c5: Expected O, but got I4
		//IL_03d9: Expected O, but got F4
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Expected O, but got Unknown
		//IL_041c: Expected O, but got I4
		//IL_0432: Expected O, but got I
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Expected O, but got Unknown
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Expected O, but got Unknown
		//IL_014d: Expected O, but got I
		//IL_061d: Expected O, but got I4
		//IL_0221: Expected O, but got F4
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Expected O, but got Unknown
		//IL_0266: Expected O, but got I4
		//IL_027c: Expected O, but got I
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Expected O, but got Unknown
		int num = default(int);
		if (num >= 0 && num < UVChannelCount)
		{
			int num2 = default(int);
			if (num2 <= 4)
			{
				IList<Vector4> list = default(IList<Vector4>);
				if (list != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
					object obj = default(object);
					if ((nint)obj > 0 && num2 > 0)
					{
						float num11 = default(float);
						if (num2 > 2)
						{
							if (num2 != 3)
							{
								SetUVs(num, list);
								return;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							int num3 = default(int);
							Vector3[] array = new Vector3[num3];
							bool flag = array == null;
							int num4 = num3;
							if (flag)
							{
								goto IL_0604;
							}
							object obj2 = array + 32;
							int num5 = 0;
							int num6 = 0;
							while (true)
							{
								if (num6 < array.Length)
								{
									nint num7 = (nint)list;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v760 @ r10_v10 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Vector4>>)+12E]");
									if ((nint)0 >= (nint)0)
									{
										goto IL_018d;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v760 @ r10_v10 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Vector4>>)+B0]");
									object obj3 = 0;
									int num8 = 0;
									while (true)
									{
										object obj4 = num8 + num8;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v807 @ r8_v30+v810 @ rax_v69*8]");
										if (0 == (nint)typeof(IList<Vector4>))
										{
											break;
										}
										num8++;
										int num9 = num8;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v760 @ r10_v10 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Vector4>>)+12E]");
										if ((nint)num9 < (nint)0)
										{
											continue;
										}
										goto IL_018d;
									}
									object obj5 = num8 + num8;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v807 @ r8_v30+8+v910 @ rcx_v59*8]");
									object obj6 = (nint)0 << 4;
									object obj7 = obj6 + 312;
									object obj8 = obj7 + num7;
									goto IL_019c;
								}
								SetUVs(num, array);
								return;
								IL_019c:
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v907 @ rax_v62+8]");
								num2 = 0;
								float x = list.get_Item(num5).x;
								bool flag2 = num5 >= array.Length;
								float num10 = num11;
								float num12 = num11;
								int num13 = num5;
								IList<Vector4> list2 = list;
								if (flag2)
								{
									break;
								}
								num5++;
								obj2 = num11;
								obj2 += 12;
								num10 = num11;
								num12 = num11;
								x = num11;
								num6 = num5;
								continue;
								IL_018d:
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
								goto IL_019c;
							}
						}
						else
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
							int num14 = default(int);
							Vector2[] array2 = new Vector2[num14];
							bool flag3 = array2 == null;
							int num4 = num14;
							if (flag3)
							{
								goto IL_0604;
							}
							object obj9 = array2 + 32;
							int num15 = 0;
							int num16 = 0;
							while (true)
							{
								if (num15 < array2.Length)
								{
									nint num17 = (nint)list;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v745 @ r10_v6 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Vector4>>)+12E]");
									if ((nint)0 >= (nint)0)
									{
										goto IL_035e;
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v745 @ r10_v6 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Vector4>>)+B0]");
									object obj10 = 0;
									int num18 = 0;
									while (true)
									{
										object obj11 = num18 + num18;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v775 @ r8_v21+v778 @ rax_v45*8]");
										if (0 == (nint)typeof(IList<Vector4>))
										{
											break;
										}
										num18++;
										int num19 = num18;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v745 @ r10_v6 (Il2CppClass<System.Collections.Generic.IList`1<UnityEngine.Vector4>>)+12E]");
										if ((nint)num19 < (nint)0)
										{
											continue;
										}
										goto IL_035e;
									}
									object obj12 = num18 + num18;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v775 @ r8_v21+8+v868 @ rcx_v40*8]");
									object obj13 = (nint)0 << 4;
									object obj14 = obj13 + 312;
									object obj15 = obj14 + num17;
									goto IL_036d;
								}
								SetUVs(num, array2);
								return;
								IL_036d:
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v865 @ rax_v38+8]");
								num2 = 0;
								Vector4 vector = list.get_Item(num16);
								float x = vector.x;
								bool flag4 = num16 >= array2.Length;
								int num13 = num16;
								IList<Vector4> list2 = list;
								if (flag4)
								{
									break;
								}
								obj9 = vector.x;
								num16++;
								obj9 += 8;
								x = num11;
								num15 = num16;
								continue;
								IL_035e:
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A4410");
								goto IL_036d;
							}
						}
						throw new IndexOutOfRangeException();
					}
				}
				if (vertUV2D != null)
				{
					vertUV2D.set_Item(num, (ResizableArray<Vector2>)null);
				}
				if (vertUV3D != null)
				{
					vertUV3D.set_Item(num, (ResizableArray<Vector3>)null);
				}
				if (vertUV4D != null)
				{
					vertUV4D.set_Item(num, (ResizableArray<Vector4>)null);
				}
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("uvComponentCount");
			ex._002Ector("uvComponentCount");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex2 = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex2;
		IL_0604:
		throw new NullReferenceException();
	}

	public void SetUVsAuto(int channel, IList<Vector4> uvs)
	{
		if (channel >= 0 && channel < UVChannelCount)
		{
			int usedUVComponents = MeshUtils.GetUsedUVComponents(uvs);
			SetUVs(channel, uvs, usedUVComponents);
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("channel");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public BlendShape[] GetAllBlendShapes()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_0055: Expected O, but got I4
		//IL_005e: Expected O, but got I4
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Expected O, but got Unknown
		if (blendShapes != null)
		{
			ResizableArray<BlendShapeContainer> resizableArray = blendShapes;
			BlendShape[] array = new BlendShape[resizableArray.length];
			if (array != null)
			{
				object obj = array + 32;
				object obj2 = 0;
				object obj3 = 0;
				BlendShapeContainer blendShapeContainer = default(BlendShapeContainer);
				while (true)
				{
					if ((nint)obj3 < array.Length)
					{
						if (blendShapes == null)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
						if (blendShapeContainer == null)
						{
							break;
						}
						obj = blendShapeContainer.ToBlendShape().ShapeName;
						obj2++;
						obj += 16;
						obj3 = obj2;
						continue;
					}
					return array;
				}
			}
			return (BlendShape[])(object)new NullReferenceException();
		}
		return null;
	}

	public unsafe BlendShape GetBlendShape(int blendShapeIndex)
	{
		//IL_0091: Expected native int or pointer, but got O
		if (blendShapes != null && blendShapeIndex >= 0)
		{
			ResizableArray<BlendShapeContainer> resizableArray = blendShapes;
			if (blendShapeIndex < resizableArray.length)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
				BlendShapeContainer blendShapeContainer = default(BlendShapeContainer);
				if (blendShapeContainer != null)
				{
					BlendShape blendShape = default(BlendShape);
					System.Runtime.CompilerServices.Unsafe.Write(&((BlendShape*)(nint)blendShape)->ShapeName, blendShapeContainer.ToBlendShape().ShapeName);
					return blendShape;
				}
				return (BlendShape)new NullReferenceException();
			}
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentOutOfRangeException ex = new ArgumentOutOfRangeException("blendShapeIndex");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public void ClearBlendShapes()
	{
		if (blendShapes != null)
		{
			blendShapes.Clear();
			blendShapes = null;
		}
	}

	public unsafe void AddBlendShape(BlendShape blendShape)
	{
		//IL_0077: Expected O, but got Ref
		BlendShapeFrame[] frames = blendShape.Frames;
		if (blendShape.Frames != null && frames.Length != 0)
		{
			if (blendShapes == null)
			{
				ResizableArray<BlendShapeContainer> resizableArray = new ResizableArray<BlendShapeContainer>(4, 0);
				blendShapes = resizableArray;
			}
			object obj = default(object);
			BlendShapeContainer item = new BlendShapeContainer((BlendShape)(&obj));
			blendShapes.Add(item);
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentException ex = new ArgumentException("The frames cannot be null or empty.", "blendShape");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public unsafe void AddBlendShapes(BlendShape[] blendShapes)
	{
		//IL_02a8: Expected I4, but got O
		//IL_021b: Expected I4, but got O
		//IL_021b: Expected O, but got I
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Expected O, but got Unknown
		//IL_00a5: Expected O, but got I4
		//IL_00e3: Expected O, but got I
		//IL_01a9: Expected I4, but got O
		//IL_01d6: Expected I4, but got O
		//IL_01d6: Expected I4, but got O
		//IL_01d6: Expected O, but got I
		//IL_0148: Expected O, but got Ref
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Expected O, but got Unknown
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Expected O, but got Unknown
		BlendShape[] array = default(BlendShape[]);
		nint num2 = default(nint);
		BlendShape[] array2 = default(BlendShape[]);
		if (array != null)
		{
			bool flag = this.blendShapes != null;
			int num = (int)num2;
			array2 = array;
			if (!flag)
			{
				int capacity = Math.Max(4, array.Length);
				ResizableArray<BlendShapeContainer> resizableArray = (this.blendShapes = new ResizableArray<BlendShapeContainer>(capacity, 0));
				nint num3 = 0;
				num = 0;
				array2 = (BlendShape[])(object)resizableArray;
			}
			object obj = array + 32;
			object obj2 = 0;
			object obj4 = default(object);
			while (true)
			{
				if ((nint)obj2 < array.Length)
				{
					bool flag2 = (nint)obj2 >= array.Length;
					num2 = num;
					if (!flag2)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ rdi_v4+8]");
						object obj3 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v174 @ rdi_v4+8]");
						bool flag3 = (nint)0 == 0;
						int num4 = 0;
						if (flag3)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v245 @ rax_v14+18]");
						bool flag4 = (nint)0 == 0;
						num4 = num;
						if (flag4)
						{
							break;
						}
						BlendShapeContainer blendShapeContainer = new BlendShapeContainer((BlendShape)(&obj4));
						this.blendShapes.Add(blendShapeContainer);
						obj2++;
						obj += 16;
						num = 0;
						array2 = (BlendShape[])(object)blendShapeContainer;
						continue;
					}
					throw new IndexOutOfRangeException();
				}
				return;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object obj5 = default(object);
			string text = $"The frames of blend shape at index {obj5} cannot be null or empty.";
			((ResizableArray<BlendShapeContainer>)(object)typeof(ArgumentException))._002Ector((int)obj5, 0);
			ArgumentException ex = new ArgumentException(text, "blendShapes");
			((ResizableArray<BlendShapeContainer>)0)._002Ector((int)text, (int)"blendShapes");
			throw ex;
		}
		((ResizableArray<BlendShapeContainer>)(object)typeof(ArgumentNullException))._002Ector((int)array2, (int)num2);
		ArgumentNullException ex2 = new ArgumentNullException("blendShapes");
		ex2._002Ector("blendShapes");
		((ResizableArray<BlendShapeContainer>)0)._002Ector((int)"blendShapes", 0);
		throw ex2;
	}

	public void Initialize(Mesh mesh)
	{
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Expected O, but got Unknown
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Expected O, but got Unknown
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Expected O, but got Unknown
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Expected O, but got Unknown
		//IL_0110: Expected O, but got I
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Expected O, but got Unknown
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Expected O, but got Unknown
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Expected O, but got Unknown
		if (mesh != null)
		{
			Vector3[] array = mesh.vertices;
			Vertices = array;
			Vector3[] normals = mesh.normals;
			object obj = this + 96;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807267B0");
			Vector4[] tangents = mesh.tangents;
			object obj2 = this + 104;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807267B0");
			Color[] colors = mesh.colors;
			object obj3 = this + 136;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807267B0");
			BoneWeight[] boneWeights = mesh.boneWeights;
			IList<Vector4> list = (IList<Vector4>)(this + 144);
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1807267B0");
			Matrix4x4[] array2 = mesh.bindposes;
			bindposes = array2;
			int num = 0;
			while (num < UVChannelCount)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
				bool flag = (nint)0 == 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+30]");
				if ((nint)0 == 0)
				{
					IList<Vector4> meshUVs = MeshUtils.GetMeshUVs(mesh, num);
					SetUVsAuto(num, meshUVs);
					num++;
					continue;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+34]");
				object obj4 = -1;
				if (!flag)
				{
					object obj5 = obj4 - 1;
					if (!flag)
					{
						object obj6 = obj5 - 1;
						if (!flag)
						{
							if ((nint)obj6 == 1)
							{
								IList<Vector4> meshUVs2 = MeshUtils.GetMeshUVs(mesh, num);
								SetUVs(num, meshUVs2);
								num++;
								continue;
							}
							goto IL_021a;
						}
						IList<Vector3> meshUVs3D = MeshUtils.GetMeshUVs3D(mesh, num);
						SetUVs(num, meshUVs3D);
						num++;
						continue;
					}
				}
				IList<Vector2> meshUVs2D = MeshUtils.GetMeshUVs2D(mesh, num);
				SetUVs(num, meshUVs2D);
				goto IL_021a;
				IL_021a:
				num++;
			}
			BlendShape[] meshBlendShapes = MeshUtils.GetMeshBlendShapes(mesh);
			if (meshBlendShapes != null && meshBlendShapes.Length != 0)
			{
				AddBlendShapes(meshBlendShapes);
			}
			subMeshCount = 0;
			subMeshOffsets = null;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808F8090");
			int num2 = mesh.subMeshCount;
			int[][] array3 = new int[num2][];
			if (num2 > 0)
			{
				object obj7 = array3 + 32;
				int num3 = 0;
				int num4 = 0;
				do
				{
					int[] array4 = mesh.GetTriangles(num4);
					obj7 = array4;
					num4++;
					num3++;
					obj7 += 8;
				}
				while (num3 < num2);
			}
			AddSubMeshTriangles(array3);
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ArgumentNullException ex = new ArgumentNullException("mesh");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex;
	}

	public unsafe void SimplifyMesh(float quality)
	{
		//IL_0aa1: Invalid comparison between I4 and F4
		//IL_0044: Expected F4, but got I4
		//IL_0061: Expected I4, but got O
		//IL_009e: Expected O, but got I
		//IL_0ae5: Expected I, but got O
		//IL_02ca: Expected O, but got I4
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Expected O, but got Unknown
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Expected O, but got Unknown
		//IL_02ef: Expected O, but got I4
		//IL_02f8: Expected O, but got I4
		//IL_0b99: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b9e: Expected O, but got Unknown
		//IL_0ba6: Invalid comparison between O and F8
		//IL_0367: Expected O, but got I4
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Expected O, but got Unknown
		//IL_0a17: Expected I, but got O
		//IL_03db: Expected I, but got O
		//IL_056d: Expected F8, but got I
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Expected O, but got Unknown
		//IL_048e: Expected O, but got I4
		//IL_0901: Expected I4, but got F8
		//IL_0940: Expected F4, but got I
		//IL_0948: Expected I, but got F8
		//IL_040f: Expected O, but got I
		//IL_042c: Expected I, but got O
		//IL_05b8: Expected O, but got I
		//IL_0976: Expected F4, but got I
		//IL_0996: Expected I, but got F8
		//IL_05e3: Expected I, but got O
		//IL_04fd: Expected O, but got I4
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Expected O, but got Unknown
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0519: Expected O, but got Unknown
		//IL_069e: Expected I, but got O
		//IL_061e: Expected I, but got O
		//IL_066f: Expected I, but got O
		//IL_06ce: Expected O, but got I
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Expected O, but got Unknown
		//IL_079f: Expected I, but got O
		//IL_071f: Expected I, but got O
		//IL_0770: Expected I, but got O
		//IL_07cf: Expected O, but got I
		//IL_088b: Expected I, but got O
		//IL_080b: Expected I, but got O
		//IL_085c: Expected I, but got O
		float num2;
		float num = default(float);
		if (!(0f > num))
		{
			bool flag = !(num > 1f);
			num2 = num;
			if (!flag)
			{
				num2 = 1f;
			}
		}
		else
		{
			num2 = 0f;
		}
		ResizableArray<bool> resizableArray = new ResizableArray<bool>(20);
		ResizableArray<bool> resizableArray2 = new ResizableArray<bool>(20);
		resizableArray2._002Ector(20);
		int num3 = (int)triangles;
		bool flag2 = triangles == null;
		nint num4 = 0;
		ResizableArray<bool> resizableArray3 = resizableArray2;
		if (!flag2)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rdx_v13 (System.Int32)+10]");
			object obj = 0;
			bool flag3 = vertices == null;
			num4 = 0;
			resizableArray3 = resizableArray2;
			if (!flag3)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rdx_v13 (System.Int32)+18]");
				float num5 = 0f * num2;
				nint num6 = (nint)typeof(Math);
				Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm6,xmm0\"");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ rcx_v21 (Il2CppClass<System.Math>)+E4]");
				double num7 = default(double);
				double num8;
				if ((nint)0 >= (nint)0)
				{
					((ResizableArray<bool>)(object)typeof(Math))._002Ector((int)(&num7));
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803C87FCh\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ rcx_v21 (Il2CppClass<System.Math>)+E4]");
					if ((nint)0 == 0)
					{
						object obj2 = num7 & 1;
						bool flag4 = obj2 == null;
						num = 0.5f;
						num8 = num7;
						if (!flag4)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm0,qword ptr [182206E88h]\"");
							num = 0.5f;
							num8 = num7;
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"addsd xmm6,xmm1\"");
						num8 = Math.Floor(num5);
						num = 0.5f;
					}
				}
				else
				{
					((ResizableArray<bool>)(object)typeof(Math))._002Ector((int)(&num7));
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,qword ptr [182206D70h]\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001803C8837h\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v273 @ rcx_v21 (Il2CppClass<System.Math>)+E4]");
					if ((nint)0 == 0)
					{
						object obj3 = num7 & 1;
						bool flag5 = obj3 == null;
						num8 = num7;
						if (!flag5)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm0,qword ptr [182206E88h]\"");
							num8 = num7;
						}
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"subsd xmm6,qword ptr [182206D18h]\"");
						num8 = Math.Ceiling(num5);
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
				bool flag6 = (nint)0 <= (nint)0;
				object obj4 = 0;
				num4 = 0;
				double num9 = num8;
				if (!flag6)
				{
					object obj5 = 0;
					obj4 = 0;
					int num10 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rdx_v13 (System.Int32)+18]");
					int num11 = 0;
					int num12 = (int)(&num7);
					num4 = 0;
					float num13 = (float)num8;
					double num14 = num8;
					ResizableArray<bool> deleted = default(ResizableArray<bool>);
					ResizableArray<bool> deleted2 = default(ResizableArray<bool>);
					ref int deletedTris = default(ref int);
					int num22 = default(int);
					ResizableArray<bool> resizableArray7 = default(ResizableArray<bool>);
					ResizableArray<bool> resizableArray8 = default(ResizableArray<bool>);
					ResizableArray<bool> resizableArray9 = default(ResizableArray<bool>);
					ResizableArray<bool> resizableArray10 = default(ResizableArray<bool>);
					object obj13 = default(object);
					ResizableArray<bool> resizableArray11 = default(ResizableArray<bool>);
					ResizableArray<bool> resizableArray13 = default(ResizableArray<bool>);
					ResizableArray<bool> resizableArray14 = default(ResizableArray<bool>);
					double num25 = default(double);
					ResizableArray<bool> resizableArray15 = default(ResizableArray<bool>);
					ResizableArray<bool> resizableArray17 = default(ResizableArray<bool>);
					while (true)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rdx_v13 (System.Int32)+18]");
						object obj6 = 0 - obj5;
						bool flag7 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj6) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num14);
						num9 = num13;
						if (flag7)
						{
							break;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"imul r14d\"");
						int num15 = num12 >> 1;
						int num16 = num15 >> 31;
						object obj7 = num15 + num16;
						object obj8 = obj7 * 4;
						object obj9 = obj7 + obj8;
						bool flag8 = num10 != (nint)obj9;
						nint num17 = num11;
						if (flag8)
						{
							goto IL_043f;
						}
						UpdateMesh(num10);
						ResizableArray<Triangle> resizableArray4 = triangles;
						bool flag9 = triangles == null;
						num3 = num10;
						num4 = unchecked((nint)null);
						resizableArray3 = (ResizableArray<bool>)(object)this;
						if (!flag9)
						{
							bool flag10 = vertices == null;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ rax_v82 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+10]");
							obj = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v202 @ rax_v82 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Triangle>)+18]");
							num17 = 0;
							num3 = num10;
							num4 = unchecked((nint)null);
							resizableArray3 = (ResizableArray<bool>)(object)this;
							if (!flag10)
							{
								goto IL_043f;
							}
						}
						goto IL_0a8c;
						IL_08db:
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rdx_v13 (System.Int32)+18]");
						RemoveVertexPass(0, (int)num8, num9, deleted, deleted2, ref deletedTris);
						num10++;
						int num18 = num10;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+20]");
						bool flag11 = (nint)num18 >= (nint)0;
						double num19 = num9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+28]");
						num = 0f;
						num4 = (nint)num8;
						if (flag11)
						{
							break;
						}
						obj5 = obj4;
						num19 = num9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+28]");
						num = 0f;
						num11 = (int)num17;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rdx_v13 (System.Int32)+18]");
						num12 = 0;
						num4 = (nint)num8;
						num13 = (float)num9;
						num14 = num8;
						continue;
						IL_043f:
						if (num17 > 0)
						{
							ResizableArray<bool> resizableArray5 = (ResizableArray<bool>)(obj + 97);
							bool flag12 = obj == null;
							int num20 = 0;
							object obj10 = 0;
							num3 = 0;
							num4 = num17;
							resizableArray3 = resizableArray5;
							if (flag12)
							{
								goto IL_0a8c;
							}
							do
							{
								object obj11 = obj10;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v186 @ rbp_v15+18]");
								bool flag13 = (nint)obj11 >= 0;
								num3 = num20;
								num4 = num17;
								resizableArray3 = resizableArray5;
								if (!flag13)
								{
									resizableArray5 = (ResizableArray<bool>)0;
									obj10++;
									resizableArray5 = (ResizableArray<bool>)(resizableArray5 + 96);
									num20++;
									continue;
								}
								throw new IndexOutOfRangeException();
							}
							while (num20 < num17);
						}
						double x = (double)num10 + 3.0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+28]");
						num9 = Math.Pow(x, 0.0);
						bool flag14 = !verbose;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"mulsd xmm6,xmm7\"");
						if (!flag14)
						{
							object[] array = new object[3];
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
							ResizableArray<bool> resizableArray6 = (ResizableArray<bool>)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
							bool flag15 = array == null;
							int num21 = (int)(&num22);
							num4 = unchecked((nint)null);
							if (!flag15)
							{
								bool flag16 = resizableArray7 == null;
								num21 = (int)(&num22);
								if (!flag16)
								{
									nint num23 = (nint)array;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1251 @ rdx_v51 (Il2CppClass<System.Object[]>)+40]");
									num21 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1251 @ rdx_v51 (Il2CppClass<System.Object[]>)+40]");
									resizableArray7._002Ector(0);
									bool flag17 = resizableArray8 == null;
									resizableArray6 = resizableArray7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1251 @ rdx_v51 (Il2CppClass<System.Object[]>)+40]");
									num3 = 0;
									num4 = unchecked((nint)null);
									resizableArray3 = resizableArray7;
									if (flag17)
									{
										resizableArray3._002Ector(num3);
										throw resizableArray9;
									}
								}
								bool flag18 = array.Length <= 0;
								num4 = unchecked((nint)null);
								if (!flag18)
								{
									array[0] = resizableArray7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B50288]");
									resizableArray6 = (ResizableArray<bool>)0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v370 @ rdx_v13 (System.Int32)+18]");
									object obj12 = 0 - obj5;
									Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
									bool flag19 = resizableArray10 == null;
									num21 = (int)(&obj13);
									if (!flag19)
									{
										nint num24 = (nint)array;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1281 @ rdx_v49 (Il2CppClass<System.Object[]>)+40]");
										num21 = 0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1281 @ rdx_v49 (Il2CppClass<System.Object[]>)+40]");
										resizableArray10._002Ector(0);
										bool flag20 = resizableArray11 == null;
										resizableArray6 = resizableArray10;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1281 @ rdx_v49 (Il2CppClass<System.Object[]>)+40]");
										int capacity = 0;
										num4 = unchecked((nint)null);
										ResizableArray<bool> resizableArray12 = resizableArray10;
										if (flag20)
										{
											resizableArray12._002Ector(capacity);
											throw resizableArray13;
										}
									}
									bool flag21 = array.Length <= 1;
									num4 = unchecked((nint)null);
									if (!flag21)
									{
										array[1] = resizableArray10;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B502C0]");
										resizableArray6 = (ResizableArray<bool>)0;
										Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
										bool flag22 = resizableArray14 == null;
										num21 = (int)(&num25);
										if (!flag22)
										{
											nint num26 = (nint)array;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1309 @ rdx_v47 (Il2CppClass<System.Object[]>)+40]");
											num21 = 0;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1309 @ rdx_v47 (Il2CppClass<System.Object[]>)+40]");
											resizableArray14._002Ector(0);
											bool flag23 = resizableArray15 == null;
											resizableArray6 = resizableArray14;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1309 @ rdx_v47 (Il2CppClass<System.Object[]>)+40]");
											int capacity2 = 0;
											num4 = unchecked((nint)null);
											ResizableArray<bool> resizableArray16 = resizableArray14;
											if (flag23)
											{
												resizableArray16._002Ector(capacity2);
												throw resizableArray17;
											}
										}
										bool flag24 = array.Length <= 2;
										num4 = unchecked((nint)null);
										if (!flag24)
										{
											array[2] = resizableArray14;
											Debug.LogFormat("iteration {0} - triangles {1} threshold {2}", array);
											num25 = num9;
											obj13 = obj12;
											num22 = num10;
											goto IL_08db;
										}
									}
								}
								throw new IndexOutOfRangeException();
							}
							throw new NullReferenceException();
						}
						goto IL_08db;
					}
				}
				CompactMesh();
				if (!verbose)
				{
					return;
				}
				object[] array2 = new object[1];
				Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
				ResizableArray<bool> resizableArray18 = default(ResizableArray<bool>);
				if (resizableArray18 != null)
				{
					nint num27 = (nint)array2;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1103 @ rdx_v27 (Il2CppClass<System.Object[]>)+40]");
					int num21 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1103 @ rdx_v27 (Il2CppClass<System.Object[]>)+40]");
					resizableArray18._002Ector(0);
					ResizableArray<bool> resizableArray19 = default(ResizableArray<bool>);
					bool flag25 = resizableArray19 == null;
					ResizableArray<bool> resizableArray6 = resizableArray18;
					if (flag25)
					{
						resizableArray6._002Ector(num21);
						ResizableArray<bool> resizableArray20 = default(ResizableArray<bool>);
						throw resizableArray20;
					}
				}
				array2[0] = resizableArray18;
				Debug.LogFormat("Finished simplification with triangle count {0}", array2);
				return;
			}
		}
		goto IL_0a8c;
		IL_0a8c:
		throw new NullReferenceException();
	}

	public void SimplifyMeshLossless()
	{
		//IL_055c: Expected I, but got O
		//IL_0112: Expected O, but got I
		//IL_0236: Expected I, but got O
		//IL_0187: Expected O, but got I4
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Expected O, but got Unknown
		//IL_0345: Expected O, but got I
		//IL_02c6: Expected I, but got O
		//IL_03fa: Expected I, but got O
		//IL_0428: Expected I, but got O
		ResizableArray<bool> resizableArray = new ResizableArray<bool>(0);
		ResizableArray<bool> resizableArray2 = new ResizableArray<bool>(0);
		resizableArray2._002Ector(0);
		bool flag = triangles == null;
		int num = 0;
		nint num2 = 0;
		ResizableArray<bool> resizableArray3 = resizableArray2;
		if (!flag)
		{
			resizableArray3 = (ResizableArray<bool>)(object)triangles;
			bool flag2 = vertices == null;
			num = 0;
			num2 = 0;
			if (!flag2)
			{
				int num3 = 0;
				int num4 = 0;
				ResizableArray<bool> resizableArray5 = default(ResizableArray<bool>);
				ResizableArray<bool> resizableArray6 = default(ResizableArray<bool>);
				ResizableArray<bool> resizableArray7 = default(ResizableArray<bool>);
				ResizableArray<bool> resizableArray8 = default(ResizableArray<bool>);
				ResizableArray<bool> resizableArray9 = default(ResizableArray<bool>);
				ResizableArray<bool> resizableArray11 = default(ResizableArray<bool>);
				ResizableArray<bool> deleted = default(ResizableArray<bool>);
				ResizableArray<bool> deleted2 = default(ResizableArray<bool>);
				ref int deletedTris = default(ref int);
				ResizableArray<bool> resizableArray12 = default(ResizableArray<bool>);
				object obj2 = default(object);
				ResizableArray<bool> resizableArray13 = default(ResizableArray<bool>);
				while (true)
				{
					UpdateMesh(num4);
					num2 = (nint)triangles;
					bool flag3 = triangles == null;
					num = num4;
					resizableArray3 = (ResizableArray<bool>)(object)this;
					if (flag3)
					{
						break;
					}
					bool flag4 = vertices == null;
					num = num4;
					resizableArray3 = (ResizableArray<bool>)(object)this;
					if (flag4)
					{
						break;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ r8_v15 (Il2CppMethodInfo)+18]");
					if ((nint)0 > (nint)0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ r8_v15 (Il2CppMethodInfo)+10]");
						num2 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ r8_v15 (Il2CppMethodInfo)+10]");
						ResizableArray<bool> resizableArray4 = (ResizableArray<bool>)((nint)0 + (nint)97);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ r8_v15 (Il2CppMethodInfo)+10]");
						bool flag5 = (nint)0 == 0;
						int num5 = num3;
						int num6 = num3;
						num = num3;
						resizableArray3 = resizableArray4;
						if (flag5)
						{
							break;
						}
						int num8;
						do
						{
							int num7 = num6;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ r8_v15 (Il2CppMethodInfo)+18]");
							if ((nint)num7 < (nint)0)
							{
								resizableArray4 = (ResizableArray<bool>)0;
								num6++;
								resizableArray4 = (ResizableArray<bool>)(resizableArray4 + 96);
								num5++;
								num8 = num5;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ r8_v15 (Il2CppMethodInfo)+18]");
								continue;
							}
							throw new IndexOutOfRangeException();
						}
						while ((nint)num8 < (nint)0);
					}
					if (verbose)
					{
						object[] array = new object[2];
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						if (resizableArray5 != null)
						{
							nint num9 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v646 @ rdx_v39 (Il2CppClass<System.Object[]>)+40]");
							int num5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v646 @ rdx_v39 (Il2CppClass<System.Object[]>)+40]");
							resizableArray5._002Ector(0);
							bool flag6 = resizableArray6 == null;
							ResizableArray<bool> resizableArray4 = resizableArray5;
							if (flag6)
							{
								resizableArray4._002Ector(num5);
								throw resizableArray7;
							}
						}
						array[0] = resizableArray5;
						Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
						if (resizableArray8 != null)
						{
							nint num10 = (nint)array;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v884 @ rdx_v37 (Il2CppClass<System.Object[]>)+40]");
							int capacity = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v884 @ rdx_v37 (Il2CppClass<System.Object[]>)+40]");
							resizableArray8._002Ector(0);
							bool flag7 = resizableArray9 == null;
							ResizableArray<bool> resizableArray10 = resizableArray8;
							if (flag7)
							{
								resizableArray10._002Ector(capacity);
								throw resizableArray11;
							}
						}
						array[1] = resizableArray8;
						Debug.LogFormat("Lossless iteration {0} - triangles {1}", array);
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v339 @ r8_v15 (Il2CppMethodInfo)+18]");
						object obj = 0;
						int num11 = num4;
						num3 = 0;
					}
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v250 @ rcx_v7 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<System.Boolean>)+18]");
					RemoveVertexPass(0, 0, 0.001, deleted, deleted2, ref deletedTris);
					if (num3 > 0)
					{
						num4++;
						bool flag8 = num4 < 9999;
						int num12 = num3;
						if (flag8)
						{
							continue;
						}
					}
					CompactMesh();
					if (!verbose)
					{
						return;
					}
					object[] array2 = new object[1];
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					if (resizableArray12 != null)
					{
						nint num13 = (nint)array2;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v909 @ rdx_v27 (Il2CppClass<System.Object[]>)+40]");
						num = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
						bool flag9 = obj2 == null;
						num2 = unchecked((nint)null);
						resizableArray3 = resizableArray12;
						if (flag9)
						{
							resizableArray3._002Ector(num);
							throw resizableArray13;
						}
					}
					array2[0] = resizableArray12;
					Debug.LogFormat("Finished simplification with triangle count {0}", array2);
					return;
				}
			}
		}
		throw new NullReferenceException();
	}

	public unsafe Mesh ToMesh()
	{
		//IL_0071: Expected O, but got I
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00bd: Expected O, but got I
		//IL_00de: Expected O, but got I4
		//IL_00e7: Expected O, but got I4
		//IL_029e: Expected O, but got I
		//IL_02c7: Expected O, but got I
		//IL_015c: Expected O, but got I4
		//IL_016c: Expected O, but got I
		//IL_02f0: Expected O, but got I
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Expected O, but got Unknown
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Expected O, but got Unknown
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Expected O, but got Unknown
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Expected O, but got Unknown
		//IL_022a: Expected O, but got I4
		//IL_023a: Expected O, but got I
		//IL_024b: Expected O, but got I4
		//IL_025b: Expected O, but got I
		//IL_0319: Expected O, but got I
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Expected O, but got Unknown
		//IL_0df8: Expected O, but got I4
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Expected O, but got Unknown
		//IL_0781: Expected O, but got I4
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Expected O, but got Unknown
		//IL_0a04: Expected O, but got I4
		//IL_0513: Expected I, but got O
		//IL_079d: Expected I, but got O
		//IL_0a17: Expected I, but got O
		//IL_0566: Expected O, but got I
		//IL_07f0: Expected O, but got I
		//IL_0463: Expected O, but got Ref
		//IL_0471: Expected I, but got O
		//IL_0a6a: Expected O, but got I
		//IL_0584: Expected O, but got I
		//IL_05a3: Expected O, but got I
		//IL_080e: Expected O, but got I
		//IL_082d: Expected O, but got I
		//IL_0a88: Expected O, but got I
		//IL_0aa7: Expected O, but got I
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Expected O, but got Unknown
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Expected O, but got Unknown
		//IL_04f3: Expected I, but got O
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Expected O, but got Unknown
		//IL_09c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ca: Expected O, but got Unknown
		//IL_0614: Expected O, but got I
		//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4d: Expected O, but got Unknown
		//IL_089e: Expected O, but got I
		//IL_0b18: Expected O, but got I
		//IL_0661: Expected I, but got O
		//IL_08eb: Expected I, but got O
		//IL_0b65: Expected I, but got O
		//IL_06a1: Expected I, but got O
		//IL_06d7: Expected I, but got O
		//IL_0936: Expected I, but got O
		//IL_0946: Expected O, but got I
		//IL_096c: Expected I, but got O
		//IL_0720: Expected I, but got O
		//IL_0728: Expected I, but got O
		//IL_0bb5: Expected O, but got I
		//IL_0bdb: Expected I, but got O
		//IL_09a1: Expected I, but got O
		//IL_09a9: Expected I, but got O
		//IL_0c24: Expected I, but got O
		//IL_0c2c: Expected I, but got O
		MeshSimplifier meshSimplifier = this;
		ResizableArray<Vertex> resizableArray = vertices;
		Vector3[] array;
		if (vertices != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rsi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
			array = new Vector3[0];
			ResizableArray<Vertex> resizableArray2 = vertices;
			bool flag = vertices == null;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rsi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
			nint num = 0;
			meshSimplifier = (MeshSimplifier)(object)typeof(Vector3[]);
			if (!flag)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ r14_v6 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rsi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
				if ((nint)0 <= (nint)0)
				{
					goto IL_0d03;
				}
				object obj2 = array + 32;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ r14_v6 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
				object obj3 = (nint)0 + (nint)40;
				object obj5 = default(object);
				object obj4 = obj5;
				object obj7 = default(object);
				object obj6 = obj7;
				string text2 = default(string);
				string text = text2;
				object obj8 = 0;
				object obj9 = 0;
				meshSimplifier = (MeshSimplifier)(object)typeof(Vector3[]);
				string text3 = default(string);
				while (true)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v196 @ r14_v6 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+10]");
					bool flag2 = (nint)0 == 0;
					obj5 = obj4;
					obj7 = obj6;
					text2 = text;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rsi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
					num = 0;
					if (flag2)
					{
						break;
					}
					object obj10 = obj9;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v197 @ r14_v7+18]");
					if ((nint)obj10 < 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm6\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"unpckhpd xmm6,xmm6\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm1,xmm6\"");
						bool flag3 = array == null;
						obj5 = 0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rsi_v35+10]");
						obj7 = 0;
						text2 = null;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rsi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
						num = 0;
						meshSimplifier = (MeshSimplifier)(object)typeof(Vector3d);
						if (flag3)
						{
							break;
						}
						if ((nint)obj9 < array.Length)
						{
							obj9++;
							obj2 = text3;
							obj8++;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rsi_v35+10]");
							_ = 0;
							obj3 -= -128;
							obj2 += 12;
							object obj11 = obj8;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v50 @ rsi_v1 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<MTAssets.UltimateLODSystem.MeshSimplifier.Internal.Vertex>)+18]");
							bool flag4 = (nint)obj11 < 0;
							obj5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rsi_v35+10]");
							obj7 = 0;
							text2 = text3;
							obj4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v212 @ rsi_v35+10]");
							obj6 = 0;
							text = text3;
							meshSimplifier = (MeshSimplifier)(object)typeof(Vector3d);
							if (flag4)
							{
								continue;
							}
							goto IL_0d03;
						}
					}
					goto IL_0ca8;
				}
			}
		}
		goto IL_0c92;
		IL_0ca8:
		return (Mesh)(object)new IndexOutOfRangeException();
		IL_0d03:
		Vector3[] normals;
		if (vertNormals != null)
		{
			ResizableArray<Vector3> resizableArray3 = vertNormals;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v630 @ rax_v116 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector3>)+10]");
			normals = (Vector3[])0;
		}
		else
		{
			normals = null;
		}
		Vector4[] tangents;
		if (vertTangents != null)
		{
			ResizableArray<Vector4> resizableArray4 = vertTangents;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1011 @ rax_v113 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Vector4>)+10]");
			tangents = (Vector4[])0;
		}
		else
		{
			tangents = null;
		}
		List<Vector4>[] uvs4D;
		if (vertColors != null)
		{
			ResizableArray<Color> resizableArray5 = vertColors;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1068 @ rax_v110 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.Color>)+10]");
			uvs4D = (List<Vector4>[])0;
		}
		else
		{
			uvs4D = null;
		}
		Matrix4x4[] array2;
		if (vertBoneWeights != null)
		{
			ResizableArray<BoneWeight> resizableArray6 = vertBoneWeights;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1098 @ rax_v107 (MTAssets.UltimateLODSystem.MeshSimplifier.ResizableArray`1<UnityEngine.BoneWeight>)+10]");
			array2 = (Matrix4x4[])0;
		}
		else
		{
			array2 = null;
		}
		int[][] array3 = new int[subMeshCount][];
		if (subMeshCount > 0)
		{
			object obj12 = array3 + 32;
			int num2 = 0;
			while (true)
			{
				int[] subMeshTriangles = GetSubMeshTriangles(num2);
				bool flag5 = array3 == null;
				List<Vector2> list = null;
				nint num = num2;
				meshSimplifier = this;
				if (flag5)
				{
					break;
				}
				if (num2 >= array3.Length)
				{
					goto IL_0ca8;
				}
				obj12 = subMeshTriangles;
				num2++;
				obj12 += 8;
				bool flag6 = num2 < subMeshCount;
				list = null;
				if (flag6)
				{
					continue;
				}
				goto IL_0d73;
			}
			goto IL_0c92;
		}
		goto IL_0d73;
		IL_0d73:
		if (blendShapes != null)
		{
			ResizableArray<BlendShapeContainer> resizableArray7 = blendShapes;
			nint num = resizableArray7.length;
			BlendShape[] array4 = new BlendShape[resizableArray7.length];
			bool flag7 = array4 == null;
			meshSimplifier = null;
			if (flag7)
			{
				goto IL_0c92;
			}
			object obj13 = array4 + 32;
			MeshSimplifier meshSimplifier2 = null;
			MeshSimplifier meshSimplifier3 = null;
			BlendShapeContainer blendShapeContainer = default(BlendShapeContainer);
			while ((nint)meshSimplifier3 < array4.Length)
			{
				meshSimplifier = (MeshSimplifier)(object)blendShapes;
				if (blendShapes != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803BA490");
					bool flag8 = blendShapeContainer == null;
					List<Vector2> list = (List<Vector2>)(&blendShapeContainer);
					nint num3 = 0;
					num = (nint)blendShapeContainer;
					if (!flag8)
					{
						BlendShape blendShape = blendShapeContainer.ToBlendShape();
						if ((nint)meshSimplifier2 < array4.Length)
						{
							string text2 = blendShape.ShapeName;
							obj13 = blendShape.ShapeName;
							meshSimplifier2 = (MeshSimplifier)(meshSimplifier2 + 1);
							obj13 += 16;
							list = null;
							num3 = 0;
							num = unchecked((nint)null);
							meshSimplifier3 = meshSimplifier2;
							continue;
						}
						goto IL_0ca8;
					}
				}
				goto IL_0c92;
			}
		}
		bool flag9 = vertUV2D == null;
		BlendShape[] array5 = null;
		if (!flag9)
		{
			nint num = UVChannelCount;
			BlendShape[] array6 = (BlendShape[])(object)new List<Vector2>[UVChannelCount];
			object obj14 = 32;
			int num4 = 0;
			object obj16 = default(object);
			List<Vector3> list3 = default(List<Vector3>);
			while (true)
			{
				nint num5 = (nint)typeof(MeshSimplifier);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1279 @ rax_v81 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier>)+B8]");
				nint num6 = 0;
				if (num4 >= UVChannelCount)
				{
					break;
				}
				UVChannels<Vector2> uVChannels = vertUV2D;
				bool flag10 = vertUV2D == null;
				meshSimplifier = (MeshSimplifier)num6;
				if (!flag10)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ rax_v82 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
					object obj15 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v229 @ rax_v82 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector2>)+10]");
					bool flag11 = (nint)0 == 0;
					meshSimplifier = (MeshSimplifier)num6;
					if (!flag11)
					{
						int num7 = num4;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ rax_v83+18]");
						if ((nint)num7 >= (nint)0)
						{
							goto IL_0ca8;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v230 @ rax_v83+v177 @ rbp_v16]");
						if ((nint)0 == 0)
						{
							goto IL_072d;
						}
						bool flag12 = array == null;
						meshSimplifier = (MeshSimplifier)num6;
						if (!flag12)
						{
							List<Vector2> list2 = new List<Vector2>(array.Length);
							GetUVs(num4, list2);
							bool flag13 = array6 == null;
							List<Vector2> list = list2;
							nint num3 = unchecked((nint)null);
							num = num4;
							meshSimplifier = this;
							if (!flag13)
							{
								if (list2 != null)
								{
									nint num8 = (nint)array6;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1678 @ rdx_v47 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.BlendShape[]>)+40]");
									num = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									bool flag14 = obj16 == null;
									list = list2;
									num3 = unchecked((nint)null);
									meshSimplifier = (MeshSimplifier)(object)list2;
									if (flag14)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										throw list3;
									}
								}
								if (num4 < array6.Length)
								{
									list = list2;
									num3 = unchecked((nint)null);
									num = (nint)list2;
									goto IL_072d;
								}
								goto IL_0ca8;
							}
						}
					}
				}
				goto IL_0c92;
				IL_072d:
				num4++;
				obj14 += 8;
			}
			array5 = array6;
		}
		if (vertUV3D != null)
		{
			nint num = UVChannelCount;
			List<Vector3>[] array7 = new List<Vector3>[UVChannelCount];
			object obj17 = 32;
			int num9 = 0;
			object obj20 = default(object);
			List<Vector4> list6 = default(List<Vector4>);
			while (true)
			{
				nint num10 = (nint)typeof(MeshSimplifier);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1359 @ rax_v64 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier>)+B8]");
				nint num11 = 0;
				if (num9 >= UVChannelCount)
				{
					break;
				}
				UVChannels<Vector3> uVChannels2 = vertUV3D;
				bool flag15 = vertUV3D == null;
				meshSimplifier = (MeshSimplifier)num11;
				if (!flag15)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ rax_v65 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
					object obj18 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v232 @ rax_v65 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector3>)+10]");
					bool flag16 = (nint)0 == 0;
					meshSimplifier = (MeshSimplifier)num11;
					if (!flag16)
					{
						int num12 = num9;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ rax_v66+18]");
						if ((nint)num12 >= (nint)0)
						{
							goto IL_0ca8;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v233 @ rax_v66+v178 @ rbp_v13]");
						if ((nint)0 == 0)
						{
							goto IL_09ae;
						}
						bool flag17 = array == null;
						meshSimplifier = (MeshSimplifier)num11;
						if (!flag17)
						{
							List<Vector3> list4 = new List<Vector3>(array.Length);
							GetUVs(num9, list4);
							bool flag18 = array7 == null;
							List<Vector2> list = (List<Vector2>)(object)list4;
							nint num3 = unchecked((nint)null);
							num = num9;
							meshSimplifier = (MeshSimplifier)(object)array7;
							if (!flag18)
							{
								bool flag19 = list4 == null;
								MeshSimplifier meshSimplifier4 = (MeshSimplifier)(object)array7;
								if (!flag19)
								{
									nint num13 = (nint)array7;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1701 @ rdx_v35 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier>)+40]");
									object obj19 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									bool flag20 = obj20 == null;
									list = (List<Vector2>)(object)list4;
									num3 = unchecked((nint)null);
									List<Vector3> list5 = list4;
									if (flag20)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										throw list6;
									}
									meshSimplifier4 = (MeshSimplifier)(object)array7;
								}
								int num14 = num9;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v767 @ rcx_v49 (MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier)+18]");
								if ((nint)num14 < (nint)0)
								{
									list = (List<Vector2>)(object)list4;
									num3 = unchecked((nint)null);
									num = (nint)list4;
									goto IL_09ae;
								}
								goto IL_0ca8;
							}
						}
					}
				}
				goto IL_0c92;
				IL_09ae:
				num9++;
				obj17 += 8;
			}
		}
		if (vertUV4D != null)
		{
			nint num = UVChannelCount;
			List<Vector4>[] array8 = new List<Vector4>[UVChannelCount];
			int num15 = 0;
			object obj21 = 32;
			object obj25 = default(object);
			object obj26 = default(object);
			while (true)
			{
				nint num16 = (nint)typeof(MeshSimplifier);
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1422 @ rax_v45 (Il2CppClass<MTAssets.UltimateLODSystem.MeshSimplifier.MeshSimplifier>)+B8]");
				nint num17 = 0;
				if (num15 >= UVChannelCount)
				{
					break;
				}
				UVChannels<Vector4> uVChannels3 = vertUV4D;
				bool flag21 = vertUV4D == null;
				meshSimplifier = (MeshSimplifier)num17;
				if (!flag21)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ rax_v46 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
					object obj22 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v235 @ rax_v46 (MTAssets.UltimateLODSystem.MeshSimplifier.Internal.UVChannels`1<UnityEngine.Vector4>)+10]");
					bool flag22 = (nint)0 == 0;
					meshSimplifier = (MeshSimplifier)num17;
					if (!flag22)
					{
						int num18 = num15;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v236 @ rax_v47+18]");
						if ((nint)num18 >= (nint)0)
						{
							goto IL_0ca8;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v199 @ r14_v11+v236 @ rax_v47]");
						if ((nint)0 == 0)
						{
							goto IL_0c31;
						}
						bool flag23 = array == null;
						meshSimplifier = (MeshSimplifier)num17;
						if (!flag23)
						{
							List<Vector4> list7 = new List<Vector4>(array.Length);
							GetUVs(num15, list7);
							bool flag24 = array8 == null;
							List<Vector2> list = (List<Vector2>)(object)list7;
							nint num3 = unchecked((nint)null);
							num = num15;
							meshSimplifier = this;
							if (!flag24)
							{
								if (list7 != null)
								{
									object obj23 = array8;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v1725 @ rdx_v25+40]");
									object obj24 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
									bool flag25 = obj25 == null;
									list = (List<Vector2>)(object)list7;
									num3 = unchecked((nint)null);
									List<Vector4> list8 = list7;
									if (flag25)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
										throw obj26;
									}
								}
								if (num15 < array8.Length)
								{
									list = (List<Vector2>)(object)list7;
									num3 = unchecked((nint)null);
									num = (nint)list7;
									goto IL_0c31;
								}
								goto IL_0ca8;
							}
						}
					}
				}
				goto IL_0c92;
				IL_0c31:
				num15++;
				obj21 += 8;
			}
		}
		Color[] colors = default(Color[]);
		BoneWeight[] boneWeights = default(BoneWeight[]);
		List<Vector2>[] uvs2D = default(List<Vector2>[]);
		List<Vector3>[] uvs3D = default(List<Vector3>[]);
		return MeshUtils.CreateMesh(array, array3, normals, tangents, colors, boneWeights, uvs2D, uvs3D, uvs4D, array2, array5);
		IL_0c92:
		throw new NullReferenceException();
	}

	public static void ValidateOptions(SimplificationOptions options)
	{
		//IL_0017: Expected O, but got I4
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0091: Expected O, but got I4
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Expected O, but got Unknown
		object obj = options.EnableSmartLink ^ options.EnableSmartLink;
		object obj2 = options.EnableSmartLink & obj;
		bool flag = (nint)obj2 < 0;
		bool flag2 = (options.EnableSmartLink ? 1 : 0) < (false ? 1 : 0);
		bool flag3 = !options.EnableSmartLink;
		if (!flag3)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm3,qword ptr [rcx+8]\"");
			bool flag4 = flag2 == flag;
			object obj3 = !flag3;
			object obj4 = flag4 & obj3;
			if (obj4 != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				ValidateSimplificationOptionsException ex = new ValidateSimplificationOptionsException("VertexLinkDistance", "The vertex link distance cannot be negative when smart linking is enabled.");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex;
			}
		}
		if (options.MaxIterationCount > 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm3,qword ptr [rcx+18h]\"");
			if (options.MaxIterationCount < 0)
			{
				object obj5 = default(object);
				if (~(options.ManualUVComponentCount ? 1u : 0u) != 0 || ((nint)obj5 >= 0 && (nint)obj5 <= 4))
				{
					return;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				ValidateSimplificationOptionsException ex2 = new ValidateSimplificationOptionsException("UVComponentCount", "The UV component count cannot be below 0 or above 4 when manual UV component count is enabled.");
				ex2._002Ector("UVComponentCount", "The UV component count cannot be below 0 or above 4 when manual UV component count is enabled.");
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
				throw ex2;
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			ValidateSimplificationOptionsException ex3 = new ValidateSimplificationOptionsException("Agressiveness", "The aggressiveness has to be above zero to make sense. Recommended is around 7.");
			ex3._002Ector("Agressiveness", "The aggressiveness has to be above zero to make sense. Recommended is around 7.");
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex3;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		ValidateSimplificationOptionsException ex4 = new ValidateSimplificationOptionsException("MaxIterationCount", "The max iteration count cannot be zero or negative, since there would be nothing for the algorithm to do.");
		ex4._002Ector("MaxIterationCount", "The max iteration count cannot be zero or negative, since there would be nothing for the algorithm to do.");
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
		throw ex4;
	}
}
