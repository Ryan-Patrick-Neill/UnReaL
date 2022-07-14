using UnReaL.Database;
using UnReaL.Models;

namespace UnReaL.Repository
{
    public class UnReaLService : IUnReaLService
    {
        private readonly UnReaLContext _context;
        private readonly IBijectionService _bijectionService;

        public UnReaLService(UnReaLContext context, IBijectionService bijectionService)
        {
            _context = context;
            _bijectionService = bijectionService;
        }

        public ShortURL GetById(int id) => _context.ShortURLs.Find(id);

        public ShortURL GetByPath(string path) => _context.ShortURLs.Find(_bijectionService.Decode(path));
            

        public ShortURL GetByUrl(string url) 
            => _context.ShortURLs.Where(shortUrl => shortUrl.Url == url).FirstOrDefault();

        public int Save(ShortURL shortUrl)
        {
            // Check for duplicates
            var existing = _context.ShortURLs.Where(url => url.Url == shortUrl.Url).FirstOrDefault();
            if (existing != null) { return existing.Id; }

            _context.ShortURLs.Add(shortUrl);
            _context.SaveChanges();

            return shortUrl.Id;
        }
    }
}
