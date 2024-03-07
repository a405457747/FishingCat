using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Pool
{
    public int MinNum;
    public int MaxNum;
    public List<int> FishIDs;
}

[Serializable]
public class Scene
{
    public int fishTypeCount;
    public List<Pool> Pools;
}

[Serializable]
public class SystemConfig
{
    public double DropHookSpeed;
    public double RecycleHookSpeed;
    public double QuickRecycleSpeed;
    public int OfflineEarningsPerLevel;
    public double DepthPerLevel;
    public double FishSpeed;
    public List<Scene> Scenes;
    //多次深度等级换一次场景暂停是5后期调整
    public int DepthPerLevelCountPerScene;
    public int MaxFishFoodCount;
    public int FindFishAward;
    public double GetFishInterval;
    public double HookMoveSpeed;
    public int MaxFishNetCount;
    public int MaxDepthLevel;
    public int MaxOffLineEarningsLevel;
}
