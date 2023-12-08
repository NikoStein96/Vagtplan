using Vagtplan.Models.Dto;

namespace Vagtplan.Interfaces.Services
{
    public interface IShiftService
    {
        bool CreateShift(CreateShiftDto shift);
    }
}
