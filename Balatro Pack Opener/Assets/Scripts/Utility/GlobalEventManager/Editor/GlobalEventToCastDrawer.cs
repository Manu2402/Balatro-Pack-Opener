using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(GlobalEventToCast))]
public class GlobalEventToCastDrawer : PropertyDrawer
{
    private const string templatesRoot = "Assets/Database/GlobalEventArgs/";
    private const string eventToCastString = "eventToCast";
    private const string messageString = "message";
    private const string messageArgsString = "args";
    private const string variableNameString = "variableName";
    private const string variableTypeString = "type";

    private EventArgsTemplate[] templatesAssets;

    private void LoadAssets()
    {
        if (templatesAssets != null) return;
        templatesAssets = new EventArgsTemplate[Enum.GetValues(typeof(GlobalEventIndex)).Length];
        int i = 0;
        string fullPath;
        foreach (string eventName in Enum.GetNames(typeof(GlobalEventIndex)))
        {
            fullPath = templatesRoot + eventName + ".asset";
            templatesAssets[i] = AssetDatabase.LoadAssetAtPath<EventArgsTemplate>(fullPath);
            i++;
        }
    }

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        LoadAssets();

        var container = new VisualElement();
        var eventToCastField = new PropertyField(property.FindPropertyRelative(eventToCastString));
        var eventArgsField = new PropertyField(property.FindPropertyRelative(messageString));

        container.Add(eventToCastField);
        container.Add(eventArgsField);

        eventToCastField.RegisterValueChangeCallback((evt) =>
        {
            UpdateEventArgsProperty(
                property.FindPropertyRelative(messageString).FindPropertyRelative(messageArgsString),
                (GlobalEventIndex)evt.changedProperty.enumValueIndex);
        });

        return container;
    }

    private void UpdateEventArgsProperty(SerializedProperty property, GlobalEventIndex eventType)
    {
        EventArgsTemplate template = templatesAssets[(int)eventType];
        property.arraySize = template.VariablesName.Length;
        for (int i = 0; i < property.arraySize; i++)
        {
            UpdateArg(property.GetArrayElementAtIndex(i), template.VariablesName[i], template.VariablesType[i]);
        }

        property.serializedObject.ApplyModifiedProperties();
    }

    private void UpdateArg(SerializedProperty property, string name, ExtendedVariableType variableType)
    {
        property.FindPropertyRelative(variableNameString).stringValue = name;
        property.FindPropertyRelative(variableTypeString).enumValueIndex = (int)variableType;
    }

}