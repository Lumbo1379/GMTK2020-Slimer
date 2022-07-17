using UnityEngine;

public class WaveSideChooserController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Dice")
        {
            var diceRoller = collider.gameObject.transform.parent.GetComponent<WaveDiceRoller>();

            if (diceRoller.IsReadyToChoose())
            {
                diceRoller.StopRolling(int.Parse(collider.gameObject.name));
            }
        }
    }
}
