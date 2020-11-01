using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnMovement : MonoBehaviour
{
    public GameObject[] pointsToGo;

    private int current = 0;

    private float rotSpeed;
    private float wpRadius = 1;
    public float speed = 2;

    private void Update()
    {
        if (Vector3.Distance(pointsToGo[current].transform.position, transform.position) < wpRadius)
        {
            current++;
            if (current >= pointsToGo.Length)
            {
                current = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, pointsToGo[current].transform.position, Time.deltaTime * speed);
    }
}
