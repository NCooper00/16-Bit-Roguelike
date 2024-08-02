using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtPlayer : MonoBehaviour
{

    private GameObject player;
    private Transform playerPos;

    public float offset = 0f;

    private void Awake() {

        player = GameObject.Find("PLAYER");
        playerPos = player.GetComponent<Transform>();
    }

    private void FixedUpdate() {
        Vector3 difference = transform.position - playerPos.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);
    }

    private void OnDrawGizmos() {
        
    }
}
