using LiveMotion.Core.Entities;
using LiveMotion.Core.Interfaces;
using System.Collections.Generic;

namespace LiveMotion.Core.Services
{
    public class PresentationService
    {
        private readonly IBaseRepository _repository;

        public PresentationService(IBaseRepository repository)
        {
            _repository = repository;
        }


        public Presentation AddOrUpdate(Presentation presentation)
        {
            var dbPresentation = presentation.Id > 0 ? _repository.Find<Presentation>(presentation.Id) : null;
            if(dbPresentation == null)
            {
                _repository.Add(presentation);
            }
            else
            {
                dbPresentation.Name = presentation.Name;
            }
            _repository.SaveChanges();

            return dbPresentation;
        }

        public List<Presentation> GetAll()
        {
            return _repository.GetAll<Presentation>();
        }

        public void Delete(int id)
        {
            var dbPresentation = id > 0 ? _repository.Find<Presentation>(id) : null;
            if (dbPresentation != null)
            {
                _repository.Delete<Presentation>(id);
            }
            _repository.SaveChanges();
        }
    }
}
