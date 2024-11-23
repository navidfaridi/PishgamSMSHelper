# PishgamSMSHelper

A C# library to help send SMS via the **Pishgam** SMS gateway.

## Features

- Simplifies sending SMS messages using the Pishgam SMS gateway.
- Easy integration with your C# projects.
  
## Installation

To use this library in your .NET project, you can clone this repository or install it via NuGet once it's published.

```bash
dotnet add package PishgamSMSHelper

Usage
Example of how to send an SMS using PishgamSMSHelper:

using PishgamSMSHelper;

var sms = new PishgamSMS();
sms.Send("YOUR_API_KEY", "YOUR_SENDER_NUMBER", "RECIPIENT_NUMBER", "Your message content");
