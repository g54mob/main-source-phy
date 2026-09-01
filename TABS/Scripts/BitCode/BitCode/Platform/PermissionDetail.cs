using System.Runtime.CompilerServices;

namespace BitCode.Platform
{
	public class PermissionDetail
	{
		public static readonly PermissionDetail NoDetail = new PermissionDetail<string>("No detail.");

		internal PermissionDetail()
		{
		}

		internal static PermissionDetail OhHJMYtkCasjeedEqCuNlufHZFXS<_0001>(_0001 P_0)
		{
			return new PermissionDetail<string>($"No rule for the permission is defined. {P_0}");
		}
	}
	public class PermissionDetail<TDetail> : PermissionDetail
	{
		[CompilerGenerated]
		private readonly TDetail QbRhEgaeLJEOqPFBjNCENnZIIaqIA;

		public TDetail Detail
		{
			[CompilerGenerated]
			get
			{
				return QbRhEgaeLJEOqPFBjNCENnZIIaqIA;
			}
		}

		public override string ToString()
		{
			return Detail.ToString();
		}

		public PermissionDetail(TDetail detail)
		{
			QbRhEgaeLJEOqPFBjNCENnZIIaqIA = detail;
		}
	}
}
