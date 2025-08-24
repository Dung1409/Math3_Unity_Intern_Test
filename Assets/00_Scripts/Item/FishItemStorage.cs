using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FishItemStorage : MonoBehaviour
{
    public Dictionary<string, Sprite> fishSprite = new Dictionary<string, Sprite>();
    private static FishItemStorage _instance;
    public static FishItemStorage instance => _instance;

    public List<FishItem> fishItems = new List<FishItem>();
    public List<bool> usedItems = new List<bool>();

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
        fishSprite = Resources.LoadAll<Sprite>(Constants.FISH_SPRITE)
                    .ToDictionary(e => e.name.ToUpper(), e => e);
    }

    void Start()
    {
        GenFishItem();
        Observer.AddListener("GenFishItem", GenFishItem);
    }

    public Sprite GetSprite(string key)
    {
        Sprite sp;
        fishSprite.TryGetValue(key, out sp);
        return sp;
    }

    public void GenFishItem()
    {
        GameSettings settings = Resources.Load<GameSettings>(Constants.GAME_SETTINGS_PATH);
        int row = settings.BoardSizeY;
        int col = settings.BoardSizeX;
        int total = row * col;

        fishItems.Clear();
        usedItems.Clear();

        int sz = fishSprite.Count - 1;

        int _idx = Random.Range(0, sz);
        int origin = _idx;
        while (total > 0)
        {

            FishItem item = new FishItem();
            item.SetState(_idx);

            for (int i = 0; i < 3 && total > 0; i++)
            {
                fishItems.Add(item);
                usedItems.Add(false);
                total--;
            }
            _idx += 1;
            if (_idx == origin)
            {
                _idx = Random.Range(0, fishSprite.Count - 1);
            }
            if (_idx > fishSprite.Count - 1)
            {
                _idx = 0;
            }
        }
        Observer.Notify("CreateBoard");
    }

    public void GetFishItem(int l, int r, out FishItem item)
    {
        item = null;
        if (l < 0 || r >= fishItems.Count || l > r)
        {
            return;
        }
        int mid = (l + r) / 2;

        if (mid < 0 || mid >= fishItems.Count)
        {
            return;
        }

        if (usedItems[mid])
        {
            int rd = Random.Range(0, 2);
            switch (rd)
            {
                case 0:
                    GetFishItem(l, mid - 1, out item);
                    if (item == null) GetFishItem(mid + 1, r, out item);
                    break;
                case 1:
                    GetFishItem(mid + 1, r, out item);
                    if (item == null) GetFishItem(l, mid - 1, out item);
                    break;
                default:
                    break;
            }
        }
        else
        {
            item = fishItems[mid];
            usedItems[mid] = true;
            return;
        }
    }

    void OnDestroy()
    {
        Observer.RemoveListener("GenFishItem", GenFishItem);
    }
}
