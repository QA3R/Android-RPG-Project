using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UserInterface
{
    public class TurnTrackerUI : MonoBehaviour
    {
        private TextMeshProUGUI currentTurnTxt;
        private TextMeshProUGUI nextTurnTxt;

        // Start is called before the first frame update
        void Start()
        {
            //Subscribe to the EventHandlers OnStateEnd with UpdateTurnTracker

            //Get access to the current unit
            //Set the currentTurnTxt to the current unit name

            //Get the unitIndex + 1
            //Set the nextTurnTxt to the unitIndex +1
        }

        private void OnDisable()
        {
            //Unsubscribe to the EventHandler's OnStateEnd with UpdateTurnTracker
        }

        void UpdateTurnTracker()
        {
            //Get access to the current unit
            //Set the currentTurnTxt to the current unit name

            //Get the unitIndex + 1
            //Set the nextTurnTxt to the unitIndex +1
        }
    }

}
