namespace EquipmentReservation.Domain;

public class Equipment
{
	public Guid Id { get; set; }
	public Guid ProductId { get; set; }
	public string SerialNumber { get; set; }
}