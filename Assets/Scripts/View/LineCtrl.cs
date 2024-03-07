using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCtrl : MonoBehaviour
{

    private LineRenderer lineRender;
    private Transform HookTrans;
    //鱼钩子和鱼线偏移
    public Vector3 offSet = new Vector3(1, 1,0);

    private void Awake()
    {
        HookTrans = transform.parent.Find("Hook");
        lineRender = GetComponent<LineRenderer>();
    }

    private void LateUpdate()
    {

        Line();
    }

    //划线
    public void Line()
    {
        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, HookTrans.position+ offSet);
    }

}
