using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackController : MonoBehaviour
{
    public GameObject crackPrefab;
    GameObject mainCamera;
    Transform crackContainer;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        crackContainer = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PlayerScript.submitCracks)
        {
            Vector3 position = mainCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            position.z = -1;
            GameObject crack = GameObject.Instantiate(crackPrefab, position, Quaternion.identity);
            crack.transform.parent = crackContainer;
        }
        if (PlayerScript.inMirror && !crackContainer.gameObject.activeSelf)
        {
            crackContainer.gameObject.SetActive(true);
        }
        else if (!PlayerScript.inMirror && crackContainer.gameObject.activeSelf)
        {
            crackContainer.gameObject.SetActive(false);
        }
    }
}
