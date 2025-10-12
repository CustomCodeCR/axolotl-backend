using Axolotl.Application.DTOs;
using Axolotl.Application.Interfaces.Repositories;
using AutoMapper;

namespace Axolotl.Application.UseCases
{
    public class GetAllCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //consructor
        public GetAllCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CategoryResponse>> ExecuteAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryResponse>>(categories);
        }
    }
}
