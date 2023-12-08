using Vagtplan.Interfaces;
using Vagtplan.Interfaces.Services;
using Vagtplan.Models;

namespace Vagtplan.Services
{
    public class DayService : IDayService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DayService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Day GetDay(int id)
        {
            return _unitOfWork.Days.GetById(id);
        }
    }
}
