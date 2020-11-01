using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryColumn : MonoBehaviour
{
    public GameObject[] pointsToGo;

    private int current = 0;

    private float rotSpeed;
    private float wpRadius = 1;

    public float speed = 2;

    private bool isReached = false;

    private void Update()
    {
        if (!isReached)
        {
            if (Vector3.Distance(pointsToGo[current].transform.position, transform.position) < wpRadius)
            {
                
                if (current >= pointsToGo.Length)
                {
                    isReached = true;
                }
                else
                {
                    current++;
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, pointsToGo[current].transform.position, Time.deltaTime * speed);
        }
    }
}
