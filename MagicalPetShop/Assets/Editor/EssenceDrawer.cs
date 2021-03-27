using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

[CustomPropertyDrawer(typeof(Essence))]
public class EssenceDrawer : PropertyDrawer
{
    private readonly GenericMenu.MenuFunction2 _onSelected;
    private GUIContent _popupContent = new GUIContent();

    private readonly int _controlHint = typeof(Essence).GetHashCode();
    private Essence _selectedEssence;
    private List<Essence> _essences = new List<Essence>();
    private int _selectedControlID;
    private bool _isChanged;

    public EssenceDrawer()
    {
        _onSelected = OnSelected;

        EditorApplication.projectChanged += _essences.Clear;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_essences.Count == 0)
        {
            GetObjects();
        }

        Draw(position, label, property);
    }


    private void Draw(Rect position, GUIContent label,
    SerializedProperty property)
    {
        if (label != null && label != GUIContent.none)
            position = EditorGUI.PrefixLabel(position, label);

        UpdateSelectionControl(position, label, property);
    }

    private void UpdateSelectionControl(Rect position, GUIContent label,
    SerializedProperty property)
    {
        Essence output = DrawSelectionControl(new Rect(position.x, position.y, position.width, 20), label, property.objectReferenceValue as Essence);
        if (_isChanged)
        {
            _isChanged = false;
            property.objectReferenceValue = output;
        }
    }

    private Essence DrawSelectionControl(Rect position, GUIContent label,
    Essence essence)
    {
        bool triggerDropDown = false;
        int controlID = GUIUtility.GetControlID(_controlHint, FocusType.Keyboard, position);

        switch (Event.current.GetTypeForControl(controlID))
        {
            case EventType.ExecuteCommand:
                if (Event.current.commandName == "ReferenceUpdated")
                {
                    if (_selectedControlID == controlID)
                    {
                        if (essence != _selectedEssence)
                        {
                            essence = _selectedEssence;
                            _isChanged = true;
                        }

                        _selectedControlID = 0;
                        _selectedEssence = null;
                    }
                }
                break;

            case EventType.MouseDown:
                if (GUI.enabled && position.Contains(Event.current.mousePosition))
                {
                    GUIUtility.keyboardControl = controlID;
                    triggerDropDown = true;
                    Event.current.Use();
                }
                break;

            case EventType.KeyDown:
                if (GUI.enabled && GUIUtility.keyboardControl == controlID)
                {
                    if (Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.Space)
                    {
                        triggerDropDown = true;
                        Event.current.Use();
                    }
                }
                break;

            case EventType.Repaint:
                if (essence == null)
                {
                    _popupContent.text = "Nothing";
                }
                else
                {
                    _popupContent.text = essence.name;
                }
                EditorStyles.popup.Draw(position, _popupContent, controlID);
                break;
        }

        if (_essences.Count != 0 && triggerDropDown)
        {
            _selectedControlID = controlID;
            _selectedEssence = essence;

            DisplayDropDown(position, essence);
        }

        return essence;
    }

    private void DisplayDropDown(Rect position, Essence selectedEssence)
    {
        var menu = new GenericMenu();

        for (int i = 0; i < _essences.Count; ++i)
        {
            Essence essence = _essences[i];

            string menuLabel = _essences[i].essenceName;
            if (string.IsNullOrEmpty(menuLabel))
                continue;

            var content = new GUIContent(menuLabel);
            menu.AddItem(content, essence == selectedEssence, _onSelected, essence);
        }

        menu.DropDown(position);
    }

    private void OnSelected(object userData)
    {
        _selectedEssence = userData as Essence;
        var ReferenceUpdatedEvent = EditorGUIUtility.CommandEvent("ReferenceUpdated");
        EditorWindow.focusedWindow.SendEvent(ReferenceUpdatedEvent);
    }

    private void GetObjects()
    {
        string[] guids = AssetDatabase.FindAssets(String.Format("t:{0}", typeof(Essence)));
        for (int i = 0; i < guids.Length; i++)
        {
            _essences.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), typeof(Essence)) as Essence);
        }
    }
}
