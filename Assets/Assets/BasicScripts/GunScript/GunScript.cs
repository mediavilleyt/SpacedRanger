using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject ball;
    public ParticleSystem shineyBall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            ball.SetActive(true);

            if (ball.transform.localScale.x >= 0.25) return;

            if(ball.transform.localScale.x >= 0.10) shineyBall.Play();

            //ball gets bigger when u hold down the mouse button    
            ball.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * Time.deltaTime * 4;
            Debug.Log("Ball is growing: " + ball.transform.localScale.x);
        }

        //if mouse button is released, ball gets shot
        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            //ball gets shot
            GameObject newBall = Instantiate(ball, ball.transform.position, ball.transform.rotation);
            newBall.GetComponent<Rigidbody>().AddForce(transform.forward * 10000);

            shineyBall.Stop();
            ball.SetActive(false);
            ball.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}
