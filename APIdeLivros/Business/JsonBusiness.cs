using Newtonsoft.Json;
using System;

namespace APIdeLivros.Business
{
    public class JsonBusiness : IJsonBusiness
    {
        public string ConverterModelParaJson(object model)
        {
            try
            {
                var json = JsonConvert.SerializeObject(model);

                return json;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
