using Pargoon.Core;
using System.Threading.Tasks;

namespace PishgamSMSHelper;

public interface ISmsService
{
	Task<TResult> SendAsync(string mobile, string text, string tag);
}

