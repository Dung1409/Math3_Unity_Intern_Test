using UnityEngine;

public class Fish : MonoBehaviour
{
    public FishItem fishItem = new FishItem();
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Transform preParent;
    void Awake()
    {
        TryGetComponent<SpriteRenderer>(out _spriteRenderer);
        if (_spriteRenderer == null)
        {
            _spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        preParent = transform.parent;
    }


    void OnDisable()
    {
        BottomBarCell c = GetComponentInParent<BottomBarCell>();
        if (c == null) return;
        c.neighbor.Clear();
    }

    public void GetFishItem(FishItem item)
    {
        if (item == null) return;
        fishItem = item;
        _spriteRenderer.sprite = fishItem.sprite;
    }
}
