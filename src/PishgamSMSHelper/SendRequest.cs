using System.ComponentModel.DataAnnotations;

namespace PishgamSMSHelper;


internal class SendRequest
{
	/// <summary>
	/// متن پیام
	/// </summary>
	[Required]
	public string[] MessageBodies { get; set; }

	/// <summary>
	/// شماره دریافت کننده
	/// </summary>
	[Required]
	public string[] RecipientNumbers { get; set; }

	/// <summary>
	/// شماره ارسال کننده
	/// </summary>
	[Required]
	public string SenderNumber { get; set; }

	/// <summary>
	/// برچسب ارسال
	/// </summary>
	[MaxLength(40)]
	public string UserTag { get; set; }
}