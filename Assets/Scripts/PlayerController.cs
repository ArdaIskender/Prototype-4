using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; // to make Player move
    private GameObject focalPoint; // to make Player movement based on where the camera is facing
    public float playerMoveSpeed = 5.0f; // to make Player move
    public float powerupStrength = 15f; // when collided with enemy to send them away with powerup
    public bool hasPowerup = false;
    public GameObject powerupIndicator;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical"); // to make Player move
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * playerMoveSpeed); // to make player move + based on where the camera is facing
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            powerupIndicator.SetActive(true);
            hasPowerup= true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - playerRb.transform.position;

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

            Debug.Log("Collided with " + collision.gameObject.name + " and " + hasPowerup);
        }
    }
}
