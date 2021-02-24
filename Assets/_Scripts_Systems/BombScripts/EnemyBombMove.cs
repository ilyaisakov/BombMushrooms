using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombMove : MonoBehaviour
{
    private Vector3 shootDir;
    public void Setup(Vector3 shootDir)
    {
        this.shootDir = shootDir;
    }


    void Update()
    {
        float moveSpeed = 30f;
        transform.position += shootDir * moveSpeed * Time.deltaTime;
    }
}
