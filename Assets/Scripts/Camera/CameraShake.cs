using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 10f;
    private float shakeDurationTimer = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        shakeDurationTimer = shakeDuration;
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDurationTimer > 0)
        {
            var shakeDamp = shakeDuration - shakeDurationTimer;

            if (shakeDamp > 0)
            {
                camTransform.localPosition = originalPos + ((Random.insideUnitSphere * shakeAmount) / (shakeDuration - shakeDurationTimer));
            }

            shakeDurationTimer -= Time.deltaTime;
        }
        else
        {
            shakeDurationTimer = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}