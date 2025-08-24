using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class BottomBar : MonoBehaviour
{
    public GameObject[] cells;
    public List<GameObject> pool;
    public int childCount;
    public int currentIdx;
    private static BottomBar _instance;
    public static BottomBar instance => _instance;


    public bool isGameOver;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        isGameOver = false;
        childCount = transform.childCount;
        cells = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            cells[i] = transform.GetChild(i).gameObject;
            TryGetComponent<BottomBarCell>(out BottomBarCell cell);
            if (cell == null)
            {
                cell = cells[i].AddComponent<BottomBarCell>();
            }
            cell.cellIndex = i;
        }
        Array.Sort(cells, (a, b) =>
        {
            return a.transform.position.x.CompareTo(b.transform.position.x);
        });
        Observer.AddListener("Clear", ClearBoard);
    }

    public void SelectedCell(int idx, GameObject child)
    {
        if (idx < 0 || idx > childCount - 1 || currentIdx > childCount - 1) return;
        if (idx != currentIdx)
        {
            for (int i = idx; i < currentIdx; i++)
            {
                GameObject c = cells[i].transform.GetChild(0).gameObject;
                StartCoroutine(WaitMove(c.transform, cells[i + 1].transform));
            }
        }
        Transform parent = cells[idx].transform;
        StartCoroutine(WaitMove(child.transform, parent));
        currentIdx += 1;
        DeleteNeigborFish(idx, child);
    }

    public void DeleteNeigborFish(int idx, GameObject child)
    {
        StartCoroutine(WaitDeleteFish(idx, child));
    }

    IEnumerator WaitDeleteFish(int idx, GameObject child)
    {
        yield return new WaitForSeconds(0.25f);
        var cell = child.GetComponentInParent<BottomBarCell>();
        if (cell != null)
        {
            cell.neighbor.Clear();
            cell.AddNeighbor(idx, -1);
            yield return new WaitForEndOfFrame();
            cell.DeleteNeibor();
            yield return new WaitForSeconds(1f);
            WinGame();
            CheckGameOver();
        }
    }

    IEnumerator WaitMove(Transform child, Transform parent)
    {
        yield return new WaitForEndOfFrame();

        child.transform.SetParent(null);

        child.transform.DOMove(parent.position, 0.2f).OnComplete(() =>
        {
            child.transform.SetParent(parent);
            BottomBarCell cell = parent.GetComponent<BottomBarCell>();
            child.transform.localPosition = Vector3.zero;
        });
    }

    public void CheckGameOver()
    {
        if (isGameOver) return;
        if (GameModeController.Game_Mode == GameModeController.GAME_MODE.NORMAL)
        {
            if (currentIdx >= childCount)
            {
                isGameOver = true;
                Observer.Notify("Lose");
            }
        }
        else if (GameModeController.Game_Mode == GameModeController.GAME_MODE.ATTACK_TIME && pool.Count > 0)
        {
            isGameOver = true;
            Observer.Notify("Lose");
        }
    }


    public void WinGame()
    {
        if (isGameOver) return;
        GameSettings settings = Resources.Load<GameSettings>(Constants.GAME_SETTINGS_PATH);
        if (pool.Count == settings.BoardSizeX * settings.BoardSizeY)
        {
            isGameOver = true;
            Observer.Notify("Win");
        }
    }

    public void ClearBoard()
    {
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].transform.childCount == 0) continue;
            GameObject g = cells[i].transform.GetChild(0).gameObject;
            if (g == null) continue;
            cells[i].transform.DetachChildren();
            g.SetActive(false);
        }
        pool.Clear();
        AutoPlay.fishParentDic.Clear();
        isGameOver = false;
        currentIdx = 0;
    }

    public enum GAME_MODE
    {
        NORMAL,
        ATTACK_TIME
    }
    void OnDestroy()
    {
        Observer.RemoveListener("Clear", ClearBoard);
        pool.Clear();
    }
}


