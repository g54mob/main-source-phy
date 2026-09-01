namespace XGamingRuntime.Interop
{
	public struct XblMultiplayerMemberInitialization
	{
		internal readonly ulong JoinTimeout;

		internal readonly ulong MeasurementTimeout;

		internal readonly ulong EvaluationTimeout;

		internal readonly NativeBool ExternalEvaluation;

		internal readonly uint MembersNeededToStart;

		internal XblMultiplayerMemberInitialization(XGamingRuntime.XblMultiplayerMemberInitialization publicObject)
		{
			JoinTimeout = publicObject.JoinTimeout;
			MeasurementTimeout = publicObject.MeasurementTimeout;
			EvaluationTimeout = publicObject.EvaluationTimeout;
			ExternalEvaluation = new NativeBool(publicObject.ExternalEvaluation);
			MembersNeededToStart = publicObject.MembersNeededToStart;
		}
	}
}
