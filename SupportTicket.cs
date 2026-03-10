namespace GasSupportApp
{
    // Clasa principala care implementeaza interfata 
    public class SupportTicket : ITicket
    {
        // Proprietati cu setteri privati pentru a proteja datele
        public int Id { get; private set; }
        public string Description { get; private set; }
        public string Diagnostics { get; set; }
        public TicketStatus Status { get; private set; }

        // Constructor pentru initializarea obiectului
        public SupportTicket(int id, string description)
        {
            Id = id;
            Description = description;
            Status = TicketStatus.Open;
        }

        // Metoda pentru a schimba starea tichetului
        public void SetStatus(TicketStatus status) => Status = status;
    }
}