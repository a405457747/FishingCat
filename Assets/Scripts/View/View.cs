using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;

public class View : MonoBehaviour
{
    public GameObject NoADS;
    public GameObject MoreGame;
    public GameObject MissEffect;
    public Font font2;
    public Font font;
    public GameObject MyMoneyPanel_Offline;
    public Text MyMoneyPanel_Offline_Money_num;
    public ParticleSystem PopPs;
    public Text Text_Fish_message;
    public RectTransform Text_Fish_message_RectTransform;
    public Text Text_noAds;
    public Text MoneyPanel_Title_Offline;
    public Text MoneyPanel_Collect_Offline;
    public Text MoneyPanel_Double_Offline;
    public Text MoneyPanel_Title;
    public Text MoneyPanel_Collect;
    public Text MoneyPanel_Double;
    public Text Text_new;
    //鱼钩击中的特效哦
    public GameObject HitEffect;
    //start
    public Text Libray_Title_name_text;
    public Text Shop_name_text;
    public Text buffButton_Text_Name1;
    public Text buffButton_Text_Name2;
    public Text buffButton_Text_Name3;
    public Text buffButton_Text_Name4;
    public Text RecoverBuy_txt;
    public Text PlayTips_txt;
    //end
    //钩子的sr;
    public SpriteRenderer HookSR;
    public Sprite voiceClose;
    public Sprite voiceOpen;
    //所有的场景做场景管理
    public GameObject[] Scenes;
    //从左往右对应buff按钮
    public Button buffBtn1;
    public Button buffBtn2;
    public Button buffBtn3;
    public Button buffBtn4;
    //对应buff按钮的当前价格
    [HideInInspector]
    public int buffPrice1 = 0;
    [HideInInspector]
    public int buffPrice2 = 0;
    [HideInInspector]
    public int buffPrice3 = 0;
    [HideInInspector]
    public int buffPrice4 = 0;
    //当前每分钟收益啊
    [HideInInspector]
    public int currenPerMinMoney;
    public Content content;

    private Image CompanyName;
    private Image GameNameImg;
    private GameObject LoadPanel;
    private GameObject MenuPanel;
    private GameObject ShowMyMoneyPanel;
    private GameObject Image_fishNetCount_Panel;
    private RectTransform Panel_Fish_libray;
    private RectTransform Panel_Shop;
    private Text moneyText;
    //已经网了多少条鱼啊
    private Text Text_NetNum;
    private Image Image_voice;
    private int currentSceneId;
    private Text ShowMyMoneyPanel_Text_money_num;
    private Model model;
    private saveData mysaveData;
    private SystemConfig systemConfig;
    //销毁鱼的钱的条目
    private RectTransform Text_Score;
    private Text Text_Score_text;
    private RectTransform Fish_repeat_change;
    private float fishInterval;
    private WaitLoadState waitLoadState;
   // private Ctrl ctrl;

    private void Awake()
    {
       //print(Application.persistentDataPath);
        MenuPanel = transform.Find("Canvas/MenuPanel").gameObject;
        CompanyName = transform.Find("Canvas/LoadPanel/CompanyName").GetComponent<Image>();
        GameNameImg = transform.Find("Canvas/LoadPanel/GameNameImg").GetComponent<Image>();
        LoadPanel = transform.Find("Canvas/LoadPanel").gameObject;
        ShowMyMoneyPanel = transform.Find("Canvas/ShowMyMoneyPanel").gameObject;
        ShowMyMoneyPanel_Text_money_num = transform.Find("Canvas/ShowMyMoneyPanel/CenterBar/Text_money_num").GetComponent<Text>();
        Image_fishNetCount_Panel = transform.Find("Canvas/Image_fishNetCount_Panel").gameObject;
        Panel_Fish_libray = transform.Find("Canvas/Panel_Fish_libray") as RectTransform;
        Panel_Shop = transform.Find("Canvas/Panel_Shop") as RectTransform;
        moneyText = transform.Find("Canvas/MenuPanel/TopBar/Text_Coin_num").GetComponent<Text>();
        Image_voice = transform.Find("Canvas/MenuPanel/TopBar/Image_voice").GetComponent<Image>();
        Text_NetNum = transform.Find("Canvas/Image_fishNetCount_Panel/Text_NetNum").GetComponent<Text>();
        Text_Score = transform.Find("Canvas/Text_Score") as RectTransform;
        Text_Score_text = Text_Score.GetComponent<Text>();
        Fish_repeat_change = transform.Find("Canvas/Fish_repeat_change") as RectTransform;

    }

