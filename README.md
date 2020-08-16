# AuthGuard's C# Implementation
> A .NET implementation of the AuthGuard.NET Licensing System
## Setup
First you'll need to get your `Secret Key`, your `App Version` and your `Variable Secret`:
* `Secret Key` + `App Version` is located at https://authguard.net/dashboard on your application's row
*  `Variable Secret` is located at https://authguard.net/dashboard/applications/variables.php?={your_app_id}

After you got the your `Secret Key`, your `App Version` and your `Variable Secret`, you can now Initialize AuthGuard and use all its features:

## Initialize
```csharp
//This connects your file to the AuthGuard.net API
Guard.Initialize("PROGRAMSECRET", "VERSION", "VARIABLESECRET");
```
> After a successful initialization, the server will send back the following information on your application based on the settings you have picked

* `GuardSettings.ProgramName` : Application name
* `GuardSettings.DeveloperMode` : DeveloperMode Enabled/Disabled
* `GuardSettings.Version` : Applications version
* `GuardSettings.Freemode` : Freemode Enabled/Disabled
* `GuardSettings.HWIDLock` : HWIDLock Enabled/Disabled

## Register
```csharp
if (Guard.Register(username, password, email, license))
{
    MessageBox.Show("You have successfully registered!", GuardSettings.ProgramName, MessageBoxButton.OK, MessageBoxImage.Information);
    // Do code of what you want after successful register here!
}
```

## Login
```csharp
if (Guard.Login(username, password))
{
    MessageBox.Show("You have successfully logged in!", UserInfo.Username, MessageBoxButton.OK, MessageBoxImage.Information);
    // Success login stuff goes here
}
```
> After a successful login, the server will send back the following information on your user

* `UserInfo.Username` : Users username
* `UserInfo.Email` : Users email
* `UserInfo.HWID` : Users hardware ID
* `UserInfo.Level` : Users Level
* `UserInfo.IP` : Users IP
* `UserInfo.Expires` : Users expiry

## Extend Subscription
```csharp
if (Guard.RedeemToken(username, password, token))
{
    MessageBox.Show("You have successfully extended your subscription!", GuardSettings.ProgramName, MessageBoxButton.OK, MessageBoxImage.Information);
    //Do code of what you want after successful extend here!
}
```
## Credits
Thanks to [Centos](https://github.com/TrinityNET) for sharing his awesome project
