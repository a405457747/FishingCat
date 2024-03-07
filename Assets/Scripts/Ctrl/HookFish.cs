using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 钩鱼，让鱼儿上钩子的脚本啊
/// </summary>
public class HookFish : MonoBehaviour
{
    //鱼儿吃钩相对父亲的偏移哦;
    //public Vector2 offset = new Vector2(-0.25f, -0.6f);

    private FishingCtrlState fishingCtrlState;
    private View view;
    private Model model;
    private saveData saveData;

    private void Awake()
    {
        HookFishScriptClose();
        fishingCtrlState = GameObject.FindGameObjectWithTag("Ctrl").GetComponentInChildren<FishingCtrlState>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        saveData = model.mysaveData;
    }

    public void HookFishScriptClose()
    {
        this.enabled = false;
        Physics2D.IgnoreLayerCollision(8, 9, true);
    }

    public void HookFishScriptOpen()
    {
        this.enabled = true;
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Fish"))
        {
            if (fishingCtrlState.CanCatchFishCount == 0) return;
            int fishId = int.Parse(collision.name.Substring(4));
            Transform fishTrans = collision.transform;
            if (Calculate.HookCanFish(saveData.currentHookID, fishId) == false)
            {
                string miss = model.returnLanguageMessage(327);
                view.ShowMissEffect(miss, fishTrans.position);
                return;
            }
            FishMove fishMove = collision.GetComponent<FishMove>();
            bool isRare = fishMove.isRare;
            fishMove.DeactivateScript();
            //认一个爸爸
            fishTrans.SetParent(this.transform, true);
            //  collision.transform.localPosition = Vector3.zero + (Vector3)offset;
            fishTrans.localPosition = Vector3.zero;

            if (isRare)
            {
                AudioManager.Instance.PlayEffect("HookRare");
            }
            else
            {
                AudioManager.Instance.PlayEffect("HookGeneric2");
            }

            fishTrans.localRotation = Quaternion.Euler(new Vector3(0, 0, -150 * fishMove.currentDir));

            Tweener tweener = fishTrans.DORotate(new Vector3(0, 0, 150 * fishMove.currentDir), 1.5f, RotateMode.LocalAxisAdd);
            //  tweener.SetAutoKill(false);
            tweener.SetLoops(-1, LoopType.Yoyo);
            //在这里做更新纪念品的写入吧
            bool isHaveTheId = Calculate.isHaveTheID(saveData.hasBeenFoundFishID, fishId);
            if (isHaveTheId == false)
            {
                saveData.hasBeenFoundFishID.Add(fishId);
                view.Show_Text_new();
                model.SaveMyData();
            }
            fishingCtrlState.CanCatchFishCount -= 1;
            //碰撞显示UI效果吧
            view.ShowHitEffect(fishMove.fishPrice, fishTrans.position);
            //更新 渔网数目UI了
            view.UpdateText_NetNum(fishingCtrlState.CanCatchFishCount);
            if (fishingCtrlState.CanCatchFishCount == 0)
            {
                fishingCtrlState.ChangeToFishingFinishState();
            }
        }
    }
}
