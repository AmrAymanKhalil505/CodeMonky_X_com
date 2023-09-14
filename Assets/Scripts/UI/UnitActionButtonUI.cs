using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button actionButton;

    
    public void setBaseAction(BaseAction baseAction)
    {
        textMeshPro.SetText (baseAction.getActionName().ToUpper());

        actionButton.onClick.AddListener(() =>
        {
            UnitActionSystem.Instance.setSelectedAction(baseAction);
        });
    }
}
