using UnityEngine;
using UnityEngine.UI;

public class GameModeController : MonoBehaviour
{

    [SerializeField] private Button AutoPlayWin;
    [SerializeField] private Button AutoPlayLose;
    [SerializeField] private Button AttackTimeMode;

    public static GAME_MODE Game_Mode = GAME_MODE.NORMAL;


    public GameObject Timer;

    void Start()
    {
        Timer.SetActive(false);
        AutoPlayWin.onClick.AddListener(AutoPlayWinMode);
        AutoPlayWin.onClick.AddListener(DeActiveButton);

        AutoPlayLose.onClick.AddListener(AutoPlayLoseMode);
        AutoPlayLose.onClick.AddListener(DeActiveButton);

        AttackTimeMode.onClick.AddListener(SwitchMode);
        Observer.AddListener("ResetBtn", ActiveButton);
    }

    public void SwitchMode(int i)
    {
        Game_Mode = (GAME_MODE)i;
        foreach (var cell in BottomBar.instance.cells)
        {
            cell.AddComponent<ReturnFishItem>();
        }
        Timer.SetActive(true);
        DeActiveButton();

    }
    public void SwitchMode() => SwitchMode(1);

    public void DeActiveButton()
    {
        AutoPlayWin.gameObject.SetActive(false);
        AutoPlayLose.gameObject.SetActive(false);
        AttackTimeMode.gameObject.SetActive(false);
    }

    public void ActiveButton()
    {
        Timer.gameObject.SetActive(false);
        AutoPlayWin.gameObject.SetActive(true);
        AutoPlayLose.gameObject.SetActive(true);
        AttackTimeMode.gameObject.SetActive(true);
        Game_Mode = GAME_MODE.NORMAL;

    }

    public void AutoPlayWinMode() => Observer.Notify("AutoPlayWinGame");

    public void AutoPlayLoseMode() => Observer.Notify("AutoPlayLoseGame");


    public enum GAME_MODE
    {
        NORMAL,
        ATTACK_TIME
    }

    void OnDestroy()
    {
          Observer.RemoveListener("ResetBtn", ActiveButton);
    }
}