    private void Start()
    {
        model = GameObject.FindWithTag("Model").GetComponent<Model>();
      // ctrl = GameObject.FindWithTag("Ctrl").GetComponent<Ctrl>();

        systemConfig = model.mySystemConfig;
        fishInterval = (float)systemConfig.GetFishInterval;
        mysaveData = model.mysaveData;
        buffPrice1 = mysaveData.buffPrice1;
        buffPrice2 = mysaveData.buffPrice2;
        buffPrice3 = mysaveData.buffPrice3;
        buffPrice4 = mysaveData.buffPrice4;

        WaitLoadState.languageConfigEventhandler += UpdateAllLanguageKey;

    }
    //所有的
    public void UpdateAllLanguageKey()
    {
        PlayTips_txt.text = model.returnLanguageMessage(307);
        RecoverBuy_txt.text = model.returnLanguageMessage(312);
        Text_noAds.text = model.returnLanguageMessage(316);
        buffButton_Text_Name1.text = model.returnLanguageMessage(301);
        buffButton_Text_Name2.text = model.returnLanguageMessage(303);
        buffButton_Text_Name3.text = model.returnLanguageMessage(305);
        buffButton_Text_Name4.text = model.returnLanguageMessage(306);
        if (model.mysaveData.LanguageKey == 2)
        {
            buffButton_Text_Name1.font = font;
            buffButton_Text_Name2.font = font;
            buffButton_Text_Name3.font = font;
            buffButton_Text_Name4.font = font;
        }
        else
        {
            buffButton_Text_Name1.font = font2;
            buffButton_Text_Name2.font = font2;
            buffButton_Text_Name3.font = font2;
            buffButton_Text_Name4.font = font2;
        }

        Shop_name_text.text = model.returnLanguageMessage(309);
        Libray_Title_name_text.text = model.returnLanguageMessage(308);
        //start离线收益界面更新
        MoneyPanel_Title_Offline.text = model.returnLanguageMessage(306);
        MoneyPanel_Collect_Offline.text = model.returnLanguageMessage(319);
        MoneyPanel_Double_Offline.text = model.returnLanguageMessage(320);
        //end
        UpdateMoneyText(mysaveData.money, mysaveData.FishFoodLevel, mysaveData.NetSize, mysaveData.WaterDepthLevel, mysaveData.OfflineEarningsLevel);
   
    }

    public void UpdateNoADS(bool isVip)
    {

        if (mysaveData.WaterDepthLevel>=4)
        {
            MoreGame.SetActive(true);
        }
        else
        {
            MoreGame.SetActive(false);
        }
        return;
        if (isVip)
        {
            NoADS.SetActive(false);
        }else
        {
            NoADS.SetActive(true);
        }
    }

    //LoadPanel的界面动画
    public void LoadPanelAnimation()
    {
        CompanyName.gameObject.SetActive(true);
        var s = DOTween.Sequence();
        s.Append(CompanyName.DOFade(1, 1.2f));
        s.Append(CompanyName.DOFade(0, 1.2f));
        s.Insert(1f, CompanyName.transform.DOScale(1.2f, 1.2f));
        s.InsertCallback(2.4f,
            delegate ()
            {
                CompanyName.gameObject.SetActive(false);
                //开启下一个游戏Logo的动画
                GameNameImgAnimation();
            }
            );
    }

