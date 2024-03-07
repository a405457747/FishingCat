using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartState : FSMState
{

    private Hook hook;

    private void Awake()
    {
        stateID = StateID.GameStart;
        AddTransition(Transition.HookToTarget, StateID.FishingCtrl);
        hook = GameObject.FindWithTag("Player").GetComponentInChildren<Hook>();
        hook.completeEventHandler += ChangeStateToFishingCtrl;
    }

    //改变状态。
    public void ChangeStateToFishingCtrl()
    {
        fsm.PerformTransition(Transition.HookToTarget);
    }

    public override void DoBeforeEntering()
    {
        AudioManager.Instance.PlayEffect("seawaterSound");
        hook.ActivateScript();
        HookCtrl hc = hook.GetComponent<HookCtrl>();
        hc.OnScript();
        hc.ChangeBoxColliders((ctrl.model.mysaveData.currentHookID - 201));
        ctrl.view.UpdateText_NetNum(ctrl.model.mysaveData.NetSize);
        ctrl.view.ShowImage_fishNetCount_Panel();
        GameManager.instance.isStartCameraFllow = true;
        GameManager.instance.isCanControlHook = true;
        ctrl.view.CreateSceneFish();
        ////请求
        //ExampleScript.Instance.PreloadAd();

        //ExampleScript.Instance.ShowAd();
        ////GeogleADManager.Instance.RequestBanner();
        ////GeogleADManager.Instance.RequestInterstitial();
        ////GeogleADManager.Instance.RequestRewardBasedVideo();
    }

    public override void DoBeforeLeaving()
    {

    }
}
