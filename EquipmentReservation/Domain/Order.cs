namespace EquipmentReservation.Domain;

public class Order
{
	public Order()
	{
		State = new DraftState();
		OrderItems = new List<OrderItem>() ;
	}


	public void SetState(OrderState state)
	{
		State = state;
	}

	public void TransitToInprogress()
	{
		State.InProgress(this);
	}

	public void TransitToBook ()
	{
		State.Book(this);
	}

	public void TransitToInvoiced ()
	{
		State.Invoiced(this);
	}

	public OrderState State { get;private set; }
	public List<OrderItem> OrderItems { get;private set; }
}