    private void GameNameImgAnimation()
    {
        GameNameImg.gameObject.SetActive(true);
        var s = DOTween.Sequence();
        s.Append(GameNameImg.DOFade(1, 1.2f));
        s.Append(GameNameImg.DOFade(0, 1.2f));
        s.Insert(1f, GameNameImg.transform.DOScale(1.2f, 1.2f));
        s.InsertCallback(2.4f,
            delegate ()
            {
                GameNameImg.gameObject.SetActive(false);
                LoadPanelFade();
            }
            );
    }

    //loadPanel消失的动画
    private void LoadPanelFade()
    {
        LoadPanel.GetComponent<Image>().DOFade(0, 1.2f).OnComplete(
            delegate ()
            {
                LoadPanel.gameObject.SetActive(false);

                //播放背景音乐如果能播放的话
                if (mysaveData.isMute == false)
                    AudioManager.Instance.PlayBg("BG2");
            }
            );
    }

    public void ShowMenuPanel()
    {
        MenuPanel.SetActive(true);
    }

    public void HideMenuPanel()
    {
        MenuPanel.SetActive(false);
    }

    public void ShowShowMyMoneyPanel(int waveMoneny)
    {
        MoneyPanel_Title.text = model.returnLanguageMessage(322);
        MoneyPanel_Collect.text = model.returnLanguageMessage(319);
        MoneyPanel_Double.text = model.returnLanguageMessage(320);
        //更新下字体哦这里
        ShowMyMoneyPanel_Text_money_num.text = waveMoneny.ToString();
        ShowMyMoneyPanel.SetActive(true);
    }

    public void HideShowMyMoneyPanel()
    {
        ShowMyMoneyPanel.SetActive(false);
    }

    public void ShowImage_fishNetCount_Panel()
    {
        Image_fishNetCount_Panel.SetActive(true);
    }

    public void HideImage_fishNetCount_Panel()
    {
        Image_fishNetCount_Panel.SetActive(false);
    }

    public void Panel_Fish_librayShow()
    {
        Panel_Fish_libray.gameObject.SetActive(true);
        Panel_Fish_libray.DOAnchorPosY(0, 0.8f);
    }

    public void Panel_Fish_librayHide()
    {
        Panel_Fish_libray.DOAnchorPosY(1500, 0.8f).OnComplete(
            delegate ()
            {
                Panel_Fish_libray.gameObject.SetActive(false);
            }
            );
    }

    //Panel_Shop
    public void Panel_ShopShow()
    {
        Panel_Shop.gameObject.SetActive(true);
        Panel_Shop.DOAnchorPosY(0, 0.8f);
    }

    public void Panel_ShopHide()
    {
        Panel_Shop.DOAnchorPosY(1700, 0.8f).OnComplete(
            delegate ()
            {
                Panel_Shop.gameObject.SetActive(false);
            }
            );
    }
    //更新自己的金钱啊  顺便更新4个按钮状态
    public void UpdateMoneyText(int money, int buff1Level, int buff2Level, int buff3Level, int buff4Level)
    {
        moneyText.text = money.ToString();
        UpdateBuffBtn2(money, buff2Level);
        UpdateBuffBtn1(money, buff1Level);
        UpdateBuffBtn3(money, buff3Level);
        UpdateBuffBtn4(money, buff4Level);
    }

    //更改音效图标哦
    public void Image_voiceChange(bool isMute)
    {
        if (isMute)
        {
            Image_voice.sprite = voiceClose;
        }
        else
        {
            Image_voice.sprite = voiceOpen;
        }
    }

    public void UpdateText_NetNum(int NetFishCount)
    {
        Text_NetNum.text = NetFishCount.ToString();
    }

    //更新场景的方法
    public void UpdateScenes()
    {
        int length = Calculate.ReturnCanShowScene(mysaveData.WaterDepthLevel);
        for (int i = 0; i < Scenes.Length; i++)
        {
            if (i < length)
            {
                Scenes[i].SetActive(true);
            }
            else
            {
                Scenes[i].SetActive(false);
            }
        }
    }

