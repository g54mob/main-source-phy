using System.Runtime.CompilerServices;

namespace Poly.Determinism
{
	public class LoggingBehaviour : OrderedBehaviour
	{
		protected new void Awake()
		{
			base.Awake();
			DeterminismLog.LogEvent(this, EventType.Awake);
		}

		protected new void OnValidate()
		{
			base.OnValidate();
		}

		protected void Start()
		{
			DeterminismLog.LogEvent(this, EventType.Start);
		}

		protected void OnEnable()
		{
			DeterminismLog.LogEvent(this, EventType.OnEnable);
		}

		protected void OnDisable()
		{
			DeterminismLog.LogEvent(this, EventType.OnDisable);
		}

		protected void OnDestroy()
		{
			DeterminismLog.LogEvent(this, EventType.OnDestroy);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected static bool Exists(object obj)
		{
			return obj != null;
		}
	}
}
