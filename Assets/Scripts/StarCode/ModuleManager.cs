using SpatialSys.UnitySDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModuleManager : MonoBehaviour
{
    [Header("Slots (en orden). El manager permitirá colocar hasta maxPlacedCount runas/objetos.")]
    public List<Transform> positions = new List<Transform>();

    [Header("Runas/objetos colocados (por índice)")]
    public List<ItemType> placedItems = new List<ItemType>();

    [Header("Máximo de objetos colocables (por diseño quieres 3)")]
    public int maxPlacedCount = 3;

    [Header("Eventos")]
    public UnityEvent OnSuccess; // se dispara si al menos 2 son Good (al verificar)
    public UnityEvent OnFail;
    public UnityEvent OnPlacedGood; // se dispara cuando se coloca correctamente un GOOD
    public UnityEvent OnPlacedBad;  // se dispara cuando se intenta colocar un item NO-GOOD

    public int indexQuest;
    public SpatialQuest quest;

    private void Awake()
    {
        NormalizePlacedList();
    }

    private void OnValidate()
    {
        NormalizePlacedList();
    }

    private void NormalizePlacedList()
    {
        if (placedItems == null) placedItems = new List<ItemType>();
        // Queremos que placedItems tenga el tamaño de positions (para indexación), pero
        // solo se permitirá colocar hasta maxPlacedCount items.
        while (placedItems.Count < positions.Count)
            placedItems.Add(null);
        while (placedItems.Count > positions.Count)
            placedItems.RemoveAt(placedItems.Count - 1);
    }

    /// <summary>
    /// Coloca en la siguiente ranura vacía, sólo si no se ha alcanzado maxPlacedCount.
    /// </summary>
    public void PlaceCurrentItemNextAvailable()
    {
        int filled = CountPlaced();
        if (filled >= maxPlacedCount)
        {
            Debug.Log("Ya alcanzaste el máximo de objetos colocados.");
            return;
        }

        int idx = NextEmptyIndex();
        if (idx < 0)
        {
            Debug.Log("No hay ranuras vacías disponibles.");
            return;
        }

        PlaceCurrentItemAtIndex(idx);
    }

    /// <summary>
    /// Coloca el objeto agarrado en el índice indicado.
    /// </summary>
    public void PlaceCurrentItemAtIndex(int index)
    {
        if (index < 0 || index >= positions.Count)
        {
            Debug.LogError("Index fuera de rango.");
            return;
        }

        if (PickModule.instance == null)
        {
            Debug.LogWarning("PickModule.instance no encontrado.");
            return;
        }

        GameObject obj = PickModule.instance.GetCurrentObject();
        if (obj == null)
        {
            Debug.Log("No hay objeto agarrado.");
            return;
        }

        ItemType item = obj.GetComponent<ItemType>();
        if (item == null)
        {
            Debug.LogWarning("El objeto agarrado no tiene ItemType.");
            return;
        }

        // Si ya hay el máximo de colocadas, no permitir más.
        if (CountPlaced() >= maxPlacedCount)
        {
            Debug.Log("No se puede colocar: ya hay el máximo de objetos colocados.");
            return;
        }

        // --- NUEVO: solo permitir colocar si es GOOD ---
        if (item.type != ItemType.Type.good)
        {
            Debug.Log($"Intento de colocar item NO-GOOD ({item.type}). Colocación cancelada.");
            OnPlacedBad?.Invoke(); // dispara evento para feedback (audio, UI, etc.)
            return; // no colocamos el item
        }
        // ------------------------------------------------

        // Si es GOOD, procedemos a colocar
        obj.transform.position = positions[index].position;
        obj.transform.rotation = Quaternion.identity;

        // Desactivar interacción del item (si aplica)
        if (item.interactable != null) item.interactable.enabled = false;

        // Guardar referencia
        placedItems[index] = item;

        // Liberar del Pick (el jugador ya no lo tiene en la mano)
        PickModule.instance.Release();

        Debug.Log($"Item GOOD colocado en slot {index}: {item.type} ({obj.name}). Total colocados: {CountPlaced()}");

        // Evento específico de colocación correcta
        OnPlacedGood?.Invoke();

        // Si alcanzamos el máximo, verificamos la condición final
        if (CountPlaced() >= maxPlacedCount)
        {
            VerifyCondition();
            return;
        }
    }

    /// <summary>
    /// Retorna el primer índice vacío (o -1).
    /// </summary>
    private int NextEmptyIndex()
    {
        for (int i = 0; i < placedItems.Count; i++)
        {
            if (placedItems[i] == null) return i;
        }
        return -1;
    }

    /// <summary>
    /// Cuenta cuántos items están colocados (no null).
    /// </summary>
    public int CountPlaced()
    {
        int c = 0;
        for (int i = 0; i < placedItems.Count; i++)
            if (placedItems[i] != null) c++;
        return c;
    }

    /// <summary>
    /// Devuelve a todos los items colocados a su transform original y limpia la lista.
    /// </summary>
    public void ResetAll()
    {
        for (int i = 0; i < placedItems.Count; i++)
        {
            if (placedItems[i] != null)
            {
                placedItems[i].ResetTransform();
                placedItems[i] = null;
            }
        }
        Debug.Log("Items reseteados.");
    }

    /// <summary>
    /// Verifica la condición: si hay al menos 2 Good entre los colocados -> success.
    /// Requiere al menos 1 item colocado para evaluar (si quieres otra lógica puedes cambiarlo).
    /// </summary>
    public bool VerifyCondition()
    {
        int goodCount = 0;
        int placedCount = 0;

        for (int i = 0; i < placedItems.Count; i++)
        {
            if (placedItems[i] == null) continue;
            placedCount++;
            if (placedItems[i].type == ItemType.Type.good) goodCount++;
        }

        // Si no hay items colocados podemos considerar fail (o decidir otra cosa).
        if (placedCount == 0)
        {
            OnFail?.Invoke();
            Debug.Log("Verificación: no hay items colocados -> Fail.");
            return false;
        }

        if (goodCount >= 2)
        {
            OnSuccess?.Invoke();
            quest.tasks[indexQuest].CompleteTask();
            Debug.Log($"Verificación OK: {goodCount} Good (>=2).");
            return true;
        }
        else
        {
            OnFail?.Invoke();
            quest.tasks[indexQuest].CompleteTask();
            Debug.Log($"Verificación Fail: solo {goodCount} Good (<2).");
            return false;
        }
    }
}
