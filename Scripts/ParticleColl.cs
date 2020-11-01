using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleColl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Flame")
        {
            GetComponent<PlayerMove>().Death();
        }
        if(other.tag == "Column")
        {
            GetComponent<PlayerMove>().Death();
        }   
    }
}
