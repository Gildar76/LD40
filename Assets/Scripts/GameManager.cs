using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState
{
    Menu, Running, GameOver, Instructions, Credits

}
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private int score;
    private int storedFood;
    private int playerHealth;
    public GameObject player;


    public GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = this;
            }
            return instance;
        }


    }

    public int Score
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int StoredFood
    {
        get
        {
            return storedFood;
        }

        set
        {
            storedFood = value;
        }
    }

    public int PlayerHealth
    {
        get
        {
            return playerHealth;
        }

        set
        {
            playerHealth = value;
        }
    }

    public void Restart()
    {
        score = 0;
        storedFood = 0;
        player.GetComponent<PlayerController>().Health = 100;


    }


}
