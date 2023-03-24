using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointSystem : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    [SerializeField]
    private int points;

    public void UpdatePoints()
    {
        points = GameData.CurrentPoints;

        pointsText.text = "Points: " + points;
    }
}
