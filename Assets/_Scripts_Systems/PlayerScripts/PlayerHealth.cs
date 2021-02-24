using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int StartHealth = 100;
    private int Health;
    [SerializeField] float respawnTime = 0f;
    [SerializeField] HealthBar healthBar;
    Vector3 startPosition;
    [SerializeField] CharacterController CharController;



    void Start()
    {
        startPosition = gameObject.transform.position;
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

        CharController.gameObject.SetActive(false);
        gameObject.transform.position = startPosition;
        CharController.gameObject.SetActive(true);
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        Health = StartHealth;
        healthBar.SetMaxHealth(Health);

    }
}
