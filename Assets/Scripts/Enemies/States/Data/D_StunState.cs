using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newStunStateData", menuName = "Data/State Data/Stun State")]

public class D_StunState : ScriptableObject
{
    public float stunnedSpeed = 0f;
    public float stunnedTime = 1.5f;
}
