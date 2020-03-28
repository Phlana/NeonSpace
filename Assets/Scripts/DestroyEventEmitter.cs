using UnityEngine;

public class DestroyEventEmitter : MonoBehaviour
{
    public delegate void OnObjectDestroyedEventHandler(DestroyEventEmitter emitter);
    public event OnObjectDestroyedEventHandler OnObjectDestroyedEvent;
    private void OnDestroy()
    {
        OnObjectDestroyedEvent?.Invoke(this);
    }
}
