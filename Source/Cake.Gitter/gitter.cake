#r "Cake.Gitter.dll"
var gitterToken         = EnvironmentVariable("GITTER_TOKEN");
var gitterWebHookUri    = EnvironmentVariable("gitterWebHookUri");

try
{
    var postMessageResult = Gitter.Chat.PostMessage(
                token:gitterToken,
                message:"Test"
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
                message:"Test",
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

Console.ReadLine();