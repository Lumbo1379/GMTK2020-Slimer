using UnityEngine;

public class SideChooserController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Dice")
        {
            var diceRoller = collider.gameObject.transform.parent.GetComponent<DiceRoller>();

            if (diceRoller._isActivated && diceRoller.IsReadyToChoose())
            {
                diceRoller.StopRolling(int.Parse(collider.gameObject.name));
            }
        }
    }
}
