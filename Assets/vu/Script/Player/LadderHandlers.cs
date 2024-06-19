using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHandlers : MonoBehaviour
{
    [SerializeField] private LayerMask ladderLayer;

    [SerializeField] private Collider2D playerCollider;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool canClimbing;

 [SerializeField] private   Animator myAnimator;
    private bool IsLadder()
    {
        return playerCollider.IsTouchingLayers(ladderLayer);
    }
   public void onClimb(bool isColliderTop, bool isColliderBottom,float inputY,float inputX,float velocity)
    {

        if(inputY!=0&&IsLadder())
        {
            canClimbing = true;
            //rb.isKinematic = true;
            rb.gravityScale = 0;
            
        }
        if (!IsLadder())
        {
            DisableClimb();
        }
        if(canClimbing)
        {
            if((!isColliderTop&&inputY>=0)||(!isColliderBottom&&inputY>=0))
            {
              
                DisableClimb();
                
                return;
            }
            else if((isColliderTop&&inputY==0)||(isColliderBottom&&inputY==0))
            {
                myAnimator.speed = 0;
            }
            else
            {
                myAnimator.speed = 1;
            }
            myAnimator.SetBool(AnimationString.IsClimbing, canClimbing);
            rb.velocity = new Vector2(inputX*velocity, inputY * velocity);
           
            
        }
        else
        {

            myAnimator.speed = 1;

        }

    }
    public void DisableClimb()
    {
        canClimbing = false;
        //rb.isKinematic = false;
        rb.gravityScale = 1;
        myAnimator.SetBool(AnimationString.IsClimbing, canClimbing);
    }
}
