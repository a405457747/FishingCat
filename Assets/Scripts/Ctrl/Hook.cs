using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Hook : MonoBehaviour
{

    //下去的距离
    public float SinkingDistance
    {
        get;
        set;
    }
    //下去的计时器
    public float SinkingTimeTimer = 0f;
    [HideInInspector]
    public Vector2 originPos;
    //是否开始移动
    private bool isStartMove;
    public delegate void CompleteEventHandler();
    //达到终点后的委托
    public CompleteEventHandler completeEventHandler;

    private SystemConfig systemConfig;
    private Model model;
    //钩子速度
    [SerializeField]
    private float Speed;
    //下去的时间
    private float SinkingTime;

    private void Awake()
    {
        isStartMove = false;
        originPos = transform.position;
    }

    private void Start()
    {
        model = GameObject.FindWithTag("Model").GetComponent<Model>();
        systemConfig = model.mySystemConfig;
        UpdateSinkingDistanceAbout();
    }

    public void UpdateSinkingDistanceAbout()
    {
        int level = model.mysaveData.WaterDepthLevel;
        Speed = (float)systemConfig.DropHookSpeed * Calculate.ReturnCanShowScene(level);
        SinkingDistance = (float)systemConfig.DepthPerLevel * level;
        SinkingTime = SinkingDistance / Speed;
    }

    public void ActivateScript()
    {
        this.enabled = true;
        isStartMove = true;
    }

    public void DeactivateScript()
    {
        this.enabled = false;
        isStartMove = false;
    }

    private void Update()
    {
        if (isStartMove)
        {
            SinkingTimeTimer += Time.deltaTime;
            transform.Translate(Vector2.down * Time.deltaTime * Speed);
            if (SinkingTimeTimer >= SinkingTime)
            {
                if (completeEventHandler != null) completeEventHandler();
                SinkingTimeTimer = 0;
                isStartMove = false;
            }
        }
    }
}
