using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombBehavior : MonoBehaviour
{

    MeshRenderer BombMesh;
    [SerializeField] private GameObject explosionFx;
    [SerializeField] private GameObject trailFx;
    CapsuleCollider BombCollider;
    [SerializeField] private float expRadius = 10;
    [SerializeField] int damageBomb = 1;
    private float distance; //beetwen explosion and enemy

    private void Start()
    {
        BombMesh = GetComponent<MeshRenderer>();
        BombMesh.enabled = true;

        BombCollider = GetComponent<CapsuleCollider>();
        explosionFx.gameObject.SetActive(false);
        trailFx.gameObject.SetActive(true);

    }

    private void OnCollisionEnter(Collision collision)
    {
        StartExplosion();
    }

    void StartExplosion()
    {
        //SetDamage();
        //fx
        explosionFx.gameObject.SetActive(true);
        trailFx.gameObject.SetActive(false);
        BombCollider.enabled = false;
        BombMesh.enabled = false;

    }

    private void FixedUpdate()
    {
        
    }

    /*void SetDamage()
    {
        Collider[] collidersInRadius = Physics.OverlapSphere(transform.position, expRadius);

        foreach (Collider enemies in collidersInRadius)          
        {
            EnemyHealth enemyHlz = enemies.GetComponent<EnemyHealth>();          
            if (enemyHlz != null)
            {
                Transform enemyPos = enemies.GetComponent<Transform>();
                distance = Vector3.Distance(transform.position, enemyPos.position);

                enemyHlz.SetDamage(Mathf.CeilToInt((expRadius - distance) + damageBomb));
            }
              
        }
    }*/
}
