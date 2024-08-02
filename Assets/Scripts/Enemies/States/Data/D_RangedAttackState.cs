using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newRangedAttackStateData", menuName = "Data/State Data/Ranged Attack State")]

public class D_RangedAttackState : ScriptableObject
{
    public float attackRadius = 0.5f;

    public float attackDamage = 10f;

    public LayerMask player;
}
