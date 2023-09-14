using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GridObject 
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;



    public GridObject (GridSystem gridSystem, GridPosition gridPosition)
    {
        this.gridSystem=gridSystem;
        this.gridPosition=gridPosition;
        unitList= new List<Unit>();
    }


    public GridPosition getGridPosition()
    {
        return this.gridPosition;
    }


    public void AddUnit (Unit unit) 
    { 
        this.unitList.Add( unit);
    }
    public Unit getUnit()
    {
        return this.unitList[0];
    }

    public List<Unit> getUnitList()
    {
        return this.unitList;
    }
    
    public Unit getUnit (int index)
    {
        return (this.unitList[index]);
    }

    public void removeUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public override string ToString()
    {   
        //if(getUnit() is not null) 
        //return gridPosition.ToString() + "/n" +getUnit().ToString() ;
        //else { return gridPosition.ToString(); }

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(gridPosition.ToString());
        foreach (Unit unit in this.unitList)
        {
            stringBuilder.AppendLine(unit.ToString());
        }
        return stringBuilder.ToString();
    }
    public bool hasAnyUnit()
    {
        return getUnitList().Count> 0;
    }
}
