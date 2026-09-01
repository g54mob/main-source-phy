namespace ModIO.API
{
	public class SubmitReportParameters : RequestParameters
	{
		public string resource
		{
			set
			{
				SetStringValue("resource", value);
			}
		}

		public int id
		{
			set
			{
				SetStringValue("id", value);
			}
		}

		public ReportType type
		{
			set
			{
				SetStringValue("type", (int)value);
			}
		}

		public string summary
		{
			set
			{
				SetStringValue("summary", value);
			}
		}

		public string name
		{
			set
			{
				SetStringValue("name", value);
			}
		}

		public string contact
		{
			set
			{
				SetStringValue("contact", value);
			}
		}
	}
}
