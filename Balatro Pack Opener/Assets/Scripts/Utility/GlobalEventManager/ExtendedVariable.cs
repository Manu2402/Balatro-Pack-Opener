using JetBrains.Annotations;
using System;
using UnityEngine;

public enum ExtendedVariableType
{
    Bool,
    Float,
    GameObject,
    Int,
    String,
    Vector2,
    Vector3,
    UInt
}

[Serializable]
public class ExtendedVariable
{
    #region Field
    [SerializeField]
    private string variableName;
    public string VariableName
    {
        get { return variableName; }
    }

    [SerializeField]
    private ExtendedVariableType type;
    [SerializeField]
    public ExtendedVariableType Type
    {
        get { return type; }
    }

    [SerializeField]
    private bool boolValue;
    [SerializeField]
    private float floatValue;
    [SerializeField]
    private GameObject gameObjectValue;
    [SerializeField]
    private int intValue;
    [SerializeField]
    private uint uIntValue;
    [SerializeField]
    private string stringValue;
    [SerializeField]
    private Vector2 vector2Value;
    [SerializeField]
    private Vector3 vector3Value;
    #endregion

    #region Methods
    public ExtendedVariable(string variableName, ExtendedVariableType type, object value)
    {
        this.variableName = variableName;
        this.type = type;
        SetValue(value);
    }

    public object GetValue()
    {
        return type switch // switch(type){...}
        {
            ExtendedVariableType.Bool => boolValue, // Returns
            ExtendedVariableType.Float => floatValue,
            ExtendedVariableType.GameObject => gameObjectValue,
            ExtendedVariableType.Int => intValue,
            ExtendedVariableType.String => stringValue,
            ExtendedVariableType.Vector2 => vector2Value,
            ExtendedVariableType.Vector3 => vector3Value,
            ExtendedVariableType.UInt => uIntValue,
            _ => null, // Default
        };
    }

    private void SetValue(object value)
    {
        switch (type)
        {
            case ExtendedVariableType.Bool:
            boolValue = (bool)value;
            break;
            case ExtendedVariableType.Float:
            floatValue = (float)value;
            break;
            case ExtendedVariableType.GameObject:
            gameObjectValue = (GameObject)value;
            break;
            case ExtendedVariableType.Int:
            intValue = (int)value;
            break;
            case ExtendedVariableType.String:
            stringValue = (string)value;
            break;
            case ExtendedVariableType.Vector2:
            vector2Value = (Vector2)value;
            break;
            case ExtendedVariableType.Vector3:
            vector3Value = (Vector3)value;
            break;
            case ExtendedVariableType.UInt:
            uIntValue = (uint)value;
            break;
        }
    }
    #endregion

    public static bool StrictEquals(ExtendedVariable a, ExtendedVariable b)
    {
        if (a.Type != b.Type) return false;
        return a.GetValue().Equals(b.GetValue());
    }
}