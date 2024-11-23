namespace PishgamSMSHelper;

public class PishgamSMSConfig
{
	public const string SectionName = "PishgamSMS";
	public string BaseUrl { get; set; } = string.Empty;
	public string Token { get; set; } = string.Empty;
	public string SenderNumber { get; set; } = string.Empty;
	public string MessageTemplate { get; set; } = string.Empty;
}

