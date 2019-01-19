using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsController : MonoBehaviour {

    [SerializeField] private Slider freezeTimeSlider;
    [SerializeField] private Slider spawnTimeSlider;
    [SerializeField] private Slider cameraHeightSlider;
    [SerializeField] private TMP_Text freezeTimeText;
    [SerializeField] private TMP_Text spawnTimeText;
    [SerializeField] private TMP_Text cameraHeightText;
    [SerializeField] private GameObject OptionsContainer;
    [SerializeField] private GameObject MainMenu;


    private void Start()
    {
        freezeTimeSlider.onValueChanged.AddListener(delegate { SetText(freezeTimeSlider.value.ToString(), freezeTimeText); });
        spawnTimeSlider.onValueChanged.AddListener(delegate { SetText(spawnTimeSlider.value.ToString(), spawnTimeText); });
        cameraHeightSlider.onValueChanged.AddListener(delegate { SetText(cameraHeightSlider.value.ToString(), cameraHeightText); });
    }

    private void SetText(string value, TMP_Text text)
    {
        text.text = value;
    }

    public void BackToMainMenu()
    {
        MainMenu.SetActive(true);
        OptionsContainer.SetActive(false);
    }
}
