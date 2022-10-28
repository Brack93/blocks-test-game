using System.Collections.Generic;

public class BombBlock : BaseBlock
{
    public override List<BaseBlock> IdentifyBlockSetToDestroy(BaseBlock[,] board)
    {
        List<BaseBlock> selected = new List<BaseBlock>();
        for (int j = bounds.ColMin; j <= bounds.ColMax; j++)
        {
            for (int i = bounds.RowMin; i <= bounds.RowMax; i++)
            {
                BaseBlock block = board[i, j];
                block.IsSelected = true;
                selected.Add(block);
            }
        }
        return selected;
    }

}
