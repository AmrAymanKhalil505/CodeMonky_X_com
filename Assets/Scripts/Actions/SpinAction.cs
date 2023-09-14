using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    // Start is called before the first frame update

    [SerializeField] float spinAddAmount;
    private float totalSpinAmount;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        spinAddAmount = 90f* Time.deltaTime;
    }

    void Update()
    {
        if (!isActive)
        {
            return;
        }
        if (isActive)
        {
            totalSpinAmount += spinAddAmount;
            transform.eulerAngles += new Vector3(0, spinAddAmount, 0);
        }
        if (totalSpinAmount>360f)
        {
            isActive = false;
            onActionComplete();
        }
    }

    public void Spin(Action onSpinCompelete)
    {
        this.onActionComplete = onSpinCompelete;
        totalSpinAmount=0f;
        isActive = true;
    }

    public override string getActionName()
    {
        return "Spin";
    }

    public override void TakeAction(BaseTakeActionParameters baseTakeActionParameters)
    {
        this.onActionComplete = baseTakeActionParameters.onActionComplete;
        totalSpinAmount=0f;
        isActive = true;
    }

    public override List<GridPosition> GetValidActionPositionList()
    {
        return new List<GridPosition> {
            unit.GetGridPostion()
        };
    }
}
