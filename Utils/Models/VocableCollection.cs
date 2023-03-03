using System.Collections.Generic;

namespace Client.Utils.Models;

public class VocableCollection
{
    public int Id { get; set; } = -1;

    public string Name { get; set; }

    public IList<Vocable> Vocables { get; set; }
}