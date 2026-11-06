using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// LECTURE 07 ka GameManager. Sirf 3 cheezen "NEW" hain (5 levels ke liye):
//   1) timeLimit + timerText   2) Update() mai countdown   3) NextLevel()
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] int targetScore = 10, lives = 3;
    [SerializeField] float respawnDelay = 0.6f, spawnProtection = 1.5f;
    [SerializeField] Health player;
    [SerializeField] Transform respawnPoint;
    [SerializeField] TMP_Text scoreText, livesText;
    [SerializeField] GameObject winPanel, losePanel;

    [SerializeField] float timeLimit = 0f;      // NEW: 0 = koi timer nahi
    [SerializeField] TMP_Text timerText;        // NEW

    int score; float timeLeft; bool ended;

    void Awake() => Instance = this;
    void Start() { timeLeft = timeLimit; UpdateUI(); }

    void Update()                                // NEW: countdown
    {
        if (timeLimit <= 0f || ended) return;
        timeLeft -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.CeilToInt(Mathf.Max(timeLeft, 0f));
        if (timeLeft <= 0f) Lose();
    }

    public void AddScore(int amount) { score += amount; UpdateUI(); if (score >= targetScore) Win(); }

    public void UpdateUI()
    {
        scoreText.text = $"Score: {score} / {targetScore}";
        livesText.text = $"Lives: {lives}   Health: {Mathf.Max(player.Current, 0)} / {player.Max}";
    }

    public void PlayerDied() { lives--; UpdateUI(); if (lives <= 0) Lose(); else StartCoroutine(Respawn()); }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        Rigidbody body = player.GetComponent<Rigidbody>();
        body.position = respawnPoint.position; body.linearVelocity = Vector3.zero;
        player.Restore(spawnProtection);
    }

    void Win()  { ended = true; winPanel.SetActive(true);  Time.timeScale = 0f; }
    void Lose() { ended = true; losePanel.SetActive(true); Time.timeScale = 0f; }

    public void Restart() { Time.timeScale = 1f; SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }

    public void NextLevel()                      // NEW: Win panel ka button isay call karega
    {
        Time.timeScale = 1f;
        int next = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(next);            // aakhri level ke baad wapas Level 1
    }
}
