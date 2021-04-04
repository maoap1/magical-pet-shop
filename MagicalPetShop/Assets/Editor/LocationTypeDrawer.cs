using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

[CustomPropertyDrawer(typeof(LocationType))]
public class LocationTypeDrawer : PropertyDrawer
{
    private readonly GenericMenu.MenuFunction2 _onSelected;
    private GUIContent _popupContent = new GUIContent();

    private readonly int _controlHint = typeof(LocationType).GetHashCode();
    private LocationType _selectedLocation;
    private List<LocationType> _locations = new List<LocationType>();
    private int _selectedControlID;
    private bool _isChanged;

    public LocationTypeDrawer()
    {
        _onSelected = OnSelected;

        EditorApplication.projectChanged += _locations.Clear;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_locations.Count == 0)
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
        LocationType output = DrawSelectionControl(new Rect(position.x, position.y, position.width, 20), label, property.objectReferenceValue as LocationType);
        if (_isChanged)
        {
            _isChanged = false;
            property.objectReferenceValue = output;
        }
    }

    private LocationType DrawSelectionControl(Rect position, GUIContent label,
    LocationType location)
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
                        if (location != _selectedLocation)
                        {
                            location = _selectedLocation;
                            _isChanged = true;
                        }

                        _selectedControlID = 0;
                        _selectedLocation = null;
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
                if (location == null)
                {
                    _popupContent.text = "Nothing";
                }
                else
                {
                    _popupContent.text = location.name;
                }
                EditorStyles.popup.Draw(position, _popupContent, controlID);
                break;
        }

        if (_locations.Count != 0 && triggerDropDown)
        {
            _selectedControlID = controlID;
            _selectedLocation = location;

            DisplayDropDown(position, location);
        }

        return location;
    }

    private void DisplayDropDown(Rect position, LocationType selectedLocation)
    {
        var menu = new GenericMenu();

        for (int i = 0; i < _locations.Count; ++i)
        {
            LocationType location = _locations[i];

            string menuLabel = _locations[i].name;
            if (string.IsNullOrEmpty(menuLabel))
                continue;

            var content = new GUIContent(menuLabel);
            menu.AddItem(content, location == selectedLocation, _onSelected, location);
        }

        menu.DropDown(position);
    }

    private void OnSelected(object userData)
    {
        _selectedLocation = userData as LocationType;
        var ReferenceUpdatedEvent = EditorGUIUtility.CommandEvent("ReferenceUpdated");
        EditorWindow.focusedWindow.SendEvent(ReferenceUpdatedEvent);
    }

    private void GetObjects()
    {
        string[] guids = AssetDatabase.FindAssets(String.Format("t:{0}", typeof(LocationType)));
        for (int i = 0; i < guids.Length; i++)
        {
            _locations.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), typeof(LocationType)) as LocationType);
        }
    }
}
