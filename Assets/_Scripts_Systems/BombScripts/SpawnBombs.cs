using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnBombs : MonoBehaviour
{
    public AllBombs allBombs;
    [SerializeField] float respawnTime = 5f;

    SphereCollider radiusCollider;
    [SerializeField] float radiusTaken = 1f;

    [SerializeField] List<MeshRenderer> bombList_meshes = new List<MeshRenderer>();

    private int randomNum;

    void Start()
    {
        radiusCollider = GetComponent<SphereCollider>();
        radiusCollider.radius = radiusTaken;


        DeactivateAllMesh();
        SetRandomMesh();

    }


    void DeactivateAllMesh()
    {
        for (int i = 0; i< bombList_meshes.Count; i++)
        {
            bombList_meshes[i].enabled = false;
        }
    }

    void SetRandomMesh()
    {
        DeactivateAllMesh();
        randomNum = Random.Range(0, bombList_meshes.Count);
        bombList_meshes[randomNum].enabled = true;
    }

    void DiableSpawner()
    {
        DeactivateAllMesh();
        radiusCollider.enabled = false;
    }

    void EnableSpawner()
    {
        radiusCollider.enabled = true;
        SetRandomMesh();
    }


    private void OnTriggerEnter(Collider other) //catch bomb
    {
        if(other.CompareTag("Player"))
        {
            allBombs.bombList_counts[randomNum]++;
            allBombs.SetCurrentBombUI();
            DiableSpawner();
            Invoke("EnableSpawner", respawnTime);

        }
    }



}
