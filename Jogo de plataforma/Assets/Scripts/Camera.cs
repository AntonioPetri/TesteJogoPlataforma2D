using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float minX, maxX, minY;

    private void Start()
    {
       player = GameObject.FindWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if(player.position.x >= transform.position.x || player.position.x <= transform.position.x)
        {
            transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, transform.position.y), transform.position.z); 
    }
}
