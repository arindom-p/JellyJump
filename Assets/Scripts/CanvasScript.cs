using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasScript : MonoBehaviour
{
    public GameObject panel;
    public GameObject cubController;
    public GameObject gameController;

    private void Start()
    {
        panel.SetActive(true);
        gameController.SetActive(false);
        cubController.SetActive(false);


    }

    public void GameStart()
    {
        panel.SetActive(false);
        gameController.SetActive(true);
        cubController.SetActive(true);
    }


}
