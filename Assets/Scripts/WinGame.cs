using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinGame : MonoBehaviour
{
    public GameObject winText;

    void Start()
    {
        StartCoroutine(CheckForWin());
    }

    IEnumerator CheckForWin()
    {
        yield return new WaitForSeconds(0.5f);

        GameData.LiveEnemies.RemoveAll(item => item == null);

        if (GameData.CurrentWave == 3)
        {
            Time.timeScale = 0;
            winText.SetActive(true);
        }
        else
            StartCoroutine(CheckForWin());
    }
}
