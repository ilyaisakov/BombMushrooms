using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarBilboard : MonoBehaviour
{
    public Transform cam;

    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
