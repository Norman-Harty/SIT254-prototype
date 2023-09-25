using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject player;
    GameObject crackingUI;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        crackingUI = GameObject.FindGameObjectWithTag("CrackingUI");
        if (crackingUI != null && PlayerScript.submitCracks) Destroy(crackingUI);
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        Vector3 newPosition;
        if (playerY < 0)
        {
            newPosition = new Vector3(playerX, 0, -10f);
            transform.position = newPosition;
        }
        else 
        {
            newPosition = new Vector3(playerX, playerY, -10f);
            transform.position = newPosition;
        }
    }
}
