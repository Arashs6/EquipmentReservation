using System;
using System.Collections.Generic;
using System.Text;
using EquipmentReservation.Domain;

namespace EquipmentReservation.Application
{
	internal class ReservationService
	{
		private readonly IReservationRepository _repository;

		public Task AddOrder()
		{
			var order = _repository.GetOrder();

			return Task.CompletedTask;
		}
	}

	public interface IReservationRepository
	{
		Order GetOrder();
	}
}
