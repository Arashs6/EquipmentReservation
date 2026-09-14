namespace EquipmentReservation.Domain;

public class OrderItem
{
	public OrderItem(Guid id, Guid equipmentId, ReservationPeriod reservationPeriod)
	{
		Id = id;
		EquipmentId = equipmentId;
		ReservationPeriod = reservationPeriod;
	}
	public Guid Id { get;private set; }
	public Guid EquipmentId { get;private set; }
	public ReservationPeriod ReservationPeriod { get;private set; }
}

