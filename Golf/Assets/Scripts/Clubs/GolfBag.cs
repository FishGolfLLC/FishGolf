using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GolfBag : MonoBehaviour
{
    
    //fuck you dylan

    public List<Club> clubs = new List<Club>();

    public int index;
    public Club currentClub;

    public void OnGolfBagScroll(InputAction.CallbackContext context) {
        if (context.performed) {
            float i = context.ReadValue<float>();

            if (i > 0) { // SCROLL UP

                index = index + 1 > clubs.Count - 1 ? 0 : index + 1;

            }
            else if (i < 0) {

                index = index - 1 < 0 ? clubs.Count - 1 : index - 1;
            }
            Debug.Log(i);
            currentClub = clubs[index];
        }
        

    }


}
