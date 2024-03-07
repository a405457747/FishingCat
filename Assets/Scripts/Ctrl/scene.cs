using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene : MonoBehaviour
{
    private BGPool[] bgPools;
    private GameManager.SceneBigSprites sbs;
    // private GameManager.SceneBigSprites sbs2;
    private Sprite[] sceneOnly;


    private void Awake()
    {
        bgPools = GetComponentsInChildren<BGPool>();
    }

    private void Start()
    {
        sbs = GameManager.instance.sbs;
        sceneOnly = GameManager.instance.sceneOnly;
    //    sbs2 = GameManager.instance.sbsOnly;
    }

    public void sceneCreateFish()
    {
        foreach (BGPool bgPools in bgPools)
        {
            bgPools.CreateFishs();
        }
    }

    public void sceneDistoryFish()
    {
        foreach (BGPool bgPools in bgPools)
        {
            bgPools.DestroyFishs();
        }
    }

    public void ChangeBGSprites(int sceneID)
    {
        int i = 0;
        foreach (BGPool bgPools in bgPools)
        {

            SpriteRenderer sr = bgPools.GetComponent<SpriteRenderer>();
            sr.sprite = sbs.smallSprites[sceneID].sprites[i];
            if (i == 9)
            {
                int random = Random.Range(0, sceneOnly.Length);
                sr.sprite = sceneOnly[random];
            }
            i++;
        } 
    }

    public void ChangeBGSpritesOnlyOne(int onlyID)
    {
       // int i = 0;
        foreach (BGPool bgPools in bgPools)
        {
            int randomIndex = Random.Range(0, sceneOnly.Length);
            SpriteRenderer sr = bgPools.GetComponent<SpriteRenderer>();
            sr.sprite = sceneOnly[randomIndex];
           // sr.sprite = sbs2.smallSprites[onlyID].sprites[i];
         //   i++;
        }
    }
}
