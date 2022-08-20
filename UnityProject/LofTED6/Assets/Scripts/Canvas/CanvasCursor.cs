using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasCursor : MonoBehaviour
{
    public enum ToolType { BRUSH, ERASER, FILL, DROPPER };

    public CanvasBehavior canvas = null;
    public int DrawSize = 1;
    public Color DrawColor = new Color(1, 0, 0);
    public ToolType CurrTool = ToolType.BRUSH;

    public List<Image> ToolButtons = null;
    public List<Color> DrawColors = null;

    Color trueColor = new Color(1, 0, 0);

    SpriteRenderer spRenderer = null;

    bool drawing = false;

    Vector2 lastMousePos;

    // Start is called before the first frame update
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        if (canvas != null)
        {
            transform.localScale = canvas.GetPixelSize() * DrawSize;
        }
        for (int i = 0; i < ToolButtons.Count; ++i)
        {
            Color curr = ToolButtons[i].color;
            curr.a = i == 0 ? 1.0f : 0.5f;
            ToolButtons[i].color = curr;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas != null)
        {
            transform.position = canvas.GetCurrPixelPos();
            Color currColor = canvas.GetCurrPixelColor();
            if (currColor.a == 0)
            {
                spRenderer.color = new Color(0, 0, 0);
            }
            else
            {
                Color newColor = new Color(1, 1, 1) - currColor;
                newColor.a = 1;
                spRenderer.color = newColor;
            }

            if (drawing)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    drawing = false;
                    return;
                }
                Vector2Int pixelCoords = canvas.GetPixelCoords(transform.position);
                Vector2Int lastPixelCoords = canvas.GetPixelCoords(lastMousePos);
                for (float i = 0.0f; i <= 1.0f; i += 0.1f)
                {
                    int currX = (int)Mathf.Lerp((int)lastPixelCoords.x, (float)pixelCoords.x, i);
                    int currY = (int)Mathf.Lerp((int)lastPixelCoords.y, (float)pixelCoords.y, i);
                    for (int x = -(DrawSize / 2); x < (DrawSize / 2) + (DrawSize % 2); ++x)
                    {
                        for (int y = -(DrawSize / 2); y < (DrawSize / 2) + (DrawSize % 2); ++y)
                        {
                            canvas.DrawPixel(new Vector2Int(currX, currY) + new Vector2Int(x, y), trueColor);
                        }
                    }
                }
                lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }

    public void StartDrawing()
    {
        if (CurrTool == ToolType.FILL)
        {
            canvas.FloodFillColor(canvas.GetPixelCoords(transform.position), DrawColor);
            return;
        }
        else if (CurrTool == ToolType.DROPPER)
        {
            Color currColor = canvas.GetCurrPixelColor();
            currColor.a = 1;
            return;
        }
        else if (CurrTool == ToolType.BRUSH)
        {
            trueColor = DrawColor;
        }
        else if (CurrTool == ToolType.ERASER)
        {
            trueColor = new Color(1, 1, 1, 1);
        }

        lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        drawing = true;
    }

    public void OnSliderChange()
    {
        transform.localScale = canvas.GetPixelSize() * DrawSize;
    }

    public void ChangeDrawSize(int newSize)
    {
        DrawSize = newSize;
        transform.localScale = canvas.GetPixelSize() * DrawSize;
    }

    public void ChangeColor(Color newColor)
    {
        DrawColor = newColor;
    }

    public void ChangeTool(int newTool)
    {
        CurrTool = (ToolType)newTool;
    }

    public void ChangeToolIndex(int newTool)
    {
        for (int i = 0; i < ToolButtons.Count; ++i)
        {
            Color curr = ToolButtons[i].color;
            curr.a = i == newTool ? 1.0f : 0.5f;
            ToolButtons[i].color = curr;
        }

        ChangeColor(DrawColors[Mathf.Min(newTool, DrawColors.Count - 1)]);
    }
}
