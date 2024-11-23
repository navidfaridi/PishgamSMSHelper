using Microsoft.Extensions.Options;
using Pargoon.Core;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PishgamSMSHelper;


public class PishgamSmsService : ISmsService
{
	private readonly PishgamSMSConfig _config;
	private readonly IHttpClientFactory _httpClientFactory;
	public PishgamSmsService(IOptions<PishgamSMSConfig> options, IHttpClientFactory httpClientFactory)
	{
		_config = options.Value;
		_httpClientFactory = httpClientFactory;
	}
	public async Task<TResult> SendAsync(string mobile, string text, string tag)
	{
		var sendRequest = new SendRequest()
		{
			MessageBodies = [text], //حداکثر 100 پیام
			RecipientNumbers = [mobile], //989********* //حداکثر 100 شماره
			SenderNumber = _config.SenderNumber, //50003****
			UserTag = tag
		};
		return await SendSmsAsync(sendRequest);
	}
	//private readonly JsonSerializerOptions JsonSerializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
	private async Task<TResult> SendSmsAsync(SendRequest request)
	{
		using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_config.BaseUrl}/Send");
		requestMessage.Content = new StringContent(JsonConvert.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json);

		var client = _httpClientFactory.CreateClient();
		client.DefaultRequestHeaders.Add("Authorization", _config.Token); //افزودن توکن
		using var responseMessage = await client.SendAsync(requestMessage);
		var response = await responseMessage.Content.ReadAsStringAsync();
		//وضعیت 200 ارسال موفق یا ناموفق - وضعیت های بازگشتی با تعداد پیام های ارسالی برابر است  
		//وضعیت 400 - ارسال ناموفق - وضعیت بازگشتی، یک مورد
		if (responseMessage.StatusCode is HttpStatusCode.OK or HttpStatusCode.BadRequest)
		{
			var res = JsonConvert.Deserialize<SendResponse>(response);
			//عملیات پس از ارسال
			var result = new TResult(responseMessage.StatusCode, string.Join(",", res.Result));
			return result;
		}
		else //عملیات ناموفق
		{
			var res = JsonConvert.Deserialize<ErrorResponse>(response);
			//عملیات پس از دریافت خطا
			if ((int)responseMessage.StatusCode == 428 && res.Message == "IpNotValid")
			{
				using var er = await client.GetAsync($"{_config.BaseUrl}/ip");
				if (er != null && er.StatusCode == HttpStatusCode.OK)
				{
					var erc = await er.Content.ReadAsStringAsync();
					var erco = JsonConvert.Deserialize<IPResponse>(erc);
					res.Message = res.Message + $" : {erco.result}";
				}
			}
			return new TResult(responseMessage.StatusCode, res.Message);
		}
	}
}
