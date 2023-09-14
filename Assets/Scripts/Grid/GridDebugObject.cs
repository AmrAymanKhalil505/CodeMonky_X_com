using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] TextMeshPro m_TextMeshPro;
    GridObject gridObject;
    public void setGridObject(GridObject gridObject)
    {
        this.gridObject = gridObject;
        this.m_TextMeshPro= this.gameObject.GetComponentInChildren<TextMeshPro>();


    }

    private void Update()
    {


        GridPosition GP = this.gridObject.getGridPosition();
        //m_TextMeshPro.text =GP.X +", "+GP.Z;
        m_TextMeshPro.text = this.gridObject.ToString();


    }
}
