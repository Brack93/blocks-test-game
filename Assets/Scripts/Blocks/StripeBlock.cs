using System.Collections.Generic;

public class StripeBlock : BaseBlock
{
    public override List<BaseBlock> IdentifyBlockSetToDestroy(BaseBlock[,] board)
    {
        List<BaseBlock> selected = new List<BaseBlock>();
        for (int i = 0; i< BoardSettings.BOARD_SIZE; i++)
        {
            BaseBlock block = board[this.RowIndex, i];
            block.IsSelected = true;
            selected.Add(block);
        }
        
        return selected;
    }

}
