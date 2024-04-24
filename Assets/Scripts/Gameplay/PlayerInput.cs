using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player myPlayer;
    public Vector2 direction;
    public float angleToRotate;
    // Start is called before the first frame update
    void Start()
    {
        myPlayer = GetComponent<Player>();
        myPlayer.transform.position = Vector3.zero;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
                myPlayer.Attack();
            Debug.Log("Pass attack method to Player");
            
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
            direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            angleToRotate = Mathf.Atan2(direction.y - transform.position.y, direction.x - transform.position.x) * Mathf.Rad2Deg;
            //sending the input as coordinates to the player script (Move)
            myPlayer.Move(new Vector2(horizontalInput, verticalInput), angleToRotate, myPlayer.speed);
    }
}
