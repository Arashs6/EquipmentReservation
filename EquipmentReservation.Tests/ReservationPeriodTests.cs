using FluentAssertions;

namespace EquipmentReservation.Tests;

public class ReservationPeriodTests
{
	private static readonly DateTimeOffset BaseUtc =
		new(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

	private static ReservationPeriod Period(int fromDay, int toDay)
	{
		return new ReservationPeriod(
			BaseUtc.AddDays(fromDay),
			BaseUtc.AddDays(toDay));
	}

	[Theory]
	[MemberData(nameof(WithOverlap))]
	public void When_period_has_overlap_return (ReservationPeriod period)
    { 
	    var reservationPeriod = Period(2, 5);

	var result = reservationPeriod.HasNoOverlap(period);

	result.Should().BeFalse();
    }

    [Theory]
	[MemberData(nameof(WithoutOverlap))]
	public void When_period_has_no_overlap_return_true(
		ReservationPeriod period)
	{
		var reservationPeriod = Period(2, 5);

		var result = reservationPeriod.HasNoOverlap(period);

		result.Should().BeTrue();
	}

	public static TheoryData<ReservationPeriod> WithOverlap =>
		new()
		{
            // هم‌پوشانی از سمت ابتدا
            Period(1, 3),

            // هم‌پوشانی از سمت انتها
            Period(4, 6),

            // کاملاً مساوی
            Period(2, 5),

            // کاملاً داخل بازه اصلی
            Period(3, 4),

            // بازه ورودی، بازه اصلی را پوشش می‌دهد
            Period(1, 6)
		};

	public static TheoryData<ReservationPeriod> WithoutOverlap =>
		new()
		{
            // پایان بازه ورودی = شروع بازه اصلی
            Period(0, 2),

            // شروع بازه ورودی = پایان بازه اصلی
            Period(5, 7),

            // کاملاً قبل از بازه
            Period(0, 1),

            // کاملاً بعد از بازه
            Period(6, 8)
		};

	[Theory]
	[InlineData(2, 2)]
	[InlineData(3, 2)]
	public void When_period_is_invalid_should_throw(
		int fromDay,
		int toDay)
	{
		Action action = () =>
			new ReservationPeriod(
				BaseUtc.AddDays(fromDay),
				BaseUtc.AddDays(toDay));

		action.Should()
			.Throw<ArgumentException>();
	}
}


public record ReservationPeriod
{
		public ReservationPeriod(DateTimeOffset FromDate, DateTimeOffset ToDate)
		{
			if (ToDate <= FromDate)
			{
				throw new ArgumentException();
			}
			this.FromDate = FromDate;
			this.ToDate = ToDate;
		}

		public DateTimeOffset FromDate { get;private set; }
		public DateTimeOffset ToDate { get;private set; }


		public bool HasNoOverlap (ReservationPeriod period)
		{
			return (period.FromDate < FromDate && period.ToDate <= FromDate) || (period.FromDate >= ToDate && period.ToDate > ToDate);
		}

		
	}

