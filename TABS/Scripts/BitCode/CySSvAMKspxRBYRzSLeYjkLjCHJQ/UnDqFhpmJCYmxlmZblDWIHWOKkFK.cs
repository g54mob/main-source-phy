using System.Runtime.CompilerServices;
using BitCode.AssetManagement;
using BitCode.SceneManagement;

namespace CySSvAMKspxRBYRzSLeYjkLjCHJQ
{
	internal class UnDqFhpmJCYmxlmZblDWIHWOKkFK : ILoadTask
	{
		private readonly IResourceManager hzPfAnSIEgQZmSOzKlgkfDGKCDnC;

		private readonly string ZfaPTDQScotQlBWMPrtaTeOHHrsEA;

		private bool DmMgMyjYefCJRoAlejpEMZwScAQEb;

		public float TaskProgress
		{
			get
			{
				if (!DmMgMyjYefCJRoAlejpEMZwScAQEb)
				{
					while (true)
					{
						uint num;
						switch ((num = 2110155542u) % 3)
						{
						case 0u:
							continue;
						case 2u:
							return 0f;
						}
						break;
					}
				}
				return 1f;
			}
		}

		public bool IsDone => DmMgMyjYefCJRoAlejpEMZwScAQEb;

		public UnDqFhpmJCYmxlmZblDWIHWOKkFK(IResourceManager P_0, string P_1)
		{
			while (true)
			{
				int num = 763913516;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x5B93386)) % 4)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						hzPfAnSIEgQZmSOzKlgkfDGKCDnC = P_0;
						num = (int)(num2 * 982683406) ^ -1894291329;
						continue;
					case 1u:
						ZfaPTDQScotQlBWMPrtaTeOHHrsEA = P_1;
						num = (int)(num2 * 1266376283) ^ -1490413770;
						continue;
					case 3u:
						return;
					}
					break;
				}
			}
		}

		public void Start(bool async)
		{
			hzPfAnSIEgQZmSOzKlgkfDGKCDnC.LoadScene(ZfaPTDQScotQlBWMPrtaTeOHHrsEA, async, delegate
			{
				DmMgMyjYefCJRoAlejpEMZwScAQEb = true;
			});
		}

		public void Complete()
		{
		}

		[CompilerGenerated]
		private void vKOuTDcMdOVwMizFEOZTMbaISNrA(string P_0, object P_1)
		{
			DmMgMyjYefCJRoAlejpEMZwScAQEb = true;
		}
	}
}
