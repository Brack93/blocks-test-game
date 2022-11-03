using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BoardManager : MonoBehaviour
{
    public static Action<int> OnBlocksDestroyed;

    [SerializeField]
    private Canvas boardCanvas;

    [SerializeField]
    private BlocksProvider blocksProvider;

    private readonly BaseBlock[,] board = new BaseBlock[BoardSettings.BOARD_SIZE, BoardSettings.BOARD_SIZE];

    private void OnEnable()
    {
        BaseBlock.OnBlockClick += HandleBlockClick;
    }

    private void OnDisable()
    {
        BaseBlock.OnBlockClick -= HandleBlockClick;
    }

    private void Awake()
    {
        blocksProvider.Initialize(boardCanvas);        
    }

    private void Start()
    {
        for (int i = 0; i < BoardSettings.BOARD_SIZE; i++)
        {
            for (int j = 0; j < BoardSettings.BOARD_SIZE; j++)
            {
                BaseBlock block = blocksProvider.GetRandomBlock();
                board[i, j] = block;
                block.Spawn(i, j);
            }
        }
        CheckGameover();
    }

    private void HandleBlockClick(BaseBlock clickedBlock)
    {
        List<BaseBlock> blocks = clickedBlock.IdentifyBlockSetToDestroy(board);
        
        if (blocks == null) return;

        OnBlocksDestroyed?.Invoke(blocks.Count);
        
        blocks.ForEach(block => {
            block.GetComponent<Image>().color = Color.black;
        });

        StartCoroutine(DelayedReplaceBlocks(blocks));
    }

    private void CheckGameover()
    {
        bool canMove = false;

        for (int i = 0; i < BoardSettings.BOARD_SIZE; i++)
        {
            if (canMove) break;

            for (int j = 0; j < BoardSettings.BOARD_SIZE; j++)
            {
                BaseBlock block = board[i, j];
                if (block is BombBlock || block is StripeBlock)
                {
                    canMove = true;
                    break;
                }
                ColorBlock colorBlock = block as ColorBlock;
                if (colorBlock != null && colorBlock.FindConsecutiveColor(board).Count > 0)
                {
                    canMove = true;
                    break;
                }
            }
        }

        if (!canMove) SceneManager.Instance.LoadScene(Scenes.GameoverScene);
    }

    private IEnumerator DelayedReplaceBlocks(List<BaseBlock> blocks)
    {
        yield return new WaitForSeconds(0.3f);
        blocks.ForEach(block => {
            block.GetComponent<Image>().color = Color.white;
            block.ResetBlock();
            ReplaceBlock(block);
        });
        CheckGameover();
    }

    private void ReplaceBlock(BaseBlock oldBlock)
    {
        BaseBlock newBlock = blocksProvider.GetRandomBlock();
        board[oldBlock.RowIndex, oldBlock.ColumnIndex] = newBlock;
        newBlock.Spawn(oldBlock);
    }

}
