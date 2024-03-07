using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ctrl : MonoBehaviour
{
    [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public AudioManager audioManager;
    [HideInInspector]
    public CameraCtrl cameraCtrl;

    private FSMSystem fsm;
    private saveData saveData;
    private int totalMoney = 0;

    private void Awake()
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        gameManager = GetComponent<GameManager>();
        cameraCtrl = GetComponent<CameraCtrl>();
        audioManager = GetComponent<AudioManager>();
    }
    private void Start()
    {
        #region 游戏初始化（除了加载文本文件）
        MakeFSM();
        saveData = model.mysaveData; 
        //一开始的时候是需要更新下UI的
        UpdateUI();
        #endregion
    }

    public void UpdateUI()
    {
        view.LoadPanelAnimation();
        //更新金钱
        view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
        //更新静音图标
        view.Image_voiceChange(saveData.isMute);
        //更新场景哦

         view.UpdateScenes();

        //更新鱼钩
        view.UpdateHook(saveData.currentHookID);
        //离线显示
        GetOfflineEarnings();
        //更新语言版本要放在最后哦
        view.UpdateAllLanguageKey();
        view.UpdateNoADS(saveData.isVip);
    }

    public bool GetOfflineEarnings()
    {
        bool needShow = false;
        int beforeTime = saveData.LastMinuteTime;
        int nowTime = GetNowTime();
        int result = nowTime - beforeTime;
        if (result > 0 && saveData.isFristGame == false)
        {
            totalMoney = view.currenPerMinMoney * result;
            needShow = true;
            view.ShowMoneyPanel_Offline(totalMoney);
        }
        else
        {
            needShow = false;
        }
        return needShow;
    }

    //一倍收益
    public void ClickCollectMoney()
    {
        EarningsFunction();
        //  ExampleScript.Instance.ShowAd();
        if (saveData.isVip == false)
            GeogleADManager.Instance.ShowInterstitial();
    }

    //看广告双倍收益
    public void ClickCollectMoneyDoubleWatchAds()
    {
        EarningsFunction(2);
        // ExampleScript.Instance.ShowAd();

        //显示广告了
        if (saveData.isVip == false)
            GeogleADManager.Instance.ShowRewardBasedVideo();
    }

    public void EarningsFunction(int multiple = 1)
    {
        int Money = totalMoney * multiple;
        view.HideMoneyPanel_Offline();
        //这里加分了
        saveData.money += Money;
        SaveCurrentTime();
        model.SaveMyData();
        view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
        //加分结束了
        //播放声音
        AudioManager.Instance.PlayEffect("getCoin");
    }


    void MakeFSM()
    {
        fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
        foreach (FSMState state in states)
        {
            fsm.AddState(state, this);
        }
        WaitLoadState s = GetComponentInChildren<WaitLoadState>();
        fsm.SetCurrentState(s);
    }

    public int GetNowTime()
    {
        TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
        int ret = Convert.ToInt32(ts.TotalMinutes);
        return ret;
    }

    public void SaveCurrentTime()
    {
        if(saveData.isFristGame==true) saveData.isFristGame = false;
        int ret = GetNowTime();
        saveData.LastMinuteTime = ret;
    }

}
