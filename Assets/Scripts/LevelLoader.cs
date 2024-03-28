using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Player playerModel;
    public Enemy enemy;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
       player = Instantiate(playerModel);
       enemy = new Enemy();
    }

    // Update is called once per frame
    void Update()
    {
        player.Move();
        
    }
}
