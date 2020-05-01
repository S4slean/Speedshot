using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameEditor : MonoBehaviour
{
    public SpriteRenderer playerName;

    public void ChangeSprite(Sprite newSpriteName)
    {
        playerName.sprite = newSpriteName;
    }
}
