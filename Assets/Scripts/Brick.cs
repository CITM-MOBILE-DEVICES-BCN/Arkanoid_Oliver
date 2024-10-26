using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    private SpriteRenderer sr;


    public int Hitpoints = 3;
    public static event Action<Brick> OnBrickDestroction;
    public ParticleSystem DestroyEffect;
    public Sprite[] Sprites;
    public Color[] BrickColors;

    public GameObject collectablePrefab;

    //public string BrickID;





    private void Awake()
    {
        this.sr = GetComponent<SpriteRenderer>();
       this.sr.sprite = Sprites[this.Hitpoints - 1];
        this.sr.color = BrickColors[this.Hitpoints - 1];

        //if (string.IsNullOrEmpty(BrickID))
        //{
        //    BrickID = "Brick_" + GetInstanceID(); 
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        ApplyCollisionLogic(ball);
       
    }
   
    //newBrick.Init(brickContainer.transform, this.Sprites[brickType - 1], this.BrickColors[brickType], brickType);

    private void ApplyCollisionLogic(Ball ball)
    {
        this.Hitpoints--;

        if (this.Hitpoints <= 0)
        {
            OnBrickDestroction?.Invoke(this);
            SpawnDestroyEffect();
            Destroy(this.gameObject);
            OnBrickDestroyBuff();
            ScoreManager.Instance.AddScore(10);
        }
        else
        {
            this.sr.sprite = Sprites[this.Hitpoints - 1];

            ScoreManager.Instance.AddScore(1);
        }

    }
    private void OnBrickDestroyBuff()
    {

        float buffSpawnChance = UnityEngine.Random.Range(0f, 10f);
        
        if (buffSpawnChance > 8f) {

            SpawnCollectable();

        
        }

    }

    private void SpawnCollectable()
    {

        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(brickPos.x, brickPos.y, brickPos.z - 0.5f);
        GameObject effect = Instantiate(collectablePrefab, spawnPos, Quaternion.identity);



    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(brickPos.x, brickPos.y, brickPos.z - 0.5f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPos, Quaternion.identity);

        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this.sr.color;
        Destroy(effect, DestroyEffect.main.startLifetime.constant);

       

    }
}
//    internal void Init(Transform Containertransform, Sprite sprite, Color color, int hitpoints)
//    {
//        this.transform.SetParent(Containertransform);
//        this.sr.sprite = sprite;
//        this.sr.color = color;
//        this.Hitpoints = hitpoints;
//    }
//
