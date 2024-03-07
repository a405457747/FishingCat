using HedgehogTeam.EasyTouch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookCtrl : MonoBehaviour
{
    private QuickSwipe qs;
    private float HookMoveSpeed;
    private BoxCollider2D[] boxColliders;
    private Model model;

    private void Awake()
    {
        this.enabled = false;
        qs = GetComponent<QuickSwipe>();
        boxColliders = GetComponents<BoxCollider2D>();
    }

    private void Start()
    {
        model = GameObject.FindWithTag("Model").GetComponent<Model>();
        HookMoveSpeed = (float)model.mySystemConfig.HookMoveSpeed;
        qs.sensibility = HookMoveSpeed;
    }

    public void OnScript()
    {
        this.enabled = true;
      
    }

    public void OffScript()
    {
        this.enabled = false;
    }

    public void ChangeBoxColliders(int currentHookIndex){
        foreach(var boxcollider in boxColliders){
            boxcollider.enabled = false;
        }
        boxColliders[currentHookIndex].enabled = true;
    }

    private void Update()
    {
        if (GameManager.instance.isCanControlHook)
        {
            qs.enabled = true;
            float x = Mathf.Clamp(transform.position.x, -2.9f, 2.9f);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        else
        {
            qs.enabled = false;
        }
    }
}
