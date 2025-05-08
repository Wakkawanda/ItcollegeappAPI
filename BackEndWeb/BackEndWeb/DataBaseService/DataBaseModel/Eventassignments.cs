namespace BackEndWeb.DataBaseService.DataBaseModel;

public partial class Eventassignments
{
    public Guid Eventassignmentid { get; set; }

    public Guid Eventid { get; set; }

    public string Studentid { get; set; } = null!;

    public string? Status { get; set; }

    public string? Notes { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Reportevent> Reportevent { get; set; } = new List<Reportevent>();
}
