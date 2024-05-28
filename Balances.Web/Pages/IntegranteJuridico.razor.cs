using Balances.DTO;
using Balances.Web.Services.FluentValidation;
using FluentValidation.Results;
using global::Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace Balances.Web.Pages
{
    public partial class IntegranteJuridico
    {
        RadzenGrid<PersonaJuridicaDto> grid;


        [Parameter]
        public string? TipoEntidad { get; set; }

        private PersonaJuridicaDto model = new PersonaJuridicaDto();
        private List<PersonaJuridicaDto> listPersonaJuridica = new List<PersonaJuridicaDto>();


        [Parameter]
        public string? balid { get; set; }

        [Parameter]
        public string sesionId { get; set; }

        public List<string> Paises { get; set; } = new List<string>();

        private List<string> Juridisccion { get; set; } = new List<string>();

        private bool isJurisdiccionDisabled = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadJsonCountry();
            await LoadJsonProvince();
            await Load();
            await base.OnInitializedAsync();
        }

        private async Task LoadJsonProvince()
        {
            try
            {
                var provincias = await socioService.GetAllProvince();


                Juridisccion.AddRange(provincias);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task LoadJsonCountry()
        {
            try
            {
                var countries = await socioService.GetAllCountries();
                List<string> countryNames = new List<string>();

                foreach (var country in countries)
                {
                    string commonName = country["name"]["common"].ToString();
                    countryNames.Add(commonName);
                }

                // Ordenar los nombres de los países en orden ascendente
                countryNames.Sort();

                foreach (var name in countryNames)
                {
                    Paises.Add(name);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        private void OnCountryChanged(object value)
        {
            string selectedCountry = value.ToString()!;
            if (selectedCountry == "Argentina")
            {
                isJurisdiccionDisabled = false;
                model.Jurisdiccion = "";
            }
            else
            {
                isJurisdiccionDisabled = true;
                model.Jurisdiccion = "Extranjero";

            }
        }
        private async Task Load()
        {
            ResponseDTO<BalanceDto> rsp = new();
            try
            {
                sesionId = await sessionStorage.GetItemAsync<string>("SessionId");

                if (sesionId == null)
                {
                    var sesionRespuesta = await sesionService.getNewSession();
                    sesionId = sesionRespuesta.Result!;
                    await sessionStorage.SetItemAsync("SessionId", sesionId);
                }
                else
                {
                    var rst = await sesionService.getBalanceId(sesionId);
                    if (rst is not null)
                    {
                        balid = rst;
                        rsp = await balanceService.getBalance(balid);
                        if (rsp.IsSuccess)
                        {
                            TipoEntidad = rsp.Result.Caratula.Entidad.TipoEntidad;
                            resultPersonaJuridica(rsp.Result.Socios.PersonasJuridicas);
                            await grid.Reload();
                            StateHasChanged();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                rsp.Message = $"SessionId: Hubo un problema con la solicitud fetch: {ex.Message}";
            }
        }


        private async Task<ResponseDTO<BalanceDto>> InsertPersonaJuridica()
        {

            ResponseDTO<BalanceDto> respuesta = new();
            try
            {

                model.SesionId = sesionId;


                PersonaJuridicaValidator personaJuridicaValidator = new();
                ValidationResult result = personaJuridicaValidator.Validate(model);

                if (result.IsValid)
                {

                    respuesta = await socioService.insertPersonaJuridica(model);

                    if (respuesta.IsSuccess)
                    {
                        resultPersonaJuridica(respuesta.Result!.Socios.PersonasJuridicas);
                        cleanInputsJuridica();
                        await grid.Reload();
                        StateHasChanged();
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;

        }

        private void cleanInputsJuridica()
        {
            // Restablecer los valores de los campos a su estado inicial o vacío
            model = new PersonaJuridicaDto();
        }

        private void resultPersonaJuridica(List<PersonaJuridicaDto> listPersonaJuridica)
        {
            this.listPersonaJuridica = listPersonaJuridica;
        }

        private async Task<ResponseDTO<BalanceDto>> deletePersonaJuridica(PersonaJuridicaDto personaJuridicaDto)
        {
            var respuesta = new ResponseDTO<BalanceDto>();
            try
            {
                personaJuridicaDto.SesionId = sesionId;
                respuesta = await socioService.deletePersonaJuridica(personaJuridicaDto);

                if (respuesta.IsSuccess)
                {

                    listPersonaJuridica.Remove(personaJuridicaDto);
                    await grid.Reload();
                    StateHasChanged();
                }
            }
            catch (Exception ex)
            {
                respuesta.Message = ex.Message;
            }

            return respuesta;
        }



    }
}