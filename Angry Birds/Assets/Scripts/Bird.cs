using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bird : MonoBehaviour
{
    public enum BirdState
    {
        Idle,
        Thrown,
        HitSomething
    }

    public GameObject Parent;
    public Rigidbody2D RigidBody;
    public CircleCollider2D Collider;

    public UnityAction OnBirdDestroyed = delegate
    {

    };
    public UnityAction<Bird> OnBirdShot = delegate
    {

    };

    public BirdState State
    {
        get
        {
            return _state;
        }
    }

    private BirdState _state;
    private float _minVelocity = .05f;
    private bool _flagDestroy = false;

    private void Start()
    {
        //mengubah bird jadi kinematic agar tidak terpengaruh gravitasi dan set state menjadi idle sebagai indicator event
        RigidBody.bodyType = RigidbodyType2D.Kinematic;
        Collider.enabled = false;
        _state = BirdState.Idle;
    }

    private void FixedUpdate()
    {
        if (_state == BirdState.Idle && RigidBody.velocity.sqrMagnitude >= _minVelocity)
        {
            _state = BirdState.Thrown;
        }

        bool isThrown = _state == BirdState.Thrown;
        bool isHitSomething = _state == BirdState.HitSomething;

        if (isThrown || isHitSomething && RigidBody.velocity.sqrMagnitude < _minVelocity && !_flagDestroy)
        {
            _flagDestroy = true;
            StartCoroutine(DestroyAfter(2));
        }
    }

    public virtual void OnTap()
    {

    }

    public virtual void OnCollision(Collision2D col)
    {

    }

    private void OnDestroy()
    {
        if(_state == BirdState.Thrown || _state == BirdState.HitSomething)
        {
            OnBirdDestroyed();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _state = BirdState.HitSomething;
        OnCollision(collision);
    }

    private IEnumerator DestroyAfter(float second)
    {
        yield return new WaitForSeconds(second);
        Destroy(gameObject);
    }

    public void MoveTo(Vector2 target, GameObject parent)
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = target;
    }

    public void  Shoot(Vector2 velocity, float distance, float speed)
    {
        Collider.enabled = true;
        RigidBody.bodyType = RigidbodyType2D.Dynamic;
        RigidBody.velocity = velocity * speed * distance;
        OnBirdShot(this);
    }
}
