using UnityEngine;
using System.Collections;


public class IOHandler : MonoBehaviour
{

    public float camSpeed = 100;
    public float lookSpeed = 3; //controls up/down
    public float rotateSpeed = 100; //controls left/right
    private Rigidbody rb;

    private GameObject selectedUnit;

    // Controls camera
    // Controls:
    /*      W: Forward
     *      S: Backward
     *      A: Turn Left
     *      D: Turn Right
     *      Q: Look Up
     *      E: Look Down
     * */

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {

        // Moves camera forward and backwards in direction camera is facing
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(0, 0, moveVertical);
        rb.AddRelativeForce(movement * camSpeed);

        // Rotate camera up and down
        if (Input.GetKey(KeyCode.E) && (transform.rotation.x < 90))
        {
            transform.Rotate(lookSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.Q) && (transform.rotation.x > -90))
        {
            transform.Rotate(-lookSpeed, 0, 0);
        }

        // Rotate camera left and right
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0, Space.World);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0, Space.World);
        }
    }

    void Update()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hitInfo);
        Transform objectHit = hitInfo.transform;
    
        if (hitInfo.transform.tag == "Agent" && Input.GetMouseButtonDown(0))
        {
            selectedUnit = hitInfo.transform.gameObject;
            selectedUnit.SendMessage("Select", 1);
        }
        if (hitInfo.transform.tag == "Ground" && Input.GetMouseButtonDown(0))
        {
            if (selectedUnit.transform.tag == "Agent")
            {
                selectedUnit.SendMessage("Destination", hitInfo.point);
            }
        }
        if (hitInfo.transform.tag == "MovBlock" && Input.GetMouseButtonDown(0))
        {
            selectedUnit = hitInfo.transform.gameObject;
            
        }

        // Allow moving of Moving blocks with arrows
        if (selectedUnit.transform.tag == "MovBlock")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                selectedUnit.transform.Translate(new Vector3(0, 0, .05f));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                selectedUnit.transform.Translate(new Vector3(0, 0, -.05f));
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                selectedUnit.transform.Translate(new Vector3(-.05f, 0, 0));
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                selectedUnit.transform.Translate(new Vector3(.05f, 0, 0));
            }
        }
    }
}