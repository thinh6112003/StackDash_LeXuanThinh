using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Expand SO", menuName = "Assets/Shop Expand")]
public class ShopExpandSO : ScriptableObject
{
	[Header("Strings")]
	public string NameItem = "";

	public string DescriptionItem = "";

	[Header("Floating")]
	public float PriceItem;

	[Header("Integer")]
	public int RefID = 0;
	public int Level = 1;
}
