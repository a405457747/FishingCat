using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//各种计算类
public static class Calculate
{

    public static bool CanCreateFish(float fishProbability)
    {
        if (Random.value <= fishProbability)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static int GetDir()
    {
        bool mybool = CanCreateFish(0.5f);
        if (mybool)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public static bool isHaveTheID(List<int> idList, int currentId)
    {
        bool temp = false;
        foreach (int id in idList)
        {
            if (id == currentId)
            {
                temp = true;
                return temp;
            }
        }
        return temp;
    }
    //计算钩子可不可以抓住鱼儿传入当前钩子ID
    public static bool HookCanFish(int hookID, int fishID)
    {
        GameManager manager = GameManager.instance;
        switch (hookID)
        {
            case 201:
                return (manager.hookCanCatchFishIDListGeneral.Contains(fishID));
            case 202:
                return (manager.hookCanCatchFishIDListStar.Contains(fishID));
            case 203:
                return (manager.hookCanCatchFishIDListTortoise.Contains(fishID));
            case 204:
                return (manager.hookCanCatchFishIDListShark.Contains(fishID));
            case 205:
                return (manager.hookCanCatchFishIDListStarAllFish.Contains(fishID));
        }
        return false;
    }
    //根据水深显示要显示的场景的个数
    public static int ReturnCanShowScene(int waterLevel)
    {
        int highestCount=0;
        if (waterLevel <= 10&&waterLevel>=1)
        {
            highestCount = 1;
        }
        else if (waterLevel <= 20 && waterLevel >10)
        {
            highestCount = 2;
        }
        else if (waterLevel <= 30 && waterLevel > 20)
        {
            highestCount = 3;
        }
        else if (waterLevel <= 40 && waterLevel > 30)
        {
            highestCount = 4;
        }
        else if (waterLevel <= 50 && waterLevel > 40)
        {
            highestCount = 5;
        }
        return highestCount;
    }


}
