namespace EquipmentReservation.Domain;

public class Order
{
	public Order()
	{
		State = new DraftState();
		OrderItems = new List<OrderItem>() ;
		CreateDateTime = DateTimeOffset.UtcNow;
		
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
	public IReadOnlyCollection<OrderItem> OrderItems { get;private set; }
	public DateTimeOffset CreateDateTime { get;private set; }
}