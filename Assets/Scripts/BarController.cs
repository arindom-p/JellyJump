using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BarController : MonoBehaviour
{
    Rigidbody rb;
    private GameController gameController;
    private WaterController waterController;
    private static bool flag; //why static?????????????????????

    void Awake()
    {
        flag = true;
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        GameObject waterControllerObject = GameObject.FindWithTag("Water");
        if (gameControllerObject && waterControllerObject)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
            waterController = waterControllerObject.GetComponent<WaterController>();
        }
        else Debug.Log("GameController/WaterController is not found from BarController", this);

        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (transform.position.y < waterController.transform.position.y)
        {
            gameController.barPool.Push(gameObject);
            rb.isKinematic = false;
            gameObject.SetActive(false);
        }
    }

    public void SetVelocity(int directionFactor)
    {
        rb.velocity = new Vector3(3 * directionFactor * gameController.speedAmplify, 0, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
     //   print("barcollision enter " + collision.collider.gameObject.name);
        if(collision.collider.gameObject.name == "Cube") return;
        rb.isKinematic = true;
        flag = !flag;
        if (flag) gameController.CheckUpdate();
    }
}
