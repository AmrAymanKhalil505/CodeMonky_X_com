using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator;
     
    GridPosition gridPosition;

    private MoveAction moveAction;
    private SpinAction spinAction;

    private BaseAction[] baseActionArray;

    [SerializeField] private int actionPoints = 2;
    [SerializeField] private int ACTION_POINTS_MAX = 2;

    public static EventHandler onActionPointsChanged;

    private void Awake()
    {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
    }

    private void Start()
    {
        unitAnimator = GetComponent<Animator>();

        gridPosition = GridLevel.Instance.GetGridPosition(transform.position);
        GridLevel.Instance.addUnitGridPosition(gridPosition, this);

        moveAction = GetComponent<MoveAction>();

        TurnSystem.instance.OnTurnEnded+= TurnSystem_OnTurnEnded;
    }

    private void Update()
    {

       
        if (GridLevel.Instance.GetGridPosition(transform.position) != gridPosition)
        {

            GridLevel.Instance.UnitMovedFromGridPostion(gridPosition, GridLevel.Instance.GetGridPosition(transform.position), this);
            this.gridPosition = GridLevel.Instance.GetGridPosition(transform.position);
        }


    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }

    public SpinAction GetSpinAction()
    {
        return spinAction;
    }
    //private void UnitMovedFromGridPostion()
    //{
    //    GridLevel.Instance.clearUnitGridPosition(myGridPosition);
    //    myGridPosition = GridLevel.Instance.GetGridPosition(transform.position);
    //    GridLevel.Instance.setUnitGridPosition(myGridPosition, this);
    //}

    public GridPosition GetGridPostion()
    {
        return gridPosition;
    }

    public BaseAction[] getBaseActionArray()
    {
        return baseActionArray;
    }

    public bool canAffordTakeAction(BaseAction action)
    {
        return action.GetActionCost()  <= this.actionPoints;
    }

    private void spendActionPoints(int amount)
    {
        setActionPoints(actionPoints - amount);
        onActionPointsChanged.Invoke(this, EventArgs.Empty);
    }

    public bool TrySpendActionPointsToTakeAction(BaseAction action)
    {
        if (canAffordTakeAction(action))
        {
            spendActionPoints(action.GetActionCost());
            return true;
        }
        return false;
    }
    public int GetActionPoints() { return actionPoints; }
    public void TurnSystem_OnTurnEnded(object sender, EventArgs e) 
    {
        setActionPoints( ACTION_POINTS_MAX); 
        
    }
    private void setActionPoints(int actionPoints)
    {
        this.actionPoints = actionPoints;
        onActionPointsChanged?.Invoke(this, EventArgs.Empty);
    }
}
