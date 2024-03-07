using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
public class Image_first_Award : MonoBehaviour
{
    public Action btnClickEventHandler;
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(BtnClick);
    }

    private void BtnClick()
    {
        if (btnClickEventHandler != null) btnClickEventHandler();
    }

    // Use this for initialization
    void Start()
    {
        Tweener tweener = transform.DORotate(new Vector3(0, 0, 90), 1.5f, RotateMode.LocalAxisAdd);
        tweener.SetLoops(-1, LoopType.Yoyo);
    }
}
