using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Order
{
    public int edges;                       // Number of edges in the order
    public Color colour;                    // Colour of the order
    public float time = 20f;

    public float timeRemaining = 20f;
}

public class Orders : MonoBehaviour
{
    // Class that defines what an order is. Needs to be able to:
    // create new orders based on the level the player is on (from a list of acceptable shape + colour combinations), 
    // add the orders to a list of currently existing orders,
    // show those current orders in the game,
    // remove the first order.
    
    List<Order> currentOrders;              // List of current orders
    List<Color> colourList;                 // List of colours that orders can be
    private List<Shape> submittedShapes;
    public int level = 1;

    // ---------------------------------------------------------------------------------------------------

    void Start()
    {
        currentOrders = new List <Order>();
        if (level == 1)                     // Change what colours are available based on what level the player is on
        {
            colourList = new List<Color> { Color.red, Color.blue, Color.green };
        }
        else
        {
            colourList = new List<Color> { Color.red, Color.yellow, Color.blue, Color.green, Color.magenta, Color.cyan };
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) { CreateNewOrder(); }
        if (Input.GetKeyDown(KeyCode.X)) { DeleteFirstOrder(); }

        foreach (var currentOrder in currentOrders)
        {
            currentOrder.timeRemaining -= Time.deltaTime;
            if (currentOrder.timeRemaining < 0f)
            {
                RemoveOrder(currentOrder);
                continue;
            }
            
            // Check if any shapes match this order
            var select = submittedShapes.Single(shape => shape.Edges == currentOrder.edges && shape.Colour == currentOrder.colour);
            if (select != null)
            {
                // Complete order
                RemoveOrder(currentOrder);
                submittedShapes.Remove(select);
            }
        }
        
        // Clear submitted shapes
        submittedShapes.Clear();
    }

    public void RemoveOrder(Order order)
    {
        // todo remove orders
    }

    public void CreateNewOrder()
    {
        // NOTE: Do some logic to pick the shape and colour from only valid options for that level.

        if (currentOrders.Count < 5)
        {
            Order order = new Order();
            order.edges = Random.Range(3, 6);
            order.colour = colourList[Random.Range(0, colourList.Count)];
            currentOrders.Add(order);
            UIManager.Instance.AddOrderToUI(order);  // Activate UI panel in correct position according to what index the order is in the currentOrders list
        }
    }

    void DeleteFirstOrder()
    {
        if (currentOrders.Count != 0)
        {
            currentOrders.RemoveAt(0);
            UIManager.Instance.RemoveOrderFromUI(); // Remove UI of first order and move all other orders to new positions according to what index they are in the currentOrders list
        }
        
        
    }

    ~Orders()
    {
        currentOrders.Clear();
        colourList.Clear();
    }
}
