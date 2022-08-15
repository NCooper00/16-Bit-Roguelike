using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    private float XCoord;
    private float YCoord;

    private float XTravelDist = 120f;
    private float YTravelDist = 67.5f;

    private float FloorToPlayerX;
    private float FloorToPlayerY;

    void Start() {
        XCoord = transform.position.x;
        YCoord = transform.position.y;
    }

    void OnTriggerExit2D(Collider2D objectInfo) {
        Player player = objectInfo.GetComponent<Player>();

        if (player != null) {
            DetermineNextPos(player.transform.position);
        }

    }

    void DetermineNextPos(Vector3 playerPos) {
        if (playerPos.x > XCoord + XTravelDist / 2) {
            MoveLeftOrRight(XTravelDist);
        } else if (playerPos.x < XCoord - XTravelDist / 2) {
            MoveLeftOrRight(-XTravelDist);
        } else if (playerPos.y > YCoord + YTravelDist / 2) {
            MoveUpOrDown(YTravelDist);
        } else if (playerPos.y - 1f < YCoord - (YTravelDist / 2)) {
            MoveUpOrDown(-YTravelDist);
        }
    }

    void MoveUpOrDown(float YDist) {
        transform.position = new Vector3(XCoord, YCoord + YDist, transform.position.z);
        YCoord = transform.position.y;
    }

    void MoveLeftOrRight(float XDist) {
        transform.position = new Vector3(XCoord + XDist, YCoord, transform.position.z);
        XCoord = transform.position.x;
    }
}