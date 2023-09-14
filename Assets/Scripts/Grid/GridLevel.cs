using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridLevel : MonoBehaviour
{
    public static GridLevel Instance { get; private set; }


    [SerializeField] private Transform gridDebugObjectPrefab;
    private GridSystem gridSystem;
    void Awake()
    {
        gridSystem= new GridSystem(10, 10, 2f);
        //gridSystem.creatDebugObjects(gridDebugObjectPrefab);
        Instance = this;

    }

    public void addUnitGridPosition(GridPosition gridPosition, Unit unit)
    {
        this.gridSystem.GetGridObject(gridPosition).AddUnit(unit);
    }
    public Unit getUnitGridPosition(GridPosition gridPosition)
    {
        return this.gridSystem.GetGridObject(gridPosition).getUnit();
    }

    public List<Unit> getUnitListGridPosition(GridPosition gridPosition)
    {
        return this.gridSystem.GetGridObject(gridPosition).getUnitList();
    }

    public void removeUnitGridPosition(GridPosition gridPosition, Unit unit)
    {
        this.gridSystem.GetGridObject(gridPosition).removeUnit(unit);
    }

    

    public void UnitMovedFromGridPostion(GridPosition gridPositionFrom, GridPosition gridPositionTo, Unit unit)
    {
        GridLevel.Instance.removeUnitGridPosition(gridPositionFrom, unit);
        GridLevel.Instance.addUnitGridPosition(gridPositionTo, unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

    public bool isValidGridPosition(GridPosition gridPosition) => gridSystem.isValidGridPosition(gridPosition);

    public Vector3 GetWorldPostion(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

    public int GetWidth()=> gridSystem.GetWidth();
    public int GetHeight()=> gridSystem.GetHeight();
    public bool hasUnitOnGridPosition(GridPosition gridPosition)
    {
        //if (!gridSystem.isValidGridPosition(gridPosition))
        //{ throw err }
        return gridSystem.GetGridObject(gridPosition).hasAnyUnit();
       
    }


}
