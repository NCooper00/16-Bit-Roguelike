using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newChaseStateData", menuName = "Data/State Data/Chase State")]

public class D_ChaseState : ScriptableObject
{
    public float ChaseSpeed = 3f;
    public float ChaseTime = 2f;

}
