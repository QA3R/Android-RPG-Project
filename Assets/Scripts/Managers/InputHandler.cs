using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class InputHandler : MonoBehaviour
    {
        private Vector2 startPos;
        private Vector2 endPos;

        public delegate void SwipeLeft();
        public SwipeLeft swipeLeft;

        public delegate void SwipeRight();
        public SwipeRight swipeRight;

        // Update is called once per frame
        void Update()
        {
            //Did the InputHandler register more than zero touches on the screen?
            if (Input.touchCount > 0)
            {
                //Only check the first registered touch on the screen
                Touch touch = Input.GetTouch(0);

                //Which phase of the touch process are we in?
                switch (touch.phase)
                {
                    //Assign the startPos when we start touching the screen
                    case TouchPhase.Began:

                        startPos = touch.position;

                        break;

                    //Finger dragging phase
                    case TouchPhase.Moved:

                        break;

                    //Calculated the direction of the swipe
                    case TouchPhase.Ended:

                        //Set the endPos to the location we lifted our finger
                        endPos = touch.position;

                        #region Direction Calculations
                        //Get the horizontal and vertical direction
                        float x = endPos.x - startPos.x;
                        float y = endPos.y - startPos.y;

                        //Is the horizontal movement greater than the vertical?
                        if ((Mathf.Abs(x)) - (Mathf.Abs(y)) > 0)
                        {
                            //Are we swiping right?
                            if (x > 0)
                            {
                                swipeRight?.Invoke();
                                Debug.Log("Right swipe detected");
                            }
                            //Are we swiping left?
                            else if (x < 0)
                            {
                                swipeLeft?.Invoke();
                                Debug.Log("Left swipe detected");
                            }
                        }
                        //Is the vertical movement greater than the horizontal?
                        else
                        {
                            //Are we swiping up?
                            if (y > 0)
                            {
                                Debug.Log("Up swipe detected");
                            }
                            //Are we swiping down?
                            else if (y < 0)
                            {
                                Debug.Log("Down swipe detected");
                            }
                        }
                        #endregion

                        break;
                }



            }



        }
    }
}