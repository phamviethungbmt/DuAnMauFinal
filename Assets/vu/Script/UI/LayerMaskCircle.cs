using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskCircle : layer
{
    [Range(0, 100)]
    [SerializeField] private float radius;

    public override void HandleCollider2D()
    {
        colliderTarget = Physics2D.OverlapCircle(pointTransform.transform.position, radius, layerMask);
    }

    public override bool IsCollider2D()
    {
        return colliderTarget != null;
    }

    public override Collider2D OnCollider2D()=>colliderTarget;

    public void OnDrawGizmos()
    {
        if(pointTransform!=null)
        {
            Gizmos.color = Color.yellow;
            if(colliderTarget != null)
            {
                Gizmos.color = Color.red;
            }
            Gizmos.DrawSphere(pointTransform.position, radius);
        }
    }
}
