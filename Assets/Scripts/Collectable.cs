using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{


    //Vector3 PadleOriginal = Paddle.Instance.transform.localScale;

    [SerializeField] private AudioSource powerUp; 

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Paddle")
            {
                ApplyEffect();
     
        }

            if (collision.tag == "Paddle" || collision.tag == "Wall")
            {
                Destroy(gameObject);
            }
        }

 

    private void ApplyEffect()
    {
        powerUp.Play();
        Paddle.Instance.transform.localScale = new Vector3(Paddle.Instance.transform.localScale.x* 1.2f, Paddle.Instance.transform.localScale.y, Paddle.Instance.transform.localScale.z);
      
      

    }
    //private IEnumerator RevertEffect(float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    Paddle.Instance.transform.localScale = PadleOriginal;

    //}


}



