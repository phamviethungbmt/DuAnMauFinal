using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D castFilter;
    [SerializeField] private float groundDistance = 0.5f;
    [SerializeField] private float wallDistance = 0.2f;
    [SerializeField] private float cellingDistance = 0.5f;

    CapsuleCollider2D touchingCol;
    Animator myAnimator;

    RaycastHit2D[] groundHits=new RaycastHit2D[5];
    RaycastHit2D[] wallHits=new RaycastHit2D[5];
    RaycastHit2D[] cellingHits=new RaycastHit2D[5];

    [SerializeField] private bool _isGrounded = true;
   public bool IsGrounded
    {
        get
        {
            return _isGrounded; 
        } 
        private set 
        {
            _isGrounded = value;
            myAnimator.SetBool(AnimationString.isGrounded, value); 
        }
    }
     [SerializeField] private bool _isOnWall = true;
   public bool IsOnWall
    {
        get
        {
            return _isOnWall; 
        } 
        private set 
        {
            _isOnWall = value;
            myAnimator.SetBool(AnimationString.isOnWall, value); 
        }
    }
    [SerializeField] private bool _isOnCelling = true;
    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    public bool IsOnCelling
    {
        get
        {
            return _isOnCelling; 
        } 
        private set 
        {
            _isOnCelling = value;
            myAnimator.SetBool(AnimationString.isOnCelling, value); 
        }
    }

    void Start()
    {
        touchingCol = GetComponent<CapsuleCollider2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance)>0;
        IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCelling = touchingCol.Cast(Vector2.up, castFilter, cellingHits, cellingDistance) > 0;
    }
}
