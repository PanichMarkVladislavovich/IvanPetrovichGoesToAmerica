using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelbuttonscript : MonoBehaviour
{
    public string AvailableWeaponName;
    public TextMeshProUGUI WeaponText;
    public Image WeaponSelected;
    public Sprite WeaponIcon;
    public Button WeaponButton;
    public WeaponController weaponController;
    public WeaponWheelController weaponWheelController;

    void Start()
    {
        WeaponButton = GetComponent<Button>();
    }

    public void HoverEnter()
    {
		WeaponText.text = AvailableWeaponName;
		
		ColorBlock colors = WeaponButton.colors;
        colors.highlightedColor = colors.pressedColor;
        WeaponButton.colors = colors;
	}

	public void HoverExit()
	{
        if (weaponWheelController.IsWeaponLeftHand)
        {
            WeaponText.text = weaponController.LeftHandWeapon?.WeaponNameUI;
        }
        else if (weaponWheelController.IsWeaponLeftHand == false)
		{
			WeaponText.text = weaponController.RightHandWeapon?.WeaponNameUI;
		}
	}

    public void ChangeWeaponWheelButtonColorToActive(Button buttonType)
    {
		ColorBlock colors = buttonType.colors;
		colors.normalColor = Color.yellow;
		buttonType.colors = colors;
	}

	public void ChangeWeaponWheelButtonColorToDefault(Button buttonType)
	{
		ColorBlock colors = buttonType.colors;
		colors.normalColor = colors.selectedColor;
		buttonType.colors = colors;
	}
}
