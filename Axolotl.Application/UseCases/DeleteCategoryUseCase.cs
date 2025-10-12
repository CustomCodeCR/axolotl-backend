    using Axolotl.Application.Interfaces.Repositories;
    using Axolotl.Domain.Entities;
    using AutoMapper;

namespace Axolotl.Application.UseCases
{

    public class DeleteCategoryUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        //constructor
        public DeleteCategoryUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> ExecuteAsync(int id)
        {
            var category = await _unitOfWork.Categories.GetByIdAsync(id);
            if (category == null)
                return false;

            _unitOfWork.Categories.Remove(category);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}