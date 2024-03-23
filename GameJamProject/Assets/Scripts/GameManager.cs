using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static TextMeshProUGUI orbsText;
    public GameObject door;
    public static int count = 0, maxCount = 2;

    void Start(){
        count = 0;
        orbsText = GameObject.Find("OrbCount").GetComponentInChildren<TextMeshProUGUI>();
    }
    
    public static void UpdateOrbCount(int countToAdd){
        count += countToAdd;
        orbsText.text = count + " / " + maxCount;
    }

    public void ActivateDoor(){
        if(count == maxCount){
            door.gameObject.SetActive(true);
        }
    }
}
