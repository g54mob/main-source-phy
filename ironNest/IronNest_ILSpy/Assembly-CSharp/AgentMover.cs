using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class AgentMover : MonoBehaviour
{
	private sealed class _003CMoveOnOffMeshLink_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AgentMover _003C_003E4__this;

		public bool reverseDirection;

		public Spline spline;

		private float _003CcurrentTime_003E5__2;

		private Vector3 _003CagentStartPosition_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CMoveOnOffMeshLink_003Ed__29(int _003C_003E1__state)
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
			//IL_0103: Expected I4, but got I8
			//IL_0588: Expected I4, but got O
			//IL_001d: Expected O, but got I4
			//IL_00d0: Expected I4, but got I8
			//IL_005a: Expected I4, but got I8
			//IL_019a: Expected O, but got F4
			//IL_01ea: Invalid comparison between I4 and F4
			//IL_0235: Expected F4, but got I4
			//IL_040c: Expected O, but got Ref
			//IL_0333: Expected O, but got Ref
			//IL_045b: Expected O, but got Ref
			object obj2 = default(object);
			object obj = (object)(&obj2);
			AgentMover agentMover = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			if (!flag)
			{
				object obj3 = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj3 == 1)
					{
						_003C_003E1__state = -1;
						if ((object)_003C_003E4__this == null || (object)agentMover._Agent == null)
						{
							goto IL_057a;
						}
						agentMover._Agent.isStopped = false;
					}
					return false;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_05b6;
				}
			}
			else
			{
				_003C_003E1__state = -1;
				_003CcurrentTime_003E5__2 = 0f;
				if ((object)_003C_003E4__this != null && (object)agentMover._Agent != null)
				{
					Transform transform = agentMover._Agent.transform;
					if ((object)transform != null)
					{
						Vector3 position = transform.position;
						_003CagentStartPosition_003E5__3 = (Vector3)position.x;
						_ = position.z;
						goto IL_05b6;
					}
				}
			}
			goto IL_057a;
			IL_0659:
			return true;
			IL_05b6:
			float value;
			Transform transform2;
			Vector3 startPos;
			if (agentMover._jumpDuration > _003CcurrentTime_003E5__2)
			{
				float deltaTime = Time.deltaTime;
				float num = (_003CcurrentTime_003E5__2 = deltaTime + _003CcurrentTime_003E5__2) / agentMover._jumpDuration;
				if (!(0f > num))
				{
					if (num > 1f)
					{
						num = 1f;
					}
				}
				else
				{
					num = 0f;
				}
				value = ((!reverseDirection) ? num : (1f - num));
				if ((object)agentMover._Agent != null)
				{
					transform2 = agentMover._Agent.transform;
					bool flag2 = !reverseDirection;
					Spline spline = this.spline;
					if (!flag2)
					{
						if ((object)this.spline != null && (object)spline._start != null)
						{
							Vector3 position2 = spline._start.position;
							if ((object)spline._middle != null)
							{
								Vector3 position3 = spline._middle.position;
								_ = _003CagentStartPosition_003E5__3;
								startPos = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (AgentMover+<MoveOnOffMeshLink>d__29)+44]");
								_ = 0;
								_ = position3.z;
								_ = position3.x;
								_ = position2.x;
								_ = position2.z;
								goto IL_061d;
							}
						}
					}
					else if ((object)this.spline != null && (object)spline._end != null)
					{
						Vector3 position4 = spline._end.position;
						if ((object)spline._middle != null)
						{
							Vector3 position5 = spline._middle.position;
							_ = position4.x;
							startPos = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 41));
							_ = position4.z;
							_ = _003CagentStartPosition_003E5__3;
							_ = position5.z;
							_ = position5.x;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (AgentMover+<MoveOnOffMeshLink>d__29)+44]");
							_ = 0;
							goto IL_061d;
						}
					}
				}
			}
			else if ((object)agentMover._Agent != null)
			{
				agentMover._Agent.CompleteOffMeshLink();
				if ((object)agentMover._Agent != null)
				{
					agentMover._Agent.isStopped = true;
					if (agentMover.OnLand != null)
					{
						agentMover.OnLand.Invoke();
					}
					agentMover._onNavMeshLink = false;
					WaitForSeconds waitForSeconds = new WaitForSeconds(agentMover._jumpFinishedDelay);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 2;
					goto IL_0659;
				}
			}
			goto IL_057a;
			IL_061d:
			Vector3 endPos = default(Vector3);
			Vector3 midPos = default(Vector3);
			Vector3 vector = this.spline.CalculatePosition(value, startPos, endPos, midPos);
			if ((object)transform2 == null)
			{
				goto IL_057a;
			}
			_ = vector.z;
			Vector3 position6 = (Vector3)System.Runtime.CompilerServices.Unsafe.AsPointer(ref System.Runtime.CompilerServices.Unsafe.SubtractByteOffset(ref obj2, 9));
			_ = vector.x;
			transform2.position = position6;
			WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
			_003C_003E2__current = waitForEndOfFrame;
			_003C_003E1__state = 1;
			goto IL_0659;
			IL_057a:
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

	private sealed class _003CPerformJumpRoutine_003Ed__27 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AgentMover _003C_003E4__this;

		public NavMeshLink link;

		public Spline spline;

		private bool _003CreverseDirection_003E5__2;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CPerformJumpRoutine_003Ed__27(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_02a2: Expected I4, but got I8
			//IL_032a: Expected I4, but got O
			//IL_00f5: Expected O, but got Ref
			//IL_019a: Expected O, but got Ref
			AgentMover agentMover = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					bool flag = _003C_003E4__this.CheckIfJumpingFromEndToStart(link);
					_003CreverseDirection_003E5__2 = flag;
					if ((object)link != null)
					{
						GameObject gameObject = link.gameObject;
						if ((object)gameObject != null)
						{
							Transform transform = gameObject.transform;
							if ((object)link != null && (object)transform != null)
							{
								Vector3 vector2 = default(Vector3);
								Vector3 vector = transform.TransformPoint((Vector3)(&vector2));
								if ((object)link != null)
								{
									GameObject gameObject2 = link.gameObject;
									if ((object)gameObject2 != null)
									{
										Transform transform2 = gameObject2.transform;
										if ((object)link != null && (object)transform2 != null)
										{
											Vector3 vector3 = transform2.TransformPoint((Vector3)(&vector2));
											bool flag2 = !_003CreverseDirection_003E5__2;
											if (!flag2)
											{
											}
											object obj2 = default(object);
											object obj3 = default(object);
											object obj = (flag2 ? obj2 : obj3);
											Spline spline = this.spline;
											if ((object)this.spline != null)
											{
												object obj4 = default(object);
												string arg = ((System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4)) ? spline.jumpDownTrigger : spline.jumpUpTrigger);
												if (agentMover.OnStartJump != null)
												{
													agentMover.OnStartJump.Invoke(arg);
												}
												WaitForSeconds waitForSeconds = new WaitForSeconds(agentMover._jumpPreparationDelay);
												_003C_003E2__current = waitForSeconds;
												_003C_003E1__state = 1;
												return true;
											}
										}
									}
								}
							}
						}
					}
				}
				goto IL_031c;
			}
			if (_003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_031c;
				}
				_003CMoveOnOffMeshLink_003Ed__29 obj5 = new _003CMoveOnOffMeshLink_003Ed__29(0);
				obj5._003C_003E1__state = 0;
				obj5._003C_003E4__this = _003C_003E4__this;
				obj5.spline = this.spline;
				obj5.reverseDirection = _003CreverseDirection_003E5__2;
				Coroutine linkRoutine = _003C_003E4__this.StartCoroutine(obj5);
				agentMover._linkRoutine = linkRoutine;
			}
			return false;
			IL_031c:
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

	private NavMeshAgent _Agent;

	private Action<float> m_OnSpeedChanged;

	private bool _onNavMeshLink;

	private float _jumpDuration = 0.8f;

	private float _jumpPreparationDelay = 0.2f;

	private float _jumpFinishedDelay = 0.2f;

	public UnityEvent OnLand;

	public UnityEvent<string> OnStartJump;

	private Coroutine _jumpRoutine;

	private Coroutine _linkRoutine;

	private NavMeshPath _reusablePath;

	public bool IsOnNavMeshLink => _onNavMeshLink;

	public event Action<float> OnSpeedChanged
	{
		add
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 40;
			Delegate obj2 = this.m_OnSpeedChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Combine(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
		remove
		{
			//IL_006f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0074: Expected O, but got Unknown
			object obj = this + 40;
			Delegate obj2 = this.m_OnSpeedChanged;
			Delegate obj5 = default(Delegate);
			while (true)
			{
				Delegate obj3 = Delegate.Remove(obj2, value);
				bool flag = (object)obj3 == null;
				Delegate obj4 = obj3;
				if (!flag)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
					if ((object)obj4 == null)
					{
						break;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802AC5F0");
				bool flag2 = (object)obj5 != obj2;
				obj2 = obj5;
				if (!flag2)
				{
					return;
				}
			}
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
		}
	}

	private void Start()
	{
		_Agent.autoTraverseOffMeshLink = false;
		if (OnStartJump == null)
		{
			UnityEvent<string> onStartJump = new UnityEvent<string>();
			OnStartJump = onStartJump;
		}
	}

	public void PauseMovement()
	{
		if (_Agent.isActiveAndEnabled && _Agent.isOnNavMesh)
		{
			_Agent.isStopped = true;
		}
	}

	public void ResumeMovement()
	{
		if (_Agent.isActiveAndEnabled && _Agent.isOnNavMesh)
		{
			_Agent.isStopped = false;
		}
	}

	public void EnableAgent(bool enable)
	{
		_Agent.enabled = enable;
	}

	public unsafe bool IsPathReachable(Vector3 target)
	{
		//IL_0123: Expected I4, but got O
		//IL_00c9: Expected O, but got Ref
		if ((object)_Agent != null)
		{
			if (!_Agent.isActiveAndEnabled)
			{
				goto IL_010f;
			}
			if ((object)_Agent != null)
			{
				if (!_Agent.isOnNavMesh)
				{
					goto IL_010f;
				}
				if (_reusablePath == null)
				{
					NavMeshPath reusablePath = new NavMeshPath();
					_reusablePath = reusablePath;
				}
				if ((object)_Agent != null)
				{
					object obj = default(object);
					bool flag = _Agent.CalculatePath((Vector3)(&obj), _reusablePath);
					if (_reusablePath != null)
					{
						NavMeshPathStatus status = _reusablePath.status;
						return status == NavMeshPathStatus.PathComplete;
					}
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
		IL_010f:
		return false;
	}

	public unsafe void SetDestination(Vector3 destination)
	{
		//IL_0031: Expected O, but got Ref
		if (!_onNavMeshLink)
		{
			object obj = default(object);
			_Agent.destination = (Vector3)(&obj);
		}
	}

	public unsafe void Teleport(Vector3 destination)
	{
		//IL_000f: Expected O, but got Ref
		object obj = default(object);
		bool flag = _Agent.Warp((Vector3)(&obj));
	}

	public bool HasReachedDestination()
	{
		//IL_01ca: Expected I4, but got O
		//IL_0199: Invalid comparison between F4 and I4
		if ((object)_Agent != null)
		{
			if (_Agent.pathPending)
			{
				goto IL_01b6;
			}
			if ((object)_Agent != null)
			{
				float remainingDistance = _Agent.remainingDistance;
				if ((object)_Agent != null)
				{
					float stoppingDistance = _Agent.stoppingDistance;
					if (stoppingDistance < remainingDistance)
					{
						goto IL_01b6;
					}
					if ((object)_Agent != null)
					{
						if (_Agent.hasPath)
						{
							if ((object)_Agent == null)
							{
								goto IL_01bc;
							}
							Vector3 velocity = _Agent.velocity;
							object obj2 = default(object);
							object obj = obj2 * obj2;
							float num = velocity.x * velocity.x;
							float num2 = velocity.z * velocity.z;
							float num3 = (float)obj + num;
							float num4 = num3 + num2;
							Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804FB78Bh\"");
							if (num4 != 0f)
							{
								goto IL_01b6;
							}
						}
						return true;
					}
				}
			}
		}
		goto IL_01bc;
		IL_01b6:
		return false;
		IL_01bc:
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private unsafe void Update()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_005a: Expected F4, but got I4
		//IL_0966: Expected O, but got I
		//IL_096e: Expected O, but got F4
		//IL_008c: Expected O, but got I4
		//IL_0367: Expected O, but got F4
		//IL_00ce: Expected I, but got O
		//IL_00e0: Expected O, but got F4
		//IL_0937: Expected O, but got Ref
		//IL_011a: Invalid comparison between I4 and F4
		//IL_09d7: Expected O, but got F4
		//IL_0165: Expected F4, but got I4
		//IL_0985: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Expected I, but got Unknown
		//IL_03dd: Expected O, but got F4
		//IL_01e0: Expected I, but got O
		//IL_020d: Expected O, but got F4
		//IL_0223: Expected I, but got O
		//IL_0233: Expected O, but got I
		//IL_026f: Expected O, but got F4
		//IL_0431: Expected O, but got F4
		//IL_028d: Expected O, but got I
		//IL_02ca: Expected O, but got F4
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Expected O, but got Unknown
		//IL_04ba: Expected O, but got F4
		//IL_0317: Expected O, but got I
		//IL_0341: Expected I, but got O
		//IL_0504: Expected O, but got I4
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Expected O, but got Unknown
		//IL_0543: Expected F4, but got I4
		//IL_054b: Expected O, but got F4
		//IL_0a2a: Expected I, but got O
		//IL_0ae9: Invalid comparison between F4 and I4
		//IL_0b12: Expected O, but got I4
		//IL_0583: Expected I, but got O
		//IL_058c: Expected O, but got F4
		//IL_05bf: Expected I, but got O
		//IL_05cc: Expected O, but got F4
		//IL_05f7: Expected O, but got F4
		//IL_05ff: Invalid comparison between F4 and O
		//IL_062c: Expected I, but got O
		//IL_0662: Expected I, but got O
		//IL_06a2: Expected I, but got O
		//IL_0b6a: Expected O, but got Ref
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Expected O, but got Unknown
		//IL_0704: Invalid comparison between O and F4
		//IL_0b8d: Expected O, but got I4
		//IL_0b95: Expected F4, but got O
		//IL_0b9a: Expected I, but got O
		//IL_0ba2: Expected O, but got Ref
		//IL_0759: Expected O, but got I4
		//IL_0761: Expected F4, but got O
		//IL_0766: Expected I, but got O
		//IL_07b2: Expected O, but got I4
		//IL_07ba: Expected F4, but got O
		//IL_07bf: Expected I, but got O
		//IL_0c04: Expected O, but got Ref
		//IL_082a: Expected O, but got F4
		//IL_0842: Invalid comparison between I4 and F4
		//IL_088d: Expected F4, but got I4
		//IL_0c6e: Invalid comparison between I4 and F4
		//IL_0c42: Expected O, but got I4
		//IL_0c4a: Expected F4, but got O
		//IL_0c4f: Expected I, but got O
		//IL_0c57: Expected O, but got Ref
		//IL_08de: Expected O, but got I4
		//IL_08e6: Expected F4, but got O
		//IL_08eb: Expected I, but got O
		//IL_090b: Expected O, but got Ref
		object obj2 = default(object);
		object obj = obj2 - 40;
		Action<float> onSpeedChanged = this.m_OnSpeedChanged;
		bool flag = this.m_OnSpeedChanged == null;
		float num = 0f;
		nint num4 = default(nint);
		Quaternion quaternion;
		NavMeshAgent agent;
		if (!flag)
		{
			agent = _Agent;
			bool flag2 = (object)_Agent == null;
			quaternion = (Quaternion)0;
			if (!flag2)
			{
				Vector3 velocity = _Agent.velocity;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
				bool flag3 = (object)_Agent == null;
				nint num2 = unchecked((nint)null);
				agent = null;
				quaternion = (Quaternion)velocity.x;
				if (!flag3)
				{
					num = _Agent.speed;
					float num3 = velocity.x / num;
					if (!(0f > num3))
					{
						if (num3 > 1f)
						{
							num3 = 1f;
						}
					}
					else
					{
						num3 = 0f;
					}
					num4 = (nint)(obj + 48);
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v11 @ rbx_v1 (System.Action`1<System.Single>)+28]");
					num2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v11 @ rbx_v1 (System.Action`1<System.Single>)+18] (should have been resolved before IL gen)");
					goto IL_094d;
				}
			}
			goto IL_0941;
		}
		goto IL_094d;
		IL_0941:
		throw new NullReferenceException();
		IL_094d:
		bool flag4 = (object)_Agent == null;
		agent = (NavMeshAgent)num4;
		quaternion = (Quaternion)num;
		if (!flag4)
		{
			bool isOnOffMeshLink = _Agent.isOnOffMeshLink;
			bool flag5 = !isOnOffMeshLink;
			agent = null;
			if (!flag5)
			{
				bool flag6 = _onNavMeshLink;
				agent = null;
				if (!flag6)
				{
					_onNavMeshLink = true;
					bool flag7 = (object)_Agent == null;
					agent = null;
					quaternion = (Quaternion)num;
					if (!flag7)
					{
						UnityEngine.Object navMeshOwner = _Agent.navMeshOwner;
						nint num5 = (nint)typeof(NavMeshLink);
						bool flag8 = (object)navMeshOwner == null;
						UnityEngine.Object obj3 = navMeshOwner;
						agent = (NavMeshAgent)(object)typeof(NavMeshLink);
						quaternion = (Quaternion)num;
						if (!flag8)
						{
							nint num2 = (nint)navMeshOwner;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ rdx_v30 (Il2CppClass<Unity.AI.Navigation.NavMeshLink>)+130]");
							object obj4 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ r8_v3 (Il2CppClass<UnityEngine.Object>)+130]");
							nint num6 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v215 @ rdx_v30 (Il2CppClass<Unity.AI.Navigation.NavMeshLink>)+130]");
							bool flag9 = num6 < 0;
							obj3 = navMeshOwner;
							agent = (NavMeshAgent)(object)typeof(NavMeshLink);
							quaternion = (Quaternion)num;
							if (!flag9)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v207 @ r8_v3 (Il2CppClass<UnityEngine.Object>)+C8]");
								object obj5 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v526 @ rax_v52+FFFFFFF8+v525 @ rax_v51*8]");
								bool flag10 = 0 != (nint)typeof(NavMeshLink);
								obj3 = navMeshOwner;
								agent = (NavMeshAgent)(object)typeof(NavMeshLink);
								quaternion = (Quaternion)num;
								if (!flag10)
								{
									object obj6 = obj + 48;
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
									_003CPerformJumpRoutine_003Ed__27 obj7 = new _003CPerformJumpRoutine_003Ed__27(0);
									obj7._003C_003E1__state = 0;
									obj7._003C_003E4__this = this;
									obj7.link = (NavMeshLink)navMeshOwner;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v10 @ rbp_v1+30]");
									obj7.spline = (Spline)0;
									Coroutine coroutine = (_jumpRoutine = StartCoroutine(obj7));
									obj3 = navMeshOwner;
									num2 = unchecked((nint)null);
									agent = (NavMeshAgent)(object)coroutine;
									goto IL_034e;
								}
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
							return;
						}
					}
					goto IL_0941;
				}
			}
			goto IL_034e;
		}
		goto IL_0941;
		IL_034e:
		bool flag11 = (object)_Agent == null;
		quaternion = (Quaternion)num;
		if (!flag11)
		{
			Vector3 forward = default(Vector3);
			if (_onNavMeshLink)
			{
				OffMeshLinkData currentOffMeshLinkData = _Agent.currentOffMeshLinkData;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v699 @ rax_v8 (UnityEngine.AI.OffMeshLinkData)+20]");
				_ = 0;
				FaceTarget((Vector3)(&forward));
				return;
			}
			if (!_Agent.isActiveAndEnabled)
			{
				return;
			}
			bool flag12 = (object)_Agent == null;
			agent = null;
			quaternion = (Quaternion)num;
			if (!flag12)
			{
				if (!_Agent.isOnNavMesh)
				{
					return;
				}
				bool flag13 = (object)_Agent == null;
				agent = null;
				quaternion = (Quaternion)num;
				if (!flag13)
				{
					if (_Agent.isStopped)
					{
						return;
					}
					object obj8 = (object)_Agent ^ (object)_Agent;
					object obj9 = (object)_Agent & obj8;
					bool flag14 = (nint)obj9 < 0;
					bool flag15 = (nint)_Agent < 0;
					bool flag16 = (object)_Agent == null;
					agent = null;
					quaternion = (Quaternion)num;
					if (!flag16)
					{
						float speed = _Agent.speed;
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtss2sd xmm1,xmm0\"");
						Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"comisd xmm1,qword ptr [182207158h]\"");
						bool flag17 = flag15 == flag14;
						object obj10 = !flag17;
						object obj11 = obj10 | flag16;
						if (obj11 != null)
						{
							return;
						}
						agent = _Agent;
						bool flag18 = (object)_Agent == null;
						float num7 = 0f;
						quaternion = (Quaternion)speed;
						if (!flag18)
						{
							Vector3 velocity2 = _Agent.velocity;
							nint num8 = (nint)typeof(Vector3);
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v442 @ rax_v17 (Il2CppClass<UnityEngine.Vector3>)+B8]");
							nint num9 = 0;
							float num10 = velocity2.x - (float)Vector3.zeroVector;
							object obj13 = default(object);
							Quaternion quaternion2 = default(Quaternion);
							object obj12 = obj13 - (object)quaternion2;
							float num11 = velocity2.z;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v750 @ rcx_v15 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
							float num12 = num11 - 0f;
							object obj14 = obj12 * obj12;
							float num13 = num10 * num10;
							num7 = num12 * num12;
							float num14 = (float)obj14 + num13;
							float num15 = num14 + num7;
							bool flag19 = 9.9999994E-11f < num15;
							float num16 = 9.9999994E-11f - num15;
							bool flag20 = num16 == 0f;
							bool flag21 = !flag19;
							bool flag22 = !flag20;
							object obj15 = flag22 & flag21;
							if (obj15 != null)
							{
								return;
							}
							bool flag23 = (object)_Agent == null;
							nint num2 = unchecked((nint)null);
							quaternion = (Quaternion)9.9999994E-11f;
							if (!flag23)
							{
								float remainingDistance = _Agent.remainingDistance;
								bool flag24 = (object)_Agent == null;
								num2 = unchecked((nint)null);
								agent = null;
								quaternion = (Quaternion)remainingDistance;
								if (!flag24)
								{
									float stoppingDistance = _Agent.stoppingDistance;
									quaternion = (Quaternion)(stoppingDistance * 1.1f);
									if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)remainingDistance) <= System.Runtime.CompilerServices.Unsafe.As<Quaternion, UIntPtr>(ref quaternion))
									{
										return;
									}
									bool flag25 = (object)_Agent == null;
									num2 = unchecked((nint)null);
									agent = null;
									if (!flag25)
									{
										Transform transform = _Agent.transform;
										bool flag26 = (object)transform == null;
										num2 = unchecked((nint)null);
										agent = null;
										if (!flag26)
										{
											Vector3 eulerAngles = transform.eulerAngles;
											agent = _Agent;
											bool flag27 = (object)_Agent == null;
											num2 = unchecked((nint)null);
											if (!flag27)
											{
												Vector3 velocity3 = _Agent.velocity;
												Vector3 upwards = default(Vector3);
												Quaternion quaternion3 = Quaternion.Internal_LookRotation(ref forward, ref upwards);
												Quaternion rotation = default(Quaternion);
												Vector3 vector = Quaternion.Internal_ToEulerRad(ref rotation);
												num13 = (float)quaternion2 * 57.29578f;
												num7 = Quaternion.Internal_MakePositive((Vector3)(&forward)).x;
												float num17 = eulerAngles.y - (float)quaternion2;
												Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
												object obj16 = num17 & 0;
												bool flag28 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj16) <= System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)90f);
												float num18 = 0.08f;
												if (!flag28)
												{
													num18 = 0.16f;
												}
												bool flag29 = (object)_Agent == null;
												object obj17 = 0;
												num15 = (float)quaternion2;
												num2 = unchecked((nint)null);
												agent = (NavMeshAgent)(&forward);
												quaternion = quaternion2;
												if (!flag29)
												{
													Transform transform2 = _Agent.transform;
													bool flag30 = (object)transform2 == null;
													obj17 = 0;
													num15 = (float)quaternion2;
													num2 = unchecked((nint)null);
													agent = null;
													quaternion = quaternion2;
													if (!flag30)
													{
														Vector3 eulerAngles2 = transform2.eulerAngles;
														agent = _Agent;
														bool flag31 = (object)_Agent == null;
														obj17 = 0;
														num15 = (float)quaternion2;
														num2 = unchecked((nint)null);
														quaternion = quaternion2;
														if (!flag31)
														{
															Vector3 velocity4 = _Agent.velocity;
															Vector3 upwards2 = default(Vector3);
															Quaternion quaternion4 = Quaternion.Internal_LookRotation(ref forward, ref upwards2);
															Vector3 vector2 = Quaternion.Internal_ToEulerRad(ref rotation);
															num13 = (float)quaternion2 * 57.29578f;
															num7 = vector2.z * 57.29578f;
															Vector3 vector3 = Quaternion.Internal_MakePositive((Vector3)(&forward));
															float num19 = (float)quaternion2 - eulerAngles2.y;
															float x = num19 / 360f;
															float num20 = MathF.Floor(x);
															quaternion = (Quaternion)(num20 * 360f);
															float num21 = num19 - (float)quaternion;
															if (!(0f > num21))
															{
																if (num21 > 360f)
																{
																	num21 = 360f;
																}
															}
															else
															{
																num21 = 0f;
															}
															if (num21 > 180f || 0f > num18 || num18 > 1f)
															{
															}
															bool flag32 = (object)_Agent == null;
															obj17 = 0;
															num15 = (float)quaternion2;
															num2 = unchecked((nint)null);
															agent = (NavMeshAgent)(&forward);
															if (!flag32)
															{
																Transform transform3 = _Agent.transform;
																bool flag33 = (object)transform3 == null;
																obj17 = 0;
																num15 = (float)quaternion2;
																num2 = unchecked((nint)null);
																agent = null;
																if (!flag33)
																{
																	float num22 = default(float);
																	transform3.eulerAngles = (Vector3)(&num22);
																	return;
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		goto IL_0941;
	}

	private void StartNavMeshLinkMovement()
	{
		//IL_0022: Expected I, but got O
		//IL_0047: Expected I, but got O
		//IL_005d: Expected I, but got O
		//IL_006d: Expected O, but got I
		//IL_00a1: Expected I, but got O
		//IL_00bf: Expected O, but got I
		//IL_00f4: Expected I, but got O
		_onNavMeshLink = true;
		bool flag = (object)_Agent == null;
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		UnityEngine.Object obj = obj2;
		IntPtr intPtr = default(IntPtr);
		nint num = intPtr;
		if (!flag)
		{
			UnityEngine.Object navMeshOwner = _Agent.navMeshOwner;
			nint num2 = (nint)typeof(NavMeshLink);
			bool flag2 = (object)navMeshOwner == null;
			obj = navMeshOwner;
			num = (nint)typeof(NavMeshLink);
			if (!flag2)
			{
				nint num3 = (nint)navMeshOwner;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rdx_v4 (Il2CppClass<Unity.AI.Navigation.NavMeshLink>)+130]");
				object obj3 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ r8_v1 (Il2CppClass<UnityEngine.Object>)+130]");
				nint num4 = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v47 @ rdx_v4 (Il2CppClass<Unity.AI.Navigation.NavMeshLink>)+130]");
				bool flag3 = num4 < 0;
				obj = navMeshOwner;
				num = (nint)typeof(NavMeshLink);
				if (!flag3)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ r8_v1 (Il2CppClass<UnityEngine.Object>)+C8]");
					object obj4 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ rax_v8+FFFFFFF8+v71 @ rax_v7*8]");
					bool flag4 = 0 != (nint)typeof(NavMeshLink);
					obj = navMeshOwner;
					num = (nint)typeof(NavMeshLink);
					if (!flag4)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
						_003CPerformJumpRoutine_003Ed__27 obj5 = new _003CPerformJumpRoutine_003Ed__27(0);
						obj5._003C_003E1__state = 0;
						obj5._003C_003E4__this = this;
						obj5.link = (NavMeshLink)navMeshOwner;
						Spline spline = default(Spline);
						obj5.spline = spline;
						Coroutine jumpRoutine = StartCoroutine(obj5);
						_jumpRoutine = jumpRoutine;
						return;
					}
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66F0");
				return;
			}
		}
		throw new NullReferenceException();
	}

	public void StopNavMeshLinkMovement()
	{
		bool flag = _jumpRoutine == null;
		_onNavMeshLink = false;
		if (!flag)
		{
			StopCoroutine(_jumpRoutine);
		}
		if (_linkRoutine != null)
		{
			StopCoroutine(_linkRoutine);
		}
	}

	private void PerformJump(NavMeshLink link, Spline spline)
	{
		_003CPerformJumpRoutine_003Ed__27 obj = new _003CPerformJumpRoutine_003Ed__27(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.link = link;
		obj.spline = spline;
		Coroutine jumpRoutine = StartCoroutine(obj);
		_jumpRoutine = jumpRoutine;
	}

	private IEnumerator PerformJumpRoutine(NavMeshLink link, Spline spline)
	{
		_003CPerformJumpRoutine_003Ed__27 obj = new _003CPerformJumpRoutine_003Ed__27(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.link = link;
		obj.spline = spline;
		return obj;
	}

	private unsafe bool CheckIfJumpingFromEndToStart(NavMeshLink link)
	{
		//IL_037d: Expected I4, but got O
		//IL_00a2: Expected O, but got Ref
		//IL_010c: Expected O, but got Ref
		//IL_038b: Expected I, but got O
		//IL_03dd: Expected I, but got O
		//IL_0322: Invalid comparison between I4 and F8
		//IL_0341: Invalid comparison between F8 and I4
		//IL_02f8: Expected F8, but got I4
		if ((object)link != null)
		{
			GameObject gameObject = link.gameObject;
			if ((object)gameObject != null)
			{
				Transform transform = gameObject.transform;
				if ((object)transform != null)
				{
					Vector3 vector2 = default(Vector3);
					Vector3 vector = transform.TransformPoint((Vector3)(&vector2));
					GameObject gameObject2 = link.gameObject;
					if ((object)gameObject2 != null)
					{
						Transform transform2 = gameObject2.transform;
						if ((object)transform2 != null)
						{
							Vector3 vector3 = transform2.TransformPoint((Vector3)(&vector2));
							if ((object)_Agent != null)
							{
								Transform transform3 = _Agent.transform;
								if ((object)transform3 != null)
								{
									Vector3 position = transform3.position;
									nint num = (nint)typeof(Math);
									float num2 = position.x - vector.x;
									object obj2 = default(object);
									object obj3 = default(object);
									object obj = obj2 - obj3;
									float num3 = position.z - vector.z;
									object obj4 = obj * obj;
									float num4 = num2 * num2;
									float num5 = num3 * num3;
									float num6 = (float)obj4 + num4;
									float num7 = num6 + num5;
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v280 @ rcx_v13 (Il2CppClass<System.Math>)+E4]");
									if ((nint)0 <= (nint)0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
									}
									else
									{
										double num8 = Math.Sqrt(num7);
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm9,xmm0\"");
									if ((object)_Agent != null)
									{
										Transform transform4 = _Agent.transform;
										if ((object)transform4 != null)
										{
											Vector3 position2 = transform4.position;
											nint num9 = (nint)typeof(Math);
											float num10 = position2.x - vector3.x;
											object obj6 = default(object);
											object obj5 = obj3 - obj6;
											float num11 = position2.z - vector3.z;
											object obj7 = obj5 * obj5;
											float num12 = num10 * num10;
											float num13 = num11 * num11;
											float num14 = (float)obj7 + num12;
											float num15 = num14 + num13;
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"ucomisd xmm0,xmm1\"");
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v224 @ rcx_v17 (Il2CppClass<System.Math>)+E4]");
											double num16;
											if ((nint)0 <= (nint)0)
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"sqrtpd xmm0,xmm1\"");
												num16 = 0.0;
											}
											else
											{
												num16 = Math.Sqrt(num15);
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"cvtsd2ss xmm0,xmm0\"");
											bool flag = 0.0 < num16;
											double num17 = 0.0 - num16;
											bool flag2 = num17 == 0.0;
											bool flag3 = !flag;
											bool flag4 = !flag2;
											return flag4 & flag3;
										}
									}
								}
							}
						}
					}
				}
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private IEnumerator MoveOnOffMeshLink(Spline spline, bool reverseDirection)
	{
		_003CMoveOnOffMeshLink_003Ed__29 obj = new _003CMoveOnOffMeshLink_003Ed__29(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.spline = spline;
		obj.reverseDirection = reverseDirection;
		return obj;
	}

	private unsafe void FaceTarget(Vector3 target)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_01b4: Expected I, but got O
		//IL_01bd: Invalid comparison between O and F4
		//IL_00c0: Expected F4, but got I
		//IL_01f8: Expected I, but got O
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Expected Ref, but got Unknown
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Expected Ref, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Expected Ref, but got Unknown
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Expected Ref, but got Unknown
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Expected O, but got Unknown
		object obj2 = default(object);
		object obj = obj2 - 95;
		Transform transform = _Agent.transform;
		Vector3 position = transform.position;
		float num = target.z - position.z;
		_ = target.x;
		_ = position.x;
		object obj3 = obj - 9;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803717E0");
		nint num2 = (nint)typeof(Vector3);
		object obj4 = default(object);
		if (System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj4) > System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)1E-05f))
		{
			float num3 = num / (float)obj4;
			float num4 = num3;
		}
		else
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v261 @ rcx_v6 (Il2CppClass<UnityEngine.Vector3>)+B8]");
			nint num5 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v276 @ rax_v20 (Il2CppStaticFields<UnityEngine.Vector3>)+8]");
			float num4 = 0f;
			_ = Vector3.zeroVector;
		}
		_ = 0;
		_ = 0;
		nint num6 = (nint)typeof(Vector3);
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v304 @ rcx_v7 (Il2CppClass<UnityEngine.Vector3>)+B8]");
		nint num7 = 0;
		ref Vector3 upwards = ref *(Vector3*)(obj - 9);
		ref Vector3 forward = ref *(Vector3*)(obj + 7);
		_ = Vector3.upVector;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v307 @ rax_v11 (Il2CppStaticFields<UnityEngine.Vector3>)+20]");
		_ = 0;
		Quaternion quaternion = Quaternion.Internal_LookRotation(ref forward, ref upwards);
		Transform transform2 = _Agent.transform;
		Transform transform3 = _Agent.transform;
		Quaternion rotation = transform3.rotation;
		_ = quaternion.x;
		_ = rotation.x;
		float deltaTime = Time.deltaTime;
		float t = deltaTime * 5f;
		Quaternion quaternion2 = Quaternion.Internal_Slerp(ref *(Quaternion*)(obj - 9), ref *(Quaternion*)(obj + 7), t);
		Quaternion rotation2 = (Quaternion)(obj + 7);
		_ = quaternion2.x;
		transform2.rotation = rotation2;
	}
}
