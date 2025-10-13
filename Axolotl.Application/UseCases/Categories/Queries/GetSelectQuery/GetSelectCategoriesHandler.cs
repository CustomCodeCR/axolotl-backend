using AutoMapper;
using MediatR;
using Axolotl.Application.Commons.Bases;
using Axolotl.Application.Commons.Select.Response;
using Axolotl.Application.Interfaces.Services;
using Axolotl.Utilities.Static;
using WatchDog;

namespace Axolotl.Application.UseCases.Categories.Queries.GetSelectQuery;

public class GetSelectCategoriesHandler : IRequestHandler<GetSelectCategoriesQuery, BaseResponse<IEnumerable<SelectResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSelectCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BaseResponse<IEnumerable<SelectResponse>>> Handle(GetSelectCategoriesQuery request, CancellationToken cancellationToken)
    {

        var response = new BaseResponse<IEnumerable<SelectResponse>>();

        try
        {
            var categories = await _unitOfWork.Categories.GetSelectAsync();

            response.IsSuccess = true;
            response.Data = _mapper.Map<IEnumerable<SelectResponse>>(categories);
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