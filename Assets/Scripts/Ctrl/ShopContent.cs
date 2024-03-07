using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContent : MonoBehaviour
{

    public GameObject Goods;
    //鱼儿的数目啊
    public const int GoodsNum = 5;

    private List<Goods> GoodsList = new List<Goods>();
    private Dictionary<string, Dictionary<string, string>> shopConfigDic;
    private Sprite[] shopSprites;
    private Model model;
    private saveData saveData;

    private void Awake()
    {
        for (int i = 0; i < GoodsNum; i++)
        {
            GameObject goods = GameObject.Instantiate(Goods);
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
            goods.name = "Goods" + BehindStr;
            goods.transform.SetParent(this.transform, false);
        }

        foreach (Transform trans in this.transform)
        {
            GoodsList.Add(trans.GetComponent<Goods>());
        }
    }

    private void Start()
    {
        model = GameObject.FindWithTag("Model").GetComponent<Model>();
        saveData = model.mysaveData;
        shopConfigDic = model.shopConfigDic;
        shopSprites = GameManager.instance.shopSprites;
        //开始的时候要更新一次，之后点击按钮更新一次
        UpdateAllData();
    }

    public void UpdateAllBuyButtonState()
    {
        foreach (Goods goods in GoodsList)
        {
            goods.UpdateButtonState(saveData.money, goods.Price);
        }
    }

    //当前全中的出现钩，全部更新哦
    public void UpdateAllImage_great()
    {
        int currentHookID = saveData.currentHookID;
        foreach (Goods goods in GoodsList)
        {
            string name = "";
            if (goods.GoodsID != currentHookID)
            {
                name = model.returnLanguageMessage(325);
            }
            else
            {
                name = model.returnLanguageMessage(326);
            }
            goods.UpdateChange_hook_text(name);
        }
    }

    public void UpdateAllData()
    {
        int i = 0;
        foreach (Goods Goods in GoodsList)
        {
            int GoodsID = int.Parse(Goods.name.Substring(5)) + 200;

            string name = model.returnLanguageMessageOtherConfig(shopConfigDic, GoodsID);
            string change_hook_text_name = "";
            if (GoodsID != saveData.currentHookID)
            {
                change_hook_text_name = model.returnLanguageMessage(325);
            }
            else
            {
                change_hook_text_name = model.returnLanguageMessage(326);
            }

            string GoodsIDStr = GoodsID.ToString();
            bool isBeenBuy = false;
            List<int> haveGoodsID = saveData.haveGoodsID;
            for (int j = 0; j < haveGoodsID.Count; j++)
            {
                if (GoodsID == haveGoodsID[j])
                {
                    isBeenBuy = true;
                }
            }

            Goods.UpdateData(shopSprites[i], int.Parse(shopConfigDic["Price"][GoodsIDStr]), isBeenBuy, GoodsID, name, change_hook_text_name, model, saveData);
            i++;
        }
    }
}
