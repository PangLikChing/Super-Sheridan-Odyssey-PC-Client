using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    public float topClamp = 70.0f;
    public float bottomClamp = -30.0f;
    [HideInInspector]
    public GameObject mainCamera;

    private float cinemachineTargetYaw;
    private float cinemachineTargetPitch;
    private const float threshold = 0.01f;

    private InputHandler input;
    

    private void Awake()
    {
        input = PlayerController.Instance.inputHandler;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnEnable()
    {
        cinemachineTargetYaw = 0;
        cinemachineTargetYaw = 0;
    }

    void LateUpdate()
    {
        // if there is an input and camera position is not fixed
        if (input.look.sqrMagnitude >= threshold)
        {
            cinemachineTargetYaw += input.look.x * Time.deltaTime;
            cinemachineTargetPitch += input.look.y * Time.deltaTime;
        }

        // clamp our rotations so our values are limited 360 degrees
        cinemachineTargetYaw = Mathf.Clamp(cinemachineTargetYaw, float.MinValue, float.MaxValue);
        cinemachineTargetPitch = Mathf.Clamp(cinemachineTargetPitch, bottomClamp, topClamp);

        // Cinemachine will follow this target
        transform.rotation = Quaternion.Euler(cinemachineTargetPitch, cinemachineTargetYaw, 0.0f);
    }
}
