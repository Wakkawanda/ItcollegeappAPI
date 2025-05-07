namespace BackEndWeb.DataBaseService.DataBaseModel;

public partial class Company
{
    public int Companyid { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Event> Event { get; set; } = new List<Event>();
}
