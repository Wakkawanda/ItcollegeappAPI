namespace BackEndWeb.DataBaseService.DataBaseModel;

public partial class Typeevent
{
    public int Typeid { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Event> Event { get; set; } = new List<Event>();
}
