using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private PlatformEffector2D effector;
    public float waitTime;
    private float waitTimeLeft;

    void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            waitTimeLeft = waitTime;
            effector.rotationalOffset = 0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if(waitTimeLeft <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTimeLeft = waitTime;
            }
            else
            {
                waitTimeLeft -= Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            effector.rotationalOffset = 0f;
        }
    }
}
