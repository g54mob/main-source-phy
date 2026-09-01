using System.Collections.Generic;

namespace BitCode.Debug.TokenResolvers
{
	public abstract class TokenResolver<T> : TokenResolverBase<T>
	{
		protected virtual bool TryConsumeToken(IReadOnlyList<string> tokens, ref int lastUsedTokenIndex, out string retrievedToken)
		{
			if (lastUsedTokenIndex + 1 >= tokens.Count)
			{
				goto IL_000c;
			}
			goto IL_0046;
			IL_000c:
			int num = -819257567;
			goto IL_0011;
			IL_0011:
			uint num2;
			switch ((num2 = (uint)(num ^ -773887368)) % 4)
			{
			case 3u:
				break;
			case 1u:
				retrievedToken = null;
				return false;
			case 0u:
				goto IL_0046;
			default:
				return true;
			}
			goto IL_000c;
			IL_0046:
			lastUsedTokenIndex++;
			retrievedToken = tokens[lastUsedTokenIndex];
			num = -1390412766;
			goto IL_0011;
		}

		protected abstract T Resolve(string token);

		public override bool TryResolve(IReadOnlyList<string> tokens, ref int lastConsumedTokenIndex, out object resolvedToken)
		{
			string retrievedToken = null;
			if (NeedsUserToken)
			{
				while (true)
				{
					int num = -1711055085;
					while (true)
					{
						uint num2;
						switch ((num2 = (uint)(num ^ -2118083420)) % 4)
						{
						case 0u:
							break;
						case 3u:
						{
							int num3;
							int num4;
							if (TryConsumeToken(tokens, ref lastConsumedTokenIndex, out retrievedToken))
							{
								num3 = -152623785;
								num4 = num3;
							}
							else
							{
								num3 = -453549608;
								num4 = num3;
							}
							num = num3 ^ ((int)num2 * -147667098);
							continue;
						}
						case 2u:
							resolvedToken = null;
							return false;
						default:
							goto end_IL_000a;
						}
						break;
					}
					continue;
					end_IL_000a:
					break;
				}
			}
			resolvedToken = Resolve(retrievedToken);
			return true;
		}
	}
}
