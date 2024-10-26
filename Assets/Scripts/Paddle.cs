using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    // Start is called before the first frame update
    #region Singleton
    private static Paddle _instance;
    public static Paddle Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {

            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

    }







    #endregion

    public float velocity = 1;

  

    void Start()
    {

    


    }

    // Update is called once per frame
    void Update()
    {
   


       if (!GameManager.Instance.IsActiveAuto)
        {
            transform.position = Vector2.Lerp(transform.position, Reference.LD.transform.position, velocity * Time.deltaTime);



        }
      

      

    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ball")
        {


            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
           



            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);

            //ballRb.velocity = Vector2.zero;
            float difference = paddleCenter.x - hitPoint.x;
            if (!GameManager.Instance.IsActiveAuto)
            {
                if (hitPoint.x < paddleCenter.x)
                {

                    ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), ballRb.velocity.y));

                }
                else
                {
                    ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), ballRb.velocity.y));
                }

            }
            else
            {
                int i = 0;
                Vector3 LastActiveBrickPosition;
                while (bricks[i] != null)
                {
                    
                    if (bricks[i] != null)
                    {

                        LastActiveBrickPosition = bricks[i].transform.position.normalized;

                        ballRb.AddForce(LastActiveBrickPosition * 200 , ForceMode2D.Impulse);


                       
                        break;
                    }
                    i++;

                }




            }


        }
    }
}
