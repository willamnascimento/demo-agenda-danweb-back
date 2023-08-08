using Dynamitey;
using Newtonsoft.Json;
using Solucao.Application.Data.Entities;
using Solucao.Application.Data.Repositories;
using Solucao.Application.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Solucao.Application.Service.Implementations
{
    public class StateService : IStateService
    {
        private StateRepository stateRepository;
        private readonly IHttpClientFactory clientFactory;

        public StateService(StateRepository _stateRepository, IHttpClientFactory _clientFactory)
        {
            stateRepository = _stateRepository;
            clientFactory = _clientFactory;
        }
        public async Task<ValidationResult> AddIBGEStatesList()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://servicodados.ibge.gov.br/api/v1/localidades/estados");

            var client = clientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                dynamic json = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

                foreach (var item in json)
                {
                    var id = Dynamic.InvokeGet(item, "id");
                    var nome = Dynamic.InvokeGet(item, "nome");
                    var sigla = Dynamic.InvokeGet(item, "sigla");

                    var state = new State { Id = id, Nome = nome, Sigla = sigla };
                    await stateRepository.Add(state);
                }

                return ValidationResult.Success;
            }
            return new ValidationResult(response.StatusCode.ToString());
        }
    }
}
