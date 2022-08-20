using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CanvasBehavior : MonoBehaviour
{
    public static Texture2D FinalTexture = null;
    public static int FinalWidth = 64;
    public static int FinalHeight = 64;

    //Width and height of the canvas in pixels, should be positive
    [SerializeField]
    int Width = 64;
    [SerializeField]
    int Height = 64;

    public CanvasCursor cursor = null;

    SpriteRenderer spRenderer = null;
    Texture2D canvasTexture = null;

    Sprite canvasSprite = null;

    // Start is called before the first frame update
    void Start()
    {
        spRenderer = gameObject.GetComponent<SpriteRenderer>();
        canvasTexture = InitializeTexture();
        canvasSprite = Sprite.Create(canvasTexture, new Rect(0.0f, 0.0f, Width, Height), new Vector2(0.5f, 0.5f), (Width + Height) / 2);
        spRenderer.sprite = canvasSprite;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            if(Input.GetKeyUp(KeyCode.S))
            {
                SaveDrawingAsPNG();
            }
        }
    }

    Texture2D InitializeTexture()
    {
        Texture2D tex = new Texture2D(Width, Height);
        tex.name = "CanvasTexture";

        for (int x = 0; x < Width; ++x)
        {
            for (int y = 0; y < Height; ++y)
            {
                tex.SetPixel(x, y, new Color(1, 1, 1, 1));
            }
        }

        tex.filterMode = FilterMode.Point;
        tex.Apply();

        return tex;
    }

    public Vector2Int GetPixelCoords(Vector2 pos)
    {
        pos -= new Vector2(transform.position.x, transform.position.y);
        pos += new Vector2(transform.localScale.x / 2, transform.localScale.y / 2);

        pos.x /= transform.localScale.x;
        pos.y /= transform.localScale.y;

        int pixelX = (int)(pos.x * Width);
        int pixelY = (int)(pos.y * Height);

        return new Vector2Int(pixelX, pixelY);
    }

    public Color GetCurrPixelColor()
    {
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int pixel = GetPixelCoords(worldMousePos);

        if (pixel.x > Width || pixel.x < 0 || pixel.y > Height || pixel.y < 0)
        {
            return new Color();
        }

        return canvasTexture.GetPixel(pixel.x, pixel.y);
    }

    public Vector2 GetCurrPixelPos()
    {
        Vector2 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int pixel = GetPixelCoords(worldMousePos);

        float pixelCenterX = transform.position.x + (pixel.x * (transform.localScale.x / Width)) - (transform.localScale.x / 2);
        float pixelCenterY = transform.position.y + (pixel.y * (transform.localScale.y / Height)) - (transform.localScale.y / 2);
        pixelCenterX += (transform.localScale.x / Width) / 2;
        pixelCenterY += (transform.localScale.y / Height) / 2;

        return new Vector2(pixelCenterX, pixelCenterY);
    }

    public Vector2 GetPixelSize()
    {
        float xSize = transform.localScale.x / Width;
        float ySize = transform.localScale.y / Height;

        return new Vector2(xSize, ySize);
    }

    void OnMouseDown()
    {
        cursor?.StartDrawing();
    }

    public void ResetCanvas()
    {
        for (int x = 0; x < Width; ++x)
        {
            for (int y = 0; y < Height; ++y)
            {
                canvasTexture.SetPixel(x, y, new Color(0, 0, 0, 0));
            }
        }

        canvasTexture.Apply();
    }

    public void DrawPixel(Vector2Int pixel, Color color)
    {
        if (pixel.x >= Width || pixel.x < 0 || pixel.y >= Height || pixel.y < 0)
        {
            return;
        }

        canvasTexture.SetPixel(pixel.x, pixel.y, color);
        canvasTexture.Apply();
    }

    public void FloodFillColor(Vector2Int firstPixel, Color color)
    {
        Color firstColor = canvasTexture.GetPixel(firstPixel.x, firstPixel.y);

        Queue<Vector2Int> pixels = new Queue<Vector2Int>();
        pixels.Enqueue(firstPixel);

        //Iterative flood fill using a queue
        while (pixels.Count > 0)
        {
            Vector2Int currPixel = pixels.Dequeue();
            if (canvasTexture.GetPixel(currPixel.x, currPixel.y) != firstColor
               || currPixel.x < 0 || currPixel.x >= Width || currPixel.y < 0 || currPixel.y >= Height)
            {
                continue;
            }

            DrawPixel(currPixel, color);
            for (int i = 0; i < 4; ++i)
            {
                pixels.Enqueue(MoveInDirection(currPixel, i));
            }
        }
    }

    //--dirs:
    //0 - left
    //1 - up
    //2 - down
    //3 - right
    Vector2Int MoveInDirection(Vector2Int previous, int dir)
    {
        if (dir == 0)
        {
            return previous + new Vector2Int(-1, 0);
        }
        else if (dir == 1)
        {
            return previous + new Vector2Int(0, 1);
        }
        else if (dir == 2)
        {
            return previous + new Vector2Int(0, -1);
        }
        else if (dir == 3)
        {
            return previous + new Vector2Int(1, 0);
        }

        return previous;
    }

    public void SaveDrawingAsPNG()
    {
        byte[] bytes = canvasTexture.EncodeToPNG();
        string savePath;

#if UNITY_STANDALONE_WIN
        savePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments).Replace("\\", "/");
        savePath += "/My Games/LofTED6/";
#else
        savePath = Application.persistentDataPath + "/"
#endif
        Directory.CreateDirectory(savePath);
        File.WriteAllBytes(savePath + "Whiteboard.png", bytes);

        FinalTexture = canvasTexture;
        FinalHeight = Height;
        FinalWidth = Width;
    }
}
