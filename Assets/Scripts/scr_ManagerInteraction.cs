using UnityEngine;
using UnityEngine.Events;

public interface scr_ManagerInteraction
{
    void OpenMenu(GameObject actor);
    UnityEvent OnUse { get; }
}