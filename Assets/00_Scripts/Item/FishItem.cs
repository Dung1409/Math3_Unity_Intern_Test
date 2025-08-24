using System;
using UnityEngine;

[Serializable]
public class FishItem
{
    public State currentState;
    public Sprite sprite;

    public FishItem() { }

    public void SetState(int i)
    {
        currentState = (State)i;
        sprite = FishItemStorage.instance.GetSprite(currentState.ToString().ToUpper());
    }

    public enum State
    {
        fish_1,
        fish_2,
        fish_3,
        fish_4,
        fish_5,
        fish_6,
        RAINBOW_FISH
    }

    public override bool Equals(object obj)
    {
        if (obj is FishItem other)
            return currentState == other.currentState;
        return false;
    }
    public override int GetHashCode()
    {
        return currentState.GetHashCode();
    }
}
