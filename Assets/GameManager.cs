using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    const string livesString = "{0:F2} m";
    const float baseGravity = -9.81f;
    const float slowdownGravity = -3.5f;
    const float slowdownSeconds = 3f;

    [SerializeField] private Camera gameCam;
    [SerializeField] private RawImage[] lives;
    [SerializeField] private TMP_Text heightText;
    [SerializeField] private Texture EmptyHeart;
    [SerializeField] private Texture FullHeart;
    [SerializeField] private GameObject spawner;
    [SerializeField] private Button resetButton;
    [SerializeField] private GameObject gameoverContainer;
    [SerializeField] private GameObject boxParent;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject GameMenu;
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private Slider cameraHeightSlider;

    private int liveCounter = 3;
    private Vector3 initCamPos;

    private void Start()
    {
        resetButton.onClick.AddListener(ResetGame);
        initCamPos = gameCam.transform.position;
    }

    public void IncreaseScore(float height)
    {
        heightText.text = string.Format(livesString, height / 10);
        if (height > 2f)
        {
            gameCam.transform.DOMove(new Vector3(0, height + cameraHeightSlider.value - 2f, -10),0.75f);
        }
    }

    public void OpenOptions()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void DecreaseLives()
    {
        liveCounter--;
        lives[liveCounter].texture = EmptyHeart;
        if (liveCounter == 0)
        {
            spawner.SetActive(false);
            gameoverContainer.SetActive(true);
        }
    }

    public void IncraeseLives()
    {
        if (liveCounter > 3)
        {
            lives[liveCounter].texture = FullHeart;
            liveCounter++;
        }
    }

    public void SlowdownGravity()
    {
        Physics2D.gravity = new Vector2(0, slowdownGravity);
        StartCoroutine(ReturnGravity());
    }

    IEnumerator ReturnGravity()
    {
        yield return new WaitForSeconds(slowdownSeconds);
        Physics2D.gravity = new Vector2(0, baseGravity);
    }

    public void StartGame()
    {
        spawner.SetActive(true);
        GameMenu.SetActive(true);
        MainMenu.SetActive(false);
    }

    private void ResetGame()
    {
        spawner.SetActive(true);
        heightText.text = string.Format(livesString, 0f);
        gameCam.transform.position = initCamPos;
        gameoverContainer.SetActive(false);
        foreach (Transform child in boxParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        liveCounter = 3;
        foreach (RawImage heart in lives)
        {
            heart.texture = FullHeart;
        }
    }
}
