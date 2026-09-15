using PostalApi.Models;

namespace PostalApi.Services;

public class PostingService : IPostingService
{
    private static readonly List<Posting> items = new()
    {
        new Posting
        {
            Id = 1,
            From = "Рівне",
            To = "Київ",
            Content = "Посилка з речами",
            DeliveryType = DeliveryType.Standard,
            Weight = 2,
            Width = 20,
            Height = 30,
            Depth = 15,
            Value = 500,
            Price = 70,
            CreatedAt = DateTime.UtcNow
        }
    };

    public Posting Create(Posting newPosting)
    {
        int newId = items.Count > 0 ? items.Max(p => p.Id) + 1 : 1;
        newPosting.Id = newId;
        newPosting.CreatedAt = DateTime.UtcNow;
        newPosting.Price = CalculatePrice(newPosting);

        items.Add(newPosting);
        return newPosting;
    }

    public List<Posting> GetAll() => items;

    public Posting? Find(int id) => items.FirstOrDefault(p => p.Id == id);

    public Posting? Update(Posting posting)
    {
        var found = Find(posting.Id);
        if (found == null) return null;

        found.From = posting.From;
        found.To = posting.To;
        found.Content = posting.Content;
        found.DeliveryType = posting.DeliveryType;
        found.Weight = posting.Weight;
        found.Width = posting.Width;
        found.Height = posting.Height;
        found.Depth = posting.Depth;
        found.Value = posting.Value;
        found.Price = CalculatePrice(found);

        return found;
    }

    public bool Delete(int id)
    {
        var found = Find(id);
        if (found == null) return false;

        items.Remove(found);
        return true;
    }

    private static float CalculatePrice(Posting posting)
    {
        float price = 50 + posting.Weight * 10;

        if (posting.DeliveryType == DeliveryType.Express)
        {
            price *= 1.5f;
        }
        else if (posting.DeliveryType == DeliveryType.Courier)
        {
            price *= 2f;
        }

        return price;
    }
}
