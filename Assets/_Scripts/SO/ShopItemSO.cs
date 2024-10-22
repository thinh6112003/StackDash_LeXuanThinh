using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item SO", menuName = "Assets/Shop Item")]
public class ShopItemSO : ScriptableObject
{
	[Header("Strings")]
	public string NameItem = "";

	public string DescriptionItem = "";

	[Header("Floating")]
	public float PriceItem;
	public float PriceMarket;
	public float deliTimeInEachItem;

	[Header("Integer")]
	public int energyCost;
	public int Level = 1;

	[Header("OBJ")]
	public GameObject OBJ;

	[Header("UI")]
	public Sprite ImageIcon;

	[Header("Boolean Manager")]
	public bool AddToBasket;

	public bool IsBigBox;

	public bool ShouldPlaceInside;

	public bool IsItem = true;

	[Header("License")]
	public LicenseSO License;

	public float GetBoxPrice()
    {
		if (!IsItem)
		//{
		//	return Mathf.RoundToInt(PriceItem * YelbController.ITEMS_PER_BOX);
		//}
		//else
			return PriceItem;
		return 0;
    }
}
