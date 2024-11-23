# PishgamSMSHelper

A C# library to help send SMS via the **Pishgam** SMS gateway.

## Features

- Simplifies sending SMS messages using the Pishgam SMS gateway.
- Easy integration with your C# projects.
  
## Installation

To use this library in your .NET project, you can clone this repository or install it via NuGet once it's published.

```bash
dotnet add package PishgamSMSHelper
```

Configuration
Add the following section to your appsettings.json file for Pishgam SMS configuration:

```json
{
  "PishgamSMS": {
    "BaseUrl": "https://api.pishgam.com",
    "Token": "YOUR_API_TOKEN",
    "SenderNumber": "YOUR_SENDER_NUMBER",
    "MessageTemplate": "Default message template" // optional
  }
}
```

This configuration will be mapped to the following class:

```csharp
public class PishgamSMSConfig
{
    public const string SectionName = "PishgamSMS";
    public string BaseUrl { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string SenderNumber { get; set; } = string.Empty;
    public string MessageTemplate { get; set; } = string.Empty;
}
```

Usage
Service Activation
In your Program.cs or Startup.cs, register the service using:

```csharp
using PishgamSMSHelper;


builder.Services.AddPishgamSMS(builder.Configuration);

```

Sending SMS
Example of how to send an SMS using PishgamSMSHelper:

```csharp
using PishgamSMSHelper;

public class YourService : IYourService
{
    private readonly ISmsService _smsService;
    
    public YourService(ISmsService smsService)
    {
      _smsService = smsService;
    }

    public Task SendMessage(string mobile, string message)
    {
      await _smsService.SendAsync(mobile, message);
    }
}

```
