using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class CreateBoard : MonoBehaviour
{
    [SerializeField] private GameSettings settings;

    public List<GameObject> childs = new List<GameObject>();
    public int boardSizeX;
    public int boardSizeY;

    void OnEnable()
    {
        Observer.AddListener("CreateBoard", _CreateBoard);
        Observer.AddListener("Reset", Reset);
    }
    void Start()
    {
        boardSizeX = settings != null ? settings.BoardSizeX : 5;
        boardSizeY = settings != null ? settings.BoardSizeY : 5;
    }

    private void _CreateBoard()
    {
        Vector3 origin = new Vector3(-boardSizeX * 0.5f + 0.5f, -boardSizeY * 0.5f + 0.5f, 0f);
        GameObject prefabBG = Resources.Load<GameObject>(Constants.CELL_BACKGROUND);
        for (int x = 0; x < boardSizeX; x++)
        {
            for (int y = 0; y < boardSizeY; y++)
            {
                GameObject go = GameObject.Instantiate(prefabBG);
                go.transform.position = origin + new Vector3(x, y, 0f);
                go.AddComponent<SelectedCell>();
                GameObject fishPrefab = Resources.Load<GameObject>(Constants.FISH_PREFAB);
                go.transform.SetParent(this.transform);
                childs.Add(go);
                GameObject fish = Instantiate(fishPrefab, go.transform.position, Quaternion.identity, go.transform);
                FishItem fI;
                FishItemStorage.instance.GetFishItem(0, 23, out fI);
                Fish f = fish.GetComponent<Fish>();
                f.GetFishItem(fI);
                AutoPlay.AddFishParent(fI, go);
            }
        }
    }

    void Reset()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject p = childs[i];
            if (p.transform.childCount != 0)
            {
                Transform child = p.transform.GetChild(0);
                child.SetParent(null);
                child.gameObject.SetActive(false);
            } 
            Destroy(p);
        }
        childs.Clear();
        Observer.Notify("GenFishItem");
    }
    void OnDisable()
    {
        Observer.RemoveListener("CreateBoard", _CreateBoard);
        Observer.RemoveListener("Reset", Reset);
    }
    void OnDestroy()
    {
        Observer.RemoveListener("CreateBoard", _CreateBoard);
        Observer.RemoveListener("Reset", Reset);
    }

}
