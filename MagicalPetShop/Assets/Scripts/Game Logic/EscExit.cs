using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscExit : MonoBehaviour
{
    private static EscExit _THIS;
    public static EscExit THIS
    {
        get
        {
            if (_THIS != null) return _THIS;
            _THIS = GameObject.FindObjectOfType<EscExit>();
            if (_THIS == null)
            {
                GameObject go = new GameObject();
                go.name = nameof(EscExit);
                _THIS = go.AddComponent<EscExit>();
            }
            GameObject.DontDestroyOnLoad(_THIS);
            return _THIS;
        }
    }

    private void Start()
    {
        if (EscExit.THIS != this)
        {
            GameObject.Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            PlayerState.THIS.Save();
            Application.Quit();
        }

    }
}
