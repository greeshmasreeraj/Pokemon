using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Truelayer.Models;
using Truelayer.Services;
using Truelayer.Utils;
using static Truelayer.Utils.Generic;

namespace Truelayer.UnitTests
{
    public class Tests
    {
        private IConfiguration _config;

        public Tests()
        {
            _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        }
        PokemonServices pokemonServices = new PokemonServices();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("hello")]
        public void DoneTranslationForYoda(string desc)
        {
            
            string result = pokemonServices.DoTranslation(Consts.YODA, desc);
          
            Assert.AreNotEqual(desc, result); //translation happened 
        }

        [Test]
        [TestCase("1234")]
        [TestCase("")]
        public void NoTranslationForYoda(string desc)
        {
            
            string result = pokemonServices.DoTranslation(Consts.YODA, desc);
            Assert.AreEqual(desc, result); //translation hasnt happened 
           
        }
        [Test]
        [TestCase("hello")]
       
        public void DoneTranslationForShakeaspere(string desc)
        {
            
            string result = pokemonServices.DoTranslation(Consts.SHAKESPEARE, desc);

            Assert.AreNotEqual(desc, result); //translation happened 
        }

        [Test]
       
        [TestCase("1234")]
        
        public void NoTranslationForShakeaspere(string desc)
        {
            
            string result = pokemonServices.DoTranslation(Consts.SHAKESPEARE, desc);
            Assert.AreEqual(desc, result); //translation hasnt happened 

        }

        [Test]
        [TestCase(1)]

        public void GetOtherDetailsForPokemon(int id)
        {
            Wrapper<Pokemon> pokemon = pokemonServices.GetOtherDetails(id, new Pokemon());
            Assert.AreEqual(Consts.SUCCESS,pokemon.Status);  //we have got the other details
          
        }

        [Test]
        [TestCase("mewtwo")]
        public void GetPokemon(string name)
        {
            Wrapper<Pokemon> pokemon = pokemonServices.GetPokemon(name,false);
            Assert.AreEqual(Consts.SUCCESS, pokemon.Status);  //we have got a pokemon

        }

        [Test]
        [TestCase("random")]
        public void NoPokemon(string name)
        {
            Wrapper<Pokemon> pokemon = pokemonServices.GetPokemon(name, false);
            Assert.AreEqual(Consts.FAIL, pokemon.Status);  //we have not got a pokemon

        }

    }
}