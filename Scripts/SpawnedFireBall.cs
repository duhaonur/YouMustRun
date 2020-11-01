using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedFireBall : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.AddForce(new Vector3(0, 0, -1) * 4);
    }
}
