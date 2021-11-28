using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;

public static class Shop
{
    [HideInInspector]
    public static bool customersComing = true;
    public static long lastArrivalTime;
    public static Customer[] customers = new Customer[5];
    private static int nullCount = 0;
    private static float updateTime = Time.time;
    private static int nextCustomerToAppear = -1;

    private static int getAnimalCount()
    {
        int result = 0;
        foreach (InventoryAnimal ia in PlayerState.THIS.animals)
        {
            result += ia.count;
        }
        return result;
    }

    public static void UpdateCustomers()
    {
        if (!customersComing)
        {
            lastArrivalTime = Utils.EpochTime();
        }
        if (nextCustomerToAppear==-1)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
            nextCustomerToAppear = (int)(GameLogic.THIS.customerArrivalFrequency * 3 * (1 / Mathf.Sqrt(getAnimalCount() + 5)) * Random.Range(0.7f, 1.3f));
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
            nextCustomerToAppear = (int)(GameLogic.THIS.customerArrivalFrequency * 3 * (1 / Mathf.Sqrt(getAnimalCount() + 5)) * Random.Range(0.7f, 1.3f));
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
        Customer customer = Customer.GenerateCustomer();
        customers[finalPosition] = customer;
        customers[finalPosition].hasValue = true;
        Analytics.LogEvent("new_customer", new Parameter("animal", customer.desiredAnimal.animal.name), new Parameter("rarity", customer.desiredAnimal.rarity.ToString()));
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
                Analytics.LogEvent("animal_sold", new Parameter("animal", customer.desiredAnimal.animal.name), new Parameter("rarity", customer.desiredAnimal.rarity.ToString()));
                return;
            }
        }
        if (Inventory.HasInInventoryPrecise(customer.desiredAnimal))
        {
            int cost = (int)(customer.desiredAnimal.animal.value * GameLogic.THIS.getRarityMultiplier(customer.desiredAnimal.rarity) * PlayerState.THIS.recipes.Find(r => r.animal == customer.desiredAnimal.animal).costMultiplier);
            Inventory.TakeFromInventoryPrecise(customer.desiredAnimal);
            Inventory.AddToInventory(cost);
            PlayerState.THIS.Save();
            Analytics.LogEvent("animal_sold", new Parameter("animal", customer.desiredAnimal.animal.name), new Parameter("rarity", customer.desiredAnimal.rarity.ToString()));
        }
    }
}

[System.Serializable]
public class Customer
{
    public bool hasValue = false;
    public InventoryAnimal desiredAnimal;

    private static InventoryAnimal RandomCraftableAnimal()
    {
        int position = Random.Range(0, PlayerState.THIS.recipes.Count);
        InventoryAnimal result = new InventoryAnimal();
        result.animal = PlayerState.THIS.recipes[position].animal;
        result.rarity = PlayerState.THIS.recipes[position].rarity;
        result.count = 1;
        return result;
    }

    public static Customer GenerateCustomer()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        float rand = Random.Range(0f, 1f);
        Customer result = new Customer();
        if (rand < GameLogic.THIS.orderFromRecipesProbability || PlayerState.THIS.animals.Count == 0)
        {
            result.desiredAnimal = RandomCraftableAnimal();
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                int position = Random.Range(0, PlayerState.THIS.animals.Count);
                result.desiredAnimal = new InventoryAnimal();
                result.desiredAnimal.animal = PlayerState.THIS.animals[position].animal;
                result.desiredAnimal.rarity = PlayerState.THIS.animals[position].rarity;
                result.desiredAnimal.count = 0;
                int animalCount = 0;
                foreach (Customer c in Shop.customers)
                {
                    if (c.hasValue && c.desiredAnimal == result.desiredAnimal)
                    {
                        animalCount++;
                    }
                }
                if (!PlayerState.THIS.animals[position].locked && animalCount + 1 <= PlayerState.THIS.animals[position].count)
                {
                    result.desiredAnimal.count = 1;
                    break;
                }
            }
            if (result.desiredAnimal.count == 0)
            {
                result.desiredAnimal = RandomCraftableAnimal();
            }
        }
        result.hasValue = true;
        return result;
    }
}