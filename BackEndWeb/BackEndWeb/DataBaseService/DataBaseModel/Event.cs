namespace BackEndWeb.DataBaseService.DataBaseModel;

public partial class Event
{
    public Guid Eventid { get; set; }

    public string Title { get; set; } = null!;

    public int Typeid { get; set; }

    public string? Information { get; set; }

    public int Companyid { get; set; }

    public string? Themeevent { get; set; }

    public string? Bodytask { get; set; }

    public DateTime? Startdate { get; set; }

    public DateTime? Enddate { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Eventassignments> Eventassignments { get; set; } = new List<Eventassignments>();

    public virtual Typeevent Type { get; set; } = null!;
}
