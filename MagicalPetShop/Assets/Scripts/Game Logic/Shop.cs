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
    private static float updateTime = Time.time;
    private static int nextCustomerToAppear = -1;

    private static int GetAnimalCount()
    {
        int result = 0;
        foreach (InventoryAnimal ia in PlayerState.THIS.animals)
        {
            result += ia.count;
        }
        return result;
    }
    private static bool HasFreeCustomer()
    {
        foreach (Customer customer in customers)
        {
            if (!customer.hasValue) return true;
        }
        return false;
    }
    private static List<int> GetFreeCustomersIndices()
    {
        List<int> freeIndices = new List<int>();
        for (int i = 0; i < customers.Length; i++)
        {
            if (!customers[i].hasValue) freeIndices.Add(i);
        }
        return freeIndices;
    }

    public static void UpdateCustomers()
    {
        if (!customersComing)
        {
            lastArrivalTime = Utils.EpochTime();
        }
        nextCustomerToAppear = (int)(114f * 1f/Mathf.Sqrt(7.4f*GetAnimalCount() + 0.6f)*Random.Range(0.7f, 1.3f));
        if (!HasFreeCustomer())
        {
            lastArrivalTime = Utils.EpochTime();
        }
        else if ((PlayerState.THIS.speed * (Utils.EpochTime() - lastArrivalTime)) > nextCustomerToAppear * 1000)
        {
            TryAddCustomer();
            lastArrivalTime += (long)((nextCustomerToAppear * 1000) / PlayerState.THIS.speed);
            Random.InitState(System.DateTime.Now.Millisecond);
            nextCustomerToAppear = (int)(114f * 1f / Mathf.Sqrt(7.4f * GetAnimalCount() + 0.6f) * Random.Range(0.7f, 1.3f));
        }
        if (Time.time - updateTime > 0.1) 
        {
            updateTime = Time.time;
        }
    }

    public static void TryAddCustomer()
    {
        if (!HasFreeCustomer())
        {
            return;
        }
        Random.InitState(System.DateTime.Now.Millisecond);
        List<int> freeCustomers = GetFreeCustomersIndices();
        int finalPosition = freeCustomers[Random.Range(0, freeCustomers.Count)];
        
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