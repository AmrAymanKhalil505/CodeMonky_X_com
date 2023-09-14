using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionSystemUI : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainerTransform;
    [SerializeField] private TextMeshProUGUI ActionPointsText;
    

    public static UnitActionSystemUI instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        UnitActionSystem.Instance.OnSelectedUnitChanged +=UnitActionSystem_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnSelectedActionChanged += UnitActionButtonGroupUI_OnSelectedActionChanged;
        UnitActionSystem.Instance.OnSelectedActionDone+=UnitActionSystem_OnSelectedActionDone;
        UnitActionSystem.Instance.OnSelectedActionStarted+=UnitActionSystem_OnSelectedActionStarted;
        Unit.onActionPointsChanged+= Unit_OnActionPointsChanged;

        CreateUnitActionButtons();
        updateActionPointsText();
    }

   





    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateUnitActionButtons()
    {
        foreach (Transform t in actionButtonContainerTransform)
        {
            Destroy(t.gameObject);
        }


        Unit SelectedUnit = UnitActionSystem.Instance.getSelectedUnit();
        foreach (BaseAction baseAction in SelectedUnit.getBaseActionArray())
        {
            Transform buttom =  Instantiate(actionButtonPrefab, actionButtonContainerTransform);
            UnitActionButtonGroupUI ActionButtomUI=buttom.GetComponent<UnitActionButtonGroupUI>();
            ActionButtomUI.setBaseAction(baseAction);
        }
        setButtonsRightStatus();


    }

    private void UnitActionSystem_OnSelectedUnitChanged (object sender, EventArgs e)
    {
        CreateUnitActionButtons();
        updateActionPointsText();
    }


    public void setCurrentButtonOn (UnitActionButtonGroupUI ActionButtonGroup)
    {
        setButtonsRightStatus();
        if (ActionButtonGroup != null)
        {
            ActionButtonGroup.setButtonOn();
        }
    }
    public void setButtonsRightStatus()
    {
        foreach (Transform t in actionButtonContainerTransform)
        {
            if (t.GetComponent<UnitActionButtonGroupUI>().GetBaseAction()!=UnitActionSystem.Instance.getSelectedAction())
            { 
                t.GetComponent<UnitActionButtonGroupUI>().setButtonOff();
            }
            else
            {
                t.GetComponent<UnitActionButtonGroupUI>().setButtonOn();
            } 
            
        }
    }
    private void UnitActionButtonGroupUI_onButtonSwitchedOn(object sender, EventArgs e)
    {
        //updateActionPointsText();
        CreateUnitActionButtons();
    }
    private void UnitActionButtonGroupUI_OnSelectedActionChanged(object sender, EventArgs e)
    {
        //updateActionPointsText();
        setButtonsRightStatus();
    }
    private void UnitActionSystem_OnSelectedActionStarted(object sender, EventArgs e)
    {
        //updateActionPointsText();
        foreach (Transform t in actionButtonContainerTransform)
        {
            t.GetComponent<UnitActionButtonGroupUI>().setBusy();
        }
        
    }

    private void UnitActionSystem_OnSelectedActionDone(object sender, EventArgs e)
    {
        //updateActionPointsText();
        foreach (Transform t in actionButtonContainerTransform)
        {
            t.GetComponent<UnitActionButtonGroupUI>().clearBusy();
        }
    }

    private void updateActionPointsText()
    {
        int curActionPoints = UnitActionSystem.Instance.getSelectedUnit().GetActionPoints();
        ActionPointsText.SetText(curActionPoints.ToString());
    }

    private void Unit_OnActionPointsChanged(object sender, EventArgs e)
    {
        updateActionPointsText();
    }

}
