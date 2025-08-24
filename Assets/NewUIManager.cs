
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewUIManager : MonoBehaviour
{
    public Button ok;

    public TextMeshProUGUI txt;

    void Awake()
    {
        ok = GetComponentInChildren<Button>();
        txt = GetComponentInChildren<TextMeshProUGUI>();
        ok.onClick.AddListener(btn_OK);
        Observer.AddListener("Win", SetWin);
        Observer.AddListener("Lose", SetLose);
        this.gameObject.SetActive(false);
    }

    public void SetText(string s)
    {
        this.gameObject.SetActive(true);
        txt.text = s;
    }


    public void SetWin() => SetText("YOU WIN");

    public void SetLose() => SetText("GAME OVER");

    void btn_OK()
    {
        Observer.Notify("Clear");
        Observer.Notify("Reset");
        Observer.Notify("ResetBtn");
        this.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        Observer.RemoveListener("Win", SetWin);
        Observer.RemoveListener("Lose", SetLose);
    }
}
