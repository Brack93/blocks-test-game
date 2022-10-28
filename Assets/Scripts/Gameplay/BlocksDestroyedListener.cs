using UnityEngine;

public abstract class BlocksDestroyedListener : MonoBehaviour
{
    protected abstract void BlocksDestroyedHandler(int count);

    protected void OnEnable()
    {
        BoardManager.OnBlocksDestroyed += BlocksDestroyedHandler;
    }

    protected void OnDisable()
    {
        BoardManager.OnBlocksDestroyed -= BlocksDestroyedHandler;
    }

}
