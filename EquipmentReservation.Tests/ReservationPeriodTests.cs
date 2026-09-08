using System.Runtime.InteropServices.JavaScript;
using FluentAssertions;

namespace EquipmentReservation.Tests
{
	public class ReservationPeriodTests
	{
		[Theory]
		[MemberData(nameof(TransactionTestData))]
		public void CanNotReserveWhenRequestedTimeAlreadyReserved(ReservationPeriod period)
		{

			var reservationPeriod = new ReservationPeriod(DateTime.Today.AddDays(2), DateTime.Today.AddDays(4));

			var result = reservationPeriod.hasOverlap(period);

			result.Should().Be(false);
		}

		public static TheoryData<ReservationPeriod> TransactionTestData =>
			new()
			{
				new ReservationPeriod( DateTime.Today.AddDays(1), DateTime.Today.AddDays(3)),
				new ReservationPeriod( DateTime.Today.AddDays(3), DateTime.Today.AddDays(5)),
				new ReservationPeriod( DateTime.Today.AddDays(3), DateTime.Today.AddDays(4)),
				new ReservationPeriod (DateTime.Today.AddDays(1), DateTime.Today.AddDays(5))
			};
	}
}


public record ReservationPeriod
{
		public ReservationPeriod(DateTime FromDate, DateTime ToDate)
		{
			if (ToDate <= FromDate)
			{
				throw new Exception();
			}
			this.FromDate = FromDate;
			this.ToDate = ToDate;
		}

		public DateTime FromDate { get;private set; }
		public DateTime ToDate { get;private set; }


		public bool hasOverlap (ReservationPeriod period)
		{
			return (period.FromDate < FromDate && period.ToDate <= FromDate) || (period.FromDate >= ToDate && period.ToDate > ToDate);
		}

		public void Deconstruct(out DateTime FromDate, out DateTime ToDate)
		{
			FromDate = this.FromDate;
			ToDate = this.ToDate;
		}
	}

