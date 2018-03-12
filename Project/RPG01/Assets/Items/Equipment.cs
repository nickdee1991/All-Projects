using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot; //Slot to store equipment in
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegion;

    public int armorModifier; // increase / decrease in armour
    public int damageModifier; // increase / decrease in damage

    // when pressed in inventory
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this); // equip it
        RemoveFromInventory(); // remove it from inventory
    }
}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Shield, Feet }
public enum EquipmentMeshRegion { Legs, Arms, Torso}; // corresponds to body blendshapes.
