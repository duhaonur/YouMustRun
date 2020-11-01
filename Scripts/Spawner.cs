using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] spawnPos;
    public GameObject fireBall;
    private GameObject cloneFireBall;

    public void Spawn()
    {
        for (int i = 0; i < spawnPos.Length; i++)
        {
            cloneFireBall = Instantiate(fireBall, spawnPos[i].transform.position, Quaternion.identity) as GameObject;
            Destroy(cloneFireBall, 6);
        }
    }
}
