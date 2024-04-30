using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : Singleton<Notification>
{
    public const string NextDay = "NextDay";

    public const string PrestigeChanged = "PrestigeChanged";

    public const string TimeChanged = "TimeChanged";

    public delegate void NotificationHandler(object data = null);

    private Dictionary<string, NotificationHandler> handlerDic = new Dictionary<string, NotificationHandler>();

    public void Register(string notificationName, NotificationHandler handler) 
    {
        if (handlerDic.TryGetValue(notificationName, out var notificationHandler))
        {
            notificationHandler += handler;
            handlerDic[notificationName] = notificationHandler;
        }
        else 
        {
            handlerDic.Add(notificationName, handler);
        }
    }

    public void Unregister(string notificationName, NotificationHandler handler) 
    {
        if (handlerDic.TryGetValue(notificationName, out var notificationHandler))
        {
            notificationHandler -= handler;
            handlerDic[notificationName] = notificationHandler;
        }
    }

    public void Notify(string notificationName, object data = null) 
    {
        if (handlerDic.TryGetValue(notificationName, out var notificationHandler)) 
        {
            notificationHandler.Invoke(data);
        }
    }
}
