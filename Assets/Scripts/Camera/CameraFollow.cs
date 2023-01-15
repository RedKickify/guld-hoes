using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float lerpSpeed;

    void Update()
    {
        Vector3 targetPos = target.position;
        targetPos.z = -10;
        this.transform.position = Vector3.Lerp(this.transform.position, targetPos, lerpSpeed * Time.deltaTime);
    }
}