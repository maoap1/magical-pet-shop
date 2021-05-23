using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIPalette", menuName = "PetShop/UI Palette")]
public class UIPalette : ScriptableObject {
    private static UIPalette _THIS;
    public static UIPalette THIS {
        get {
            if (_THIS != null) return _THIS;
            _THIS = GameObject.FindObjectOfType<UIPalette>();
            if (_THIS == null) {
                _THIS = Resources.Load<UIPalette>("UIPalette");
            }
            GameObject.DontDestroyOnLoad(_THIS);
            return _THIS;
        }
    }

    public Color DarkColor;
    public Color HeaderColor;
    public Color BackgroundColor;
    public Color TabColor;
    public Color SelectedTabColor;
    public Color GridItemColor;
    public Color NotificationColor;
    public Color InactiveColor;
    public Color HighlightColor;
    public Color HighlightLightColor;
    public Color HighlightDarkColor;
    public Color TextDarkColor;
    public Color TextLightColor;

    public Color GetColor(PaletteColor color) {
        switch (color) {
            case PaletteColor.DarkColor:
                return DarkColor;
            case PaletteColor.Header:
                return HeaderColor;
            case PaletteColor.Background:
                return BackgroundColor;
            case PaletteColor.Tab:
                return TabColor;
            case PaletteColor.SelectedTab:
                return SelectedTabColor;
            case PaletteColor.GridItem:
                return GridItemColor;
            case PaletteColor.Notification:
                return NotificationColor;
            case PaletteColor.Inactive:
                return InactiveColor;
            case PaletteColor.HighlightLight:
                return HighlightLightColor;
            case PaletteColor.HighlightDark:
                return HighlightDarkColor;
            case PaletteColor.Highlight:
                return HighlightColor;
            case PaletteColor.TextDark:
                return TextDarkColor;
            case PaletteColor.TextLight:
                return TextLightColor;
            default:
                return new Color(0, 0, 0);
        }
    }
}

public enum PaletteColor {
    DarkColor,
    Header,
    Background,
    Tab,
    SelectedTab,
    GridItem,
    Notification,
    Inactive,
    HighlightDark,
    Highlight,
    TextDark,
    TextLight,
    HighlightLight
}
