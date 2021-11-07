using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject ball;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ball, transform.position, Quaternion.identity);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
