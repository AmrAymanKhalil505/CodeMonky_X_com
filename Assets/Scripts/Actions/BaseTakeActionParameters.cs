using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTakeActionParameters
{
    public Action onActionComplete;
    public BaseTakeActionParameters(Action onActionComplete)
    {
        this.onActionComplete = onActionComplete;
    }
}

public class MoveTakeActionParameters : BaseTakeActionParameters
{
    public GridPosition gridPosition;
    public MoveTakeActionParameters(GridPosition gridPosition,Action onActionComplete) : base(onActionComplete)
    {
        this.gridPosition = gridPosition;
    }
}

public class UniversalTakeActionParameters : BaseTakeActionParameters
{
    public GridPosition gridPosition;
    public UniversalTakeActionParameters(GridPosition gridPosition, Action onActionComplete) : base(onActionComplete)
    {
        this.gridPosition = gridPosition;
    }
}
