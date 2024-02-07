using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask wallLayer;
    public float overlapRadius = 0.2f;

    public GameObject attackArea;

    private Rigidbody2D rb;
    private Camera mainCamera;

    public float timerDuration = 10f;
    private float currentTime;

    public TextMeshProUGUI timerText;

    public GameObject gameOverCanvas;
    public Image gameOverBG;
    public TextMeshProUGUI gameOverText;

    private bool gameIsPaused = false;
    private bool isAttacking = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        currentTime = timerDuration;

        gameOverCanvas.SetActive(false);

        attackArea.SetActive(false);
    }

    void FixedUpdate()
    {
        if (!gameIsPaused)
        {
            MovePlayer();
            UpdateTimer();
        }
    }

    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        Vector2 newPosition = rb.position + movement * moveSpeed * Time.deltaTime;

        Collider2D hitCollider = Physics2D.OverlapCircle(newPosition, overlapRadius, wallLayer);

        if (hitCollider != null && hitCollider.CompareTag("Wall"))
        {
            rb.MovePosition(rb.position);
        }
        else
        {
            rb.MovePosition(newPosition);

            if (mainCamera != null)
            {
                Vector3 newCameraPosition = new Vector3(newPosition.x, newPosition.y, mainCamera.transform.position.z);
                mainCamera.transform.position = newCameraPosition;
            }
        }

        rb.velocity *= 0.9f;
    }

    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            GameOver();
        }

        if (timerText != null)
        {
            timerText.text = "Timer: " + Mathf.Ceil(currentTime).ToString();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0f;

        gameOverCanvas.SetActive(true);
        gameOverBG.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);

        gameIsPaused = true;
    }

    public void AddTime(float timeToAdd)
    {
        currentTime += timeToAdd;

    }

    void Update()
    {
        if (!gameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;

        attackArea.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        attackArea.SetActive(false);

        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentTime -= 2f;
        }
    }
}
