using UnityEngine;
using UnityEngine.EventSystems;

public class SelectedCell : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        ChoiceCell(this.gameObject);
    }

    public void ChoiceCell(GameObject oc)
    {
        if (oc.transform.childCount == 0) return;
        GameObject child = oc.transform.GetChild(0).gameObject;
        BottomBar.instance.SelectedCell(GetRightPosition(child) , child);
    }

    public int GetRightPosition(GameObject child)
    {
        int currentIdx = BottomBar.instance.currentIdx;
        int res = currentIdx;
        Fish f1 = child.GetComponent<Fish>();
        for (int i = 0; i < currentIdx; i++)
        {
            BottomBarCell cell = BottomBar.instance.cells[i].GetComponent<BottomBarCell>();
            Fish f2 = cell.fish;
            if (f2 == null) break;
            if (f1.fishItem.currentState == f2.fishItem.currentState && cell.neighbor.Count + 1 >= 3)
            {
                res = i + 1;
                break;
            }
        }
        return res;
    }

}
