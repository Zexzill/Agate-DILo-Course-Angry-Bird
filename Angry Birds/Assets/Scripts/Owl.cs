using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Owl : Bird
{
    [SerializeField] private float _explodePower = 2000;
    [SerializeField] private float _explodeRadius = .5f;
    public bool _hasExplode = false;
    
    public void Explode(Collision2D col)
    {
        if(State == BirdState.HitSomething && !_hasExplode)
        {
            Collider2D[] colliderAround = Physics2D.OverlapCircleAll(transform.position, _explodeRadius);
            foreach(Collider2D obj in colliderAround)
            {
                Vector2 direction = obj.transform.position - transform.position;
                if(obj.GetComponent<Rigidbody2D>() != null)
                {
                    obj.GetComponent<Rigidbody2D>().AddForce(direction * _explodePower);
                }
            }
            RigidBody.AddForce(Vector2.up * (_explodePower / 2));
            _hasExplode = true;
            Debug.Log("boom");
        }
    }

    public override void OnCollision(Collision2D col)
    {
        Explode(col);
    }
}
