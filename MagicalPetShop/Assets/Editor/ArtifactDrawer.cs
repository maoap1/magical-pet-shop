using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

[CustomPropertyDrawer(typeof(Artifact))]
public class ArtifactDrawer : PropertyDrawer
{
    private readonly GenericMenu.MenuFunction2 _onSelected;
    private GUIContent _popupContent = new GUIContent();

    private readonly int _controlHint = typeof(Artifact).GetHashCode();
    private Artifact _selectedArtifact;
    private List<Artifact> _artifacts = new List<Artifact>();
    private int _selectedControlID;
    private bool _isChanged;

    public ArtifactDrawer()
    {
        _onSelected = OnSelected;

        EditorApplication.projectChanged += _artifacts.Clear;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (_artifacts.Count == 0)
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
        Artifact output = DrawSelectionControl(new Rect(position.x, position.y, position.width, 20), label, property.objectReferenceValue as Artifact);
        if (_isChanged)
        {
            _isChanged = false;
            property.objectReferenceValue = output;
        }
    }

    private Artifact DrawSelectionControl(Rect position, GUIContent label,
    Artifact artifact)
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
                        if (artifact != _selectedArtifact)
                        {
                            artifact = _selectedArtifact;
                            _isChanged = true;
                        }

                        _selectedControlID = 0;
                        _selectedArtifact = null;
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
                if (artifact == null)
                {
                    _popupContent.text = "Nothing";
                }
                else
                {
                    _popupContent.text = artifact.name;
                }
                EditorStyles.popup.Draw(position, _popupContent, controlID);
                break;
        }

        if (_artifacts.Count != 0 && triggerDropDown)
        {
            _selectedControlID = controlID;
            _selectedArtifact = artifact;

            DisplayDropDown(position, artifact);
        }

        return artifact;
    }

    private void DisplayDropDown(Rect position, Artifact selectedArtifact)
    {
        var menu = new GenericMenu();

        for (int i = 0; i < _artifacts.Count; ++i)
        {
            Artifact artifact = _artifacts[i];

            string menuLabel = _artifacts[i].name;
            if (string.IsNullOrEmpty(menuLabel))
                continue;

            var content = new GUIContent(menuLabel);
            menu.AddItem(content, artifact == selectedArtifact, _onSelected, artifact);
        }

        menu.DropDown(position);
    }

    private void OnSelected(object userData)
    {
        _selectedArtifact = userData as Artifact;
        var ReferenceUpdatedEvent = EditorGUIUtility.CommandEvent("ReferenceUpdated");
        EditorWindow.focusedWindow.SendEvent(ReferenceUpdatedEvent);
    }

    private void GetObjects()
    {
        string[] guids = AssetDatabase.FindAssets(String.Format("t:{0}", typeof(Artifact)));
        for (int i = 0; i < guids.Length; i++)
        {
            _artifacts.Add(AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(guids[i]), typeof(Artifact)) as Artifact);
        }
    }
}
