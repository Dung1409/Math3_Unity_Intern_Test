using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AutoPlay : MonoBehaviour
{

    public static Dictionary<FishItem, List<GameObject>> fishParentDic = new Dictionary<FishItem, List<GameObject>>();

    public List<FishItem> list;

    void Awake()
    {
        Observer.AddListener("AutoPlayWinGame", AutoPlayWinGame);
        Observer.AddListener("AutoPlayLoseGame", AutoPlayLoseGame);
    }

    public void AutoPlayWinGame()
    {
        StartCoroutine(AutoPlayWinCoroutine());
    }

    public void AutoPlayLoseGame()
    {
        StartCoroutine(AutoPlayLoseCoroutine());
    }

    IEnumerator AutoPlayWinCoroutine()
    {
        var keys = fishParentDic.Keys.ToList();
        foreach (var key in keys)
        {
            var parents = fishParentDic[key];
            foreach (var parent in parents)
            {
                if (parent != null)
                {
                    SelectedCell selectedCell = parent.GetComponent<SelectedCell>();
                    selectedCell.ChoiceCell(parent);
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }

    IEnumerator AutoPlayLoseCoroutine()
    {
        var keys = fishParentDic.Keys.ToList();
        list = keys;
        for (int i = 0; i < BottomBar.instance.childCount; i++)
        {
            int idx = i % keys.Count;
            GameObject parent = fishParentDic[keys[idx]][0];
            fishParentDic[keys[idx]].RemoveAt(0);
            if (fishParentDic[keys[idx]].Count == 0)
            {
                keys.RemoveAt(idx);
                fishParentDic.Remove(keys[idx]);
            }
            if (parent != null)
            {
                SelectedCell selectedCell = parent.GetComponent<SelectedCell>();
                selectedCell.ChoiceCell(parent);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public static void AddFishParent(FishItem fI, GameObject parent)
    {
        if (!fishParentDic.ContainsKey(fI))
        {
            fishParentDic.Add(fI, new List<GameObject>());
        }
        fishParentDic[fI].Add(parent);
    }

    void OnDisable()
    {
        Observer.RemoveListener("AutoPlayWinGame", AutoPlayWinGame);
        Observer.RemoveListener("AutoPlayLoseGame", AutoPlayLoseGame);
    }
    void OnDestroy()
    {
        Observer.RemoveListener("AutoPlayWinGame", AutoPlayWinGame);
        Observer.RemoveListener("AutoPlayLoseGame", AutoPlayLoseGame);
    }
}
