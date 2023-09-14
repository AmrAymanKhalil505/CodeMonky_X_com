using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private ParticleSystem PS_VFX_UnitVisual;

    private void Awake()
    {
        PS_VFX_UnitVisual = GetComponent<ParticleSystem>();
    }
    void Start()
    {
        PS_VFX_UnitVisual?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        UnitActionSystem.Instance.OnSelectedUnitChanged+= UnitActionSystem_OnSelectedUnitChanged;

        updateVisual();
    }

    // Update is called once per frame
    void Update()
    {
        //if( UnitActionSystem.Instance.getSelectedUnit())
    }

    private void UnitActionSystem_OnSelectedUnitChanged( object sender , EventArgs e)
    {
        updateVisual();
    }

    private void updateVisual()
    {
        if (UnitActionSystem.Instance.getSelectedUnit().Equals(unit))
        {
            PS_VFX_UnitVisual?.Play();
        }
        else
        {
            PS_VFX_UnitVisual?.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
