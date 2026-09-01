namespace BitCode.Debug.TokenResolvers
{
	internal class DebugConsoleTokenResolver : TokenResolver<DebugConsole>
	{
		public override bool NeedsUserToken => false;

		protected override DebugConsole Resolve(string token)
		{
			return owningConsole;
		}
	}
}
