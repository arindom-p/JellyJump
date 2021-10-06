using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    public GameController gameController;
    private Rigidbody rb;
    private void Awake()
    {
        transform.localScale = new Vector3(1.5f, 1.0f, 2.0f);
        transform.Rotate(5.0f, 1.0f, 1.0f, Space.Self);
        transform.position = new Vector3(0, -2, 0);
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 1f, 0f);
    }

    private void SpeedUp(int amplify)
    {
        rb.velocity = new Vector3(0f, 1f, 0f) * amplify;
    }
}
