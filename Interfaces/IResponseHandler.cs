using IBKR_Service.Handlers;

namespace IBKR_Service.Interfaces
{
    public interface IResponseHandler
    {
        Task Handle(string jsonResponse, ResponseHandler? middleWorkHandler = null);
        void SetNext(IResponseHandler handler);
    }
}