    public void CreateSceneFish()
    {
        int randomSceneId = Random.Range(0, Scenes.Length);
       // bool hasBeenAchieveMaxDepthLevel = mysaveData.hasBeenAchieveMaxDepthLevel;
        int length = Calculate.ReturnCanShowScene(mysaveData.WaterDepthLevel);
        for (int i = 0; i < Scenes.Length; i++)
        {
            if (i < length)
            {
                scene scene = Scenes[i].GetComponent<scene>();
                scene.sceneCreateFish();
                if (i == 0)
                {
                    scene.ChangeBGSprites(randomSceneId);
                  
                }else
                {
                    scene.ChangeBGSpritesOnlyOne(randomSceneId);
                }
            }
            else
            {
            }
        }

    }

    public void DestroySceneFish()
    {
        int length = Calculate.ReturnCanShowScene(mysaveData.WaterDepthLevel);
        for (int i = 0; i < Scenes.Length; i++)
        {
            if (i < length)
            {
                Scenes[i].GetComponent<scene>().sceneDistoryFish();
            }
            else
            {
            }
        }
    }

    //从左向右对应4个功能按钮。
    public void UpdateBuffBtn1(int currentMoney, int currentLevel)
    {
        bool isMax = mysaveData.FishFoodLevel >= systemConfig.MaxFishFoodCount;
        Text Text_Price = buffBtn1.transform.Find("CoinBottomBar/Text_Price").GetComponent<Text>();
        Text Text_Size = buffBtn1.transform.Find("Text_Size").GetComponent<Text>();
        if (isMax)
        {
            Text_Price.text = model.returnLanguageMessage(324);
          //  Text_Size.text = "Lv." + currentLevel.ToString();
            buffBtn1.interactable = false;
        }
        else
        {
            Text_Price.text = buffPrice1.ToString();
          

            if (currentMoney < buffPrice1)
            {
                buffBtn1.interactable = false;
            }
            else
            {
                buffBtn1.interactable = true;
            }
        }
        Text_Size.text = "Lv." + currentLevel.ToString();
    }

    public void UpdateBuffBtn2(int currentMoney, int currentLevel)
    {
        bool isMax = mysaveData.NetSize >= systemConfig.MaxFishNetCount;
        Text Text_Price = buffBtn2.transform.Find("CoinBottomBar/Text_Price").GetComponent<Text>();
        Text Text_Size = buffBtn2.transform.Find("Text_Size").GetComponent<Text>();
        if (isMax)
        {
            Text_Price.text = model.returnLanguageMessage(324);
          //  Text_Size.text = "Lv." + currentLevel.ToString();
            buffBtn2.interactable = false;
        }
        else
        {

            Text_Price.text = buffPrice2.ToString();
          

            if (currentMoney < buffPrice2)
            {
                buffBtn2.interactable = false;
            }
            else
            {
                buffBtn2.interactable = true;
            }
        }
        Text_Size.text = "Lv." + currentLevel.ToString();
    }

    public void UpdateBuffBtn3(int currentMoney, int currentLevel)
    {//需要的level不是真正的level

        bool isAchieveMaxDepth = mysaveData.hasBeenAchieveMaxDepthLevel;
        Text Text_Price = buffBtn3.transform.Find("CoinBottomBar/Text_Price").GetComponent<Text>();
        Text Text_Size = buffBtn3.transform.Find("Text_Size").GetComponent<Text>();
        if (isAchieveMaxDepth)
        {
            Text_Price.text = model.returnLanguageMessage(324);
            buffBtn3.interactable = false;
        }
        else
        {

            Text_Price.text = buffPrice3.ToString();
            if (currentMoney < buffPrice3)
            {
                buffBtn3.interactable = false;
            }
            else
            {
                buffBtn3.interactable = true;
            }
        }
        Text_Size.text = currentLevel * 10 + "M";
    }

