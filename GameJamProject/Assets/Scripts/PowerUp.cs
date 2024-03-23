using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 public enum PowerUpType{
        None,
        MovementSpeed,
        ExtraArmor,
        Attack,
        InstantHealing
    }
public class PowerUp : MonoBehaviour
{
   public static PowerUpType powerUpType;
}
