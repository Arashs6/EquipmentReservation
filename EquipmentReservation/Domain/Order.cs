namespace EquipmentReservation.Domain;

public class Order
{
	private readonly List<OrderItem> _orderItems = new();

	public OrderState State { get; private set; }
	public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();
	public DateTimeOffset CreateDateTime { get; private set; }

	public Order()
	{
		State = new DraftState();
		CreateDateTime = DateTimeOffset.UtcNow;
	}

	public void AddItems(OrderItem items)
	{
		if (items is null)
		{
			throw new ArgumentNullException();
		}

		if (_orderItems.Any(a=>a.EquipmentId == items.EquipmentId) )
		{
			throw new InvalidOperationException();
		}
		_orderItems.AddRange(items);
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


}