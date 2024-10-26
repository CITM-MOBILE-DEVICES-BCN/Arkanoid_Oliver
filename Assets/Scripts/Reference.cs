using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class Reference : MonoBehaviour
{

    public static Reference LD;
    public Slider sliderPosition;
    public float spacewith = 15;


    void Awake()
    {
        LD = this;
    }

    void Start()
    {
        sliderPosition.value = 0;

    }

   
    void Update()
    {
        transform.position = new Vector3 (sliderPosition.value*spacewith,transform.position.y, transform.position.z);

    

    }
}