    public void UpdateBuffBtn4(int currentMoney, int currentLevel)
    {
        bool isMax = mysaveData.OfflineEarningsLevel >= systemConfig.MaxOffLineEarningsLevel;
        Text Text_Price = buffBtn4.transform.Find("CoinBottomBar/Text_Price").GetComponent<Text>();
        Text Text_Size = buffBtn4.transform.Find("Text_Size").GetComponent<Text>();
        if (isMax)
        {
          // currenPerMinMoney = currentLevel * systemConfig.OfflineEarningsPerLevel + 2;
            Text_Price.text = model.returnLanguageMessage(324);
            //Text_Size.text = currenPerMinMoney.ToString() + "/MIN";
            buffBtn4.interactable = false;
        }
        else
        {
           
            Text_Price.text = buffPrice4.ToString();
           

            if (currentMoney < buffPrice4)
            {
                buffBtn4.interactable = false;
            }
            else
            {
                buffBtn4.interactable = true;
            }
        }
        currenPerMinMoney = currentLevel * systemConfig.OfflineEarningsPerLevel + 2;
        Text_Size.text = currenPerMinMoney.ToString() + "/MIN";
    }
    //更新new;这个方法被制作人弃用了
    /*
    public void UpdateIsShowNewText()
    {
        List<int> hasBeenFoundFishID = mysaveData.hasBeenFoundFishID;
        List<int> hasBeenGetAwardFishID = mysaveData.hasBeenGetAwardFishID;

        List<int> temp = new List<int>(hasBeenFoundFishID);
        List<int> valueTemp = new List<int>();
        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i] > 140)
            {
                valueTemp.Add(temp[i]);
            }
        }
        for (int j = 0; j < valueTemp.Count; j++)
        {
            temp.Remove(valueTemp[j]);
        }
        //这里来更新是否有外面的New;
        Text_new.gameObject.SetActive(temp.Count > hasBeenGetAwardFishID.Count);
    }
    */

    public void Show_Text_new()
    {
        Text_new.gameObject.SetActive(true);
    }

    public void Hide_Text_new()
    {
        Text_new.gameObject.SetActive(false);
    }

    public void UpdateFishLibrayData()
    {
        content.UpdateAllData();
        // UpdateIsShowNewText();
    }
    //这个就是那个不断改变的分数ui 不断堆叠 这个是鱼的稀有信息呢
    public void Text_Fish_message_Repeat(string fisheMessage)
    {
        GameObject go = Text_Fish_message.gameObject;
        go.SetActive(true);
        Text_Fish_message.text = fisheMessage;
        var s = DOTween.Sequence();
        s.Append(Text_Fish_message_RectTransform.DOAnchorPosY(0, fishInterval));
        s.InsertCallback(fishInterval,
            delegate ()
            {
                go.SetActive(false);
                Text_Fish_message_RectTransform.DOAnchorPosY(-230, 0.00001f);
            }
            );
    }

    //这个就是那个不断改变的分数ui 不断堆叠
    public void Text_ScoreChange(int score)
    {
        GameObject go = Text_Score.gameObject;
        go.SetActive(true);
        Image image = go.GetComponentInChildren<Image>();
        Text_Score_text.text = score.ToString();
        var s = DOTween.Sequence();
        s.Append(Text_Score.DOAnchorPosY(50, fishInterval));
        s.Insert(0f, Text_Score_text.DOFade(0, fishInterval));
        s.Insert(0f, image.DOFade(0, fishInterval));
        s.InsertCallback(fishInterval,
            delegate ()
            {
                go.SetActive(false);
                Text_Score.DOAnchorPosY(-100, 0.00001f);
                Text_Score_text.DOFade(1, 0.00001f);
                image.DOFade(1, 0.00001f);
            }
            );
    }
    //这个就是那个不断改变的不断堆叠 不过这个是图片哦 Fish_repeat_change

