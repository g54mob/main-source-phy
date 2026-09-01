using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

public class ImpactExplosionSpawner : MonoBehaviour
{
	private sealed class _003CSpawnExplosionNextFrame_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ImpactExplosionSpawner _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CSpawnExplosionNextFrame_003Ed__9(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0008: Expected O, but got Ref
			//IL_001c: Expected I4, but got I8
			//IL_006d: Expected I4, but got I8
			//IL_0599: Expected I4, but got O
			//IL_00a3: Expected O, but got I
			//IL_00c5: Expected O, but got I
			//IL_00dc: Expected O, but got I
			//IL_05ea: Expected O, but got Ref
			//IL_01aa: Expected O, but got I
			//IL_010a: Expected O, but got I
			//IL_014a: Expected O, but got I
			//IL_022a: Expected O, but got I
			//IL_0240: Expected O, but got I
			//IL_031b: Expected O, but got Ref
			//IL_0530: Expected F4, but got I
			//IL_03ce: Expected O, but got Ref
			//IL_0655: Expected O, but got Ref
			//IL_03fc: Expected F4, but got I
			//IL_03fc: Expected O, but got Ref
			//IL_03fc: Expected O, but got Ref
			//IL_0489: Expected O, but got Ref
			//IL_044b: Expected F4, but got I
			//IL_044b: Expected O, but got Ref
			//IL_044b: Expected O, but got Ref
			//IL_04b6: Expected O, but got Ref
			//IL_04e9: Expected O, but got Ref
			object obj2 = default(object);
			object obj = (object)(&obj2);
			UnityEngine.Object obj3 = _003C_003E4__this;
			_ = 0;
			_ = 0;
			_ = 0;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				_003C_003E2__current = null;
				_003C_003E1__state = 1;
				return true;
			}
			GameObject gameObject2;
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_058b;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+20]");
				string name;
				string text;
				if ((UnityEngine.Object)0 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+28]");
					UnityEngine.Object obj4 = (UnityEngine.Object)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+28]");
					if ((UnityEngine.Object)0 == null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+30]");
						GameObject gameObject = GameObject.Find((string)0);
						if (!(gameObject != null))
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+30]");
							string message = "[ImpactExplosionSpawner] Could not find GameObject named '" + (string)0 + "'. The VFX will be instantiated at root.";
							Debug.LogWarning(message);
						}
						else
						{
							if ((object)gameObject == null)
							{
								goto IL_058b;
							}
							Transform transform = gameObject.transform;
							obj4 = transform;
						}
					}
					object obj5 = (object)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.AddByteOffset(ref obj2, 80));
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+50]");
					Transform transform3;
					if ((UnityEngine.Object)0 == null)
					{
						Transform transform2 = _003C_003E4__this.transform;
						if ((object)transform2 == null)
						{
							goto IL_058b;
						}
						transform3 = transform2;
					}
					else
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+50]");
						if ((nint)0 == 0)
						{
							goto IL_058b;
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v6 @ rbp_v1+50]");
						transform3 = (Transform)0;
					}
					Vector3 localPosition = transform3.localPosition;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+20]");
					gameObject2 = UnityEngine.Object.Instantiate((GameObject)0);
					if (gameObject2 != null)
					{
						bool flag = obj4 == null;
						if ((object)gameObject2 != null)
						{
							Transform transform4;
							Transform parent;
							if (flag)
							{
								transform4 = gameObject2.transform;
								if ((object)transform4 == null)
								{
									goto IL_058b;
								}
								parent = null;
							}
							else
							{
								transform4 = gameObject2.transform;
								if ((object)transform4 == null)
								{
									goto IL_058b;
								}
								parent = (Transform)obj4;
							}
							transform4.SetParent(parent, worldPositionStays: false);
							Transform transform5 = gameObject2.transform;
							if ((object)transform5 != null)
							{
								float num = default(float);
								transform5.localPosition = (Vector3)(&num);
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+3C]");
								if ((nint)0 == 0 || !(obj4 != null))
								{
									goto IL_04ee;
								}
								Transform transform6 = gameObject2.transform;
								if ((object)transform6 != null)
								{
									Vector3 localPosition2 = transform6.localPosition;
									if ((object)obj4 != null)
									{
										Vector3 vector = ((Transform)obj4).TransformPoint((Vector3)(&num));
										Vector3 vector2 = ((Transform)obj4).TransformDirection((Vector3)(&num));
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+44]");
										float num2 = default(float);
										int layerMask = default(int);
										QueryTriggerInteraction queryTriggerInteraction = default(QueryTriggerInteraction);
										bool flag2 = Physics.Raycast((Vector3)(&num), (Vector3)(&num2), out var hitInfo, 0f, layerMask, queryTriggerInteraction);
										float num3 = default(float);
										num2 = num3;
										if (!flag2)
										{
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+44]");
											if (!Physics.Raycast((Vector3)(&num), (Vector3)(&num2), out hitInfo, 0f, layerMask, queryTriggerInteraction))
											{
												goto IL_04ee;
											}
											num2 = vector2.x;
										}
										Transform transform7 = gameObject2.transform;
										Vector3 point = hitInfo.point;
										if ((object)transform7 != null)
										{
											transform7.position = (Vector3)(&num2);
											Transform transform8 = gameObject2.transform;
											Vector3 point2 = hitInfo.point;
											Vector3 vector3 = ((Transform)obj4).InverseTransformPoint((Vector3)(&num2));
											if ((object)transform8 != null)
											{
												transform8.localPosition = (Vector3)(&num2);
												goto IL_04ee;
											}
										}
									}
								}
							}
						}
						goto IL_058b;
					}
					name = _003C_003E4__this.name;
					text = "[ImpactExplosionSpawner] Failed to instantiate explosionPrefab on '";
				}
				else
				{
					name = _003C_003E4__this.name;
					text = "[ImpactExplosionSpawner] Explosion prefab not assigned on '";
				}
				string message2 = text + name + "'.";
				Debug.LogWarning(message2);
			}
			goto IL_0535;
			IL_04ee:
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+38]");
			if ((nint)0 > (nint)0)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v35 @ rbx_v1 (UnityEngine.Object)+38]");
				UnityEngine.Object.Destroy(gameObject2, 0f);
			}
			goto IL_0535;
			IL_0535:
			return false;
			IL_058b:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public GameObject explosionPrefab;

	public Transform externalParent;

	public string externalEnvironmentName;

	public float destroyAfterSeconds;

	public bool enableSurfaceSnap;

	public float raycastStartOffset;

	public float raycastMaxDistance;

	public LayerMask raycastLayerMask;

	private void OnEnable()
	{
		_003CSpawnExplosionNextFrame_003Ed__9 obj = new _003CSpawnExplosionNextFrame_003Ed__9(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator SpawnExplosionNextFrame()
	{
		_003CSpawnExplosionNextFrame_003Ed__9 obj = new _003CSpawnExplosionNextFrame_003Ed__9(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public ImpactExplosionSpawner()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3AA96]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		externalEnvironmentName = "External Enviroment";
		enableSurfaceSnap = true;
		raycastStartOffset = 50f;
		raycastMaxDistance = 200f;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @181DC3B50");
		LayerMask layerMask = default(LayerMask);
		raycastLayerMask = layerMask;
		base._002Ector();
	}
}
