using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(ExtendedVariable))]
public class ExtendedVariableDrawer : PropertyDrawer
{
    private const string valueFieldNodeName = "valueField";
    private const string nameValueString = "variableName";
    private const string typeValueString = "type";
    private const string boolValueString = "boolValue";
    private const string floatValueString = "floatValue";
    private const string gameObjectValueString = "gameObjectValue";
    private const string intValueString = "intValue";
    private const string stringValueString = "stringValue";
    private const string uintValueString = "uIntValue";
    private const string vector2ValueString = "vector2Value";
    private const string vector3ValueString = "vector3Value";

    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        SerializedProperty nameProperty = property.FindPropertyRelative(nameValueString);

        var namePropertyField = new PropertyField(nameProperty);
        container.name = nameProperty.stringValue;
        var variableTypeField = new PropertyField(property.FindPropertyRelative(typeValueString));
        ExtendedVariableType variableType = (ExtendedVariableType)property.FindPropertyRelative(typeValueString).enumValueIndex;

        container.Add(namePropertyField);
        container.Add(variableTypeField);
        container.Add(GetCorrectElement(container, variableType, property));

        variableTypeField.RegisterValueChangeCallback((evt) =>
        {
            container.Add(GetCorrectElement(
                container,
                (ExtendedVariableType)evt.changedProperty.enumValueIndex,
                property));
        });

        return container;
    }

    private BindableElement GetCorrectElement(VisualElement container, ExtendedVariableType type, SerializedProperty property)
    {
        var existingField = container.Q(valueFieldNodeName);
        if (existingField != null)
        {
            container.Remove(existingField);
        }

        BindableElement createdElement = null;

        switch (type)
        {
            case ExtendedVariableType.Bool:
                createdElement = new Toggle(boolValueString);
                createdElement.BindProperty(property.FindPropertyRelative(boolValueString));
                break;
            case ExtendedVariableType.Float:
                createdElement = new FloatField(floatValueString);
                createdElement.BindProperty(property.FindPropertyRelative(floatValueString));
                break;
            case ExtendedVariableType.GameObject:
                createdElement = new ObjectField(gameObjectValueString);
                createdElement.BindProperty(property.FindPropertyRelative(gameObjectValueString));
                break;
            case ExtendedVariableType.Int:
                createdElement = new IntegerField(intValueString);
                createdElement.BindProperty(property.FindPropertyRelative(intValueString));
                break;
            case ExtendedVariableType.String:
                createdElement = new TextField(stringValueString);
                createdElement.BindProperty(property.FindPropertyRelative(stringValueString));
                break;
            case ExtendedVariableType.UInt:
                createdElement = new UnsignedIntegerField(uintValueString);
                createdElement.BindProperty(property.FindPropertyRelative(uintValueString));
                break;
            case ExtendedVariableType.Vector2:
                createdElement = new Vector2Field(vector2ValueString);
                createdElement.BindProperty(property.FindPropertyRelative(vector2ValueString));
                break;
            case ExtendedVariableType.Vector3:
                createdElement = new Vector3Field(vector3ValueString);
                createdElement.BindProperty(property.FindPropertyRelative(vector3ValueString));
                break;
        }

        createdElement.name = valueFieldNodeName;
        return createdElement;
    }

}