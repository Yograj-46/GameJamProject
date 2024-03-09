using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTEMP : MonoBehaviour
{
    public Animator ani;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            ani.SetTrigger("PowerUp");
        }
    }
}
