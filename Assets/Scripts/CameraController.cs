using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float rotationSpeed = 100.0f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float MIN_FOLLOW_Y_ZOOM_OFFEST = 2F;
    [SerializeField] private float MAX_FOLLOW_Y_ZOOM_OFFEST = 15F;
    [SerializeField] private float zoomAmount = 2f;
    [SerializeField] private float zoomSpeed = 2.5f;

    private Vector3 targetZoomOffest;
    // Start is called before the first frame update
    void Start()
    {
        CinemachineTransposer CT = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetZoomOffest = CT.m_FollowOffset;

    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleRotation();
        handleZoom();

    }
    private void handleMovement()
    {
        Vector3 offestTransformDir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            offestTransformDir.z +=1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            offestTransformDir.z -=1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            offestTransformDir.x -=1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            offestTransformDir.x +=1;
        }

        Vector3 ActuraloffestTransformDir = transform.forward*offestTransformDir.z + transform.right*offestTransformDir.x;

        this.transform.position += ActuraloffestTransformDir * movementSpeed * Time.deltaTime;
    }
    private void handleRotation()
    {
        Vector3 offestRotationTransform = Vector3.zero;

        if (Input.GetKey(KeyCode.E))
        {
            offestRotationTransform.y +=1;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            offestRotationTransform.y -=1;
        }
        this.transform.Rotate(offestRotationTransform* rotationSpeed *Time.deltaTime);

    }
    private void handleZoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetZoomOffest.y+=zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetZoomOffest.y-=zoomAmount;
        }
        CinemachineTransposer CT = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetZoomOffest.y = Mathf.Clamp(targetZoomOffest.y, MIN_FOLLOW_Y_ZOOM_OFFEST, MAX_FOLLOW_Y_ZOOM_OFFEST);
        CT.m_FollowOffset =Vector3.Lerp(CT.m_FollowOffset, targetZoomOffest, Time.deltaTime*zoomSpeed);
    }
}
