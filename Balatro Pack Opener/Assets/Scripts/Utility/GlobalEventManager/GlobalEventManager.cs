using UnityEngine.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GlobalEvent : UnityEvent<GlobalEventArgs> { }

public static class GlobalEventManager
{
    private readonly static GlobalEvent[] globalEvents;

    static GlobalEventManager()
    {
        globalEvents = new GlobalEvent[Enum.GetValues((typeof(GlobalEventIndex))).Length];
        for (int i = 0; i < globalEvents.Length; i++)
        {
            globalEvents[i] = new GlobalEvent();
        }
    }

    public static void AddListener(GlobalEventIndex eventToListen, UnityAction<GlobalEventArgs> call)
    {
        globalEvents[(int)eventToListen].AddListener(call);
    }

    public static void RemoveListener(GlobalEventIndex eventToRemove, UnityAction<GlobalEventArgs> call)
    {
        globalEvents[(int)eventToRemove].RemoveListener(call);
    }

    public static void CastEvent(GlobalEventIndex eventToCast, GlobalEventArgs message)
    {
#if DEBUG
        Debug.Log(eventToCast + GlobalEventArgsFactory.GetDebugString(eventToCast, message));
#endif
        globalEvents[(int)eventToCast]?.Invoke(message);
    }

}

[Serializable]
public class GlobalEventArgs : EventArgs
{
    public ExtendedVariable[] Args;

    public ExtendedVariable GetVariableByName(string name)
    {
        for (int i = 0; i < Args.Length; i++)
        {
            if (Args[i].VariableName == name)
            {
                return Args[i];
            }
        }

        return null;
    }

    public GlobalEventArgs() { }

    public GlobalEventArgs(ExtendedVariable[] args)
    {
        Args = args;
    }

    public static bool operator ==(GlobalEventArgs a, GlobalEventArgs b)
    {
        if (a.Args.Length != b.Args.Length) return false;
        for (int i = 0; i < a.Args.Length; i++)
        {
            if (!ExtendedVariable.StrictEquals(a.Args[i], b.Args[i]))
            {
                return false;
            }
        }

        return true;
    }

    public static bool operator !=(GlobalEventArgs a, GlobalEventArgs b)
    {
        return !(a == b);
    }

    #region Overrides
    public override bool Equals(object obj)
    {
        var args = obj as GlobalEventArgs;
        return args != null &&
               EqualityComparer<ExtendedVariable[]>.Default.Equals(this.Args, args.Args);
    }

    public override int GetHashCode()
    {
        var hashCode = 1134202432;
        hashCode = hashCode * -1521134295 + EqualityComparer<ExtendedVariable[]>.Default.GetHashCode(Args);
        return hashCode;
    }
    #endregion
}

public enum GlobalEventIndex
{
    PackOpened
}