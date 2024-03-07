using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    public bool isRare = false;
   // public GameObject Trail;
    //当前的鱼儿的方向 1 (v2.left方向移动)   或者 -1（变成了-1后 缩放比例*-1  v2.left*-1）
    public int currentDir;
    //假如从中点开始游泳，游泳多少米后转方向呢
    public float changeDirDistance = 5.0f;
    public int fishPrice;
    //延迟多少秒特效出现
  //  public float delayEffectShow = 1.2f;
   // public string TheRareFishMessage = "";

    //记录当前的缩放比例
    private float currentScale;
    private float moveSpeed;
    private SpriteRenderer sr;
    //身下的闪光特效
    private GameObject GoldEffect;
    private BoxCollider2D boxCollider2;

    private void Awake()
    {
        DeactivateScript();
        sr = GetComponent<SpriteRenderer>();
        GoldEffect = transform.Find("GoldEffect").gameObject;
        boxCollider2 = GetComponent<BoxCollider2D>();
    }

    public void DeactivateScript()
    {
        this.enabled = false;
    }

    public void ActivateScript()
    {
        this.enabled = true;
    }
    private void ChangeBoxColliderSize(float shapeFactor){
        boxCollider2.size = new Vector2(shapeFactor, shapeFactor);
    }

    private void Update()
    {
        ChangeDir();
        transform.Translate(Vector2.left * Time.deltaTime * moveSpeed * currentDir);
    }

    //初始化复制
    public void Init(float currentScale, int fishPrice, Sprite fishSprite, float moveSpeed, int currentDir, bool isGod, bool isRare)
    {
        if (isGod)
        {
            GoldEffect.SetActive(true);
        }
        else
        {
            GoldEffect.SetActive(false);
        }
        this.isRare = isRare;
      //  this.TheRareFishMessage = TheRareFishMessage;
        this.currentScale = currentScale;
        this.fishPrice = fishPrice;
        sr.sprite = fishSprite;
        this.moveSpeed = moveSpeed;
        this.currentDir = currentDir;
        this.transform.localScale = new Vector3(1 * currentDir, 1, 1) * currentScale;
        ChangeBoxColliderSize(this.currentScale);
    }

    //自动检测改变方向哦
    public void ChangeDir()
    {
        if (transform.position.x < -changeDirDistance)
        {
            currentDir = -1;
            this.transform.localScale = new Vector3(1 * currentDir, 1, 1) * currentScale;
        }

        if (transform.position.x > changeDirDistance)
        {
            currentDir = 1;
            this.transform.localScale = new Vector3(1 * currentDir, 1, 1) * currentScale;
        }
    }
}
