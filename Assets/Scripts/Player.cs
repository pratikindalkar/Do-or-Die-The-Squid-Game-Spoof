using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Finish")
        {
            StartCoroutine(FindObjectOfType<GameManager>().Playerwin());
        }
    }
}
