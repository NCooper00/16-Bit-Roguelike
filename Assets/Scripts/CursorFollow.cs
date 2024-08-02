using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    public GameObject FollowPoint;

    private GameObject player;
    private Transform playerPos;

    public float offset = 0f;

    private Transform followTransform;

    private void Awake() {
        followTransform = FollowPoint.GetComponent<Transform>();

        player = GameObject.Find("PLAYER");
        playerPos = player.GetComponent<Transform>();
    }

    private void FixedUpdate() {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        followTransform.rotation = Quaternion.Euler(0f, 0f, rotation_z + offset);
    }

    private void OnDrawGizmos() {
        
    }
}
