using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem instance;

    private int turnNumber;
    public event EventHandler OnTurnEnded;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        NextTurn();
    }

    public void NextTurn()
    {
        turnNumber++;
        OnTurnEnded?.Invoke(this, EventArgs.Empty);
    }

    public int GetTurnNumber() { return turnNumber; }
}