    public void Fish_repeat_change_Repeat(Sprite FishSprite)
    {
        GameObject fish_repeat = Fish_repeat_change.gameObject;
        fish_repeat.SetActive(true);
        Image image = Fish_repeat_change.GetComponent<Image>();
        image.sprite = FishSprite;
        var s = DOTween.Sequence();
        s.Append(Fish_repeat_change.DOAnchorPosY(0, fishInterval));
        s.Insert(0f, image.DOFade(0, fishInterval));
        s.InsertCallback(fishInterval,
            delegate ()
            {
                fish_repeat.SetActive(false);
                Fish_repeat_change.DOAnchorPosY(-200, 0.00001f);
                image.DOFade(1, 0.00001f);
            }
            );
    }

    //更新钩子了; 第一次在ctrl里面更新，点击了更换按钮更新一次。
    public void UpdateHook(int HookCurrentId)
    {
        int index = HookCurrentId - 201;
        HookSR.sprite = GameManager.instance.shopSprites[index];
    }
    //missEffect  MissEffect
    public void ShowMissEffect(string TheRareFishMessage, Vector3 fishCollisionPos)
    {
        GameObject effect = GameObject.Instantiate(MissEffect, fishCollisionPos, Quaternion.identity) as GameObject;
        Text text = effect.transform.Find("Canvas/Text_Miss").GetComponent<Text>();
        RectTransform textRectTrans = text.GetComponent<RectTransform>();
        text.text = TheRareFishMessage;
        var s = DOTween.Sequence();
        //放大
        s.Append(textRectTrans.DOScale(new Vector3(0.02f, 0.02f, 0.02f), 0.3f));
        //缩小
        s.Append(textRectTrans.DOScale(new Vector3(0.015f, 0.015f, 0.015f), 0.3f));
        //fade
        s.Append(text.DOFade(0, 0.3f));
        s.InsertCallback(1f,
            delegate ()
            {
                Destroy(effect);
            }
            );
    }

    //HitEffect
    public void ShowHitEffect(int fishPrice, Vector3 fishCollisionPos)
    {
        GameObject effect = GameObject.Instantiate(HitEffect, fishCollisionPos, Quaternion.identity) as GameObject;
        Text text = effect.transform.Find("Canvas/Text").GetComponent<Text>();
        Image image = effect.transform.Find("Canvas/Text/Image").GetComponent<Image>();
      //  Text messageText = effect.transform.Find("Canvas/Text_message").GetComponent<Text>();
        RectTransform textRectTrans = text.GetComponent<RectTransform>();
      //  RectTransform messageTextTextRectTrans = messageText.GetComponent<RectTransform>();
      //  messageText.text = TheRareFishMessage;
        text.text = fishPrice.ToString();
        var s = DOTween.Sequence();

        //放大
        s.Append(textRectTrans.DOScale(new Vector3(0.02f, 0.02f, 0.02f), 0.3f));
        //缩小
        s.Append(textRectTrans.DOScale(new Vector3(0.015f, 0.015f, 0.015f), 0.3f));
        //fade
        s.Append(text.DOFade(0, 0.3f));
        s.Insert(0.6f, image.DOFade(0, 0.3f));
      //  s.Insert(0f, messageTextTextRectTrans.DOScale(new Vector3(0.02f, 0.02f, 0.02f), 0.3f));
       // s.Insert(0.3f, messageTextTextRectTrans.DOScale(new Vector3(0.015f, 0.015f, 0.015f), 0.3f));
       // s.Insert(0.6f, messageText.DOFade(0, 0.3f));

        s.InsertCallback(1f,
            delegate ()
            {
                Destroy(effect);
            }
            );
    }

    public void PopPsPlay()
    {
        AudioManager.Instance.BgVolume = 0.5f;
            PopPs.Play();

    }
    public void PopPsStop()
    {
        AudioManager.Instance.BgVolume = 1f;

            PopPs.Stop();

    }

    public void ShowMoneyPanel_Offline(int totalMoney)
    {
        MyMoneyPanel_Offline.SetActive(true);
        MyMoneyPanel_Offline_Money_num.text = totalMoney.ToString();
    }

    public void HideMoneyPanel_Offline()
    {
        MyMoneyPanel_Offline.SetActive(false);
    }
}
