using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float moveInterval = 0.15f;
    public GameObject bodyPrefab;
    public AudioClip eatSFX;
    public AudioClip wallSFX;

    private Vector2 direction = Vector2.right;
    private Vector2 nextDirection = Vector2.right;
    private List<Transform> body = new List<Transform>();
    private float timer;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)  nextDirection = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)    nextDirection = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)  nextDirection = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)   nextDirection = Vector2.right;

        if (Input.GetKeyDown(KeyCode.UpArrow)    && direction != Vector2.down)  nextDirection = Vector2.up;
        if (Input.GetKeyDown(KeyCode.DownArrow)  && direction != Vector2.up)    nextDirection = Vector2.down;
        if (Input.GetKeyDown(KeyCode.LeftArrow)  && direction != Vector2.right) nextDirection = Vector2.left;
        if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)  nextDirection = Vector2.right;

        timer += Time.deltaTime;
        if (timer >= moveInterval)
        {
            timer = 0;
            Move();
        }
    }

    void Move()
    {
        direction = nextDirection;

        Vector2 newPos = (Vector2)transform.position + direction;

        // Colision con cuerpo
        foreach (Transform segment in body)
        {
            if ((Vector2)segment.position == newPos)
            {
                audioSource.PlayOneShot(wallSFX);
                GameManager.Instance.GameOver();
                return;
            }
        }

        // Mover cuerpo
        for (int i = body.Count - 1; i > 0; i--)
            body[i].position = body[i - 1].position;

        if (body.Count > 0)
            body[0].position = transform.position;

        transform.position = new Vector3(newPos.x, newPos.y, 0);
    }

    public void Grow()
    {
        audioSource.PlayOneShot(eatSFX);
        GameObject segment = Instantiate(bodyPrefab);
        segment.transform.position = body.Count > 0
            ? body[body.Count - 1].position
            : transform.position;
        body.Add(segment.transform);
        GameManager.Instance.AddScore(10);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            audioSource.PlayOneShot(wallSFX);
            GameManager.Instance.GameOver();
        }
        if (other.CompareTag("Food"))
        {
            Grow();
            Destroy(other.gameObject);
            FindObjectOfType<FoodSpawner>().SpawnFood();
        }
    }
}
