using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler OnSelectedActionDone;
    public event EventHandler OnSelectedActionStarted;

    [SerializeField] private LayerMask UnitLayerMask;
    [SerializeField] private Unit selectedUnit;
    
    
    private bool isBusy;
    private BaseAction selectedAction;

    public void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnitActionSystem! "+ transform+"-"+ Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start() { 
        setSelectedUnit(selectedUnit);
      
    }

    private void Update()
    {
        if (isBusy)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            if (TryHandleUnitSelection()) { return; }

            BaseAction baseAction = getSelectedAction();
            //print(getSelectedAction().getActionName());


            GridPosition gridPosition = GridLevel.Instance.GetGridPosition(MouseWorld.GetPostion());

            if (baseAction.isValidActionPosition(gridPosition))
            {
                if (selectedUnit.canAffordTakeAction(selectedAction))
                {
                    selectedUnit.TrySpendActionPointsToTakeAction(selectedAction);
                    setBusy();
                    baseAction.TakeAction(new UniversalTakeActionParameters(gridPosition, clearBusy));
                }
                
            }

            MouseWorld.playSelectPositionVFXAnimation();
        }
        
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (TryHandleUnitSelection()) { return; }

        //    MoveAction moveAction = selectedUnit.GetMoveAction();

        //    GridPosition gridPosition = GridLevel.Instance.GetGridPosition(MouseWorld.GetPostion());

        //    if (moveAction.isValidActionPosition(gridPosition))
        //    {
        //        setBusy();
        //        moveAction.Move(gridPosition, clearBusy);
        //    }

        //    MouseWorld.playSelectPositionVFXAnimation();
        //}
        //else if (Input.GetMouseButtonUp(1))
        //{
        //    setBusy();
        //    SpinAction spinAction = selectedUnit.GetSpinAction();
        //    spinAction.Spin(clearBusy);
        //}
    }

    private bool TryHandleUnitSelection()
    {
        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit raycastHit;
        if (Physics.Raycast(r, out raycastHit, float.MaxValue, UnitLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                if (unit== selectedUnit)
                {
                    return false;
                }
                setSelectedUnit(unit);
                return true;
                
                
            }
        }
        return false;
    }

    private void setSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        setSelectedAction(selectedUnit.GetMoveAction());
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public Unit getSelectedUnit() { return selectedUnit; }
    public void setSelectedAction(BaseAction action) { this.selectedAction = action; OnSelectedActionChanged?.Invoke(this, EventArgs.Empty); }
    public BaseAction getSelectedAction() { return this.selectedAction; }
    public void setBusy() { isBusy = true; OnSelectedActionStarted?.Invoke(this, EventArgs.Empty); }
    public void clearBusy() { isBusy = false; OnSelectedActionDone?.Invoke(this, EventArgs.Empty); }


}
