using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBlock : MonoBehaviour
{
    public static Action<BaseBlock> OnBlockClick;

    public int RowIndex { get; private set; }
    public int ColumnIndex { get; private set; }
    public bool IsShown { get; protected set; }
    public bool IsSelected { get; set; }

    protected BlockBounds bounds;

    private RectTransform rectTransform;

    public void Spawn(int rowIndex, int columnIndex)
    {
        Show(rowIndex, columnIndex);
        bounds = new BlockBounds(this);
    }

    public void Spawn(BaseBlock block)
    {
        Show(block.RowIndex, block.ColumnIndex);
        bounds = block.bounds;
    }

    public void ResetBlock()
    {
        rectTransform.position = BoardSettings.BLOCK_DEFAULT_POSITION;
        IsShown = false;
        IsSelected = false;
    }

    public void Select()
    {
        if (IsSelected) return;
        OnBlockClick?.Invoke(this);        
    }

    public abstract List<BaseBlock> IdentifyBlockSetToDestroy(BaseBlock[,] board);

    protected virtual void Show(int rowIndex, int columnIndex)
    {
        RowIndex = rowIndex;
        ColumnIndex = columnIndex;
        rectTransform.localPosition =
            new Vector3(BoardSettings.IndexToLocation(columnIndex), BoardSettings.IndexToLocation(rowIndex));
        IsShown = true;    
    }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(BoardSettings.BLOCK_SIZE, BoardSettings.BLOCK_SIZE);
        ResetBlock();
    }

}
