﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AllBombs allBombs;
    [SerializeField] List<Rigidbody> bombTypes = new List<Rigidbody>();

    LineRenderer lineRend;


    private int currentBomb;
    public Rigidbody projectile;
    public GameObject cursor;
    public Transform shootPoint;
    public LayerMask layer;
    public LineRenderer lineVisual;
    public int lineSegment = 10;
    public float flightTime = 1f;

    private Camera cam;
    private bool isShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.enabled = false;

        cam = Camera.main;
        lineVisual.positionCount = lineSegment + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if ((allBombs.bombList_counts[allBombs.currentBomb] > 0) && (Input.GetMouseButton(0) == true))
        {
            isShoot = true;
            lineRend.enabled = true;
            LaunchProjectile();
        }
        else
        {
            lineRend.enabled = false;
            LaunchProjectile();
        }
            
    }

    void LaunchProjectile()
    {
        Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(camRay, out hit, 100f, layer))
        {
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;

            Vector3 vo = CalculateVelocty(hit.point, shootPoint.position, flightTime);

            Visualize(vo, cursor.transform.position); //we include the cursor position as the final nodes for the line visual position

            transform.rotation = Quaternion.LookRotation(vo);

            if (Input.GetMouseButtonUp(0) && (allBombs.bombList_counts[allBombs.currentBomb] > 0))
            {

                if(isShoot == true)
                {
                    allBombs.bombList_counts[allBombs.currentBomb]--;
                    allBombs.SetCurrentBombUI();
                    isShoot = false;
                }

                Rigidbody obj = Instantiate(bombTypes[allBombs.currentBomb], shootPoint.position, Quaternion.identity);
                obj.velocity = vo;
            }
        }
    }

    //added final position argument to draw the last line node to the actual target
    void Visualize(Vector3 vo, Vector3 finalPos)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, (i / (float)lineSegment) * flightTime);
            lineVisual.SetPosition(i, pos);
        }

        lineVisual.SetPosition(lineSegment, finalPos);
    }

    Vector3 CalculateVelocty(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXz = distance;
        distanceXz.y = 0f;

        float sY = distance.y;
        float sXz = distanceXz.magnitude;

        float Vxz = sXz / time;
        float Vy = (sY / time) + (0.5f * Mathf.Abs(Physics.gravity.y) * time);

        Vector3 result = distanceXz.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = shootPoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;

        result.y = sY;

        return result;
    }
}