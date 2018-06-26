using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace testajax
{
    public class FormDataInputFormatter : TextInputFormatter
    {
        private const string MultipartFormData = "multipart/form-data";
        private const string ApplicationFormUrlEncodedFormMediaType = "application/x-www-form-urlencoded";

        public FormDataInputFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(ApplicationFormUrlEncodedFormMediaType));
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(MultipartFormData));
            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
            SupportedEncodings.Add(UTF16EncodingLittleEndian);
        }

        public override Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            encoding = encoding ?? SelectCharacterEncoding(context);
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }
            var request = context.HttpContext.Request;

            try
            {
                object inputModel = null;
                var formCollection = request.Form.ToDictionary(f => f.Key, f => f.Value.ToString());
                if (formCollection.Count > 0 && FormUrlEncodedJson.TryParse(formCollection, out var jObject))
                {
                    inputModel = jObject.ToObject(context.ModelType);
                }
                return Task.FromResult(inputModel != null ? InputFormatterResult.Success(inputModel) : InputFormatterResult.Failure());
            }
            catch (Exception)
            {
                return Task.FromResult(InputFormatterResult.Failure());
            }
        }
    }
}
