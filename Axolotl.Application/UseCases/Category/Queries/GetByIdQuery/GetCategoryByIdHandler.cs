using AutoMapper;
using MediatR;
using WatchDog;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Dtos.Category.Response;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Utilities.Static;

namespace Axolotl.Application.UseCases.Category.Queries.GetByIdQuery;

public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, BaseResponse<CategoryByIdResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<CategoryByIdResponseDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse<CategoryByIdResponseDto>();

        try
        {
            var category = await _unitOfWork.Category.GetByUUIDAsync(request.CategoryId);

            if (category is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                return response;
            }

            response.IsSuccess = true;
            response.Data = _mapper.Map<CategoryByIdResponseDto>(category);
            response.Message = ReplyMessage.MESSAGE_QUERY;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            WatchLogger.LogError(ex.Message);
        }

        return response;
    }
}