namespace BackEndWeb.DataBaseService.DataBaseModel;

public partial class Reportevent
{
    public int Reportid { get; set; }

    public Guid Taskassignmentsid { get; set; }

    public string? Body { get; set; }

    public int Resultid { get; set; }

    public string? Status { get; set; }

    public virtual Resultevent Result { get; set; } = null!;

    public virtual Eventassignments Taskassignments { get; set; } = null!;
}
