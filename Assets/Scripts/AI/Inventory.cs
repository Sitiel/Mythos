using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    //liste de chiffre d'arme
    // 0 unarmed
    // 1 2Handed sword
    // 2 2Handed spear
    // 3 2Handed axe
    // 4 shield spear
    // 
    // 
    // 
    // 
    // 

    private List<int> weapons = new List<int>(0);
    private int currentWeapon = 0;

    
    private bool twoHandedSword = true;
    private bool twoHandedSpear = true;
    private bool twoHandedAxe = true;
    private bool shieldAndSpear = true;

    public void InitialiseInventory()
    {
        weapons.Add(0);
        if (twoHandedSword)
        {
            weapons.Add(1);
        }
        if (twoHandedSpear)
        {
            weapons.Add(2);
        }
        if (twoHandedAxe)
        {
            weapons.Add(3);
        }
        if (shieldAndSpear)
        {
            weapons.Add(4);
        }
    }

    public Inventory()
    {
        this.InitialiseInventory();
    }

    public void AddWeapon(int weaponNB)
    {
        if (!weapons.Contains(weaponNB))
        {
            weapons.Add(weaponNB);
        }
    }

    public void RemoveWeapon(int weaponNB)
    {
        if (weapons.Contains(weaponNB))
        {
            weapons.Remove(weaponNB);
        }
    }

    public void SelectWeapon(int nb)
    {aaaaa
        if (nb < weapons.Count && nb >=0)
        {
            currentWeapon = weapons[nb];
            print(weapons.IndexOf(currentWeapon));
        }
    }

    public void SelectWeapon(bool up)
    {
        int indexLastWeapon = weapons.IndexOf(currentWeapon);
        
        if (up){
            if (indexLastWeapon == weapons.Count-1){
                indexLastWeapon = -1;
            }
            currentWeapon = weapons[indexLastWeapon+1];
        }
        else{
            
            if (indexLastWeapon == 0){
                indexLastWeapon = weapons.Count;
            }
            currentWeapon = weapons[indexLastWeapon-1];
        }
    }

    public int GetCurrentWeapon()
    {
        return currentWeapon;
    }

}
