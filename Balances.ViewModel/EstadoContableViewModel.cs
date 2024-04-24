using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Balances.DTO;
using Balances.Http.Client;

namespace Balances.ViewModel;

public class EstadoContableViewModel:BaseViewModel<EstadoContableDto,RubroPatrimonioNetoDto>
{
    private readonly EstadoContableService _estadoContableService;

    public EstadoContableViewModel(EstadoContableService estadoContableService)
    {
        ToRegistry = new EstadoContableDto();
        OtherRegistry= new RubroPatrimonioNetoDto();
        _estadoContableService = estadoContableService;
    }

    public async Task<ResponseDTO<BalanceDto>> InsertEECC()
    {

        ResponseDTO<BalanceDto> rsp = new();
        rsp.IsSuccess = false;

        try
        {
            var result = await _estadoContableService.PostAsJsonAsync<EstadoContableDto, ResponseDTO<BalanceDto>>("InsertEECC", ToRegistry);

            rsp = result!;
            rsp.IsSuccess = true;
        }
        catch (Exception ex)
        {
            rsp.Message = ex.Message;
        }

        return rsp;
    }


    public async Task<ResponseDTO<BalanceDto>> InsertRubro()
    {
        ResponseDTO<BalanceDto> rsp = new();
        rsp.IsSuccess = false;

        try
        {
            var result = await _estadoContableService.PostAsJsonAsync<RubroPatrimonioNetoDto, ResponseDTO<BalanceDto>>("InsertRubro", OtherRegistry);

            rsp = result!;
            rsp.IsSuccess = true;
        }
        catch (Exception ex)
        {
            rsp.Message = ex.Message;
        }

        return rsp;
    }

    public async Task<ResponseDTO<BalanceDto>> DeleteRubro()
    {
        ResponseDTO<BalanceDto> rsp = new ResponseDTO<BalanceDto>();
        rsp.IsSuccess = false;

        try
        {         
            var respuesta = await _estadoContableService.DeleteJsonAsync("DeleteRubro",OtherRegistry);

            if (respuesta.IsSuccessStatusCode)
            {
                var result = await respuesta.Content.ReadFromJsonAsync<ResponseDTO<BalanceDto>>();
                rsp = result!;
                rsp.IsSuccess = true;
            }
            else
            {
                rsp.Message = $"Error en la solicitud DELETE. Código de estado: {respuesta.StatusCode}";
            }
        }
        catch (Exception ex)
        {
            rsp.Message = ex.Message;
        }

        return rsp;
    }



}

