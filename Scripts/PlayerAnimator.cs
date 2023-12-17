using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;


    protected override void Start()
    {
        base.Start();
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;

        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>(new EquipmentEqualityComparer());

        foreach (WeaponAnimations a in weaponAnimations)
        {
            // Check if the Equipment is not null before adding to the dictionary
            if (a.weapon != null)
            {
                weaponAnimationsDict.Add(a.weapon, a.clips);
            }
            else
            {
                Debug.LogError("Trying to add a null key to weaponAnimationsDict.");
            }
        }
    }

    // Custom equality comparer for Equipment to compare based on equipment type
    public class EquipmentEqualityComparer : IEqualityComparer<Equipment>
    {
        public bool Equals(Equipment x, Equipment y)
        {
            // Compare based on equipment type
            return x != null && y != null && x.GetType() == y.GetType();
        }

        public int GetHashCode(Equipment obj)
        {
            return obj.GetType().GetHashCode();
        }
    }



    void OnEquipmentChanged (Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);
            if (weaponAnimationsDict.ContainsKey(newItem))
            {
                currentAttackAnimSet = weaponAnimationsDict[newItem];
            }

        }

        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 0);
            currentAttackAnimSet = defaultAttackAnimSet;
        }

        if (newItem != null && newItem.equipSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 1);
        }

        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Shield)
        {
            animator.SetLayerWeight(2, 0);
        }
    }

    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
