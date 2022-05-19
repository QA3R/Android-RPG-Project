using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

#region Declarations

#region Interface
public interface States
{
    public void EnterState();
    public void ExecuteTurn();
    public void EndState();
}
#endregion

#region StateMachine Class
public class StateMachine
{
    public static States currentState;

    // Method which handles the changing of the turn-system.
    public void ChangeState(States newState)
    {
        // Refreshes the state machine when this gets called and sets it to the state that gets passed through.
        if (currentState == null)
        {
            //Sets the state machine to the new passed state.
            currentState = newState;
            // Calls the start functionality of the newly passed state.
            currentState.EnterState();
        }
        else if (currentState != null)
        {
            // Calls the end functionality of the current state.
            currentState.EndState();
            //Sets the state machine to the new passed state.
            currentState = newState;
            // Calls the start functionality of the newly passed state.
            currentState.EnterState();
        }
        else
        {
            // If the Statemachine returns empty for whatever reason.
            Debug.Log("currentState is null");
            return;
        }
    }

    public void Update()
    {
      /*  // If the State Machine returns that we are in a state, execute it's functionality.
        if (currentState != null)
        {
            currentState.ExecuteTurn();
        }*/
    }
}
#endregion

#endregion

#region States

#region StartCombatState
public class StartCombat : States
{
    TurnSystem turnSystem;
    public StartCombat (TurnSystem turnSystem)
    {
        this.turnSystem = turnSystem;
    }

    //handles all functionality when entering Combat such as initializing units' data (setting starting hp values, setting/resetting buffs/debuffs, etc...)
    public void EnterState()
    {
        Debug.Log("entered combat");
        Debug.Log("Initializing Units' data");
        ExecuteTurn();
    }

    public void ExecuteTurn() 
    {
        Debug.Log("Turn was executed");
        //EndState();
    }

    //Passes the state to the TurnSorting State (the state which determines which state to go to next).
    public void EndState()
    {
        Debug.Log("passing the current state to the TurnSorting state");
    }
}
#endregion

#endregion

public class TurnSystem : MonoBehaviour
{
    public GameObject UITurnSystem;
    public TurnSystemUI TurnUIScript;
    UnityEvent turnUpdatePlayer;

    // Start is called before the first frame update
    StateMachine stateMachine = new StateMachine();
    void Start()
    {
        stateMachine.ChangeState(new StartCombat(this));
        TurnUIScript = UITurnSystem.GetComponent <TurnSystemUI>();

        if (turnUpdatePlayer == null)
        {
            turnUpdatePlayer = new UnityEvent();
        }
        
        if (turnUpdatePlayer != null)
        {
            turnUpdatePlayer.AddListener(TurnUIScript.UpdatePlayerTurn);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //stateMachine.Update();
    }
}
