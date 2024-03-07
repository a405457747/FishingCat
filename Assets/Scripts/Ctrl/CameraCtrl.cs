using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    private Camera camera;
    private Transform hookTrans;
    private Vector2 offset;

    private void Awake()
    {
        camera = Camera.main;
        hookTrans = GetComponentInChildren<Hook>().transform;
        offset = camera.transform.position - hookTrans.position;
    }

    private void LateUpdate()
    {
        if (GameManager.instance.isStartCameraFllow)
        {
            Vector2 v2 = (Vector2)hookTrans.position + offset;
            camera.transform.position = new Vector3(camera.transform.position.x, v2.y, camera.transform.position.z);
        }
    }
}
