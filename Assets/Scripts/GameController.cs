using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.WSA.Input;

public class GameController : MonoBehaviour
{
    public GameObject barObject, cube, water;
    [NonSerialized]
    public Stack<GameObject> barPool ;
    private GameObject bar1, bar2;
    private float lastBarInY;
    public bool gameOver;
    [SerializeField]
    private Transform camTransform;
    public GameObject gameOverText;

    [NonSerialized]
    public int speedAmplify;

    void Start()
    {
        camTransform.position = new Vector3(0, 2, -10);
        gameOver = false;
        gameOverText.SetActive(false);
        barPool = new Stack<GameObject>();
        speedAmplify = 1;
        lastBarInY = 2;

        for (int i = 0; i < 6; i++)
        {
            bar1 = Instantiate(barObject);
            bar1.transform.localScale = new Vector3(5.0f, 0.3f, 1.5f);
            bar1.SetActive(false);
            barPool.Push(bar1);
        }
        bar1 = Instantiate(barObject, new Vector3(-2.5f, lastBarInY, 0.0f), Quaternion.identity);
        bar2 = Instantiate(barObject, new Vector3(2.5f, lastBarInY, 0.0f), Quaternion.identity);
    }

    private void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        CheckForGameOver();
    }

    public void CheckUpdate()
    {
        //  CheckForGameOver();
        
        if (!gameOver && cube.transform.position.y > lastBarInY) StartCoroutine("CameraUp");
        if (barPool.Count == 0) { print("BarPool empty"); return; }
        lastBarInY += 3;
        bar1 = barPool.Pop();
        bar2 = barPool.Pop();
        bar1.transform.position = new Vector3(-7, lastBarInY, 0);
        bar2.transform.position = new Vector3(7, lastBarInY, 0);
        bar1.GetComponent<BarController>().SetVelocity(1);
        bar2.GetComponent<BarController>().SetVelocity(-1);
        bar1.SetActive(true);
        bar2.SetActive(true);
    }

    IEnumerator CameraUp()
    {
        float res, startTime = Time.time;
        camTransform = Camera.main.transform;
        Vector3 startPosition = camTransform.position;
        Vector3 targetPosition = startPosition + Vector3.up * 3;
        while (true)
        {
            res = (Time.time - startTime) * 3;
            camTransform.position = Vector3.Lerp(startPosition, targetPosition, res);
            if (res >= 1) break;
            yield return null;
        }
        water.transform.position = new Vector3(0, cube.transform.position.y - 3, 0);
    }

    private void CheckForGameOver()
    {
        if (gameOver || cube.activeSelf) return;

        gameOver = true;
        gameOverText.SetActive(true);
        water.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
