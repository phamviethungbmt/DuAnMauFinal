using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class layer : MonoBehaviour
{
    [SerializeField] protected LayerMask layerMask;
    [SerializeField] protected Transform pointTransform;

    protected Collider2D colliderTarget;

    public abstract void HandleCollider2D();

    public abstract bool IsCollider2D();

    public abstract Collider2D OnCollider2D();
}
