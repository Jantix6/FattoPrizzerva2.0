using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName = "Karma Data Container",menuName = "Karma data container")]
public class DC_Karma : ScriptableObject
{
    [SerializeField] private int localKarma;            // carma adquired or lost on the dialog
    [SerializeField] private int globalKarma;           // carma adquired or lost during the game

    private void OnEnable()
    {
        localKarma = 0;
        globalKarma = 0;
    }


    /// <summary>
    /// Increase or decrease the amount of LOCAL CARMA
    /// </summary>
    /// <param name="_delta"></param>
    public void ModifyLocalKarma(int _delta)
    {
        localKarma += _delta;

        Debug.Log("Local karma increased by " + _delta);
    }
    /// <summary>
    /// Apply the acumulated LOCAL KARMA TO THE GLOBAL KARMA
    /// </summary>
    public void ApplyLocakToGlobalKarma()
    {
        globalKarma += localKarma;
        Debug.Log("Aplying local karma: " + localKarma + "to global karma: " + globalKarma);
        localKarma = 0;

        Debug.Log("Global karma is now: " + globalKarma);
    }

}
