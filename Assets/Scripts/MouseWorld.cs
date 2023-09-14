using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld instance;
    [SerializeField]
    private LayerMask mousePlaneLayerMask;
    
    [SerializeField] private GameObject selectPositionVFXAnimation;

    private void Awake()
    {
        instance= this;
    }

    void Update()
    {
        transform.position = MouseWorld.GetPostion();
    }

    public static Vector3 GetPostion()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit raycastHit;
        Physics.Raycast(r, out raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
    public static void playSelectPositionVFXAnimation()
    {
        instance.selectPositionVFXAnimation.transform.position = MouseWorld.GetPostion(); 
        instance.selectPositionVFXAnimation.GetComponent<ParticleSystem>().Stop();
        instance.selectPositionVFXAnimation.GetComponent<ParticleSystem>().Play();
    }

}
