using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    public GameObject playButton;
    public GameObject controlButton;
    public GameObject exitButton;

    public GameObject returnFromControlButton;

    public Transform controlPanel;
    public Transform mainPanel;

	// Use this for initialization
	void Start () {
        UIEventListener.Get(playButton).onClick += OnPlayButtonPressed;
        UIEventListener.Get(controlButton).onClick += OnControlButtonPressed;
        UIEventListener.Get(exitButton).onClick += OnExitButtonPressed;
        UIEventListener.Get(returnFromControlButton).onClick += OnReturnButtonPressed;
	}

    void OnPlayButtonPressed(GameObject button)
    {
        Application.LoadLevel("Game");
    }

    void OnControlButtonPressed(GameObject button)
    {
        controlPanel.localPosition = new Vector3(0, 0, -2);
        mainPanel.localPosition = new Vector3(1000, 0, 0);
    }

    void OnExitButtonPressed(GameObject button)
    {

    }

    void OnReturnButtonPressed(GameObject button)
    {
        controlPanel.localPosition = new Vector3(1000, 0, -2);
        mainPanel.localPosition = new Vector3(0, 0, 0);
    }
}
