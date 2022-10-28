using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/BlocksProviderScriptableObject", order = 1)]
public class BlocksProvider : ScriptableObject
{
    [SerializeField]
    private GameObject colorBlockPrefab;
    [SerializeField]
    private GameObject stripeBlockPrefab;
    [SerializeField]
    private GameObject bombBlockPrefab;

    private BlocksBuffer<ColorBlock> colorBlocksBuffer;
    private BlocksBuffer<StripeBlock> stripeBlocksBuffer;
    private BlocksBuffer<BombBlock> bombBlocksBuffer;

    public void Initialize(Canvas boardCanvas)
    {
        colorBlocksBuffer = new BlocksBuffer<ColorBlock>(BoardSettings.COLOR_BLOCKS_BUFFER_SIZE, colorBlockPrefab, boardCanvas.transform);
        stripeBlocksBuffer = new BlocksBuffer<StripeBlock>(BoardSettings.STRIPE_BLOCKS_BUFFER_SIZE, stripeBlockPrefab, boardCanvas.transform);
        bombBlocksBuffer = new BlocksBuffer<BombBlock>(BoardSettings.BOMB_BLOCKS_BUFFER_SIZE, bombBlockPrefab, boardCanvas.transform);
    }

    public BaseBlock GetRandomBlock()
    {
        if (IsColorBlock())
        {
            return colorBlocksBuffer.GetAvailableBlock();
        }
        else
        {
            return IsBombBlock() ? bombBlocksBuffer.GetAvailableBlock() : stripeBlocksBuffer.GetAvailableBlock();
        }
    }

    private bool IsColorBlock()
    {
        return Random.Range(0f, 1f) <= BoardSettings.COLOR_BLOCKS_PROBABILITY;
    }

    private bool IsBombBlock()
    {
        return Random.Range(0f, 1f) <= BoardSettings.BOMB_BLOCKS_PROBABILITY;
    }

}
