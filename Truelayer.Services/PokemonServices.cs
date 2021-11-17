using System;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Truelayer.Models;
using Truelayer.Utils;

namespace Truelayer.Services
{
    public class PokemonServices
    {
        private IConfiguration _config;

        public PokemonServices()
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }

        public Generic.Wrapper<Pokemon> GetPokemon(string name,bool doTranslation)
        {
            Generic.Wrapper<Pokemon> wrapper = new Generic.Wrapper<Pokemon>();
            try
            {
                Pokemon pokemon = new Pokemon();
                dynamic data = DoAPICall(_config["PokeAPI_Root"], "pokemon/" + name);
               
                if (data!=null)
                {
                    if (data.species.name != null)
                        pokemon.name = data.species.name;
                    if (data.species.url != null)
                    //get the description
                    {
                        int id = data.id;
                        wrapper = GetOtherDetails(id, pokemon);
                        if (wrapper.Status == Consts.SUCCESS)
                        {
                            pokemon = wrapper.SingleObject;
                            if (doTranslation)
                            {
                                string translation = string.Empty;
                                if (pokemon.habitat.ToLower().Trim().Equals(Consts.CAVE) || pokemon.isLegendary)
                                {
                                    //do yoda transalation
                                    translation = DoTranslation(Consts.YODA, pokemon.description);
                                }
                                else
                                {
                                    //do shakespeare translation
                                    translation = DoTranslation(Consts.SHAKESPEARE, pokemon.description); 
                                }
                                pokemon.description = translation;
                            }

                            wrapper.Status = Consts.SUCCESS;
                            wrapper.Message = Consts.SUCCESS;
                            wrapper.SingleObject = pokemon;
                            return wrapper;
                        }
                        else
                        {
                            wrapper.Status = Consts.FAIL;
                            wrapper.Message = Consts.GENERIC_ERROR;
                            return wrapper;
                        }
                    }
                    else
                    {
                        wrapper.Status = Consts.FAIL;
                        wrapper.Message = Consts.GENERIC_ERROR;
                        return wrapper;
                    }

                   
                }
                else
                {
                    //some error occured. so return the error in the wrapper
                    wrapper.Status = Consts.FAIL;
                    wrapper.Message = "No Pokeman can be found with the specified name";
                    return wrapper;
                }

            }

            catch (Exception ex)
            {
                wrapper.Status = Consts.FAIL;
                wrapper.Message = Consts.GENERIC_ERROR;
                return wrapper;
            }
        }

        public Generic.Wrapper<Pokemon> GetOtherDetails(int id, Pokemon pokemon)
        {
            Generic.Wrapper<Pokemon> wrapper = new Generic.Wrapper<Pokemon>();

            try
            {
               // assign default values 
                pokemon.description = "";
                pokemon.isLegendary = false;
                pokemon.habitat = "";

                dynamic data = DoAPICall(_config["PokeAPI_Root"], "pokemon-species/" + id);

                if (data != null)
                {
                    if (data.flavor_text_entries != null)
                    {
                        foreach (dynamic fText in data.flavor_text_entries)
                        {
                            //get the english description 
                            if (fText.language.name == "en")
                                pokemon.description = fText.flavor_text;
                        }
                    }

                    if (data.is_legendary != null)
                        pokemon.isLegendary = data.is_legendary;

                    if (data.habitat != null)
                        pokemon.habitat = data.habitat.name;
                }
                
                wrapper.Status = Consts.SUCCESS;
                wrapper.SingleObject = pokemon;
                return wrapper;

            }
            catch(Exception ex)
            {
                wrapper.Status = Consts.FAIL;
                wrapper.Message = Consts.GENERIC_ERROR;
                return wrapper;
            }
        }

        public string DoTranslation(string type, string desc)
        {
            try
            {
                string translated = desc; //sometimes the API call may have exceeded the per hour request. then use the english desc as the translated one. also for any reason the transaltion hasn't happened, then also use the english desc 

                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(_config["Translation_API_Root"])
                };
                string url = httpClient.BaseAddress + type;
                Translation translation = new Translation
                { text = desc };

                string contentJson = System.Text.Json.JsonSerializer.Serialize(translation);

                var result = httpClient.PostAsync(url, new StringContent(contentJson, Encoding.UTF8, "application/json")).Result;
                if(result.IsSuccessStatusCode)
                {
                    var responseContent = result.Content.ReadAsStringAsync().Result;
                    dynamic data = JObject.Parse(responseContent);
                    if(data!=null)
                        translated = data.translated;
                }

                return translated;
            }
            catch(Exception ex)
            {
                return desc;
            }
        }

        internal dynamic  DoAPICall(string rootUri, string urlPath)
        {
            try
            {
                dynamic data = null;
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(rootUri)
                };
                string url = httpClient.BaseAddress + urlPath;
                var result = httpClient.GetAsync(url).Result;
                if (result.IsSuccessStatusCode)   
                {
                    var responseContent = result.Content.ReadAsStringAsync().Result;

                    data = JObject.Parse(responseContent);
                }
                return data;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
