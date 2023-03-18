using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targetList;
    private float spawnRate = 1;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private int score = 0;
    public bool isGameActive;
    public GameObject titleScreen;

    private int lives = 3;
    public TextMeshProUGUI livesText;
    public Slider volumeSlider;
    private AudioSource music;
    public Image pauseMenu;
    private bool isPauseActive = false;
    private float timeScale = 1;
    public GameObject lineTrail;
    private TrailRenderer trailRenderer;
    // Start is called before the first frame update
    void Start()
    {
        trailRenderer = lineTrail.gameObject.GetComponent<TrailRenderer>();
        trailRenderer.emitting = false;
        isGameActive = false;
        music = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        music.volume = volumeSlider.value;
        volumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive)
        {
            timeScale = timeScale == 0 ? 1 : 0;
            Time.timeScale = timeScale;
            isPauseActive = !isPauseActive;
            pauseMenu.gameObject.SetActive(isPauseActive);
        }

        if (Input.GetMouseButtonDown(0) && isGameActive && !isPauseActive)
        {
            trailRenderer.emitting = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            trailRenderer.emitting = false;
        }

        Vector3 mouse = Input.mousePosition;
        mouse.z = 10;
        lineTrail.transform.position = Camera.main.ScreenToWorldPoint(mouse);
    }

    private IEnumerator SpawnTargets()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int randomIndex = Random.Range(0, targetList.Count);
            Instantiate(targetList[randomIndex]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToSubtract)
    {
        lives -= livesToSubtract;
        lives = lives < 0 ? 0 : lives;
        livesText.text = "Lives: " + lives;

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTargets());
        UpdateScore(0);
        UpdateLives(0);
    }

    private void UpdateVolume()
    {
        Debug.Log("Slider value: " + volumeSlider.value);
        music.volume = volumeSlider.value;
    }
}
