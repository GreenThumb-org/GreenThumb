using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public static FieldManager Instance { get; private set; }

    private readonly List<FieldData> fields = new List<FieldData>();

    public IReadOnlyList<FieldData> Fields => fields;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void RegisterField(FieldData field)
    {
        if (field != null && !fields.Contains(field))
            fields.Add(field);
    }

    public void UnregisterField(FieldData field)
    {
        if (field != null)
            fields.Remove(field);
    }

    public void SimulateAllFields(WeatherDataPoint weather, float hoursElapsed)
    {
        for (int i = 0; i < fields.Count; i++)
            if (fields[i] != null)
                fields[i].SimulateWeatherStep(weather, hoursElapsed);
    }
}