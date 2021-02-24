using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthAI : MonoBehaviour
{
    [SerializeField] int StartHealth = 100;
    private int Health;
    [SerializeField] float respawnTime = 5f;
    [SerializeField] HealthBar healthBar;
    [SerializeField] BoxCollider enemyCollider;
    [SerializeField] MeshRenderer enemyMesh;
    [SerializeField] NavMeshFollowPlayer navMesh;
    [SerializeField] MeshRenderer cube;
    void Start()
    {
        Respawn();
    }

    public void SetDamage(int dmg)
    {
        Health -= dmg;
        healthBar.SetHealth(Health);
        if (Health <= 0)
        {
            Dead();
        }
    }

    void Dead()
    {
        enemyCollider.enabled = false;
        enemyMesh.enabled = false;
        healthBar.gameObject.SetActive(false);
        cube.enabled = false;
        navMesh.enabled = false;
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        cube.enabled = true;
        navMesh.enabled = true;
        Health = StartHealth;
        healthBar.SetMaxHealth(Health);
        healthBar.gameObject.SetActive(true);
        enemyCollider.enabled = true;
        enemyMesh.enabled = true;

    }
}
