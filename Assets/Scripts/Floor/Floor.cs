using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{

    private List<Vector3> DiscoveredTiles = new List<Vector3>();

    private Vector3 CurrentPos;
    private Vector3 BottomLeftPos;

    private float XCoord;
    private float YCoord;

    private float XTravelDist = 120f;
    private float YTravelDist = 67.5f;


    // TODO: Make prefab tables with different prefabs that are weighted for rarity
    [SerializeField]
    private GameObject Coin;



// FOR NEW TILE (start of game)
    // On awake send Vector3 position of tile to array
    // Run a function to determine what should spawn in the tile if anything (structures, stray enemies, ect...)

// Narrow down array of positions saved to only show the nearest 25 (test with 9)

// FOR MOVING TILE
    // Deactivate all objects in previous tile position
    // Relocate tile to new position
        // Check if position has been saved (IsSaved())
            // IF HAS BEEN SAVED: Wake up all objects within the new tile position 
            // IF NOT: Run function to determine what should spawn in the tile if anything
                // Save new Vector3 position of tile to array 

    void Awake() {
        XCoord = transform.position.x;
        YCoord = transform.position.y;

        CurrentPos = transform.position;
        BottomLeftPos = new Vector3(CurrentPos.x - 20f, CurrentPos.y - 11.25f, CurrentPos.z);

        AddToList(CurrentPos);
    }

    void OnTriggerExit2D(Collider2D objectInfo) {
        Player player = objectInfo.GetComponent<Player>();

        if (player != null) {

            // Function to detect all GameObjects in tile and deactivate them

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

        CurrentPos = transform.position;
        BottomLeftPos = new Vector3(CurrentPos.x - 20f, CurrentPos.y - 11.25f, CurrentPos.z);

        YCoord = transform.position.y;

        // Check if transform.position is saved
            // If (IsSaved()) wake up GameObjects
            // Else run function to spawn new stuff

        if (CheckIfSaved(CurrentPos)) {
            // Activate GameObjects
            Debug.Log("We have been here");
        } else {
            // Spawn new stuff
            Debug.Log("We have not been here");
            AddToList(CurrentPos);

            InstantiatePrefabs(BottomLeftPos, Coin);
        }

    }

    void MoveLeftOrRight(float XDist) {
        transform.position = new Vector3(XCoord + XDist, YCoord, transform.position.z);

        CurrentPos = transform.position;
        BottomLeftPos = new Vector3(CurrentPos.x - 20f, CurrentPos.y - 11.25f, CurrentPos.z);

        XCoord = transform.position.x;

        // Check if transform.position is saved
            // If (IsSaved()) wake up GameObjects
            // Else run function to spawn new stuff

        if (CheckIfSaved(CurrentPos)) {
            // Activate GameObjects
            Debug.Log("We have been here");
        } else {
            // Spawn new stuff
            Debug.Log("We have not been here");
            AddToList(CurrentPos);

            InstantiatePrefabs(BottomLeftPos, Coin);
        }

    }

    bool CheckIfSaved(Vector3 position) {
        for (int i = 0; i < DiscoveredTiles.Count; i++) {
            if (position == DiscoveredTiles[i]) {
                return true;
            }
        }
        return false;
    }

    void AddToList(Vector3 position) {
        DiscoveredTiles.Add(position);
    }

    void InstantiatePrefabs(Vector3 botLeftPos, GameObject prefab) {
        GameObject newPrefab = Instantiate(prefab, botLeftPos, Quaternion.identity);
    }
}