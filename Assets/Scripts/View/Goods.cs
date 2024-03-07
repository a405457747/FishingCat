using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goods : MonoBehaviour
{
    public int GoodsID;
    //[HideInInspector]
    //public GameObject Image_great;
    public int Price;
    public Text Text_yellow_Price_hookName;
    public Text change_hook_text;
    public Text Buy_hook_text;

    //名字和价格
    private Text Text_yellow_name;
    private Image Image_Fish;
    //是否已经购买
    private bool isBeenBuy = false;
    private GameObject buyBtn;
    private GameObject changeBtn;
   // private Dictionary<string, Dictionary<string, string>> shopConfigDic;
    private saveData mysaveData;
    private Model model;
    //private bool isFirstLoad = false;
   // public bool isUse = false;

    private void Awake()
    {
        Text_yellow_name = transform.Find("Image_Buy_btn/Text_yellow_Price").GetComponent<Text>();
        Image_Fish = transform.Find("Image_bg/Image_Fish").GetComponent<Image>();
        buyBtn = transform.Find("Image_Buy_btn").gameObject;
        changeBtn = transform.Find("Image_Change_btn").gameObject;
        //  Image_great = transform.Find("Image_great").gameObject;
        buyBtn.GetComponent<Button>().onClick.AddListener(BuySuccess);
        changeBtn.GetComponent<Button>().onClick.AddListener(changeSuccess);
    }

    public void UpdateText_yellow_Price_hookName(string name)
    {
        Text_yellow_Price_hookName.text = name;
    }
    private void changeSuccess()
    {
        //修过鱼钩子成功了
        //写入数据
        AudioManager.Instance.PlayEffect("SelectHook");
        mysaveData.currentHookID = GoodsID;
        model.SaveMyData();
        ShopContent shopContent = this.transform.parent.GetComponent<ShopContent>();
        shopContent.UpdateAllImage_great();
    }

    private void BuySuccess()
    {
        //购买成功
        mysaveData.money -= this.Price;
        mysaveData.haveGoodsID.Add(GoodsID);
        model.SaveMyData();
        this.isBeenBuy = true;
        UpdateBtn();
        ShopContent shopContent = this.transform.parent.GetComponent<ShopContent>();
        shopContent.UpdateAllBuyButtonState();
    }

    //更新自己的所有数据相关
    public void UpdateData(Sprite sr, int price, bool isBeenBuy, int goodsID, string name, string change_hook_text_name, Model model, saveData saveData)
    {
        this.model = model;
        this.mysaveData = saveData;
        this.GoodsID = goodsID;
        this.Price = price;
        //  this.isUse = isUse;
        this.isBeenBuy = isBeenBuy;
        //其中一个更新自己的名字哦
        UpdateText_yellow_name(price);
        UpdateImage_Fish(sr);
        UpdateBtn();
        UpdateButtonState(mysaveData.money, this.Price);
        UpdateText_yellow_Price_hookName(name);
        UpdateChange_hook_text(change_hook_text_name);
    }
    public void UpdateChange_hook_text(string change_hook_text_name)
    {
        change_hook_text.text = change_hook_text_name;
    }

    //更新购买按钮的状态
    public void UpdateButtonState(int money, int price)
    {
        Button btn = buyBtn.GetComponent<Button>();
        if (money >= price)
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
        }
    }

    private void UpdateText_yellow_name(int price)
    {
        if (isBeenBuy)
        {
            Text_yellow_name.text = "";
        }
        else
        {
            Text_yellow_name.text = price.ToString();
        }
    }

    private void UpdateImage_Fish(Sprite sr)
    {
        Image_Fish.sprite = sr;
    }

    private void UpdateBtn()
    {
        if (isBeenBuy)
        {
            buyBtn.SetActive(false);
            changeBtn.SetActive(true);
        }
        else
        {
            buyBtn.SetActive(true);
            changeBtn.SetActive(false);
        }
    }
}
