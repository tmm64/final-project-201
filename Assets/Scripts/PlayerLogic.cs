using UnityEngine;

public class PlayerLogic : MonoBehaviour
{   // Dash will be done by briefly turning on kinematics then turning it off again.
    // Jump will be a basic jump
    //Stomp is uhhhh basically the dash logic but downwards.
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

   void extraJump(Rigidbody rb)
   {
       // Implement extra jump logic here
   }
}
