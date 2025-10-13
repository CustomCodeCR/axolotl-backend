using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Utilities.Static;
using AutoMapper;
using MediatR;
using WatchDog;
using Entity = Axolotl.Domain.Entities;

namespace Axolotl.Application.UseCases.Categories.Commands.CreateCommand;

public class CreateCategoriesHandler : IRequestHandler<CreateCategoriesCommand, BaseResponse<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    //constructor
    public CreateCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<bool>> Handle(CreateCategoriesCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<bool>();

        try
        {
            var category = _mapper.Map<Entity.Categories>(request);
            await _unitOfWork.Categories.CreateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            response.IsSuccess = true;
            response.Message = ReplyMessage.MESSAGE_SAVE;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}