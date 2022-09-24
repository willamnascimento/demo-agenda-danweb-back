using System;

namespace Solucao.Application.Utils
{
    public class ApplicationError : Exception
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
}
