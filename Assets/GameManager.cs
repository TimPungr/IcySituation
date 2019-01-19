using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    const string livesString = "{0:F2} m";

    [SerializeField] private Camera gameCam;
    [SerializeField] private RawImage[] lives;
    [SerializeField] private TMP_Text heightText;
    [SerializeField] private Texture EmptyHeart;
    [SerializeField] private Texture FullHeart;
    [SerializeField] private GameObject spawner;
    [SerializeField] private Button resetButton;
    [SerializeField] private GameObject gameoverContainer;
    [SerializeField] private GameObject boxParent;
    [SerializeField] private Button[] easterEgg;
    [SerializeField] private TMP_Text eeText;

    private int liveCounter = 3;
    private Vector3 initCamPos;
    private int[] easterEggSequence;
    private int easterEggSequenceCounter;

    private void Start()
    {
        easterEggSequenceCounter = 0;
        easterEggSequence = new[] { 0, 2, 1, 3, 1 };
        resetButton.onClick.AddListener(ResetGame);
        initCamPos = gameCam.transform.position;
        foreach (Button but in easterEgg)
        {
            but.onClick.AddListener(() => EasterTime(but));
        }
    }

    public void IncreaseScore(float height)
    {
        heightText.text = string.Format(livesString, height / 10);
        if (height > 2f)
        {
            gameCam.transform.position = new Vector3(0, height + 4.5f, -10);
        }
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

    public void EasterTime(Button clicked)
    {
        if (clicked.gameObject.name == easterEggSequence[easterEggSequenceCounter].ToString())
        {
            easterEggSequenceCounter++;
            if (easterEggSequenceCounter == easterEgg.Length + 1)
            {
                easterEggSequenceCounter = 0;
                if (liveCounter < 3)
                {
                    eeText.gameObject.SetActive(true);
                    lives[liveCounter].texture = FullHeart;
                    liveCounter++;
                }
            }
        }
        else
        {
            easterEggSequenceCounter = 0;
        }
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
