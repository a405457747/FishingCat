using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FishBox : MonoBehaviour
{
    //public Text newText;
    //已经被发现吗
    public bool isBeenFound = false;
  //  [HideInInspector]
    //public GameObject Image_first_Award;

    private Text Text_yellow_name;
    private Image Image_Fish;
    private GameObject Image_Fish_Gold;
 //   private int FishID;
    //private bool isFirstLoad = false;
   // private Model model;
  //  private Dictionary<string, Dictionary<string, string>> fishConfigDic;
    //是否领取奖励
    //   private bool isCashingPrize = false;
   // private bool isShowNew = false;

    private void Awake()
    {
        Text_yellow_name = transform.Find("Image_bottomBar/Text_yellow_name").GetComponent<Text>();
        Image_Fish = transform.Find("Image_bg/Image_Fish").GetComponent<Image>();
        Image_Fish_Gold = transform.Find("Image_bg/Image_Fish_Gold").gameObject;
        // Image_first_Award = transform.Find("Image_bg/Image_first_Award").gameObject;
    }

    //更新自己的所有数据相关
    public void UpdateData(Sprite sr, bool isBeenFound, bool GoldBeFound, string name)
    {
        this.isBeenFound = isBeenFound;
        //  this.isCashingPrize = isGetAward;
        //   if (isBeenFound == true && isCashingPrize == false) Image_first_Award.SetActive(true);
        UpdateImage_Fish(sr);
        UpdateGoldFish(GoldBeFound);
        UpdateText_yellow_name(name);
    }

    public void UpdateText_yellow_name(string name)
    {
        if (isBeenFound)
        {
            Text_yellow_name.text = name;
        }
        else
        {
            Text_yellow_name.text = "???";
        }
    }

    private void UpdateImage_Fish(Sprite sr)
    {
        Image_Fish.sprite = sr;
        if (isBeenFound)
        {
            Image_Fish.color = Color.white;
        }
        else
        {
            Image_Fish.color = Color.black;
        }
    }
    //更新下自己有没有捕捉到金色的
    private void UpdateGoldFish(bool GoldBeFound)
    {
        if (GoldBeFound)
        {
            Image_Fish_Gold.SetActive(true);
            //更改图片呐
            Image_Fish_Gold.GetComponent<Image>().sprite = GameManager.instance.FishSprites[int.Parse(gameObject.name.Substring(7)) + 39];
        }
    }

    /*
    public void Image_first_AwardBtnOnClick()
    {
        //播放动画
        var s = DOTween.Sequence();
        Image image = Image_first_Award.GetComponent<Image>();
        Text text = Image_first_Award.transform.Find("Text_Price").GetComponent<Text>();
        int price = model.mySystemConfig.FindFishAward;
        text.text = price.ToString();
        s.Append(image.transform.DOScale(2, 0.3f));
        s.Insert(0f, image.DOFade(0, 0.3f));
        s.InsertCallback(0.29f,
            delegate ()
            {
                text.transform.DOScale(2, 0.3f).OnComplete(
                    delegate ()
                    {
                        AudioManager.Instance.PlayEffect("getCoin");
                        model.mysaveData.money += price;
                        model.mysaveData.hasBeenGetAwardFishID.Add(FishID);
                        model.SaveMyData();
                        //   isCashingPrize = true;
                        Image_first_Award.SetActive(false);
                        newText.gameObject.SetActive(true);
                        isShowNew = true;
                    }
                );
            }
            );
    }
    */
}
