namespace Client.Utils.Dto.Vocables;

public class VocableCollectionsCreateDto
{
    public VocableCollectionsCreateDto(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}