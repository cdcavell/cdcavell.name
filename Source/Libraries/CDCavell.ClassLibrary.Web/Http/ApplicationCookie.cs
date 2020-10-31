using CDCavell.ClassLibrary.Web.Security;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CDCavell.ClassLibrary.Web.Http
{
    /// <summary>
    /// Cookie class
    /// </summary>
    /// <revision>
    /// __Revisions:__~~
    /// | Contributor | Build | Revison Date | Description |~
    /// |-------------|-------|--------------|-------------|~
    /// | Christopher D. Cavell | 1.0.0.0 | 09/29/2020 | Initial build |~ 
    /// </revision>
    public class ApplicationCookie
    {
        private HttpRequest _request;
        private HttpResponse _response;
        private CookieOptions _cookieOptions;

        /// <summary>
        /// Initializes a new instance reading, writing or removing cookie
        /// </summary>
        /// <method>ApplicationCookie(IHttpContextAccessor httpContextAccessor)</method>
        public ApplicationCookie(IHttpContextAccessor httpContextAccessor)
        {
            _request = httpContextAccessor.HttpContext.Request;
            _response = httpContextAccessor.HttpContext.Response;
            _cookieOptions = GetDefaultCookieOptions();
        }

        /// <summary>
        /// Initializes a new instance reading, writing or removing cookie
        /// </summary>
        /// <method>ApplicationCookie(IHttpContextAccessor httpContextAccessor, CookieOptions cookieOptions)</method>
        public ApplicationCookie(IHttpContextAccessor httpContextAccessor, CookieOptions cookieOptions)
        {
            _request = httpContextAccessor.HttpContext.Request;
            _response = httpContextAccessor.HttpContext.Response;
            _cookieOptions = cookieOptions;
        }

        /// <summary>
        /// Method to return value for given key in dictonary stored in cookie
        /// </summary>
        /// <param name="cookieKey">string</param>
        /// <param name="key">string</param>
        /// <returns>string</returns>
        /// <exception>Requires HttpRequest</exception>
        /// <method>GetValue(string cookieKey, string key)</method>
        public string GetValue(string cookieKey, string key)
        {
            if (_request == null)
                throw new Exception("Invalid operation, request cannot be null");

            string value = string.Empty;

            if (_request.Cookies[cookieKey] != null)
            {
                var cookieValue = _request.Cookies[cookieKey];
                Dictionary<string, string> form = JsonConvert.DeserializeObject<Dictionary<string, string>>(cookieValue);
                if (form.ContainsKey(key))
                    value = AESGCM.Decrypt(form[key]);
            }

            return value;
        }

        /// <summary>
        /// Method to return dictionary of values for given key stored in cookie
        /// </summary>
        /// <param name="cookieKey">string</param>
        /// <returns>Dictionary&lt;string, string%gt;</returns>
        /// <exception>Requires HttpRequest</exception>
        /// <method>GetAllValues(string sessionkey)</method>
        public Dictionary<string, string> GetAllValues(string cookieKey)
        {
            if (_request == null)
                throw new Exception("Invalid operation, request cannot be null");

            Dictionary<string, string> form = new Dictionary<string, string>();

            if (_request.Cookies[cookieKey] != null)
            {
                var cookieValue = _request.Cookies[cookieKey];
                Dictionary<string, string> encryptedForm = JsonConvert.DeserializeObject<Dictionary<string, string>>(cookieValue);
                foreach (var item in encryptedForm)
                {
                    form.Add(item.Key, AESGCM.Decrypt(item.Value));
                }
            }

            return form;
        }

        /// <summary>
        /// Method to save a record in a dictonary of key value records in cookie
        /// </summary>
        /// <param name="cookieKey">string</param>
        /// <param name="key">string</param>
        /// <param name="value">string</param>
        /// <exception>Requires HttpRequest and HttpResponse</exception>
        /// <method>SetValue(string cookiekey, string key, string value)</method>
        public void SetValue(string cookieKey, string key, string value)
        {
            if (_request == null)
                throw new Exception("Invalid operation, request cannot be null");

            if (_response == null)
                throw new Exception("Invalid operation, response cannot be null");

            Dictionary<string, string> form = new Dictionary<string, string>();

            if (_request.Cookies[cookieKey] != null)
            {
                var cookieValue = _request.Cookies[cookieKey];
                form = JsonConvert.DeserializeObject<Dictionary<string, string>>(cookieValue);
            }

            if (form.ContainsKey(key))
                form.Remove(key);

            form.Add(key, AESGCM.Encrypt(value));

            _response.Cookies.Append(cookieKey, JsonConvert.SerializeObject(form), _cookieOptions);
        }

        /// <summary>
        /// Method to save a dictonary of key value records in cookie
        /// </summary>
        /// <param name="cookieKey">string</param>
        /// <param name="values">Dictionary&lt;string, string&gt;</param>
        /// <exception>Requires HttpRequest and HttpResponse</exception>
        /// <method>SetAllValues(string cookiekey, Dictionary&lt;string, string&gt;)</method>
        public void SetAllValues(string cookieKey, Dictionary<string, string> values)
        {
            if (_request == null)
                throw new Exception("Invalid operation, request cannot be null");

            if (_response == null)
                throw new Exception("Invalid operation, response cannot be null");

            Dictionary<string, string> form = new Dictionary<string, string>();
            foreach (var item in values)
            {
                form.Add(item.Key, AESGCM.Encrypt(item.Value));
            }

            _response.Cookies.Append(cookieKey, JsonConvert.SerializeObject(form), _cookieOptions);
        }

        /// <summary>  
        /// Delete the cookie  
        /// </summary>  
        /// <param name="cookieKey">string</param>  
        /// <exception>Requires HttpResponse</exception>
        /// <method>Remove(string cookiekey)</method>
        public void Remove(string cookieKey)
        {
            if (_response == null)
                throw new Exception("Invalid operation, response cannot be null");

            _response.Cookies.Delete(cookieKey);
        }

        private CookieOptions GetDefaultCookieOptions()
        {
            CookieOptions option = new CookieOptions();
            option.HttpOnly = true;
            option.Secure = true;
            option.SameSite = SameSiteMode.Strict;
            option.Expires = DateTime.MinValue;
            option.Path = "/";

            return option;
        }
    }
}
