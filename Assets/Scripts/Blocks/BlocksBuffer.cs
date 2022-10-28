using System.Collections.Generic;
using UnityEngine;

public class BlocksBuffer<T> where T : BaseBlock {

    private readonly List<T> blockList = new List<T>();
    private readonly int bufferSize;
    private readonly GameObject blockPrefab;
    private readonly Transform canvasTransform;
    
    public BlocksBuffer(int bufferSize, GameObject blockPrefab, Transform canvasTransform)
    {
        this.bufferSize = bufferSize;
        this.blockPrefab = blockPrefab;
        this.canvasTransform = canvasTransform;
        SpawnBlocks();
    }

    public BaseBlock GetAvailableBlock()
    {
        return blockList.Find(block => !block.IsShown) ?? SpawnBlock();
    }

    private void SpawnBlocks()
    {
        for (int i = 0; i < bufferSize; i++)
        {
            SpawnBlock();
        }
    }

    private BaseBlock SpawnBlock()
    {
       var clone = Object.Instantiate(blockPrefab, canvasTransform);
       T block = clone.GetComponent<T>();
       blockList.Add(block);
       return block;
    }

}
