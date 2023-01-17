using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    private Transform target;
    private float speed = 15f;
    private bool homing;

    private float rocketStrength = 15f;
    private float aliveTimer = 5f;

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        homing = true;
        Destroy(gameObject, aliveTimer);
    }

    private void Update()
    {
        if (homing && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (target != null)
        {
            if (col.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = col.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -col.contacts[0].normal;
                targetRigidbody.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
