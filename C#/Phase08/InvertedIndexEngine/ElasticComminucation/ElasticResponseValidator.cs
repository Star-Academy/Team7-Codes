using Nest;
using System;
using System.Threading.Tasks;
using InvertedIndexEngine.CustomException;

namespace InvertedIndexEngine.ElasticCumminucation
{
    public class ElasticResponseValidator
    {
        public static async Task ValidateResponseAndLogConsole<T>(Task<T> responseTask) where T : IResponse
        {
            try
            {
                await ValidateResponse(responseTask);
            }
            catch (Exception e)
            {
                if (e is RequestNotSentException
                        || e is ElasticClientErrorException
                        || e is ElasticServerErrorException
                        || e is ServerErrorException)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
                throw;
            }
        }
        public static async Task ValidateResponse<T>(Task<T> responseTask) where T : IResponse
        {
            IResponse response;
            try
            {
                response = await responseTask;
            }
            catch
            {
                throw new RequestNotSentException();
            }

            CheckServerException(response);
            CheckClientException(response);
            CheckServerError(response);
        }

        private static void CheckServerError(IResponse response)
        {
            var error = response.ServerError;
            if (error != null)
            {
                throw new ServerErrorException(error.ToString());
            }
        }

        private static void CheckClientException(IResponse response)
        {
            Exception e = response.OriginalException;
            if (e != null)
            {
                throw new ElasticClientErrorException(e.Message);
            }
        }

        private static void CheckServerException(IResponse response)
        {
            Exception e = response.ApiCall.OriginalException;
            if (e != null)
            {
                throw new ElasticServerErrorException(e.Message);
            }
        }
    }
}