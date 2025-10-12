using Axolotl.Application.Interfaces.Repositories;


namespace Axolotl.Application.UseCases {

    public class CreateCategoryUseCase  
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //consructor
        public CreateCategoryUseCase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> ExecuteAsync(CreateCategoryRequest request)
        {
            var category = _mapper.Map<Category>(request);
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<CategoryResponse>(category);
        }
    }
}