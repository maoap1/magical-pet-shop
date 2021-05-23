using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shop
{
    [HideInInspector]
    public static bool customersComing = true;
    public static long lastArrivalTime;
    public static Customer[] customers = new Customer[5];
    private static int nullCount = 0;
    private static float updateTime = Time.time;
    private static int nextCustomerToAppear = -1;

    public static void UpdateCustomers()
    {
        if (!customersComing)
        {
            lastArrivalTime = Utils.EpochTime();
        }
        if (nextCustomerToAppear==-1)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            nextCustomerToAppear = (int)(GameLogic.THIS.customerArrivalFrequency * Random.Range(0.7f, 1.3f));
        }
        if (nullCount == 0)
        {
            lastArrivalTime = Utils.EpochTime();
        }
        else if ((PlayerState.THIS.speed * (Utils.EpochTime() - lastArrivalTime)) > nextCustomerToAppear * 1000)
        {
            TryAddCustomer();
            lastArrivalTime += (long)((nextCustomerToAppear * 1000) / PlayerState.THIS.speed);
            Random.InitState(System.DateTime.Now.Millisecond);
            nextCustomerToAppear = (int)(GameLogic.THIS.customerArrivalFrequency * Random.Range(0.7f, 1.3f));
        }
        if (Time.time - updateTime > 0.1) {
            updateTime = Time.time;
            nullCount = 0;
            for (int i = 0; i < 5; i++) {
                if (!customers[i].hasValue) {
                    nullCount++;
                }
            }
        }
    }

    public static void TryAddCustomer()
    {
        if (nullCount == 0)
        {
            return;
        }
        Random.InitState(System.DateTime.Now.Millisecond);
        int position = Random.Range(0, nullCount);
        int currentPosition = 0;
        int finalPosition = 0;
        for (int i = 0; i < 5; i++)
        {
            if (!customers[i].hasValue)
            {
                if (position == currentPosition)
                {
                    finalPosition = i;
                }
                currentPosition++;
            }
        }
        nullCount--;
        customers[finalPosition] = Customer.GenerateCustomer();
        customers[finalPosition].hasValue = true;
        PlayerState.THIS.Save();
    }


    public static void RemoveCustomer(Customer customer)
    {
        for (int i = 0; i<5; i++)
        {
            if (customers[i]==customer)
            {
                nullCount++;
                customers[i].hasValue = false;
                PlayerState.THIS.Save();
                break;
            }
        }
    }

    public static void SellTo(Customer customer)
    {
        for (int i = 0; i < 5; i++)
        {
            if (customers[i] == customer && Inventory.HasInInventoryPrecise(customer.desiredAnimal))
            {
                int cost = (int)(customer.desiredAnimal.animal.value * GameLogic.THIS.getRarityMultiplier(customer.desiredAnimal.rarity) * PlayerState.THIS.recipes.Find(r => r.animal == customer.desiredAnimal.animal).costMultiplier);
                Inventory.TakeFromInventoryPrecise(customer.desiredAnimal);
                Inventory.AddToInventory(cost);
                nullCount++;
                customers[i].hasValue = false;
                PlayerState.THIS.Save();
                return;
            }
        }
        if (Inventory.HasInInventoryPrecise(customer.desiredAnimal))
        {
            int cost = (int)(customer.desiredAnimal.animal.value * GameLogic.THIS.getRarityMultiplier(customer.desiredAnimal.rarity) * PlayerState.THIS.recipes.Find(r => r.animal == customer.desiredAnimal.animal).costMultiplier);
            Inventory.TakeFromInventoryPrecise(customer.desiredAnimal);
            Inventory.AddToInventory(cost);
            PlayerState.THIS.Save();
        }
    }
}

[System.Serializable]
public class Customer
{
    public bool hasValue = false;
    public InventoryAnimal desiredAnimal;

    public static Customer GenerateCustomer()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        float rand = Random.Range(0f, 1f);
        Customer result = new Customer();
        if (rand < GameLogic.THIS.orderFromRecipesProbability || PlayerState.THIS.animals.Count == 0)
        {
            int position = Random.Range(0, PlayerState.THIS.recipes.Count);
            result.desiredAnimal = new InventoryAnimal();
            result.desiredAnimal.animal = PlayerState.THIS.recipes[position].animal;
            result.desiredAnimal.rarity = PlayerState.THIS.recipes[position].rarity;
            result.desiredAnimal.count = 1;
        }
        else
        {
            int position = Random.Range(0, PlayerState.THIS.animals.Count);
            result.desiredAnimal = new InventoryAnimal();
            result.desiredAnimal.animal = PlayerState.THIS.animals[position].animal;
            result.desiredAnimal.rarity = PlayerState.THIS.animals[position].rarity;
            result.desiredAnimal.count = 1;
        }
        result.hasValue = true;
        return result;
    }
}