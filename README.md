
<h1> Pokemon API </h1>

<b>There are 2 end points in this API </b>

<h4>First API</h4>   To See a pokeman's details. using the pokeapi to get the pokeman details and parsing the result 
<hr>Sample Request: </hr>
HTTPGET  https://localhost:44380/pokemon/mewtwo   

Sample Response:
{
  "name": "mewtwo",
  "description": "Psychic power has augmented its muscles.\nIt has a grip strength of one ton and can sprint\na hundred meters in two seconds flat!",
  "habitat": "rare",
  "isLegendary": true
}

  <h4>Second API</h4>  Translated description of a pokeman (using shakespeare or yoda translaton based on the requirement)
 <hr> Sample Request:</hr>
  HTTPGET  https://localhost:44380/pokemon/translated/ditto
 
 Sample Response:
  {
  "name": "ditto",
  "description": "When it encounters another Ditto, it will move\nfaster than normal to duplicate that opponent exactly.",
  "habitat": "urban",
  "isLegendary": false
}

<h4>API Swagger </h4>can be found on https://localhost:44380/swagger/index.html  

<h4>To Run the solution </h4>
Open the solution file Truelayer.sln in visual studio 2019
This has been done in .net core 3.1
Run the solution
Go to your browser and can load the above mentioned 2 end points. Or the API end point details can be seen from the API swagger doc https://localhost:44380/swagger/index.html 

<h4>To Test </h4>
Run the test projects in visual studio 2019

<h4>For Production - What else need to do </h4>
I would add the following featues as well if this API is going to be used in a production environment <hr>
<ul>
 <li>Implement a in memory cache (may be Redis) to avoid multiple calls to the third party API, and that will definitely improve performance. And also the translation API has a per hour and per day AI call limit and we can get the required data from cache (if it's there) when the funtranslation API thrown 429 because of too many requests </li>
<li> Add API security - I would implement some security/tokenisation mechanism to make sure that the API will be utilised by only the required parties.Will implement the environment restriction unless the API need to be accessed publicly. </li>
<li> Implement loggers - Logs are key part to monitor the customer journey as well as that helps in improving the product in the next phases. I would implement a logging mechanisms by using a logger class from which all the activities and errors will be saved into a mongo database collection/s. Since it's mongo, the logs doesnt need to be in certain format and can be json format and for APIs, that's very beneficial.</li>
<li>Implement some error monitoring mechanism to get notifications to the dev team if anything unexpected or error happened on any part of the product. It helps to fix the issue at the earliest</li>
  <li>Can add integration tests as well </li>
  <li>The repo will be private  :) </li>
</ul>

  
  








