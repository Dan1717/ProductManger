using System.Collections.Generic;

namespace ProductsManager.Services
{
    public interface IServiceResponse
    {
        void AddError(string key, string value);
    }

    public class ServiceResponse : IServiceResponse
    {
        public ServiceResponse()
        {
            Errors = new Dictionary<string, string>();
            StatusCode = 200;
        }

        public IDictionary<string, string> Errors { get; }
        public int StatusCode { get; set; }

        public bool IsSuccess
        {
            get
            {
                return StatusCode >= 200 && StatusCode < 300;
            }
        }

        public void AddError(string key, string value)
        {
            if (IsSuccess)
            {
                StatusCode = 400;
            }
            Errors.Add(key, value);
        }
    }

	public class ServiceResponse<TResponse> : ServiceResponse
    {
		public TResponse Response { get; set; }
	}
}
