using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//保存数据解析映射类
using System;
[Serializable]
public class saveData
{
    public int money;
    public bool isMute;
    public bool isVip;
    public int NetSize;
    public int WaterDepthLevel;
    public int OfflineEarningsLevel;
    public int FishFoodLevel;
    public int LanguageKey;
    public List<int> hasBeenFoundFishID;
    public List<int> haveGoodsID;
    public int currentSceneID;
    public bool isFristGame;
    public int currentHookID;
    public List<int> hasBeenGetAwardFishID;
    public bool hasBeenAchieveMaxDepthLevel;
    public int LastMinuteTime;
    public int buffPrice1;
    public int buffPrice2;
    public int buffPrice3;
    public int buffPrice4;


}
