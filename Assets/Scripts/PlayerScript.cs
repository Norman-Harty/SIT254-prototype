using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
	public float walkSpeed = 10f;
    public float jumpSpeed = 10f;
    public float maxJumpTime = 0.5f;
    float jumpTime = 0;
    int layer_mask; //layer for objects that can be collided with/jump off of
    Vector3 downDirection; //used for raycasts to detect if the player should be able to jump
    Rigidbody2D rigidBody; //the RigidBody component of the player

    GameObject mirrorText;
    GameObject buttonText;
    GameObject goalText;
    GameObject crackContainer;

    public static bool submitCracks;
    public static bool inMirror;

    // Start is called before the first frame update
    void Start()
    {
        layer_mask = LayerMask.GetMask("Ground");
        rigidBody = GetComponent<Rigidbody2D>();

        mirrorText = GameObject.FindGameObjectWithTag("MirrorText");
        if(mirrorText != null) mirrorText.SetActive(false);
        buttonText = GameObject.FindGameObjectWithTag("ButtonText");
        if (buttonText != null) buttonText.SetActive(false);
        goalText = GameObject.FindGameObjectWithTag("GoalText");
        if (goalText != null) goalText.SetActive(false);

        crackContainer = GameObject.FindGameObjectWithTag("CrackController");
        if (crackContainer != null) crackContainer = crackContainer.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        //prevent player from falling over
        if (rigidBody.rotation != 0) rigidBody.rotation = 0;
        if (rigidBody.angularVelocity != 0) rigidBody.angularVelocity = 0;

        //player movement
        if (!inMirror || submitCracks)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * walkSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * walkSpeed * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.W))
            {
                if (jumpTime > 0)
                {
                    transform.position += Vector3.up * jumpSpeed * Time.deltaTime;
                    jumpTime -= Time.deltaTime;
                }
            }
            else
            {
                downDirection = transform.TransformDirection(Vector3.down);
                if (Physics2D.Raycast(transform.position, downDirection, 1.5f, layer_mask))
                {
                    jumpTime = maxJumpTime;
                }
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mirror" && Input.GetKey(KeyCode.Space))
        {
            submitCracks = false;
            SceneManager.LoadScene("MirrorWorld");
            inMirror = true;
        }
        if (collision.gameObject.tag == "Button" && Input.GetKey(KeyCode.Space))
        {
            if (!inMirror)
            {
                submitCracks = true;
                SceneManager.LoadScene("MirrorWorld");
                inMirror = true;
            }
            else
            {
                submitCracks = false;
                SceneManager.LoadScene("OutsideWorld");
                inMirror = false;
                for (int i = 0; i < crackContainer.transform.childCount; i++)
                {
                    Destroy(crackContainer.transform.GetChild(i).gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mirror")
        {
            mirrorText.SetActive(true);
        }
        if (collision.gameObject.tag == "Button")
        {
            buttonText.SetActive(true);
        }
        if (collision.gameObject.tag == "Goal")
        {
            goalText.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mirror")
        {
            mirrorText.SetActive(false);
        }
        if (collision.gameObject.tag == "Button")
        {
            buttonText.SetActive(false);
        }
    }
}
