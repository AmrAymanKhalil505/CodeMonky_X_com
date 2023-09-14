using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private Animator unitAnimator;
    [SerializeField] int MaxMoveGridDistance = 4;

    float stoppingDistance = 0.05f;
    float rotationSpeed = 12.5f;
    float moveSpeed = 4f;

    private Vector3 targetPostion;
    protected override void Awake()
    {
        base.Awake();
        this.cost = 1;
        targetPostion = transform.position;
    }
    // Start is called before the first frame update

    private void Start()
    {
        unitAnimator = GetComponent<Animator>();
        unit = GetComponent<Unit>();
    }
    // Update is called once per frame
    void Update()
    {
        float stoppingDistance = 0.05f;

        if (!isActive)
        {
            return;
        }
        Vector3 moveDir = (targetPostion- transform.position).normalized;
        if (Mathf.Abs( Vector3.SignedAngle(moveDir, transform.forward, Vector3.up))>2f)
        {
            rotateTo(moveDir);
        }
        if (Vector3.Distance(targetPostion, transform.position) > stoppingDistance)
        {
            unitAnimator.SetBool("isWalking", true);
            TransitionTo(targetPostion);
        }
        else
        {
            unitAnimator.SetBool("isWalking", false);
            isActive = false;
            onActionComplete();
        }
    }

    public void TransitionTo(Vector3 targetPostion)
    {
        Vector3 moveDir = (targetPostion- transform.position).normalized;
        transform.position += moveDir*Time.deltaTime*moveSpeed;
        
    }

    public void rotateTo(Vector3 targetDir)
    {
        transform.forward = Vector3.Lerp(transform.forward, targetDir, Time.deltaTime*rotationSpeed);
    }
    public void Move(Vector3 tragetPostion, Action onMoveCompelete)
    {
        this.targetPostion = tragetPostion;
        isActive = true;
        this.onActionComplete = onMoveCompelete;
    }

    public override List<GridPosition> GetValidActionPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        for (int x = -MaxMoveGridDistance; x<=MaxMoveGridDistance; x++)
        {
            for (int z = -MaxMoveGridDistance; z<=MaxMoveGridDistance; z++)
            {
                GridPosition V = new GridPosition(x, z);
                GridPosition testGridPosition = V +  unit.GetGridPostion();

                if (!GridLevel.Instance.isValidGridPosition(testGridPosition))
                {
                    continue;
                }
                if (testGridPosition == unit.GetGridPostion())
                {
                    continue;
                }
                if (GridLevel.Instance.hasUnitOnGridPosition(testGridPosition))
                {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
              //  Debug.Log(testGridPosition);
            }
        }
        return validGridPositionList;
    }

    public void Move(GridPosition gridPosition, Action onMoveCompelete) => this.Move(GridLevel.Instance.GetWorldPostion(gridPosition), onMoveCompelete);

    public override string getActionName()
    {
        return "Move";
    }

    public override void TakeAction(BaseTakeActionParameters baseTakeActionParameters)
    {
        UniversalTakeActionParameters moveTakeActionParameters = (UniversalTakeActionParameters) baseTakeActionParameters;
        Move(moveTakeActionParameters.gridPosition, moveTakeActionParameters.onActionComplete);
    }
}
