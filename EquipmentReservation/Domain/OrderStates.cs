namespace EquipmentReservation.Domain;

public enum OrderStates
{
	Draft,
	InProgress,
	Booked,
	Invoiced,
	Canceled
}

public abstract class OrderState
{
	public virtual void Draft (Order order) => GenerateExceptionMessage("Draft",order.State);
	public virtual void InProgress (Order order) => GenerateExceptionMessage("InProgress", order.State);
	public virtual void Book (Order order) => GenerateExceptionMessage("Book", order.State);

	private static void GenerateExceptionMessage(string state,OrderState orderState)
	{
		 throw new InvalidOperationException( $"Can not transit from {orderState.GetType().Name} state to {state}.");
	}

	public virtual void Invoiced (Order order) => GenerateExceptionMessage("Invoiced", order.State);
}

public class DraftState : OrderState
{
	public override void InProgress (Order order)
	{
		order.SetState(new InprogressState());
	}
}

public class InprogressState : OrderState
{
	public override void Book(Order order)
	{
		order.SetState(new BookState());
	}
}

public class BookState : OrderState
{
	public override void Invoiced(Order order)
	{
		order.SetState(new InvoicedState());
	}
}
public class InvoicedState : OrderState
{
	public override void Invoiced (Order order)
	{
		
	}
}