using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int StartHealth = 100;
    private int Health;
    [SerializeField] float respawnTime = 5f;
    [SerializeField] HealthBar healthBar;
    [SerializeField] BoxCollider enemyCollider;
    [SerializeField] MeshRenderer enemyMesh;

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
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        Health = StartHealth;
        healthBar.SetMaxHealth(Health);
        healthBar.gameObject.SetActive(true);
        enemyCollider.enabled = true;
        enemyMesh.enabled = true;
    }
}
