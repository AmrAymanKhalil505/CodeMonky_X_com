using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit;
    protected bool isActive;
    protected Action onActionComplete;
    protected int cost = 0;


    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract string getActionName();
    public abstract void TakeAction(BaseTakeActionParameters baseTakeActionParameters);
    public virtual bool isValidActionPosition(GridPosition gridPosition)
    {
        return GetValidActionPositionList().Contains(gridPosition);
    }
    public abstract List<GridPosition> GetValidActionPositionList();

    public virtual int GetActionCost()
    {
        return cost;
    }
}
