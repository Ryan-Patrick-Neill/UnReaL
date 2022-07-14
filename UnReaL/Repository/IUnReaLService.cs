using UnReaL.Models;

namespace UnReaL.Repository
{
    public interface IUnReaLService
    {
        public ShortURL GetById(int id);
        public ShortURL GetByPath(string path);

        public ShortURL GetByUrl(string url);
        public int Save(ShortURL shortUrl);
    }
}
