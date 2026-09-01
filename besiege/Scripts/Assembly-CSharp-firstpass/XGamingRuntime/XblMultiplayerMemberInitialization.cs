using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerMemberInitialization
	{
		public ulong JoinTimeout { get; set; }

		public ulong MeasurementTimeout { get; set; }

		public ulong EvaluationTimeout { get; set; }

		public bool ExternalEvaluation { get; set; }

		public uint MembersNeededToStart { get; set; }

		public XblMultiplayerMemberInitialization()
		{
		}

		internal XblMultiplayerMemberInitialization(XGamingRuntime.Interop.XblMultiplayerMemberInitialization interopStruct)
		{
			JoinTimeout = interopStruct.JoinTimeout;
			MeasurementTimeout = interopStruct.MeasurementTimeout;
			EvaluationTimeout = interopStruct.EvaluationTimeout;
			ExternalEvaluation = interopStruct.ExternalEvaluation.Value;
			MembersNeededToStart = interopStruct.MembersNeededToStart;
		}
	}
}
