using Microsoft.AspNetCore.Mvc;
using Pargoon.Core;
using PishgamSMSHelper;

namespace PishgamSmsHelper.Sample.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
	private readonly ILogger<SampleController> _logger;
	private readonly ISmsService _smsService;

	public SampleController(ILogger<SampleController> logger, ISmsService smsService)
	{
		_logger = logger;
		_smsService = smsService;
	}

	[HttpPost]
	[ProducesResponseType(typeof(TResult), 200)]
	public async Task<IActionResult> SendSMS(string phoneNumber, string message)
	{
		var result = await _smsService.SendAsync(phoneNumber, message, "otp");
		return Ok(result);
	}
}
