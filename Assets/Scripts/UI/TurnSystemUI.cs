using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textTurnNumber;
    [SerializeField] private Button endTurnButton;

    public static TurnSystemUI instance;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        updateTurnText();
        endTurnButton.onClick.AddListener(() => { onEndTurnButtonPressed(); });
        TurnSystem.instance.OnTurnEnded += TurnSystem_onEndTurnButtonEnds;
    }
    public void onEndTurnButtonPressed()
    {
        TurnSystem.instance.NextTurn();
    }

    public void TurnSystem_onEndTurnButtonEnds(object sender, EventArgs e)
    {
        updateTurnText();
    }

    private void updateTurnText()
    {
        textTurnNumber.SetText("Turn: "+TurnSystem.instance.GetTurnNumber().ToString());
    }
}
