using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrackingCameraMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.ForceSoftware;
    public Vector2 hotSpot = Vector2.zero;

    GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        if (!PlayerScript.submitCracks)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            playerCamera.SetActive(false);
        }
        else 
        {
            gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //player movement
        if (Input.GetKey(KeyCode.W) && gameObject.transform.position.y < 19)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S) && gameObject.transform.position.y > 1)
        {
            transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.SetCursor(null, hotSpot, cursorMode);
            PlayerScript.inMirror = false;
            SceneManager.LoadScene("OutsideWorld");
        }
    }
}
