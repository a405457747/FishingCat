using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingCtrlState : FSMState
{ //要开始回钩子吗
    [HideInInspector]
    public bool isStartHook;
    public int CanCatchFishCount
    {
        get
        {
            return canCatchFishCount;
        }
        set
        {
            canCatchFishCount = value;
        }
    }

    //可以捉鱼的数量
    private int canCatchFishCount;
    private HookFish hookFish;
    private Hook hook;
    //回钩子的速度
    private float speed;
    //回钩的时间
    private float hookRunTime;
    //回钩子的距离
    private float hookRunDistance;
    //要开始回钩子的timer;
    private float isStartHookTimer = 0f;
    private SystemConfig systemConfig;
    private Model model;
    private saveData saveData;

    private void Awake()
    {
        stateID = StateID.FishingCtrl;
        AddTransition(Transition.AchieveToDrawIn, StateID.FishingFinish);
        hook = GameObject.FindWithTag("Player").GetComponentInChildren<Hook>();
        hookFish = hook.GetComponent<HookFish>();
    }

    private void Start()
    {
        model = ctrl.model;
        saveData = model.mysaveData;
        systemConfig = model.mySystemConfig;
        speed = (float)systemConfig.RecycleHookSpeed;
        UpdatehookRunDistanceAbout();
    }

    public void UpdatehookRunDistanceAbout()
    {
        int level = saveData.WaterDepthLevel;
        hookRunDistance = (float)systemConfig.DepthPerLevel *level;
        hookRunTime = hookRunDistance / speed;
    }

    public override void DoBeforeEntering()
    {
        ctrl.view.PopPsPlay();
        isStartHook = true;
        hookFish.HookFishScriptOpen();
        CanCatchFishCount = saveData.NetSize;
      
    }

    public void ChangeToFishingFinishState()
    {
        if (CanCatchFishCount <= 0)
        {
            fsm.PerformTransition(Transition.AchieveToDrawIn);
        }
        else if (isStartHook == false)
        {
            fsm.PerformTransition(Transition.AchieveToDrawIn);
        }
    }

    public override void DoBeforeLeaving()
    {
        hookFish.HookFishScriptClose();
        CanCatchFishCount = 0;
        isStartHook = false;
        isStartHookTimer = 0;
        GameManager.instance.isCanControlHook = false;
       
    }

    private void Update()
    {
        if (isStartHook)
        {
            ChangeToFishingFinishState();
            isStartHookTimer += Time.deltaTime;
            hook.transform.Translate(Vector2.up * Time.deltaTime * speed);
            if (isStartHookTimer >= hookRunTime)
            {
                isStartHook = false;
                ChangeToFishingFinishState();
            }
        }
    }
}
