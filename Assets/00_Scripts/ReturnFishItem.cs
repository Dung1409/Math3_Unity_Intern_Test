using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReturnFishItem : MonoBehaviour, IPointerDownHandler
{
    BottomBarCell cell => GetComponent<BottomBarCell>();
    public void OnPointerDown(PointerEventData eventData)
    {
        if (transform.childCount == 0) return;
        GameObject child = transform.GetChild(0).gameObject;
        Transform preParent = child.GetComponent<Fish>().preParent;
        child.transform.DOMove(preParent.transform.position, 0.2f).OnComplete(() =>
        {
            child.transform.SetParent(preParent);
            cell.neighbor.Clear();
            cell.fish = null;
            MoveFishToPreCell(cell.cellIndex);
        });
    }

    void MoveFishToPreCell(int idx)
    {
        cell.MoveFish(idx , 1);
    }

}
