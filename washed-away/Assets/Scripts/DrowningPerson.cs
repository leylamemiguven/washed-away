using UnityEngine;

public class DrowningPerson : MonoBehaviour
{
    [Header("Drowning Settings")]
    public float timeToDrown = 5f;     // total HP / countdown time

    private float timer;               // current HP
    private bool isSaved = false;
    private bool isPlayerInside = false;

    private SpriteRenderer sprite;

    void Start()
    {
        timer = timeToDrown;
        sprite = GetComponent<SpriteRenderer>();

        // Register with the DrowningGameManager
        DrowningGameManager.Instance.RegisterPerson();
    }

    void Update()
    {
        // If game over, freeze
        if (DrowningGameManager.Instance.gameOver)
            return;

        // If not saved yet, decrease HP
        if (!isSaved)
        {
            timer -= Time.deltaTime;

            UpdateColor(); // === HP COLOR FEEDBACK ===

            // If HP reaches 0 → they drown → lose
            if (timer <= 0f)
            {
                DrowningGameManager.Instance.PersonDrowned();
                Destroy(gameObject);
            }
        }

        // Rescue interaction
        if (isPlayerInside && !isSaved && Input.GetKeyDown(KeyCode.E))
        {
            SavePerson();
        }
    }

    void SavePerson()
    {
        isSaved = true;

        // Notify DrowningGameManager
        DrowningGameManager.Instance.PersonSaved();

        // You can add particle effects or sound here
        Destroy(gameObject, 0.1f);
    }

    void UpdateColor()
    {
        if (sprite == null) return;

        float t = timer / timeToDrown;

        // Green → Yellow → Red depending on remaining HP
        if (t > 0.6f)
            sprite.color = Color.green;
        else if (t > 0.3f)
            sprite.color = Color.yellow;
        else
            sprite.color = Color.red;
    }

    // Player enters rescue range
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            Debug.Log("Press E to rescue!");
        }
    }

    // Player leaves rescue range
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
