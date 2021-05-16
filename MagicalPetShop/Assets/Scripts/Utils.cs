using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public static class Utils
{
    ///"miliseconds from 1970-01-01T00:00:00Z (UTC)")
    public static long EpochTime()
    {
        System.DateTime epochStart = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        long cur_time = (long)(System.DateTime.UtcNow - epochStart).TotalMilliseconds;
        return cur_time;
    }

    public static bool IsPointerOverGameObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public static Color WithA(this Color c, float a)
        => new Color(c.r, c.g, c.b, a);
    public static Color WithR(this Color c, float r)
        => new Color(r, c.g, c.b, c.a);
    public static Color WithG(this Color c, float g)
        => new Color(c.r, g, c.b, c.a);
    public static Color WithB(this Color c, float b)
        => new Color(c.r, c.g, b, c.a);

    public static List<T> FindObject<T>() where T : Component {
        List<T> result = new List<T>();
        foreach (GameObject root in SceneManager.GetActiveScene().GetRootGameObjects()) {

            AddHidden(root, result);
        }
        return result;
    }

    private static void AddHidden<T>(GameObject root, List<T> result) where T : Component {
        if (root == null) return;
        foreach (T component in root.GetComponents<T>()) {
            result.Add(component);
        }

        for (int i = 0; i < root.transform.childCount; i++) {
            AddHidden(root.transform.GetChild(i).gameObject, result);
        }
    }

}
