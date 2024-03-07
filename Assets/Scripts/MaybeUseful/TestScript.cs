using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    private static TestScript _instance;

    public static TestScript instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TestScript();
            }
            return _instance;
        }
    }

    UnityEngine.UI.Text t;

    private TestScript()
    {
        t = GameObject.Find("PlayTips_txt2").GetComponent<UnityEngine.UI.Text>();
    }

    public void Log(string str)
    {
        t.text = str;
    }

}
