using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudAnimation : MonoBehaviour
{

    public Sprite[] cloundSprites;

    private float CloundMoveSpeed;
    private SpriteRenderer sr;
    private Vector2 Point1;

    private void Awake()
    {
        Point1 = transform.position;
        sr = GetComponent<SpriteRenderer>();
        Change();
    }

    private void Change()
    {
        CloundMoveSpeed = Random.Range(0.2f, 0.5f);
        sr.sprite = cloundSprites[Random.Range(0, 3)];
    }

    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * CloundMoveSpeed);
        if (transform.position.x < -5.3f)
        {
            transform.position = Point1;
            Change();
        }
    }
}
