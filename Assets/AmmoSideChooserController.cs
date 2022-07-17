using UnityEngine;

public class AmmoSideChooserController : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Dice")
        {
            var diceRoller = collider.gameObject.transform.parent.GetComponent<AmmoDiceRoller>();

            if (diceRoller.IsReadyToChoose())
            {
                diceRoller.StopRolling(int.Parse(collider.gameObject.name));
            }
        }
    }
}
