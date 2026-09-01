using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class CatController : MonoBehaviour
{
	private sealed class _003CDelayedNavmeshPositionSet_003Ed__39 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CatController _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CDelayedNavmeshPositionSet_003Ed__39(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_026e: Expected I4, but got I8
			//IL_0015: Expected O, but got I4
			//IL_0235: Expected I4, but got I8
			//IL_005b: Expected I4, but got I8
			//IL_02a6: Expected I4, but got O
			//IL_0091: Expected O, but got I
			//IL_00e4: Expected O, but got I
			//IL_0148: Expected O, but got I
			//IL_0192: Expected I4, but got I8
			//IL_0192: Expected O, but got Ref
			//IL_02e1: Expected O, but got Ref
			//IL_0213: Expected O, but got Ref
			Component component = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			bool result;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					bool flag2 = (nint)obj != 1;
					result = false;
					if (!flag2)
					{
						_003C_003E1__state = -1;
						if ((object)_003C_003E4__this != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Component)+B0]");
							if (!((UnityEngine.Object)0 != null))
							{
								goto IL_0218;
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Component)+B0]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Component)+B0]");
								Transform transform = ((GameObject)0).transform;
								if ((object)transform != null)
								{
									Vector3 position = transform.position;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Component)+B0]");
									if ((nint)0 != 0)
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v33 @ rbx_v1 (UnityEngine.Component)+B0]");
										Transform transform2 = ((GameObject)0).transform;
										if ((object)transform2 != null)
										{
											Vector3 forward = transform2.forward;
											NavMeshHit navMeshHit = default(NavMeshHit);
											Transform transform3;
											if (!NavMesh.SamplePosition((Vector3)(&navMeshHit), out var _, 6f, -1))
											{
												transform3 = _003C_003E4__this.transform;
												if ((object)transform3 == null)
												{
													goto IL_0298;
												}
											}
											else
											{
												transform3 = _003C_003E4__this.transform;
												if ((object)transform3 == null)
												{
													goto IL_0298;
												}
											}
											transform3.position = (Vector3)(&navMeshHit);
											Transform transform4 = _003C_003E4__this.transform;
											if ((object)transform4 != null)
											{
												float num = default(float);
												transform4.rotation = (Quaternion)(&num);
												goto IL_0218;
											}
										}
									}
								}
							}
						}
						goto IL_0298;
					}
					goto IL_02cf;
				}
				_003C_003E1__state = -1;
				WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
				_003C_003E2__current = waitForEndOfFrame;
				_003C_003E1__state = 2;
				return true;
			}
			_003C_003E1__state = -1;
			WaitForEndOfFrame waitForEndOfFrame2 = new WaitForEndOfFrame();
			_003C_003E2__current = waitForEndOfFrame2;
			_003C_003E1__state = 1;
			return true;
			IL_0298:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_0218:
			result = false;
			goto IL_02cf;
			IL_02cf:
			return result;
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

	private sealed class _003CResumeActivities_003Ed__38 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CatController _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CResumeActivities_003Ed__38(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0097: Expected I4, but got I8
			//IL_025b: Expected I4, but got O
			CatController catController = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					WaitForSeconds waitForSeconds = new WaitForSeconds(catController._pauseTimeAfterDrop);
					_003C_003E2__current = waitForSeconds;
					_003C_003E1__state = 1;
					return true;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					goto IL_0287;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					AgentMover movement = catController._movement;
					if ((object)catController._movement != null && (object)movement._Agent != null)
					{
						if (movement._Agent.isActiveAndEnabled)
						{
							if ((object)movement._Agent == null)
							{
								goto IL_024d;
							}
							if (movement._Agent.isOnNavMesh)
							{
								if ((object)movement._Agent == null)
								{
									goto IL_024d;
								}
								movement._Agent.isStopped = false;
							}
						}
						AgentMover movement2 = catController._movement;
						if ((object)catController._movement != null && (object)movement2._Agent != null)
						{
							movement2._Agent.enabled = true;
							catController._currentState = CatState.Idle;
							catController._resumeRoutine = null;
							catController.RecoveryState = false;
							goto IL_0287;
						}
					}
				}
			}
			goto IL_024d;
			IL_0287:
			return false;
			IL_024d:
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

	private AgentMover _movement;

	private AgentAnimation _agentAnimation;

	private Rigidbody _rigidbody;

	private CatMovementManager _manager;

	private string _pauseAnimationTrigger;

	private float _dropDistance;

	private float _pauseTimeAfterDrop;

	private float activityTimeInPlace;

	private string loopEndTrigger;

	public UnityEvent onPickedUp;

	public UnityEvent onReleased;

	public UnityEvent onShooed;

	public UnityEvent onPetted;

	private CatState _currentState;

	private CatState _previousState;

	private float _activityTimer;

	private float _activityLocationTimer;

	private float _currentActivityDuration;

	private float _afterLoopActivityDuration;

	private bool _isLoopingActivity;

	private Coroutine _resumeRoutine;

	private bool selectClosestPoint;

	private bool selectFurtherPoint;

	public bool RecoveryState;

	private GameObject _cachedPlayer;

	public CatState CurrentState => _currentState;

	private void Start()
	{
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Expected O, but got Unknown
		CatMovementManager manager = UnityEngine.Object.FindObjectOfType<CatMovementManager>();
		_manager = manager;
		AgentMover movement = _movement;
		Action<float> b = _agentAnimation.SetSpeed;
		if ((object)_movement != null)
		{
			Delegate obj = movement.OnSpeedChanged;
			AgentAnimation agentAnimation = (AgentAnimation)(_movement + 40);
			Delegate obj2;
			float speed = default(float);
			Delegate obj3 = default(Delegate);
			bool flag2;
			Delegate obj4 = default(Delegate);
			do
			{
				obj2 = Delegate.Combine(obj, b);
				if ((object)obj2 != null)
				{
					((AgentAnimation)(object)obj2).SetSpeed(speed);
					bool flag = (object)obj3 == null;
					movement = (AgentMover)(object)obj2;
					if (flag)
					{
						((AgentAnimation)(object)movement).SetSpeed(speed);
						return;
					}
				}
				agentAnimation.SetSpeed(speed);
				flag2 = (object)obj4 != obj;
				obj = obj4;
			}
			while (flag2);
			AgentMover movement2 = _movement;
			bool flag3 = (object)_movement == null;
			movement = (AgentMover)(object)obj2;
			if (!flag3)
			{
				UnityAction<string> call = _agentAnimation.Jump;
				bool flag4 = movement2.OnStartJump == null;
				movement = (AgentMover)(object)movement2.OnStartJump;
				if (!flag4)
				{
					movement2.OnStartJump.AddListener(call);
					AgentAnimation agentAnimation2 = _agentAnimation;
					bool flag5 = (object)_agentAnimation == null;
					movement = (AgentMover)(object)movement2.OnStartJump;
					if (!flag5)
					{
						bool flag6 = (object)agentAnimation2._animator == null;
						movement = (AgentMover)(object)movement2.OnStartJump;
						if (!flag6)
						{
							agentAnimation2._animator.SetFloat(agentAnimation2._movementSpeed, 0f);
							return;
						}
					}
				}
			}
		}
		throw new NullReferenceException();
	}

	private void Update()
	{
		//IL_006d: Expected O, but got I4
		//IL_037a: Invalid comparison between F4 and I4
		//IL_0347: Invalid comparison between F4 and I4
		if (_currentState == CatState.Paused || _currentState == CatState.Carried)
		{
			return;
		}
		bool flag = _currentState == CatState.Idle;
		if (!flag)
		{
			object obj = _currentState - 1;
			if (!flag)
			{
				if ((nint)obj != 1)
				{
					return;
				}
				float deltaTime = Time.deltaTime;
				float activityTimer = deltaTime + _activityTimer;
				_activityTimer = activityTimer;
				float deltaTime2 = Time.deltaTime;
				float activityLocationTimer = deltaTime2 + _activityLocationTimer;
				_activityLocationTimer = activityLocationTimer;
				if (_activityTimer < _currentActivityDuration)
				{
					return;
				}
				AgentAnimation agentAnimation = _agentAnimation;
				if (_isLoopingActivity)
				{
					if (!string.IsNullOrEmpty(loopEndTrigger))
					{
						agentAnimation._animator.SetTrigger(loopEndTrigger);
					}
					float currentActivityDuration = _afterLoopActivityDuration + _currentActivityDuration;
					_isLoopingActivity = false;
					_afterLoopActivityDuration = 0f;
					_currentActivityDuration = currentActivityDuration;
					return;
				}
				if (!string.IsNullOrEmpty(loopEndTrigger))
				{
					agentAnimation._animator.ResetTrigger(loopEndTrigger);
				}
				if (activityTimeInPlace > _activityLocationTimer)
				{
					PickRandomActivity();
					return;
				}
			}
			else
			{
				AgentMover movement = _movement;
				if (movement._onNavMeshLink || movement._Agent.pathPending)
				{
					return;
				}
				float remainingDistance = movement._Agent.remainingDistance;
				float stoppingDistance = movement._Agent.stoppingDistance;
				if (stoppingDistance < remainingDistance)
				{
					return;
				}
				if (movement._Agent.hasPath)
				{
					Vector3 velocity = movement._Agent.velocity;
					object obj3 = default(object);
					object obj2 = obj3 * obj3;
					float num = velocity.x * velocity.x;
					float num2 = velocity.z * velocity.z;
					float num3 = (float)obj2 + num;
					float num4 = num3 + num2;
					Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp near ptr 00000001804FEFF4h\"");
					if (num4 != 0f)
					{
						return;
					}
				}
				TurretController instance = TurretController.Instance;
				if (!(instance.observedRotationSpeed > 0f))
				{
					_activityLocationTimer = 0f;
					PickRandomActivity();
					_currentState = CatState.PerformingActivity;
					return;
				}
			}
			_currentState = CatState.Idle;
		}
		else
		{
			HandleIdleState();
		}
	}

	private unsafe void HandleIdleState()
	{
		//IL_0ba3: Expected O, but got Ref
		//IL_0c0a: Expected O, but got Ref
		//IL_0210: Expected O, but got Ref
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Expected F4, but got Unknown
		//IL_0307: Expected O, but got Ref
		//IL_04f3: Expected O, but got Ref
		//IL_0409: Expected O, but got Ref
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Expected F4, but got Unknown
		//IL_057e: Expected O, but got I4
		//IL_05a0: Expected O, but got I4
		//IL_05b8: Expected O, but got I4
		//IL_0d14: Expected O, but got Ref
		//IL_05f1: Expected O, but got Ref
		//IL_0625: Expected O, but got Ref
		//IL_08d4: Expected O, but got Ref
		//IL_08fe: Expected O, but got Ref
		//IL_0a6e: Expected O, but got Ref
		//IL_0937: Expected O, but got Ref
		//IL_0979: Expected O, but got Ref
		//IL_0ad5: Expected O, but got Ref
		//IL_0714: Expected O, but got Ref
		//IL_07cf: Invalid comparison between F4 and O
		if (!(_manager != null))
		{
			return;
		}
		CatMovementManager manager = _manager;
		bool flag = (object)_manager == null;
		UnityEngine.Object manager2 = _manager;
		Transform transform;
		if (!flag)
		{
			manager2 = (UnityEngine.Object)(object)manager._floors;
			if (manager._floors != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v774 @ rcx_v7 (UnityEngine.Object)+18]");
				if ((nint)0 == 0)
				{
					return;
				}
				if (manager._playerTransform != null)
				{
					CatMovementManager manager3 = _manager;
					bool flag2 = (object)_manager == null;
					manager2 = manager._playerTransform;
					if (!flag2)
					{
						transform = manager3._playerTransform;
						bool flag3 = (object)manager3._playerTransform == null;
						manager2 = manager._playerTransform;
						if (!flag3)
						{
							goto IL_0b73;
						}
					}
				}
				else
				{
					Transform transform2 = base.transform;
					bool flag4 = (object)transform2 == null;
					manager2 = this;
					if (!flag4)
					{
						transform = transform2;
						goto IL_0b73;
					}
				}
			}
		}
		goto IL_0aea;
		IL_0b73:
		Vector3 position = transform.position;
		CatMovementManager manager4 = _manager;
		bool flag5 = (object)_manager == null;
		object obj = default(object);
		manager2 = (UnityEngine.Object)(&obj);
		if (!flag5)
		{
			manager2 = (UnityEngine.Object)(object)manager4._floors;
			if (manager4._floors != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				float num = 3.4028235E+38f;
				CatFloor catFloor = null;
				List<CatFloor>.Enumerator enumerator = default(List<CatFloor>.Enumerator);
				CatFloor catFloor2 = default(CatFloor);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag6 = catFloor2 == null;
					manager2 = (UnityEngine.Object)(&enumerator);
					if (!flag6)
					{
						float averageY = catFloor2.GetAverageY();
						float num2 = averageY - position.y;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
						float num3 = num2 & 0;
						if (num > num3)
						{
							num = num3;
							catFloor = catFloor2;
						}
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				CatMovementManager manager5 = _manager;
				bool flag7 = (object)_manager == null;
				manager2 = (UnityEngine.Object)(&enumerator);
				if (!flag7)
				{
					manager2 = (UnityEngine.Object)(object)manager5._floors;
					if (manager5._floors != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
						CatFloor catFloor3 = null;
						float num4 = 3.4028235E+38f;
						List<CatFloor>.Enumerator enumerator2 = default(List<CatFloor>.Enumerator);
						while (enumerator2.MoveNext())
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
							bool flag8 = catFloor2 == null;
							GameObject gameObject = (GameObject)(&enumerator2);
							if (!flag8)
							{
								float averageY2 = catFloor2.GetAverageY();
								GameObject gameObject2 = base.gameObject;
								if ((object)gameObject2 != null)
								{
									Transform transform3 = gameObject2.transform;
									if ((object)transform3 != null)
									{
										float num5 = averageY2 - transform3.position.y;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C70]");
										float num6 = num5 & 0;
										if (num4 > num6)
										{
											catFloor3 = catFloor2;
											num4 = num6;
										}
										continue;
									}
									throw new NullReferenceException();
								}
								throw new NullReferenceException();
							}
							throw new NullReferenceException();
						}
						enumerator2.Dispose();
						if (catFloor3 != null)
						{
							bool flag9 = catFloor == null;
							manager2 = (UnityEngine.Object)(&enumerator2);
							if (flag9)
							{
								goto IL_0aea;
							}
							bool flag10 = catFloor3.floorName != catFloor.floorName;
							bool flag11 = !flag10;
							manager2 = (UnityEngine.Object)(object)catFloor3.floorName;
							if (!flag11)
							{
								CatMovementManager manager6 = _manager;
								bool flag12 = (object)_manager == null;
								manager2 = (UnityEngine.Object)(object)catFloor3.floorName;
								if (flag12)
								{
									goto IL_0aea;
								}
								bool flag13 = manager6.EnabledCatFollow;
								manager2 = (UnityEngine.Object)(object)catFloor3.floorName;
								if (!flag13)
								{
									catFloor = catFloor3;
									manager2 = (UnityEngine.Object)(object)catFloor3.floorName;
								}
							}
						}
						else
						{
							bool flag14 = catFloor == null;
							manager2 = (UnityEngine.Object)(&enumerator2);
							if (flag14)
							{
								return;
							}
						}
						List<Transform> spots = catFloor.spots;
						if (catFloor.spots != null)
						{
							if (spots._size <= 0)
							{
								return;
							}
							List<Transform> list = new List<Transform>();
							bool flag15 = !selectFurtherPoint;
							Transform transform4 = (Transform)1;
							if (!flag15)
							{
								selectFurtherPoint = false;
								transform4 = (Transform)4;
							}
							if (catFloor.spots != null)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								object obj2 = 0;
								object obj4 = default(object);
								object obj3 = obj4;
								List<Transform>.Enumerator enumerator3 = default(List<Transform>.Enumerator);
								Transform transform5 = default(Transform);
								object obj5 = default(object);
								float num7 = default(float);
								while (enumerator3.MoveNext())
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
									AgentMover movement = _movement;
									bool flag16 = (object)transform5 == null;
									GameObject gameObject = (GameObject)(&enumerator3);
									if (!flag16)
									{
										Vector3 position2 = transform5.position;
										bool flag17 = (object)_movement == null;
										gameObject = (GameObject)(&obj5);
										if (flag17)
										{
											throw new NullReferenceException();
										}
										if ((object)movement._Agent != null)
										{
											if (!movement._Agent.isActiveAndEnabled)
											{
												continue;
											}
											if ((object)movement._Agent != null)
											{
												if (!movement._Agent.isOnNavMesh)
												{
													continue;
												}
												if (movement._reusablePath == null)
												{
													NavMeshPath reusablePath = new NavMeshPath();
													movement._reusablePath = reusablePath;
												}
												if ((object)movement._Agent != null)
												{
													bool flag18 = movement._Agent.CalculatePath((Vector3)(&num7), movement._reusablePath);
													if (movement._reusablePath != null)
													{
														if (movement._reusablePath.status != NavMeshPathStatus.PathComplete)
														{
															continue;
														}
														Vector3 position3 = transform5.position;
														Transform transform6 = base.transform;
														if ((object)transform6 != null)
														{
															Vector3 position4 = transform6.position;
															Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803716B0");
															float x = position4.x;
															bool flag19 = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)x) <= System.Runtime.CompilerServices.Unsafe.As<Transform, UIntPtr>(ref transform4);
															obj3 = transform4;
															if (flag19)
															{
																continue;
															}
															GameObject gameObject3 = transform5.gameObject;
															if ((object)gameObject3 != null)
															{
																bool activeInHierarchy = gameObject3.activeInHierarchy;
																bool flag20 = !activeInHierarchy;
																obj3 = transform4;
																if (!flag20)
																{
																	if (list == null)
																	{
																		throw new NullReferenceException();
																	}
																	list.Add(transform5);
																	obj3 = transform4;
																}
																continue;
															}
															throw new NullReferenceException();
														}
														throw new NullReferenceException();
													}
													throw new NullReferenceException();
												}
												throw new NullReferenceException();
											}
											throw new NullReferenceException();
										}
										throw new NullReferenceException();
									}
									throw new NullReferenceException();
								}
								enumerator3.Dispose();
								bool flag21 = list == null;
								manager2 = (UnityEngine.Object)(&enumerator3);
								if (!flag21)
								{
									if (list._size <= 0)
									{
										AgentMover movement2 = _movement;
										bool flag22 = catFloor == null;
										manager2 = (UnityEngine.Object)(&enumerator3);
										if (!flag22)
										{
											bool flag23 = (object)catFloor.defaultSpot == null;
											manager2 = (UnityEngine.Object)(&enumerator3);
											if (!flag23)
											{
												Vector3 position5 = catFloor.defaultSpot.position;
												bool flag24 = (object)_movement == null;
												manager2 = (UnityEngine.Object)(&obj2);
												if (!flag24 && (object)movement2._Agent != null)
												{
													bool flag25 = movement2._Agent.Warp((Vector3)(&obj));
													return;
												}
											}
										}
									}
									else
									{
										int num8 = UnityEngine.Random.Range(0, list._size);
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
										bool flag26 = !selectClosestPoint;
										Transform transform8 = default(Transform);
										Transform transform7 = transform8;
										if (!flag26)
										{
											Func<Transform, float> keySelector = delegate(Transform d)
											{
												if ((object)d != null)
												{
													Vector3 position7 = d.position;
													Transform transform9 = base.transform;
													if ((object)transform9 != null)
													{
														Vector3 position8 = transform9.position;
														float num9 = position7.z - position8.z;
														float num10 = position7.x - position8.x;
														object obj7 = default(object);
														object obj8 = default(object);
														object obj6 = obj7 - obj8;
														float num11 = num9 * num9;
														object obj9 = obj6 * obj6;
														float num12 = num10 * num10;
														float num13 = (float)obj9 + num12;
														return num13 + num11;
													}
												}
												throw new NullReferenceException();
											};
											IOrderedEnumerable<Transform> source = Enumerable.OrderBy(list, keySelector);
											List<Transform> list2 = Enumerable.ToList(source);
											if (list2 == null)
											{
												goto IL_0aea;
											}
											Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
											selectClosestPoint = false;
											transform7 = transform4;
										}
										AgentMover movement3 = _movement;
										if ((object)transform7 != null)
										{
											Vector3 position6 = transform7.position;
											bool flag27 = (object)_movement == null;
											manager2 = (UnityEngine.Object)(&obj2);
											if (!flag27)
											{
												if (!movement3._onNavMeshLink)
												{
													if ((object)movement3._Agent == null)
													{
														goto IL_0aea;
													}
													movement3._Agent.destination = (Vector3)(&obj);
												}
												_currentState = CatState.WalkingToSpot;
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
		goto IL_0aea;
		IL_0aea:
		throw new NullReferenceException();
	}

	private void HandleWalkingState()
	{
		//IL_016f: Invalid comparison between F4 and I4
		//IL_013c: Invalid comparison between F4 and I4
		AgentMover movement = _movement;
		if (movement._onNavMeshLink || movement._Agent.pathPending)
		{
			return;
		}
		float remainingDistance = movement._Agent.remainingDistance;
		float stoppingDistance = movement._Agent.stoppingDistance;
		if (stoppingDistance < remainingDistance)
		{
			return;
		}
		if (movement._Agent.hasPath)
		{
			Vector3 velocity = movement._Agent.velocity;
			object obj2 = default(object);
			object obj = obj2 * obj2;
			float num = velocity.x * velocity.x;
			float num2 = velocity.z * velocity.z;
			float num3 = (float)obj + num;
			float num4 = num3 + num2;
			Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 00000001804FD986h\"");
			if (num4 != 0f)
			{
				return;
			}
		}
		TurretController instance = TurretController.Instance;
		if (!(instance.observedRotationSpeed > 0f))
		{
			_activityLocationTimer = 0f;
			PickRandomActivity();
			_currentState = CatState.PerformingActivity;
		}
		else
		{
			_currentState = CatState.Idle;
		}
	}

	private unsafe void PickRandomActivity()
	{
		//IL_00ce: Expected F4, but got I4
		//IL_00f5: Expected O, but got Ref
		//IL_013e: Expected O, but got I
		//IL_01a6: Expected O, but got I
		//IL_01de: Expected O, but got I4
		//IL_01e7: Expected F4, but got I4
		//IL_028a: Expected O, but got Ref
		//IL_020f: Expected O, but got Ref
		//IL_02bf: Expected O, but got I
		//IL_0264: Expected O, but got I4
		//IL_026c: Expected O, but got Ref
		//IL_032f: Expected F4, but got I
		//IL_032f: Expected F4, but got I
		//IL_034f: Expected F4, but got I
		//IL_030d: Expected O, but got I
		if (!(_manager != null))
		{
			return;
		}
		CatMovementManager manager = _manager;
		bool flag = (object)_manager == null;
		UnityEngine.Object manager2 = _manager;
		if (!flag)
		{
			manager2 = (UnityEngine.Object)(object)manager._activities;
			if (manager._activities != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rcx_v5 (UnityEngine.Object)+18]");
				if ((nint)0 == 0)
				{
					return;
				}
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
				float num = 0f;
				List<CatActivity>.Enumerator enumerator = default(List<CatActivity>.Enumerator);
				object obj = default(object);
				while (enumerator.MoveNext())
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
					bool flag2 = obj == null;
					manager2 = (UnityEngine.Object)(&enumerator);
					if (!flag2)
					{
						float num2 = num;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v392 @ stack_20_v7+28]");
						num = num2 + 0f;
						continue;
					}
					throw new NullReferenceException();
				}
				enumerator.Dispose();
				float num3 = UnityEngine.Random.Range(0f, num);
				manager2 = _manager;
				if ((object)_manager != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rcx_v5 (UnityEngine.Object)+30]");
					manager2 = (UnityEngine.Object)0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rcx_v5 (UnityEngine.Object)+30]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						manager2 = _manager;
						if ((object)_manager != null)
						{
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rcx_v5 (UnityEngine.Object)+30]");
							manager2 = (UnityEngine.Object)0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v359 @ rcx_v5 (UnityEngine.Object)+30]");
							if ((nint)0 != 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
								object obj2 = 0;
								float num4 = 0f;
								List<CatActivity>.Enumerator enumerator2 = default(List<CatActivity>.Enumerator);
								object obj3;
								while (true)
								{
									if (enumerator2.MoveNext())
									{
										Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
										bool flag3 = 0 == 0;
										manager2 = (UnityEngine.Object)(&enumerator2);
										if (!flag3)
										{
											float num5 = num4;
											Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v506 @ stack_-88_v6+28]");
											num4 = num5 + 0f;
											if (!(num4 < num3))
											{
												Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
												obj3 = 0;
												manager2 = (UnityEngine.Object)(&enumerator2);
												break;
											}
											continue;
										}
										throw new NullReferenceException();
									}
									enumerator2.Dispose();
									obj3 = obj;
									manager2 = (UnityEngine.Object)(&enumerator2);
									break;
								}
								AgentAnimation agentAnimation = _agentAnimation;
								if (obj3 != null && (object)_agentAnimation != null)
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v7+18]");
									if (!string.IsNullOrEmpty((string)0))
									{
										if ((object)agentAnimation._animator == null)
										{
											goto IL_0371;
										}
										Animator animator = agentAnimation._animator;
										Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v7+18]");
										animator.SetTrigger((string)0);
									}
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v7+20]");
									nint num6 = 0;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v7+24]");
									float currentActivityDuration = UnityEngine.Random.Range(num6, 0f);
									_currentActivityDuration = currentActivityDuration;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v7+30]");
									_afterLoopActivityDuration = 0f;
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rsi_v7+2C]");
									_isLoopingActivity = false;
									_activityTimer = 0f;
									return;
								}
							}
						}
					}
				}
			}
		}
		goto IL_0371;
		IL_0371:
		throw new NullReferenceException();
	}

	private void HandleActivityState()
	{
		float deltaTime = Time.deltaTime;
		float activityTimer = deltaTime + _activityTimer;
		_activityTimer = activityTimer;
		float deltaTime2 = Time.deltaTime;
		float activityLocationTimer = deltaTime2 + _activityLocationTimer;
		_activityLocationTimer = activityLocationTimer;
		if (_activityTimer < _currentActivityDuration)
		{
			return;
		}
		AgentAnimation agentAnimation = _agentAnimation;
		if (!_isLoopingActivity)
		{
			if (!string.IsNullOrEmpty(loopEndTrigger))
			{
				agentAnimation._animator.ResetTrigger(loopEndTrigger);
			}
			if (!(activityTimeInPlace > _activityLocationTimer))
			{
				_currentState = CatState.Idle;
			}
			else
			{
				PickRandomActivity();
			}
			return;
		}
		if (!string.IsNullOrEmpty(loopEndTrigger))
		{
			agentAnimation._animator.SetTrigger(loopEndTrigger);
		}
		float currentActivityDuration = _afterLoopActivityDuration + _currentActivityDuration;
		_isLoopingActivity = false;
		_afterLoopActivityDuration = 0f;
		_currentActivityDuration = currentActivityDuration;
	}

	public void PauseBehavior(bool pause)
	{
		if (!pause)
		{
			if (_currentState == CatState.Paused)
			{
				_currentState = _previousState;
				if (_previousState == CatState.WalkingToSpot)
				{
					_movement.ResumeMovement();
				}
			}
		}
		else
		{
			if (_currentState != CatState.WalkingToSpot)
			{
				return;
			}
			AgentMover movement = _movement;
			if (!movement._onNavMeshLink)
			{
				_previousState = _currentState;
				_currentState = CatState.Paused;
				_movement.PauseMovement();
				AgentAnimation agentAnimation = _agentAnimation;
				if (!string.IsNullOrEmpty(_pauseAnimationTrigger))
				{
					agentAnimation._animator.SetTrigger(_pauseAnimationTrigger);
				}
			}
		}
	}

	public void StartCustomization()
	{
		//IL_00e4: Expected O, but got I
		//IL_0123: Expected O, but got I
		AgentMover movement = _movement;
		_currentState = CatState.Paused;
		if (movement._Agent.isActiveAndEnabled && movement._Agent.isOnNavMesh)
		{
			movement._Agent.isStopped = true;
		}
		AgentMover movement2 = _movement;
		movement2._Agent.enabled = false;
		MonoBehaviour movement3 = _movement;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rdi_v3 (UnityEngine.MonoBehaviour)+50]");
		bool flag = (nint)0 == 0;
		_ = 0;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rdi_v3 (UnityEngine.MonoBehaviour)+50]");
			movement3.StopCoroutine((Coroutine)0);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rdi_v3 (UnityEngine.MonoBehaviour)+58]");
		if ((nint)0 != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ rdi_v3 (UnityEngine.MonoBehaviour)+58]");
			movement3.StopCoroutine((Coroutine)0);
		}
		UnityEngine.Object.Destroy(_rigidbody);
		AgentAnimation agentAnimation = _agentAnimation;
		if (!string.IsNullOrEmpty("Idle"))
		{
			agentAnimation._animator.SetBool("Idle", value: true);
		}
		AgentAnimation agentAnimation2 = _agentAnimation;
		if (!string.IsNullOrEmpty("InstantIdle"))
		{
			agentAnimation2._animator.SetTrigger("InstantIdle");
		}
		if (_resumeRoutine != null)
		{
			StopCoroutine(_resumeRoutine);
			_resumeRoutine = null;
		}
	}

	public void StopCustomization()
	{
		AgentAnimation agentAnimation = _agentAnimation;
		if (!string.IsNullOrEmpty("Idle"))
		{
			agentAnimation._animator.SetBool("Idle", value: false);
		}
		AgentAnimation agentAnimation2 = _agentAnimation;
		if (!string.IsNullOrEmpty("InstantIdle"))
		{
			agentAnimation2._animator.ResetTrigger("InstantIdle");
		}
		GameObject gameObject = base.gameObject;
		Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
		_rigidbody = rigidbody;
		_rigidbody.isKinematic = true;
		_rigidbody.useGravity = false;
		if (_resumeRoutine != null)
		{
			StopCoroutine(_resumeRoutine);
			_resumeRoutine = null;
		}
		_003CResumeActivities_003Ed__38 obj = new _003CResumeActivities_003Ed__38(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine resumeRoutine = StartCoroutine(obj);
		_resumeRoutine = resumeRoutine;
		RecoveryState = true;
	}

	public void StartCarrying()
	{
		//IL_0131: Expected O, but got I
		//IL_0170: Expected O, but got I
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A892]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		_currentState = CatState.Carried;
		if (onPickedUp != null)
		{
			onPickedUp.Invoke();
		}
		AgentMover movement = _movement;
		if (movement._Agent.isActiveAndEnabled && movement._Agent.isOnNavMesh)
		{
			movement._Agent.isStopped = true;
		}
		AgentMover movement2 = _movement;
		movement2._Agent.enabled = false;
		MonoBehaviour movement3 = _movement;
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rdi_v3 (UnityEngine.MonoBehaviour)+50]");
		bool flag = (nint)0 == 0;
		_ = 0;
		if (!flag)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rdi_v3 (UnityEngine.MonoBehaviour)+50]");
			movement3.StopCoroutine((Coroutine)0);
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rdi_v3 (UnityEngine.MonoBehaviour)+58]");
		if ((nint)0 != 0)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v75 @ rdi_v3 (UnityEngine.MonoBehaviour)+58]");
			movement3.StopCoroutine((Coroutine)0);
		}
		AgentAnimation agentAnimation = _agentAnimation;
		if (!string.IsNullOrEmpty("Carrying"))
		{
			agentAnimation._animator.SetBool("Carrying", value: true);
		}
		if (_resumeRoutine != null)
		{
			StopCoroutine(_resumeRoutine);
			_resumeRoutine = null;
		}
	}

	public void StopCarrying()
	{
		AgentAnimation agentAnimation = _agentAnimation;
		if (!string.IsNullOrEmpty("Carrying"))
		{
			agentAnimation._animator.SetBool("Carrying", value: false);
		}
		if (onReleased != null)
		{
			onReleased.Invoke();
		}
		Transform transform = base.transform;
		transform.parentInternal = null;
		if (_cachedPlayer == null)
		{
			FirstPersonController firstPersonController = UnityEngine.Object.FindFirstObjectByType<FirstPersonController>();
			GameObject cachedPlayer = firstPersonController.gameObject;
			_cachedPlayer = cachedPlayer;
		}
		_003CDelayedNavmeshPositionSet_003Ed__39 obj = new _003CDelayedNavmeshPositionSet_003Ed__39(0);
		obj._003C_003E4__this = this;
		obj._003C_003E1__state = 0;
		Coroutine coroutine = StartCoroutine(obj);
		if (_resumeRoutine != null)
		{
			StopCoroutine(_resumeRoutine);
			_resumeRoutine = null;
		}
		_003CResumeActivities_003Ed__38 obj2 = new _003CResumeActivities_003Ed__38(0);
		obj2._003C_003E1__state = 0;
		obj2._003C_003E4__this = this;
		Coroutine resumeRoutine = StartCoroutine(obj2);
		_resumeRoutine = resumeRoutine;
		selectFurtherPoint = false;
		selectClosestPoint = true;
	}

	private IEnumerator ResumeActivities()
	{
		_003CResumeActivities_003Ed__38 obj = new _003CResumeActivities_003Ed__38(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private IEnumerator DelayedNavmeshPositionSet()
	{
		_003CDelayedNavmeshPositionSet_003Ed__39 obj = new _003CDelayedNavmeshPositionSet_003Ed__39(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	public void ShooCat(bool initiatedByPlayer)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A896]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (_currentState != CatState.Carried)
		{
			AgentMover movement = _movement;
			if (movement._Agent.isActiveAndEnabled && movement._Agent.isOnNavMesh)
			{
				movement._Agent.isStopped = true;
			}
			AgentMover movement2 = _movement;
			movement2._Agent.enabled = false;
			AgentAnimation agentAnimation = _agentAnimation;
			_currentState = CatState.Paused;
			if (!string.IsNullOrEmpty("Carrying"))
			{
				agentAnimation._animator.SetBool("Carrying", value: false);
			}
			AgentAnimation agentAnimation2 = _agentAnimation;
			if (!string.IsNullOrEmpty("Idle"))
			{
				agentAnimation2._animator.SetBool("Idle", value: false);
			}
			AgentAnimation agentAnimation3 = _agentAnimation;
			if (!string.IsNullOrEmpty("Shoo"))
			{
				agentAnimation3._animator.SetTrigger("Shoo");
			}
			if (_resumeRoutine != null)
			{
				StopCoroutine(_resumeRoutine);
				_resumeRoutine = null;
			}
			_003CResumeActivities_003Ed__38 obj = new _003CResumeActivities_003Ed__38(0);
			obj._003C_003E1__state = 0;
			obj._003C_003E4__this = this;
			Coroutine resumeRoutine = StartCoroutine(obj);
			_resumeRoutine = resumeRoutine;
			RecoveryState = true;
			if (onShooed != null)
			{
				onShooed.Invoke();
			}
			if (initiatedByPlayer)
			{
				selectClosestPoint = false;
			}
		}
	}

	public void PetTheCat()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A897]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (_currentState == CatState.Carried)
		{
			return;
		}
		if (_currentState != CatState.PerformingActivity)
		{
			AgentMover movement = _movement;
			if (movement._Agent.isActiveAndEnabled && movement._Agent.isOnNavMesh)
			{
				movement._Agent.isStopped = true;
			}
			AgentMover movement2 = _movement;
			movement2._Agent.enabled = false;
			AgentAnimation agentAnimation = _agentAnimation;
			_currentState = CatState.Paused;
			if (!string.IsNullOrEmpty("Carrying"))
			{
				agentAnimation._animator.SetBool("Carrying", value: false);
			}
			AgentAnimation agentAnimation2 = _agentAnimation;
			if (!string.IsNullOrEmpty("Idle"))
			{
				agentAnimation2._animator.SetBool("Idle", value: false);
			}
			AgentAnimation agentAnimation3 = _agentAnimation;
			if (!string.IsNullOrEmpty("PetIdle"))
			{
				agentAnimation3._animator.SetTrigger("PetIdle");
			}
			if (_resumeRoutine != null)
			{
				StopCoroutine(_resumeRoutine);
				_resumeRoutine = null;
			}
			IEnumerator routine = ResumeActivities();
			Coroutine resumeRoutine = StartCoroutine(routine);
			_resumeRoutine = resumeRoutine;
			RecoveryState = true;
		}
		else
		{
			AgentAnimation agentAnimation4 = _agentAnimation;
			if (!string.IsNullOrEmpty("Pet"))
			{
				agentAnimation4._animator.SetTrigger("Pet");
			}
		}
		if (onPetted != null)
		{
			onPetted.Invoke();
		}
	}

	public void InterruptCat()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A898]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (_currentState != CatState.Carried)
		{
			AgentAnimation agentAnimation = _agentAnimation;
			if (!string.IsNullOrEmpty("Carrying"))
			{
				agentAnimation._animator.SetBool("Carrying", value: false);
			}
			AgentAnimation agentAnimation2 = _agentAnimation;
			if (!string.IsNullOrEmpty("Idle"))
			{
				agentAnimation2._animator.SetBool("Idle", value: false);
			}
			AgentAnimation agentAnimation3 = _agentAnimation;
			if (!string.IsNullOrEmpty("PetIdle"))
			{
				agentAnimation3._animator.SetTrigger("PetIdle");
			}
			_currentState = CatState.Idle;
			RecoveryState = false;
		}
	}

	public CatController()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A899]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		_pauseAnimationTrigger = "Pause";
		_dropDistance = 0.5f;
		_pauseTimeAfterDrop = 2f;
		activityTimeInPlace = 1200f;
		loopEndTrigger = "loopEnd";
		base._002Ector();
	}

	private float _003CHandleIdleState_003Eb__28_0(Transform d)
	{
		if ((object)d != null)
		{
			Vector3 position = d.position;
			Transform transform = base.transform;
			if ((object)transform != null)
			{
				Vector3 position2 = transform.position;
				float num = position.z - position2.z;
				float num2 = position.x - position2.x;
				object obj2 = default(object);
				object obj3 = default(object);
				object obj = obj2 - obj3;
				float num3 = num * num;
				object obj4 = obj * obj;
				float num4 = num2 * num2;
				float num5 = (float)obj4 + num4;
				return num5 + num3;
			}
		}
		throw new NullReferenceException();
	}
}
