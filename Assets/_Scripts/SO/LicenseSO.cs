using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "License SO", menuName = "Assets/License")]
public class LicenseSO : ScriptableObject
{
	[Header("Strings")]
	public string NameLicense = "";
	public string DescriptionLicense = "";

	[Header("Floating")]
	public float PriceLicense;

	[Header("Integer")]
	public int Level = 1;

	[Header("UI")]
	public Sprite ImageIcon;
}
