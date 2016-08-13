# Post Message

## Using Token

```csharp
#addin "Cake.Gitter"

var gitterToken         = EnvironmentVariable("GITTER_TOKEN");
var gitterRoomId        = EnvironmentVariable("gitterRoomId");

try
{
    var postMessageResult = Gitter.Chat.PostMessage(
                message:"Hello from Cake.Gitter API",
				messageSettings:new GitterChatMessageSettings { Token = gitterToken, RoomId = gitterRoomId}
        );

    if (postMessageResult.Ok)
    {
        Information("Message {0} succcessfully sent", postMessageResult.TimeStamp);
    }
    else
    {
        Error("Failed to send message: {0}", postMessageResult.Error);
    }
}
catch(Exception ex)
{
    Error("{0}", ex);
}
```

Cake output will be similar to below:

```
Message 2015-10-16T11:18:14.078Z successfully sent
```

This will result in a message appearing in the Gitter Room similar to the following:

![image](https://cloud.githubusercontent.com/assets/1271146/10540458/1c5fd648-7400-11e5-9529-1f3729850300.png)

## Using Web Hook Url

```csharp
#addin "Cake.Gitter"

var gitterWebHookUri    = EnvironmentVariable("gitterWebHookUri");
try
{
    var postMessageResult = Gitter.Chat.PostMessage(
                message:"Hello from Cake.Gitter WebHook",
                messageSettings:new GitterChatMessageSettings { IncomingWebHookUrl = gitterWebHookUri }
        );

    if (postMessageResult.Ok)
    {
        Information("Message {0} succcessfully sent", postMessageResult.TimeStamp);
    }
    else
    {
        Error("Failed to send message: {0}", postMessageResult.Error);
    }
}
catch(Exception ex)
{
    Error("{0}", ex);
}
```

Cake output will be similar to below:

```
Message 2015-10-16 11:26:23Z successfully sent
```

This will result in a message appearing in the Activity Feed for the Gitter Room similar to the following:

![image](https://cloud.githubusercontent.com/assets/1271146/10540466/2eb1905c-7400-11e5-89ad-6e58b6b7508b.png)```

*NOTE:* You can control the Gitter Message Level, either Info or Error, using the `GitterMessageLevel` property of the `GitterChatMessageSettings`.  To specify that a message should be an error, you can use the following:

```csharp
#addin "Cake.Gitter"

var gitterWebHookUri    = EnvironmentVariable("gitterWebHookUri");
try
{
    var postMessageResult = Gitter.Chat.PostMessage(
                message:"Hello from Cake.Gitter WebHook - GitterMessageLevel.Error",
                messageSettings:new GitterChatMessageSettings { IncomingWebHookUrl = gitterWebHookUri, MessageLevel = GitterMessageLevel.Error }
        );

    if (postMessageResult.Ok)
    {
        Information("Message {0} succcessfully sent", postMessageResult.TimeStamp);
    }
    else
    {
        Error("Failed to send message: {0}", postMessageResult.Error);
    }
}
catch(Exception ex)
{
    Error("{0}", ex);
}
```

This will result in a message appearing in the Activity Feed for the Gitter Room similar to the following:

![image](https://cloud.githubusercontent.com/assets/1271146/10902874/2967d120-81fb-11e5-988b-192cea7e8bac.png)
