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
    }
}
