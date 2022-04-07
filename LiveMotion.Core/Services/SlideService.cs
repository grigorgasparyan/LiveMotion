using LiveMotion.Core.Entities;
using LiveMotion.Core.Interfaces;
using System.Collections.Generic;

namespace LiveMotion.Core.Services
{
    public class SlideService
    {
        private readonly IBaseRepository _repository;

        public SlideService(IBaseRepository repository)
        {
            _repository = repository;
        }

        public void AddOrUpdate(Slide slide)
        {
            var dbSlide = slide.Id > 0 ? _repository.Find<Slide>(slide.Id) : null;
            if (dbSlide == null)
            {
                _repository.Add(slide);
            }
            else
            {
                dbSlide.Name = slide.Name;
            }
            _repository.SaveChanges();
        }

        public void AddOrUpdateRange(List<Slide> slides)
        {
            foreach (var slide in slides)
            {
                var dbSlide = slide.Id > 0 ? _repository.Find<Slide>(slide.Id) : null;
                if (dbSlide == null)
                {
                    _repository.Add(slide);
                }
                else
                {
                    dbSlide.Stream = slide.Stream;
                    dbSlide.Name = slide.Name;
                }
            }
            _repository.SaveChanges();
        }

        public void Delete(int id)
        {
            var dbSlide = id > 0 ? _repository.Find<Slide>(id) : null;
            if (dbSlide != null)
            {
                _repository.Delete<Slide>(id);
            }
            _repository.SaveChanges();
        }

        public List<Slide> GetSlidesByPresentationId(int presentationId)
        {
            return _repository.GetList<Slide>(x => x.PresentationId == presentationId);
        }
    }
}
