using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionButtonGroupUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button buttonOn;
    [SerializeField] Button buttonOff;
    [SerializeField] TextMeshProUGUI textMeshProOn;
    [SerializeField] TextMeshProUGUI textMeshProOff;

    [SerializeField] bool isButtonOn;
    [SerializeField] bool isBusy;


    private BaseAction baseAction;

    public void setBaseAction(BaseAction baseAction)
    {
        textMeshProOn.SetText(baseAction.getActionName().ToUpper());
        textMeshProOff.SetText(baseAction.getActionName().ToUpper());
        this.baseAction = baseAction;

        buttonOff.onClick.AddListener(() =>
        {
            //setButtonOn();
            UnitActionSystem.Instance.setSelectedAction(baseAction);
        });
        
    }

    public void setButtonOn()
    {
        isButtonOn = true;
        buttonOn.gameObject.SetActive(true);
        buttonOff.gameObject.SetActive(false);
        //onButtonSwitchedOn!.Invoke(this, EventArgs.Empty);
       // UnitActionSystemUI.instance
    }

    public void setButtonOff()
    {
        isButtonOn = false;
        buttonOn.gameObject.SetActive(false);
        buttonOff.gameObject.SetActive(true);
    }

    public BaseAction GetBaseAction()
    {
        return baseAction;
    }

    public void setBusy() 
    { 
        isBusy = true; 
        buttonOn.GetComponent<Image>().color = Color.red;
        buttonOff.GetComponent<Image>().color = Color.red;
        buttonOn.enabled = false; buttonOff.enabled = false;
    }

    public void clearBusy() 
    {
        isBusy = false;
        buttonOn.GetComponent<Image>().color = Color.white;
        buttonOff.GetComponent<Image>().color = Color.white;
        buttonOn.enabled = true; buttonOff.enabled = true;

    }


}
