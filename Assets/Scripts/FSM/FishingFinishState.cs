using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingFinishState : FSMState
{
    public ParticleSystem ps;
    //要开始回去吗
    public bool isStartMove = false;
    public int waveMoney = 0;

   // private HookFish hookFish;
    private Hook hook;
    private Transform HookTrans;
    private View view;
    // 上去回拉钩的速度
    [SerializeField]
    private float speed;
    private Vector3 targetPos;
    private saveData saveData;
    private float GetFishInterval;
    private AudioManager audioManager;
    private SystemConfig systemConfig;

    private void Awake()
    {
        stateID = StateID.FishingFinish;
        AddTransition(Transition.HasBeenCollectedMoney, StateID.WaitLoad);
        hook = GameObject.FindWithTag("Player").GetComponentInChildren<Hook>();
        HookTrans = hook.transform;
       // hookFish = hook.GetComponent<HookFish>();
        targetPos = hook.transform.position;
        ps.Stop();
    }
    private void Start()
    {
        UpdateQuickReturnHookSpeed();
        systemConfig = ctrl.model.mySystemConfig;
        GetFishInterval = (float)systemConfig.GetFishInterval;
        saveData = ctrl.model.mysaveData;
        audioManager = AudioManager.Instance;
        view = ctrl.view;
    }

    public void UpdateQuickReturnHookSpeed()
    {
        speed = (float)ctrl.model.mySystemConfig.QuickRecycleSpeed * Calculate.ReturnCanShowScene(ctrl.model.mysaveData.WaterDepthLevel);
    }

    private void Update()
    {
        if (isStartMove)
        {
            HookTrans.Translate(Vector2.up * speed * Time.deltaTime);
            //4.5   Vector3.Distance(HookTrans.position, targetPos) < 6.2f
            if (HookTrans.position.y>=targetPos.y)
            {
                view.ShowMenuPanel();
                view.PopPsStop();
                isStartMove = false;
                HookTrans.position = targetPos;
                StartCoroutine(DestoryFishAndAddMoney());
            }
        }
    }
    //销毁鱼儿然后加分吧
    public IEnumerator DestoryFishAndAddMoney()
    {
        if (HookTrans.childCount > 0)
        {
            foreach (Transform transChild in HookTrans)
            {
                audioManager.PlayEffect("statisticsEffect");
                yield return new WaitForSeconds(GetFishInterval);
                FishMove fishMove = transChild.GetComponent<FishMove>();
                int fishPrice = fishMove.fishPrice;
               // string fishMessage = fishMove.TheRareFishMessage;
                waveMoney += fishPrice;
                ps.Stop();
                ps.Play();
                view.Fish_repeat_change_Repeat(transChild.GetComponent<SpriteRenderer>().sprite);
                view.Text_ScoreChange(fishPrice);
               // view.Text_Fish_message_Repeat(fishMessage);
                transChild.gameObject.SetActive(false);
            }
            foreach (Transform transChild in HookTrans)
            {
                Destroy(transChild.gameObject);
            }
        }
       
        //销毁所有鱼儿
        view.DestroySceneFish();
        //更新鱼库的数据了
        //   view.UpdateFishLibrayData();
        //显示结算面板了
        //  yield return new WaitForSeconds(0.4f);
        view.ShowShowMyMoneyPanel(waveMoney);
        GameManager.instance.isStartCameraFllow = false;
    }
    //一倍收益
    public void ClickCollectMoney()
    {
        EarningsFunction();
      //  ExampleScript.Instance.ShowAd();
      if(saveData.isVip==false)
        GeogleADManager.Instance.ShowInterstitial();

    }

    //看广告双倍收益
    public void ClickCollectMoneyDoubleWatchAds()
    {
        EarningsFunction(2);
        //  ExampleScript.Instance.ShowAd();

        //显示广告了
        if (saveData.isVip == false)
            GeogleADManager.Instance.ShowRewardBasedVideo();

    }
    public void EarningsFunction(int multiple = 1)
    {
        fsm.PerformTransition(Transition.HasBeenCollectedMoney);
        view.HideShowMyMoneyPanel();
        int Money = waveMoney * multiple;
        saveData.money += Money;
        ctrl.SaveCurrentTime();
        ctrl.model.SaveMyData();
        audioManager.PlayEffect("getCoin");
        view.UpdateMoneyText(saveData.money, saveData.FishFoodLevel, saveData.NetSize, saveData.WaterDepthLevel, saveData.OfflineEarningsLevel);
    }

    public override void DoBeforeEntering()
    {
        //隐藏这个渔网个数面板
        view.HideImage_fishNetCount_Panel();
        UpdateQuickReturnHookSpeed();
        audioManager.PlayEffect("TakeUp");
        isStartMove = true;
        waveMoney = 0;
       
    }

    public override void DoBeforeLeaving()
    {
        //if (ctrl.model.mysaveData.hasBeenAchieveMaxDepthLevel)
        //{
            //int random = Random.Range(0, 5);
            //ctrl.TempSceneID = random;
            //ctrl.view.UpdateScenes(ctrl.TempSceneID);
       // }
    }
}
