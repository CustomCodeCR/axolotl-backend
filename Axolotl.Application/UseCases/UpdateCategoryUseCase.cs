namespace Axolotl.Application.UseCases
{
    public class UpdateCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //consructor
        public UpdateCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ExecuteAsync(int id, UpdateCategoryRequest request)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new NotFoundException($"Category with Id {id} not found.");
            }

            _mapper.Map(request, category);
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }

}