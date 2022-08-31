using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    [Header("References")]
    public SoundManager sndManager;
    public GameObject inControlControls;

    [Header("Game Object References")]
    public Ghost[] ghosts;
    public Pacman pacman;
    public Transform pellets;

    [Header("UI")]
    public Text gameOverText;
    public Text scoreText;
    public Text livesText;
    public Transform gameOverButtons;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }
    private Coroutine introCoroutine;

    private void Start()
    {
        NewGame();
    }

    private IEnumerator IntroSequence()
    {
        sndManager.PlayIntro();
        PauseGame();
        yield return new WaitForSeconds(5f);
        sndManager.PlayBackgroundMusicStandard();
        ResumeGame();
    }

    public void NewGame()
    {
        SetScore(0);
        SetLives(1);
        NewRound();
    }
    private void NewRound()
    {
        gameOverText.enabled = false;
        HideMenuButtons();

        foreach (Transform pellet in pellets) {
            pellet.gameObject.SetActive(true);
        }
        
        ResetState();
        EnableInControlJoystick();
    }

    private void PlayIntroSequence()
    {
        introCoroutine = StartCoroutine(IntroSequence());
    }

    private void ResetState()
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].ResetState();
        }

        pacman.ResetState();
        PlayIntroSequence();
    }

    private void GameOver()
    {
        gameOverText.enabled = true;

        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].gameObject.SetActive(false);
        }

        pacman.gameObject.SetActive(false);
        Invoke(nameof(ShowMenuButtons), 2f);
    }
    private void ShowMenuButtons()
    {
        gameOverButtons.gameObject.SetActive(true);
    }
    private void HideMenuButtons()
    {
        gameOverButtons.gameObject.SetActive(false);
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
        livesText.text = "x" + lives.ToString();
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(2, '0');
    }


    public void PacmanEaten()
    {
        DisableInControlJoystick();
        sndManager.StopBackgroundMusic();
        sndManager.PlayDeath();
        pacman.DeathSequence();

        SetLives(lives - 1);

        if (lives > 0) {
            Invoke(nameof(ResetState), 3f);
        } else {
            GameOver();
        }
    }

    public void GhostEaten(Ghost ghost)
    {
        sndManager.PlayEatGhost();
        int points = ghost.points * ghostMultiplier;
        SetScore(score + points);

        ghostMultiplier++;
    }

    public void PelletEaten(Pellet pellet)
    {
        sndManager.PlayChomp();
        pellet.gameObject.SetActive(false);

        SetScore(score + pellet.points);

        if (!HasRemainingPellets())
        {
            pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3f);
        }
    }

    public void PowerPelletEaten(PowerPellet pellet)
    {
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].frightened.Enable(pellet.duration);
        }

        PelletEaten(pellet);
        sndManager.PlayBackgroundMusicFrightened();
        CancelInvoke(nameof(ResetGhostMultiplier));
        sndManager.CancelInvoke(nameof(sndManager.PlayBackgroundMusicStandard));
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
        sndManager.Invoke(nameof(sndManager.PlayBackgroundMusicStandard), pellet.duration);
    }


    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet.gameObject.activeSelf) {
                return true;
            }
        }

        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }


    private void PauseGame()
    {
        // Ghost and pac-man can't move
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].GetComponent<Movement>().speed = 0;
        }
        pacman.GetComponent<Movement>().speed = 0;
    }

    private void ResumeGame()
    {
        // Ghosts and pac-man can move
        for (int i = 0; i < ghosts.Length; i++) {
            ghosts[i].GetComponent<Movement>().speed = ghosts[i].GetComponent<Movement>().GetInitialSpeed();
        }
        pacman.GetComponent<Movement>().speed = pacman.GetComponent<Movement>().GetInitialSpeed();
    }

    private void DisableInControlJoystick()
    {
        inControlControls.SetActive(false);
    }

    private void EnableInControlJoystick()
    {
        inControlControls.SetActive(true);
    }
}
