using PostalApi.Models;

namespace PostalApi.Dtos;

public class PostingPutDto
{
    public int Id { get; set; }
    public string From { get; set; } = "";
    public string To { get; set; } = "";
    public string Content { get; set; } = "";
    public DeliveryType DeliveryType { get; set; }
    public float Weight { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Depth { get; set; }
    public float Value { get; set; }
}
