using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DropDownCtrl2 : MonoBehaviour
{

    private Dropdown dropdown;
    private WaitLoadState waitLoadState;
    private Ctrl ctrl;

    private void Start()
    {
        dropdown = transform.parent.GetComponent<Dropdown>();
        ctrl = GameObject.FindWithTag("Ctrl").GetComponent<Ctrl>();
        waitLoadState = ctrl.GetComponentInChildren<WaitLoadState>();
        dropdown.value = ctrl.model.mysaveData.LanguageKey;
    }

    public void Show()
    {
        int value = dropdown.value;
        if (value == 0)
        {
            waitLoadState.saveData.LanguageKey = 0;
        }
        else
        if (value == 1)
        {
            waitLoadState.saveData.LanguageKey = 1;
        }
        else
        if (value == 2)
        {
            waitLoadState.saveData.LanguageKey = 2;
        }

        ctrl.model.SaveMyData();
        //执行委托吧
        WaitLoadState.PlayLanguageConfigEventhandler();
    }
}
