using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class InputHandler : MonoBehaviour
    {
        #region Singleton Implementation
        private static InputHandler instance;
        public static InputHandler Instance => instance;
        #endregion

        private Vector2 startPos;
        private Vector2 endPos;

        #region InputHandler delegates
        //Pings when swiping left on the device
        public delegate void SwipeLeft();
        public SwipeLeft swipeLeft;

        //Pings when swiping right on the device
        public delegate void SwipeRight();
        public SwipeRight swipeRight;
        #endregion

        private void Awake()
        {
            #region Singleton Implementation
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
            #endregion
        }

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
                            }
                            //Are we swiping left?
                            else if (x < 0)
                            {
                                swipeLeft?.Invoke();
                            }
                        }
                        //Is the vertical movement greater than the horizontal?
                        else
                        {
                            //Are we swiping up?
                            if (y > 0)
                            {

                            }
                            //Are we swiping down?
                            else if (y < 0)
                            {

                            }
                        }
                        #endregion

                        break;
                }



            }



        }
    }
}