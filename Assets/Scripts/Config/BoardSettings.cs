using UnityEngine;

public static class BoardSettings
{
    public static readonly Vector2 BLOCK_DEFAULT_POSITION  = new Vector2(-1000, -1000);
    public static readonly Color[] COLORS =
        new Color[] { 
            Color.red, 
            Color.blue, 
            Color.green, 
            Color.yellow, 
            new Color(0.5608f, 0, 1), 
            new Color(1, 0.6471f, 0) 
        };

    public const int BOARD_SIZE = 8;
    public const float BLOCK_SIZE = (REF_SCREEN_WIDTH - (BOARD_SIZE + 1) * BLOCKS_OFFSET) / BOARD_SIZE;
    public const float COLOR_BLOCKS_PROBABILITY = 0.95f;
    public const float BOMB_BLOCKS_PROBABILITY = 0.5f;
    public const int COLOR_BLOCKS_BUFFER_SIZE = 64;
    public const int STRIPE_BLOCKS_BUFFER_SIZE = 8;
    public const int BOMB_BLOCKS_BUFFER_SIZE = 8;
    public const float COUNTDOWN = 120;

    private const float REF_SCREEN_WIDTH = 1080;
    private const int BLOCKS_OFFSET = 35;

    public static float IndexToLocation(int index)
    {
        return BLOCKS_OFFSET * (1 + index) + BLOCK_SIZE * (0.5f + index);
    }
}
