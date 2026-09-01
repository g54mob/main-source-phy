using System;
using System.Runtime.CompilerServices;
using BitCode;
using JetBrains.Annotations;
using UnityEngine;

namespace dycJggssKJBbYomRwEcQasvEaFIib
{
	internal class DFaEhMkrGEEOwIAPDVnDUgbLXWaeB : IDisposable, IUpdateableService
	{
		private const uint JFHymyiHXHVBsKFDRLmpcLbLUeMo = 1u;

		[CompilerGenerated]
		private FrameTiming? WKagxTHSOlFDTjueUzxYPpGZJHTn;

		private readonly FrameTiming[] iiSxohggOhhyAFmMsjZrbCUfJpAcb = new FrameTiming[1];

		private readonly IServiceUpdater UeSkHcBnUDZDTHkgfGCVbjKIjUXtA;

		private bool tlRiOzSchvnYldbxynOFisSYraBV;

		public FrameTiming? fZddJOAHbxABIWmFfYFDKAMKNTlzA
		{
			[CompilerGenerated]
			get
			{
				return WKagxTHSOlFDTjueUzxYPpGZJHTn;
			}
			[CompilerGenerated]
			private set
			{
				WKagxTHSOlFDTjueUzxYPpGZJHTn = wKagxTHSOlFDTjueUzxYPpGZJHTn;
			}
		}

		public DFaEhMkrGEEOwIAPDVnDUgbLXWaeB([NotNull] IServiceUpdater P_0)
		{
			while (true)
			{
				int num = -1011675605;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1808623543)) % 3)
					{
					case 0u:
						break;
					default:
						return;
					case 2u:
						if (P_0 != null)
						{
							goto IL_0045;
						}
						throw new ArgumentNullException("serviceUpdater");
					case 1u:
						return;
					}
					break;
					IL_0045:
					UeSkHcBnUDZDTHkgfGCVbjKIjUXtA = P_0;
					P_0.RegisterService(this);
					num = (int)(num2 * 290965016) ^ -1995417313;
				}
			}
		}

		void IUpdateableService.Update()
		{
			if (tlRiOzSchvnYldbxynOFisSYraBV)
			{
				while (true)
				{
					uint num;
					switch ((num = 210370501u) % 3)
					{
					case 2u:
						continue;
					case 1u:
						throw new ObjectDisposedException(GetType().FullName);
					}
					break;
				}
			}
			FrameTimingManager.CaptureFrameTimings();
			uint latestTimings = FrameTimingManager.GetLatestTimings(1u, iiSxohggOhhyAFmMsjZrbCUfJpAcb);
			fZddJOAHbxABIWmFfYFDKAMKNTlzA = ((latestTimings != 0) ? new FrameTiming?(iiSxohggOhhyAFmMsjZrbCUfJpAcb[0]) : ((FrameTiming?)null));
		}

		public void Dispose()
		{
			if (tlRiOzSchvnYldbxynOFisSYraBV)
			{
				while (true)
				{
					uint num;
					switch ((num = 1110976777u) % 3)
					{
					case 2u:
						continue;
					case 1u:
						return;
					}
					break;
				}
			}
			tlRiOzSchvnYldbxynOFisSYraBV = true;
			UeSkHcBnUDZDTHkgfGCVbjKIjUXtA.DeregisterService(this);
		}
	}
}
