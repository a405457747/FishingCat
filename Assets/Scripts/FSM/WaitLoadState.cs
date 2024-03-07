using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaitLoadState : FSMState
{
    public ShopContent shopContent;
    public static event Action languageConfigEventhandler;
    public saveData saveData;

    private SystemConfig systemConfig;
    private Hook hook;
    private FishingCtrlState fishingCtrlState;


    private void Awake()
    {
        stateID = StateID.WaitLoad;
        AddTransition(Transition.ClickStartArea, StateID.GameStart);
    }

    private void Start()
    {
        saveData = ctrl.model.mysaveData;
        systemConfig = ctrl.model.mySystemConfig;
        hook = ctrl.transform.Find("Player/Hook").GetComponent<Hook>();
        fishingCtrlState = ctrl.transform.Find("State").GetComponent<FishingCtrlState>();
    }

    public static void PlayLanguageConfigEventhandler()
    {
        if (languageConfigEventhandler != null) languageConfigEventhandler();
    }

    public override void DoBeforeEntering()
    {
    }

    public override void DoBeforeLeaving()
    {
        ctrl.view.HideMenuPanel();
    }

    public void ClickStartArea()
    {
        fsm.PerformTransition(Transition.ClickStartArea);
    }

    public void ClickOpenFishLibrary()
    {
        ctrl.view.UpdateFishLibrayData();
        ctrl.view.Hide_Text_new();
        AudioManager.Instance.PlayEffect("Cursor_002");
        ctrl.view.Panel_Fish_librayShow();
    }

    public void ClickCloseFishLibrary()
    {
        AudioManager.Instance.PlayEffect("Exit2");
        ctrl.view.Panel_Fish_librayHide();
    }

    public void ClickOpenShop()
    {
        AudioManager.Instance.PlayEffect("Cursor_002");
        shopContent.UpdateAllData();
        ctrl.view.Panel_ShopShow();
    }

    public void ClickCloseShop()
    {
        AudioManager.Instance.PlayEffect("Exit2");
        //更新钩子
        ctrl.view.UpdateHook(saveData.currentHookID);
        //关闭商店更新金币UI
        ctrl.view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
        ctrl.view.Panel_ShopHide();
    }

    //点击音效按钮
    public void ClickVoiceButton()
    {
        bool isMute = !saveData.isMute;
        ctrl.view.Image_voiceChange(isMute);
        if (isMute) AudioManager.Instance.StopBg();
        if (!isMute) AudioManager.Instance.PlayBg("BG2");
        saveData.isMute = isMute;
        ctrl.model.SaveMyData();
    }
    //这个
    //加美味的鱼食品
    public void ClickAddBuffFishFood()
    {
        bool isMax = saveData.FishFoodLevel >= systemConfig.MaxFishFoodCount;
        if (isMax)
        {
            //更新金钱的ui即可;
            ctrl.view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
        }
        else
        {
            AudioManager.Instance.PlayEffect("BuffBtnClick");
            //加成功要保存哦;
            saveData.money -= ctrl.view.buffPrice1;
            saveData.FishFoodLevel++;

            //更新金钱的ui即可;
            //公式
            ctrl.view.buffPrice1 = ctrl.view.buffPrice1 + ctrl.view.buffPrice1 / 4;
            saveData.buffPrice1 = ctrl.view.buffPrice1;
            ctrl.model.SaveMyData();
            ctrl.view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
        }
    }
    //这个
    //加网数目
    public void ClickAddBuffAddNetSize()
    {
        bool isMax = saveData.NetSize >= systemConfig.MaxFishNetCount;
        if (isMax)
        {
            ctrl.view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
        }
        else
        {
            AudioManager.Instance.PlayEffect("BuffBtnClick");
            //加成功要保存哦;
            saveData.money -= ctrl.view.buffPrice2;
            saveData.NetSize++;
            //更新金钱的ui即可;
            ctrl.view.buffPrice2 = ctrl.view.buffPrice2 + ctrl.view.buffPrice2 / 4;
            saveData.buffPrice2 = ctrl.view.buffPrice2;
            ctrl.model.SaveMyData();

            ctrl.view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
        }
    }

    //加深度
    public void ClickAddBuffAddDepthSize()
    {
        AudioManager.Instance.PlayEffect("BuffBtnClick");
        //加成功要保存哦;
        if (saveData.hasBeenAchieveMaxDepthLevel == false)
        {
            saveData.money -= ctrl.view.buffPrice3;
        }
        saveData.WaterDepthLevel++;
        if (saveData.WaterDepthLevel == systemConfig.MaxDepthLevel)
        {
            saveData.hasBeenAchieveMaxDepthLevel = true;
        }
        ctrl.view.buffPrice3 = ctrl.view.buffPrice3 + ctrl.view.buffPrice3 / 4;
        saveData.buffPrice3 = ctrl.view.buffPrice3;
        ctrl.model.SaveMyData();
        saveData.currentSceneID = saveData.WaterDepthLevel / 10;

        //更新背景
        ctrl.view.UpdateScenes();
        //更新两个下潜的距离哦;
        hook.UpdateSinkingDistanceAbout();
        fishingCtrlState.UpdatehookRunDistanceAbout();
        //更新金钱的ui即可;
        ctrl.view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
    }

    //加离线收益
    public void ClickAddBuffAddOffLineEarning()
    {
        bool isMax = saveData.OfflineEarningsLevel >= systemConfig.MaxOffLineEarningsLevel;
        if (isMax)
        {
            ctrl.view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
        }
        else
        {
            AudioManager.Instance.PlayEffect("BuffBtnClick");
            //加成功要保存哦;
            saveData.money -= ctrl.view.buffPrice4;
            saveData.OfflineEarningsLevel++;
            //更新金钱的ui即可;
            ctrl.view.buffPrice4 = ctrl.view.buffPrice4 + ctrl.view.buffPrice4 / 4;
            saveData.buffPrice4 = ctrl.view.buffPrice4;
            ctrl.model.SaveMyData();

            ctrl.view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
        }
    }

    public void ClickMoreGame()
    {
        //Application.OpenURL("https://itunes.apple.com/cn/app/id1192211309");


#if UNITY_ANDROID
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.funcoregames.thebomb");
#elif UNITY_IPHONE
          Application.OpenURL("https://itunes.apple.com/cn/app/id1170927710");
#endif

    }

    public void RemoveADS()
    {
        saveData.isVip = true;
        ctrl.model.SaveMyData();
        ctrl.view.UpdateNoADS(saveData.isVip);
    }
}
