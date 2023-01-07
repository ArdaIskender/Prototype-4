using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb; // to make Player move
    private GameObject focalPoint; // to make Player movement based on where the camera is facing
    public float playerMoveSpeed = 5.0f; // to make Player move
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
}
