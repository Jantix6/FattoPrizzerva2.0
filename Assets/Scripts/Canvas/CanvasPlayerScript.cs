using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPlayerScript : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    [SerializeField] private Image progresBarStamina;
    [SerializeField] private Image progresBarStaminaToRecover;

    [SerializeField] private Image progresBarAdrenalina;


    public void ChangeStamina()
    {
        progresBarStamina.fillAmount = player.stamina.Stamina / player.stamina.MaxStamina;
        progresBarStaminaToRecover.fillAmount = (player.stamina.Stamina + player.stamina.CurrentRegenStamina) / player.stamina.MaxStamina;
    }

    public void ChangeAdrenalina()
    {
        progresBarAdrenalina.fillAmount = player.adrenalina.Adrenalina / player.adrenalina.MaxAdrenalina;
    }

    public void ShowAdrenalina(bool _show)
    {
        progresBarStamina.enabled = !_show;
        progresBarStaminaToRecover.enabled = !_show;
        progresBarAdrenalina.enabled = _show;
    }

}
