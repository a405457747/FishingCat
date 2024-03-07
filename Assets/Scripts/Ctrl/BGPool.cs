using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BGPool : MonoBehaviour
{
    public GameObject fishPrefabs;

    private SystemConfig systemConfig;
    private SpriteRenderer sr;
    //父亲场景id
    private int parentIndex;
    //自己一小屏id
    private int selfIndex;
    private Vector2 originPos;
    private int minNum;
    private int maxNum;
    private List<int> CreteFishIdLIst;
    //边界值的一半
    private float boundsHalfX;
    //边界值的一半
    private float boundsHalfY;
    private Dictionary<string, Dictionary<string, string>> fishConfigDic;
    private Model model;
    private Sprite[] fishSprites;
    //概率系数等会用来相加计算稀有种类
    private double probabilityCoefficient;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        parentIndex = int.Parse(transform.parent.name.Substring(5));
        selfIndex = int.Parse(gameObject.name.Substring(2)) - 1;
        originPos = transform.position;
        boundsHalfX = sr.bounds.size.x / 2f;
        boundsHalfY = sr.bounds.size.y / 2f;
    }

    private void Start()
    {
        model = GameObject.FindWithTag("Model").GetComponent<Model>();
        systemConfig = model.mySystemConfig;
        fishConfigDic = model.fishConfigDic;
        minNum = systemConfig.Scenes[parentIndex].Pools[selfIndex].MinNum;
        maxNum = systemConfig.Scenes[parentIndex].Pools[selfIndex].MaxNum;
        CreteFishIdLIst = systemConfig.Scenes[parentIndex].Pools[selfIndex].FishIDs;
        fishSprites = GameManager.instance.FishSprites;
        probabilityCoefficient = 0.01;
    }

    //生成鱼
    public void CreateFishs()
    {
        //要生成鱼的数量
        int fishCount = Random.Range(minNum, maxNum + 1);

        for (int j = 0; j < CreteFishIdLIst.Count; j++)
        {
            //fishId目前是id 根据id获得价格 体型 出生率
            int fishId = CreteFishIdLIst[j];
            string fishIdStr = fishId.ToString();
            int priceFish = int.Parse(fishConfigDic["Price"][fishIdStr]);
            float BodyType = float.Parse(fishConfigDic["BodyType"][fishIdStr]);
            float ProbabilityBirth = float.Parse(fishConfigDic["ProbabilityBirth"][fishIdStr]);
            float moveSpeed = float.Parse(fishConfigDic["Speed"][fishIdStr]);
            int TwoId = fishId - 101;
            Sprite fishSprite = fishSprites[TwoId];
            //是否稀有
            bool isRare = false;
            //鱼的类型分为稀有和不稀有两种,这里我们规定如果出生率大于等于0.5那么就是非稀有了。
            if (ProbabilityBirth >= 0.5)
            {
                //不稀有的情况
                //如果可以生产出来的话
                if (Calculate.CanCreateFish(ProbabilityBirth + (float)probabilityCoefficient * model.mysaveData.FishFoodLevel))
                {
                    isRare = false;
                    for (int i = 0; i < fishCount; i++)
                    {
                        InstantiateFish(fishIdStr, priceFish, BodyType, fishSprite, false, isRare, moveSpeed);
                    }
                }
            }
            else
            {
                //稀有的情况分两种一种带金，一种不带啊。
                if (Calculate.CanCreateFish(ProbabilityBirth + (float)probabilityCoefficient * model.mysaveData.FishFoodLevel))
                {
                    isRare = true;
                  //  string fishMessage = "";
                    bool isGold = false;
                    if (fishId > 140)
                    {
                        isGold = true;
                       // fishMessage = model.returnLanguageMessage(314);
                    }
                    else
                    {
                        isGold = false;
                       // fishMessage = model.returnLanguageMessage(313);
                    }
                    //只调用一次
                    InstantiateFish(fishIdStr, priceFish, BodyType, fishSprite, isGold, isRare, moveSpeed);
                }
            }
        }
    }
    //实例化鱼的方法
    public void InstantiateFish(string IdName, int priceFish, float BodyType, Sprite fishSprite, bool isGold, bool isRare, float moveSpeed)
    {
        GameObject fish = GameObject.Instantiate(fishPrefabs);
        fish.name = "fish" + IdName;
        float z = fish.transform.position.z;
        float x = (originPos.x + Random.Range(-boundsHalfX, boundsHalfX));
        float y = (originPos.y + Random.Range(-boundsHalfY, boundsHalfY));
        fish.transform.SetParent(this.transform, false);
        fish.transform.position = new Vector3(x, y, z);
        FishMove fishMove = fish.GetComponent<FishMove>();
        int fishDir = Calculate.GetDir();
        fishMove.Init(BodyType, priceFish, fishSprite, moveSpeed, fishDir, isGold, isRare);
        //初始化过后然后开启
        fishMove.ActivateScript();
    }

    //销毁鱼
    public void DestroyFishs()
    {
        if (transform.childCount > 0)
        {
            foreach (Transform TransChild in this.transform)
            {
                Destroy(TransChild.gameObject);
            }
        }
    }
}
