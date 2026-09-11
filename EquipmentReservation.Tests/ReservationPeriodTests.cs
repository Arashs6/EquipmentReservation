using System.Runtime.InteropServices.JavaScript;
using FluentAssertions;

namespace EquipmentReservation.Tests
{
	public class ReservationPeriodTests
	{
		private static DateTimeOffset FiveDaysLater { get; set; } = DateTimeOffset.UtcNow.AddDays(5);
		[Theory]
		[MemberData(nameof(WithOverLap))]
		public void when_period_has_overLap_return_false(ReservationPeriod period)
		{

			var reservationPeriod = new ReservationPeriod(DateTimeOffset.UtcNow.AddDays(2), DateTimeOffset.UtcNow.AddDays(4));

			var result = reservationPeriod.hasNoOverlap(period);

			result.Should().Be(false);
		}

		[Theory]
		[MemberData(nameof(WithNoOverLap))]
		public void when_period_has_no_overLap_return_true (ReservationPeriod period)
		{

			var reservationPeriod = new ReservationPeriod(DateTimeOffset.UtcNow.AddDays(3), FiveDaysLater);

			var result = reservationPeriod.hasNoOverlap(period);

			result.Should().Be(true);
		}

		public static TheoryData<ReservationPeriod> WithOverLap =>
			new()
			{
				new ReservationPeriod( DateTimeOffset.UtcNow.AddDays(2), DateTimeOffset.UtcNow.AddDays(3)),
				new ReservationPeriod( DateTimeOffset.UtcNow.AddDays(3), FiveDaysLater),
				new ReservationPeriod( DateTimeOffset.UtcNow.AddDays(3), DateTimeOffset.UtcNow.AddDays(4)),
				new ReservationPeriod (DateTimeOffset.UtcNow.AddDays(1), FiveDaysLater),
			};

		public static TheoryData<ReservationPeriod> WithNoOverLap
		{
			get
			{
				
				return new()
				{
					new ReservationPeriod( DateTimeOffset.UtcNow.AddDays(1), DateTimeOffset.UtcNow.AddDays(2)),
					new ReservationPeriod( DateTimeOffset.UtcNow.AddDays(1), DateTimeOffset.UtcNow.AddDays(3)),
					new ReservationPeriod(FiveDaysLater, DateTimeOffset.UtcNow.AddDays(6)),
					new ReservationPeriod( DateTimeOffset.UtcNow.AddDays(6), DateTimeOffset.UtcNow.AddDays(8)),
				};
			}
		}
	}
}


public record ReservationPeriod
{
		public ReservationPeriod(DateTimeOffset FromDate, DateTimeOffset ToDate)
		{
			if (ToDate <= FromDate)
			{
				throw new Exception();
			}
			this.FromDate = FromDate;
			this.ToDate = ToDate;
		}

		public DateTimeOffset FromDate { get;private set; }
		public DateTimeOffset ToDate { get;private set; }


		public bool hasNoOverlap (ReservationPeriod period)
		{
			return (period.FromDate < FromDate && period.ToDate <= FromDate) || (period.FromDate >= ToDate && period.ToDate > ToDate);
		}

		
	}

