using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnReviveColl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Flame")
        {
            GetComponent<OnRevivePlayerMove>().Death();
        }
        if (other.tag == "Column")
        {
            GetComponent<OnRevivePlayerMove>().Death();
        }
    }
}
