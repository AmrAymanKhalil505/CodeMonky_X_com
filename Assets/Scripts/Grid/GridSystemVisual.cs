using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{

    [SerializeField] private GameObject ValidMovementPlace_VFX;
    [SerializeField] private List<GameObject> ValidMovementPlace_VFX_List;

    
    
    // Start is called before the first frame update
    void Start()
    {
        ValidMovementPlace_VFX_List = new List<GameObject> ();
        HandleEventsFromEachObject();
        // createGridVisual2D();
        UpdateGridVisual();
    }

    


    public void PlayValidMovementPlace_VFX(GridPosition VFX_gridPosition) =>
        PlayValidMovementPlace_VFX(GridLevel.Instance.GetWorldPostion(VFX_gridPosition));


    public void PlayValidMovementPlace_VFX(Vector3 VFX_position)
    {
        if(ValidMovementPlace_VFX != null)
        {
            GameObject VMP_VFX = Instantiate(ValidMovementPlace_VFX, VFX_position, Quaternion.identity);
            ValidMovementPlace_VFX_List.Add(VMP_VFX);
            ParticleSystem PS_VFX = VMP_VFX.GetComponent<ParticleSystem>();
            PS_VFX.Play();
        }
    }

    public void stopAllValidMovementPlace_VFX()
    {
        foreach (GameObject VFX in ValidMovementPlace_VFX_List)
        {
            stopValidMovmentPlace_VFX(VFX);
        }
        ValidMovementPlace_VFX_List.Clear();
    }

    public void stopValidMovmentPlace_VFX(GameObject VFX)
    {
        //VFX.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        VFX.GetComponent<ParticleSystem>().Stop(true, ParticleSystemStopBehavior.StopEmitting);
        float dur = VFX.GetComponent<ParticleSystem>().main.duration;
        float sim_speed = VFX.GetComponent<ParticleSystem>().main.simulationSpeed;
        Destroy(VFX, dur / sim_speed);
    }

    
    public void createGridVisual2D()
    {
        int width = GridLevel.Instance.GetWidth();
        int height = GridLevel.Instance.GetHeight();

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                PlayValidMovementPlace_VFX(gridPosition);
                //debugGridObjectArray2[x,z] = debugGridTransform;
            }
        }
    }

    public void createGridVisualPositionList(List<GridPosition> gridPositions)
    {
        foreach (GridPosition gridPosition in gridPositions)
        {
            PlayValidMovementPlace_VFX(gridPosition);
        }
    }

    private void UpdateGridVisual()
    {
        stopAllValidMovementPlace_VFX();
        //Unit unit = UnitActionSystem.Instance.getSelectedUnit();
        List<GridPosition> validPositoins = UnitActionSystem.Instance.getSelectedAction().GetValidActionPositionList();
        createGridVisualPositionList(validPositoins);
    }

    private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }
    private void UnitActionSystem_OnSelectedActionChanged(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }
    private void UnitActionSystem_OnSelectedActionDone(object sender, EventArgs e)
    {
        UpdateGridVisual();
    }
    private void UnitActionSystem_OnSelectedActionStarted(object sender, EventArgs e)
    {
        stopAllValidMovementPlace_VFX();
    }

    private void HandleEventsFromEachObject()
    {
        UnitActionSystem.Instance.OnSelectedActionChanged+=UnitActionSystem_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnSelectedUnitChanged+=UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionDone+=UnitActionSystem_OnSelectedActionDone;
        UnitActionSystem.Instance.OnSelectedActionStarted+=UnitActionSystem_OnSelectedActionStarted;
    }
}
