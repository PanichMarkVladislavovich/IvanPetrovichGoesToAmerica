using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponWheelbuttonscript : MonoBehaviour
{
    public int WeaponId;
    public string WeaponName;
    public TextMeshProUGUI WeaponText;
    public Image WeaponSelected;
    private bool selected = false;
    public Sprite WeaponIcon;
    public Button WeaponButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        WeaponButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            WeaponSelected.sprite = WeaponIcon;
            WeaponText.text = WeaponName;
        }
    }

    public void Selected()
    {
        selected = true;
    }

	public void DeSelected()
	{
		selected = false;
	}


    public void HoverEnter()
    {
        WeaponText.text = WeaponName;
        ColorBlock colors = WeaponButton.colors;
        colors.highlightedColor = Color.yellow;
        WeaponButton.colors = colors;


	}

	public void HoverExit()
	{
		WeaponText.text = "";
	}

}
