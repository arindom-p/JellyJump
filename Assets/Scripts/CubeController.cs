using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public GameObject gameControllerObject;
    public Transform water;

    public bool attached = false;

    private GameController gameController;
    private Rigidbody rb;

    void Awake()
    {
        gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        gameController = gameControllerObject.GetComponent<GameController>();
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }

    private void Update()
    {
        if (!gameController.gameOver && attached && Input.GetKeyDown(KeyCode.LeftControl))
            rb.AddForce(0, 8.5f, 0, ForceMode.VelocityChange);

        if (water.position.y > transform.position.y)
            gameObject.SetActive(false);
    }

    private void OnCollisionStay(Collision collision) => attached = true;
    private void OnCollisionEnter(Collision collision) => attached = true;
    private void OnCollisionExit(Collision collision) => attached = false;
}
