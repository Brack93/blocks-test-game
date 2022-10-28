using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorBlock : BaseBlock
{
    private int ColorIndex;

    protected override void Show(int rowIndex, int columnIndex)
    {
        base.Show(rowIndex, columnIndex);
        ColorIndex = Random.Range(0, BoardSettings.COLORS.Length);
        GetComponent<Image>().color = BoardSettings.COLORS[ColorIndex];
    }

    public override List<BaseBlock> IdentifyBlockSetToDestroy(BaseBlock[,] board)
    {
        List<BaseBlock> selected = new List<BaseBlock>();
        RecursiveFindConsecutiveColor(selected, board);
        if (selected.Count < 2)
        {
            IsSelected = false;
            return null;
        }
        return selected;
    }

    public List<ColorBlock> FindConsecutiveColor(BaseBlock[,] board)
    {
        List<ColorBlock> consecutiveColor = new List<ColorBlock>();
        PopulateConsecutiveColor(board[RowIndex, bounds.ColMin], consecutiveColor);
        PopulateConsecutiveColor(board[RowIndex, bounds.ColMax], consecutiveColor);
        PopulateConsecutiveColor(board[bounds.RowMin, ColumnIndex], consecutiveColor);
        PopulateConsecutiveColor(board[bounds.RowMax, ColumnIndex], consecutiveColor);
        return consecutiveColor;
    }

    private void RecursiveFindConsecutiveColor(List<BaseBlock> selected, BaseBlock[,] board)
    {
        if (IsSelected) return;
        IsSelected = true;
        selected.Add(this);
        foreach (var colorBlock in FindConsecutiveColor(board))
        {
            colorBlock.RecursiveFindConsecutiveColor(selected, board);
        }
    }

    private void PopulateConsecutiveColor(BaseBlock block, List<ColorBlock> consecutiveColor)
    {
        ColorBlock colorBlock = block as ColorBlock;
        if (colorBlock != null && colorBlock != this && !colorBlock.IsSelected && colorBlock.ColorIndex == this.ColorIndex)
        {
            consecutiveColor.Add(colorBlock);
        }
    }
}
