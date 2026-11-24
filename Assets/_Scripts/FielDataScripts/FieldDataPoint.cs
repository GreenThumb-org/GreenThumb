using UnityEngine;

public class Field
{
    public SoilManager.SoilTypes SoilType;
    public float Moisture;
    public float Nitrogen;
    public float Phosphorus;
    public float Potasium;
    public float PHValue;
    public float Organicmatter;
    public float Compaction;
    
    private float WaterLossFactor;
    private float WaterIntakeFactor;
    private float WaterIntakeThreshhold;
    public Field(SoilManager.SoilTypes cSoilType, float cMoisture, float cNitrogen, float cPhosphorus, float cPotasium, float cPHValue, float cOrganicmatter, float cCompaction)
    {
        this.SoilType = cSoilType;
        this.Moisture = cMoisture;
        this.Nitrogen = cNitrogen;
        this.Phosphorus = cPhosphorus;
        this.Potasium = cPotasium;
        this.PHValue = cPHValue;
        this.Organicmatter = cOrganicmatter;
        this.Compaction = cCompaction;

        switch (this.SoilType)
        {
            case SoilManager.SoilTypes.Sand:
                this.WaterLossFactor = 0.9f;
                this.WaterIntakeFactor = 0.95f;
                this.WaterIntakeThreshhold = 25f;
            break;
            case SoilManager.SoilTypes.SandyLoam:
            break;
            case SoilManager.SoilTypes.Loam:
            break;
            case SoilManager.SoilTypes.ClayLoam:
            break;
            case SoilManager.SoilTypes.Clay:
            break;
            default:
                this.WaterLossFactor = 0.5f;
                this.WaterIntakeFactor = 0.5f;
                this.WaterIntakeThreshhold = 20f;
            break;
        }
    }
    public void ReactToWeather()
    {
        CalculateMoisture();
    }
    private void CalculateMoisture()
    {
        if(WeatherDataManager.Manager.CurrentDataPoint.precipitation > 0)
        {
            if(this.Moisture > this.WaterIntakeThreshhold)
            {
                this.Moisture += (WeatherDataManager.Manager.CurrentDataPoint.precipitation * 0f);
            }
            else
            {
                this.Moisture += (WeatherDataManager.Manager.CurrentDataPoint.precipitation * this.WaterIntakeFactor);
            }
        }
        else
        {
            float Waterlost = this.WaterLossFactor * (1f + (0.2f - 1f) * (1f - Mathf.Exp(-0.05f * WeatherDataManager.Manager.CurrentDataPoint.temprature)));
            if(Waterlost <= 0)
            {
                Waterlost = 0;
            }
            this.Moisture = this.Moisture * Waterlost;
        }
    }
    public void PrintFieldData()
    {
        Debug.Log("SoilType: " + SoilType);
        Debug.Log("Moisture: " + Moisture);
        Debug.Log("Nitrogen: " + Nitrogen);
        Debug.Log("Phosphorus: " + Phosphorus);
        Debug.Log("Potasium: " + Potasium);
        Debug.Log("PHValue: " + PHValue);
        Debug.Log("Organicmatter: " + Organicmatter);
        Debug.Log("Compaction: " + Compaction);
    }
}
