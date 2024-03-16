using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI orbsText;
    public GameObject door;
    public int count = 0;
    public int maxCount = 2;

    void Start(){
        count = 0;
    }

    void Update(){
        ActivateDoor();
    }
    
    public void UpdateOrbCount(int countToAdd){
        count += countToAdd;
        orbsText.text = count + " / " + maxCount;
    }

    public void ActivateDoor(){
        if(count == maxCount){
            door.gameObject.SetActive(true);
        }
    }
}
