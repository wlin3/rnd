using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class cameraFollow : MonoBehaviour
{
    public float followSpeed;
    private float currentFollowSpeed;
    private Transform player;

    // Start is called before the first frame update
    
    void Start()
    {
        currentFollowSpeed = followSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player").transform;

        Follow();
    }

    void Follow()
    {
    Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);
        
    Vector3 smoothPos = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

    transform.position = smoothPos;
    }
}
