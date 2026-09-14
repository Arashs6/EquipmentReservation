using System;
using System.Collections.Generic;
using System.Text;

namespace EquipmentReservation.Domain
{
	public record ReservationPeriod
	{
		public ReservationPeriod(DateTimeOffset FromDate, DateTimeOffset ToDate)
		{
			if (ToDate <= FromDate)
			{
				throw new ArgumentException("To date should always be later than from date");
			}
			this.FromDate = FromDate;
			this.ToDate = ToDate;
		}

		public DateTimeOffset FromDate { get; private set; }
		public DateTimeOffset ToDate { get; private set; }


		public bool HasNoOverlap(ReservationPeriod period)
		{
			return period is null ? throw new ArgumentException("Reservation period can not be empty.") : (period.FromDate < FromDate && period.ToDate <= FromDate) || (period.FromDate >= ToDate && period.ToDate > ToDate);
		}
	}
}
