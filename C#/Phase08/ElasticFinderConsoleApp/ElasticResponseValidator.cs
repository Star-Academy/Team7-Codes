using Nest;
using System;
using System.Threading.Tasks;

namespace ElasticFinderConsoleApp
{
    public class ElasticResponseValidator<T> where T : IResponse
    {
        public static async Task ValidateResponseAndLogConsole(Task<T> responseTask)
        {
            try
            {
                await ValidateResponse(responseTask);
            }   
            catch (Exception e)
            {
                if (e is CustomException.RequestNotSentException 
                        || e is CustomException.ElasticClientErrorException
                        || e is CustomException.ElasticServerErrorException
                        || e is CustomException.ServerErrorException)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
                throw;
            }
        }
        public static async Task ValidateResponse(Task<T> responseTask)
        {
            IResponse response;
            try
            {
                response = await responseTask;
            }
            catch
            {
                throw new CustomException.RequestNotSentException();
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
                throw new CustomException.ServerErrorException(error.ToString());
            }
        }

        private static void CheckClientException(IResponse response)
        {
            Exception e = response.OriginalException;
            if (e != null)
            {
                throw new CustomException.ElasticClientErrorException(e.Message);
            }
        }

        private static void CheckServerException(IResponse response)
        {
            Exception e = response.ApiCall.OriginalException;
            if (e != null)
            {
                throw new CustomException.ElasticServerErrorException(e.Message);
            }
        }
    }
}