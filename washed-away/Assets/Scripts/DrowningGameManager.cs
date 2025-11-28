using UnityEngine;

public class DrowningGameManager : MonoBehaviour
{
    public static DrowningGameManager Instance; // simple singleton

    [HideInInspector] public int totalPeople;
    [HideInInspector] public int savedPeople;
    [HideInInspector] public bool gameOver;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void RegisterPerson()
    {
        totalPeople++;
    }

    public void PersonSaved()
    {
        if (gameOver) return;

        savedPeople++;

        if (savedPeople >= totalPeople)
        {
            Win();
        }
    }

    public void PersonDrowned()
    {
        if (gameOver) return;

        gameOver = true;
        Debug.Log("GAME OVER: Someone drowned :(");
        // Here you can show a UI panel, restart button, etc.
    }

    void Win()
    {
        gameOver = true;
        Debug.Log("YOU WIN: Everyone was saved!");
        // Show win UI here
    }
}
