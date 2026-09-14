using EquipmentReservation.Domain;
using FluentAssertions;

namespace EquipmentReservation.Tests;

public class OrderTests
{
	private  Order order;

	public OrderTests()
	{
		order = new Order();
	}
	[Fact]
	public void Order_Should_Create_With_Draft_State()
	{
		order = new Order();

		order.State.Should().BeOfType(typeof(DraftState));
	}

	[Fact]
	public void Order_Can_Transit_To_InProgress_From_Draft()
	{
		Order_Should_Create_With_Draft_State();
		order.TransitToInprogress();

		order.State.Should().BeOfType(typeof(InprogressState));
	}

	[Fact]
	public void Order_Can_Transit_To_Booked_From_Transit ()
	{
		Order_Can_Transit_To_InProgress_From_Draft();
		order.TransitToBook();

		order.State.Should().BeOfType(typeof(BookState));
	}

	[Fact]
	public void Order_Can_Transit_To_Invoiced_From_Booked ()
	{
		Order_Can_Transit_To_Booked_From_Transit();
		order.TransitToInvoiced();
		
		order.State.Should().BeOfType(typeof(InvoicedState));
	}

	[Fact]
	public void Order_Can_Not_Transit_To_Booked_From_Invoiced()
	{
		Order_Can_Transit_To_Invoiced_From_Booked();

		Action action = () => order.TransitToBook();

		action.Should().Throw<InvalidOperationException>().WithMessage($"Can not transit from InvoicedState state to book.");

		order.State.Should().BeOfType(typeof(InvoicedState));
	}
}


