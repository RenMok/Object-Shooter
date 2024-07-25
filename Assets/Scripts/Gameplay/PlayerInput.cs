using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private Player myPlayer;
    public Vector2 direction;
    public float angleToRotate;
    void Start()
    {
        myPlayer = GetComponent<Player>();
        myPlayer.transform.position = Vector3.zero;
    }

    private void Update()
    {
        if (myPlayer != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                myPlayer.Attack();
            }
            if (Input.GetMouseButtonDown(1))
            {
                myPlayer.UseNuke();
            }
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                myPlayer.Die();
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        direction = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        angleToRotate = Mathf.Atan2(direction.y - transform.position.y, direction.x - transform.position.x) * Mathf.Rad2Deg;
        myPlayer.Move(new Vector2(horizontalInput, verticalInput), angleToRotate, myPlayer.speed);
    }
}
