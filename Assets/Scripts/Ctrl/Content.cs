using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//这是鱼库的content;
public class Content : MonoBehaviour
{
    public GameObject FishBox;
    //鱼儿的数目啊
    public const int FishNum = 40;
    public List<FishBox> fishBoxList = new List<FishBox>();

    private Dictionary<string, Dictionary<string, string>> fishConfigDic;
    private Sprite[] fishSprites;
    private Model model;
    //已经发现 鱼 的id;
    private List<int> foundIDList;
   // private List<int> hasBeenGetAwardFishID;

    private void Awake()
    {
        for (int i = 0; i < FishNum; i++)
        {
            GameObject fish = GameObject.Instantiate(FishBox);
            string BehindStr = "";
            int j = i + 1;
            if (j < 10)
            {
                BehindStr = "0" + j;
            }
            else
            {
                BehindStr = j.ToString();
            }

            fish.name = "FishBox" + BehindStr;
            fish.transform.SetParent(this.transform, false);
        }

        foreach (Transform trans in this.transform)
        {
            fishBoxList.Add(trans.GetComponent<FishBox>());
        }
    }

    private void Start()
    {
        model = GameObject.FindWithTag("Model").GetComponent<Model>();
        fishConfigDic = model.fishConfigDic;
        foundIDList = model.mysaveData.hasBeenFoundFishID;
      //  hasBeenGetAwardFishID = model.mysaveData.hasBeenGetAwardFishID;
        fishSprites = GameManager.instance.FishSprites;
        //开始的时候要更新一次，之后每次点击按钮后
        UpdateAllData();
    }

    public void UpdateAllData()
    {
        int i = 0;
        foreach (FishBox fishBox in fishBoxList)
        {
            bool beFound = false;
            //金色的被发现
            bool GoldBeFound = false;
            int fishBoxID = int.Parse(fishBox.name.Substring(7)) + 100;
            int GoldFishBoxID = fishBoxID + 40;
            string name = model.returnLanguageMessageOtherConfig(fishConfigDic, fishBoxID);
            for (int j = 0; j < foundIDList.Count; j++)
            {
                if (fishBoxID == foundIDList[j])
                {
                    beFound = true;
                }
            }
            
            for (int j = 0; j < foundIDList.Count; j++)
            {
                if (GoldFishBoxID == foundIDList[j])
                {
                    GoldBeFound = true;
                }
            }
            //这个的前提是鱼儿已经发现
            fishBox.UpdateData(
                fishSprites[i],
                beFound, GoldBeFound, name);
            i++;
        }
    }
}
