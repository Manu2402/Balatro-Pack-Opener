using UnityEngine;

[CreateAssetMenu(fileName = "GlobalEventArgs",
    menuName = "GlobalEventManager/GlobalEventArgs", order = 1)]
public class EventArgsTemplate : ScriptableObject
{
    [SerializeField]
    private GlobalEventIndex eventTemplate;
    [SerializeField]
    private string[] variablesName;
    [SerializeField]
    private ExtendedVariableType[] variablesType;

    public GlobalEventIndex EventTemplate
    {
        get { return eventTemplate; }
    }

    public string[] VariablesName
    {
        get { return variablesName; }
    }

    public ExtendedVariableType[] VariablesType
    {
        get { return variablesType; }
    }

}