using System.Collections.Generic;
using UnityEngine;

public static class GlobalEventArgsFactory
{
#if DEBUG
    private delegate string EventDebug(GlobalEventArgs message);
    private static Dictionary<GlobalEventIndex, EventDebug> methodDebugString;

    static GlobalEventArgsFactory()
    {
        methodDebugString = new Dictionary<GlobalEventIndex, EventDebug>();
        methodDebugString.Add(GlobalEventIndex.PackOpened, new EventDebug(PackOpenedDebug));
    }

    public static string GetDebugString(GlobalEventIndex eventType, GlobalEventArgs message)
    {
        return methodDebugString[eventType](message);
    }
#endif

    #region PackOpened
    public static GlobalEventArgs PackOpenedFactory(uint collectablesAmount, GameObject[] collectables)
    {
        GlobalEventArgs message = new GlobalEventArgs();

        message.Args = new ExtendedVariable[collectablesAmount + 1];

        const string collectablesAmountString = "Collectables Amount";
        message.Args[0] = new ExtendedVariable(collectablesAmountString,
            ExtendedVariableType.UInt,
            collectablesAmount
        );

        const string collectableString = "Collectable N° ";

        for (int i = 1; i < message.Args.Length; i++)
        {
            message.Args[i] = new ExtendedVariable(collectableString + i.ToString(), 
                ExtendedVariableType.GameObject,
                collectables[i - 1]);
        }

        return message;
    }

    public static void PackOpenedParser(GlobalEventArgs message, out GameObject[] collectables, out uint collectablesAmount)
    {
        collectablesAmount = (uint)message.Args[0].GetValue();

        collectables = new GameObject[collectablesAmount];
        for (int i = 0; i < collectablesAmount; i++)
        {
            collectables[i] = (GameObject)message.Args[i + 1].GetValue();
        }
    }

    public static string PackOpenedDebug(GlobalEventArgs message)
    {
        string finalString =  " A pack has been opened. It contains " + 
            (uint)message.Args[0].GetValue() + " collectables and have been obtained the " +
            "following collectables:";

        for (int i = 1; i < message.Args.Length; i++)
        {
            GameObject currentCollectable = (GameObject)message.Args[i].GetValue();
            finalString += "\n" + currentCollectable.name;
        }

        return finalString;
    }
    #endregion
}