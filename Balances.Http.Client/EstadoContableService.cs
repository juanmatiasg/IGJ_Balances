using System;
using Balances.DTO;

namespace Balances.Http.Client
{
	public class EstadoContableService:BaseService<EstadoContableDto>
	{
		public EstadoContableService(HttpClient httpClient) : base(httpClient, "EstadoContable") { }
			
	}
}

