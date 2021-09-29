using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public float Health = 50;

    public UnityAction<GameObject> OnEnemyDestroyed = delegate
    {

    };

    private bool _isHit = false;

    private void OnDestroy()
    {
        if(_isHit)
        {
            OnEnemyDestroyed(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.GetComponent<Rigidbody2D>() == null)
        {
            return;
        }

        if(col.gameObject.tag == "Bird")
        {
            _isHit = true;
            Destroy(gameObject);
        }
        else if(Health <= 0)
        {
            _isHit = true;
            Destroy(gameObject);
        }
    }
}
