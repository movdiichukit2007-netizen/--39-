using PostalApi.Models;

namespace PostalApi.Services;

public interface IPostingService
{
    Posting Create(Posting newPosting);
    List<Posting> GetAll();
    Posting? Find(int id);
    Posting? Update(Posting posting);
    bool Delete(int id);
}
