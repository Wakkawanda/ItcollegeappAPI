namespace BackEndWeb.DataBaseService.DataBaseModel;

public partial class Resultevent
{
    public int Resultid { get; set; }

    public string Result { get; set; } = null!;

    public virtual ICollection<Reportevent> Reportevent { get; set; } = new List<Reportevent>();
}
