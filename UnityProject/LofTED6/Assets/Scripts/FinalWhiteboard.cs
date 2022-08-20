using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWhiteboard : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        r.sprite = Sprite.Create(CanvasBehavior.FinalTexture,
            new Rect(0.0f, 0.0f, CanvasBehavior.FinalWidth, CanvasBehavior.FinalHeight), new Vector2(0.5f, 0.5f), 
            (CanvasBehavior.FinalWidth + CanvasBehavior.FinalHeight) / 2);
    }
}
