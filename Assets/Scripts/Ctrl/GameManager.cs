using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    [System.Serializable]
    public struct SceneBigSprites
    {
        public smallSprites[] smallSprites;
    }
    [System.Serializable]
    public struct smallSprites
    {
        public Sprite[] sprites;
    }
    public SceneBigSprites sbs;
    // public SceneBigSprites sbsOnly;
    public Sprite[] sceneOnly;
    public Sprite[] shopSprites;
    public Sprite[] FishSprites;
    public static GameManager instance;
    //开始跟随吗
    public bool isStartCameraFllow = false;
    public bool isCanControlHook = false;
    //钩子能捕捉到鱼的 IDLIst，一共有4个LIst，动态生成了。
    public List<int> hookCanCatchFishIDListGeneral;
    public List<int> hookCanCatchFishIDListStar;
    public List<int> hookCanCatchFishIDListTortoise;
    public List<int> hookCanCatchFishIDListShark;
    public List<int> hookCanCatchFishIDListStarAllFish;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 101; i < 181; i++)
        {
            hookCanCatchFishIDListStarAllFish.Add(i);
        }
        //海星的Id 114 125 154 165   鲨鱼的Id 108 137 148 177 //海龟的Id 104 136 144 176
        List<int> GeneralTempList = new List<int>(hookCanCatchFishIDListStarAllFish);

        GeneralTempList.Remove(114);
        GeneralTempList.Remove(125);
        GeneralTempList.Remove(154);
        GeneralTempList.Remove(165);

        GeneralTempList.Remove(108);
        GeneralTempList.Remove(137);
        GeneralTempList.Remove(148);
        GeneralTempList.Remove(177);

        GeneralTempList.Remove(104);
        GeneralTempList.Remove(136);
        GeneralTempList.Remove(144);
        GeneralTempList.Remove(176);

        hookCanCatchFishIDListGeneral = GeneralTempList;
        //end
        List<int> StarTempList = new List<int>(hookCanCatchFishIDListStarAllFish);

        StarTempList.Remove(108);
        StarTempList.Remove(137);
        StarTempList.Remove(148);
        StarTempList.Remove(177);

        StarTempList.Remove(104);
        StarTempList.Remove(136);
        StarTempList.Remove(144);
        StarTempList.Remove(176);

        hookCanCatchFishIDListStar = StarTempList;

        //end

        List<int> SharkTempList = new List<int>(hookCanCatchFishIDListStarAllFish);

        SharkTempList.Remove(114);
        SharkTempList.Remove(125);
        SharkTempList.Remove(154);
        SharkTempList.Remove(165);

        SharkTempList.Remove(104);
        SharkTempList.Remove(136);
        SharkTempList.Remove(144);
        SharkTempList.Remove(176);

        hookCanCatchFishIDListShark = SharkTempList;

        //end

        List<int> TortoiseTempList = new List<int>(hookCanCatchFishIDListStarAllFish);

        TortoiseTempList.Remove(114);
        TortoiseTempList.Remove(125);
        TortoiseTempList.Remove(154);
        TortoiseTempList.Remove(165);

        TortoiseTempList.Remove(108);
        TortoiseTempList.Remove(137);
        TortoiseTempList.Remove(148);
        TortoiseTempList.Remove(177);

        hookCanCatchFishIDListTortoise = TortoiseTempList;
    }
}
