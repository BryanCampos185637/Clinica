using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace UserInterface.Helpers
{
    public static class SessionHelper
    {
        public static void crearCookieSession(this ISession session, string key, object value)
        {
            session.SetString(key, CifrarCadenas.cifrar(JsonConvert.SerializeObject(value)));
        }
        public static T obtenerObjetoSesion<T>(this ISession session, string key) 
        {
            var value = session.GetString(key);
            if (value != null && value != "") { value = CifrarCadenas.descifrar(value); }
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
