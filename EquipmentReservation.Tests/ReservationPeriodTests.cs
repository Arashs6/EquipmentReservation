using System.Runtime.InteropServices.JavaScript;
using FluentAssertions;

namespace EquipmentReservation.Tests
{
	public class ReservationPeriodTests
	{
		[Theory]
		[MemberData(nameof(WithOverLap))]
		public void when_period_has_overLap_return_false(ReservationPeriod period)
		{

			var reservationPeriod = new ReservationPeriod(DateTime.Today.AddDays(2), DateTime.Today.AddDays(4));

			var result = reservationPeriod.hasNoOverlap(period);

			result.Should().Be(false);
		}

		[Theory]
		[MemberData(nameof(WithNoOverLap))]
		public void when_period_has_no_overLap_return_true (ReservationPeriod period)
		{

			var reservationPeriod = new ReservationPeriod(DateTime.Today.AddDays(3), DateTime.Today.AddDays(5));

			var result = reservationPeriod.hasNoOverlap(period);

			result.Should().Be(true);
		}

		public static TheoryData<ReservationPeriod> WithOverLap =>
			new()
			{
				new ReservationPeriod( DateTime.Today.AddDays(1), DateTime.Today.AddDays(3)),
				new ReservationPeriod( DateTime.Today.AddDays(3), DateTime.Today.AddDays(5)),
				new ReservationPeriod( DateTime.Today.AddDays(3), DateTime.Today.AddDays(4)),
				new ReservationPeriod (DateTime.Today.AddDays(1), DateTime.Today.AddDays(5)),
			};

		public static TheoryData<ReservationPeriod> WithNoOverLap =>
			new()
			{
				new ReservationPeriod( DateTime.Today.AddDays(1), DateTime.Today.AddDays(2)),
				new ReservationPeriod( DateTime.Today.AddDays(1), DateTime.Today.AddDays(3)),
				new ReservationPeriod( DateTime.Today.AddDays(5), DateTime.Today.AddDays(6)),
				new ReservationPeriod( DateTime.Today.AddDays(6), DateTime.Today.AddDays(8)),
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


		public bool hasNoOverlap (ReservationPeriod period)
		{
			return (period.FromDate < FromDate && period.ToDate <= FromDate) || (period.FromDate >= ToDate && period.ToDate > ToDate);
		}

		public void Deconstruct(out DateTime FromDate, out DateTime ToDate)
		{
			FromDate = this.FromDate;
			ToDate = this.ToDate;
		}
	}

