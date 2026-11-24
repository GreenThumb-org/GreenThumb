using UnityEditor.EditorTools;
using UnityEngine;

public class SoilManager : MonoBehaviour
{
    [Tooltip("Enum defining the diffrent soil types with IDs")]
    public enum SoilTypes
    {
        Sand = 2001,
        SandyLoam = 2002,
        Loam = 2003,
        ClayLoam = 2004,
        Clay = 2005
    }
    [Header("Diffrent predefined SoilTypes")]
    [SerializeField]
    Field Paris = new Field(
    cSoilType: SoilTypes.Loam,
    cMoisture: 22.5f,
    cNitrogen: 45.0f,
    cPhosphorus: 30.0f,
    cPotasium: 140.0f,
    cPHValue: 6.8f,
    cOrganicmatter: 3.2f,
    cCompaction: 1.35f
    );
    [Space(2)]

    public static SoilManager Manager;
    private void Awake()
    {
        if(Manager != null && Manager != this)
        {
            Destroy(this);
        }
        Manager = this;
        DontDestroyOnLoad(Manager);
    }
    
}
