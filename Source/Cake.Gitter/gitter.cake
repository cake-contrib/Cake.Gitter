#r "Cake.Gitter.dll"
var gitterToken         = EnvironmentVariable("GITTER_TOKEN");
var gitterWebHookUri    = EnvironmentVariable("gitterWebHookUri");
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

try
{
	var postMessageResult = Gitter.Chat.PostMessage(
                message:"Hello from Cake.Gitter WebHook - No Message Level",
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

    postMessageResult = Gitter.Chat.PostMessage(
                message:"Hello from Cake.Gitter WebHook - GitterMessageLevel.Info",
                messageSettings:new GitterChatMessageSettings { IncomingWebHookUrl = gitterWebHookUri, MessageLevel = GitterMessageLevel.Info }
        );

    if (postMessageResult.Ok)
    {
        Information("Message {0} succcessfully sent", postMessageResult.TimeStamp);
    }
    else
    {
        Error("Failed to send message: {0}", postMessageResult.Error);
    }

    postMessageResult = Gitter.Chat.PostMessage(
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

Console.ReadLine();