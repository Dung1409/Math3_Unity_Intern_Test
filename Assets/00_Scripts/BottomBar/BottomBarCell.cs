using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BottomBarCell : MonoBehaviour
{
    public int cellIndex;
    public List<Fish> neighbor = new List<Fish>();

    public Fish fish
    {
        get
        {
            Fish f = GetComponentInChildren<Fish>();
            return f;
        }
        
        set
        {

        }
    }

    public void AddNeighbor(int curIdx, int dir)
    {
        if (curIdx < 0 || curIdx >= BottomBar.instance.childCount) return;
        Fish f = BottomBar.instance.cells[curIdx].GetComponentInChildren<Fish>();
        if (f == null || f.fishItem.currentState != fish.fishItem.currentState)
        {
            return;
        }

        if (!neighbor.Contains(f)) neighbor.Add(f);
        AddNeighbor(curIdx + dir, dir);
    }

    public void DeleteNeibor()
    {
        int length = neighbor.Count;
        if (length < 3) return;

        int max = 0;
        for (int i = length - 1; i >= 0; i--)
        {
            Fish f = neighbor[i];
            BottomBarCell cell = f.gameObject.GetComponentInParent<BottomBarCell>();
            cell.neighbor.Clear();
            max = Mathf.Max(max, cell.cellIndex);
            GameObject go = f.gameObject;
            go.transform.DOScale(0f, 0.2f).OnComplete(() =>
            {
                BottomBar.instance.pool.Add(go);
                go.SetActive(false);
                go.transform.SetParent(null);
            });
        }
        neighbor.Clear();
        MoveFish(max, length);
    }

    public void MoveFish(int max , int length)
    {
        int n = BottomBar.instance.currentIdx;
        BottomBar.instance.currentIdx -= length;
        BottomBar.instance.isGameOver = false;
        for (int i = max + 1; i < n; i++)
        {
            GameObject c = BottomBar.instance.cells[i].transform.GetChild(0).gameObject;
            Transform parent = BottomBar.instance.cells[i - length].transform;
            BottomBarCell cell = parent.GetComponent<BottomBarCell>();
            c.transform.DOMove(parent.position, 0.1f).OnComplete(() =>
            {
                c.transform.SetParent(parent);
                c.transform.localPosition = Vector3.zero;
                BottomBar.instance.DeleteNeigborFish(cell.cellIndex, cell.gameObject);
            });
        }
    }
}
