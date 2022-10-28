using UnityEngine;
public struct BlockBounds
{
    public int ColMin { get; private set; }
    public int ColMax { get; private set; }
    public int RowMin { get; private set; }
    public int RowMax { get; private set; }

    public BlockBounds(BaseBlock block)
    {
        ColMin = Mathf.Max(0, block.ColumnIndex - 1);
        ColMax = Mathf.Min(BoardSettings.BOARD_SIZE - 1, block.ColumnIndex + 1);
        RowMin = Mathf.Max(0, block.RowIndex - 1);
        RowMax = Mathf.Min(BoardSettings.BOARD_SIZE - 1, block.RowIndex + 1);
    }

}
