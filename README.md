# Explanation of requests

Two types of relevant requests in this application. **GET** requests generally retrieve information, while **POST** requests 
push information to the server.. If you're getting a weird error, it's usually worth checking to see if it was caused 
by the wrong request type.

Postman is the recommended tool for sending these requests. It allows quick creation and organization, and can save and run your requests in batches.

All requests should be formatted as:
   `[url of the server]/api[endpoint]`
where `[endpoint]` matches one of the patterns listed in this README
   
The server will expect certain requests to include *params* or *bodies*, sometimes both.

*params* are information about the request itself. In Postman, they can set under the *Params* tab, or specified 
directly in the url by appending `?paramName=paramValue` at the end, separating parameters with `&`.
 
A *body* usually contains data that will be used to update information on the server. They cannot be provided in the
url text, but Postman allows you to set it in the *body* tab, and all requests currently are of type *raw*. The blue
dropdown will allow you to choose either JSON (for sending objects) or Text (for sending single values). Objects are 
described in more detail at the end of the document.

A URL endpoint usually begins with a specifier for the type of object that the query involves. For example, the path to
adding an actor is `[url of the server]/api/actors/add`. Each of the following headers specifies a category and any
functionality it has on its own (such as `/actors`), followed by subheaders that provide additional functionality 
(such as `/add` for `/actors/add`). **All category endpoints (like `/actors`) take GET requests, while all endpoints
in their subheaders (like `actors/add`) take POST requests.**

## /actors 

Returns all actor objects

*params*
* id (optional): returns only the actor with the specified id

#### /add 

Adds a provided actor object to the database

*body:*

* A JSON representation of the actor to be added

#### /remove 

Removes all actors from the database

*params*
* id (optional): deletes only the actor with the specified id

## /outcomes 

Returns all outcome objects

*params*
* id (optional): returns only the outcome with the specified id
* sceneId (optional): returns only the outcomes associated with the scene represented by sceneId

#### /add 

Adds a provided outcome object to the database

*body:*

* A JSON representation of the outcome to be added

#### /remove 

Removes all outcomes from the database

*params*
* id (optional): deletes only the outcome with the specified id
* sceneId (optional): deletes only the outcomes associated with the scene represented by sceneId

## /performances 

Returns all performance objects

*params*
* id (optional): returns only the performance with the specified id

#### /add 

Adds a provided performance object to the database

*body:*

* A JSON representation of the performance to be added

#### /remove 

Removes all performances from the database

*params*
* id (optional): deletes only the performance with the specified id

#### /set-winning-vote 

Assigns a value to the winningVote field of the specified performance

*params*
* id: the id of the vote to modify

*body:*
* A text value of the winning vote's id

## /requirements 

Returns all requirement objects

*params*
* id (optional): returns only the requirement with the specified id
* sceneId (optional): returns only the requirements associated with the scene represented by sceneId

#### /add 

Adds a provided requirement object to the database

*body:*

* A JSON representation of the performance to be added

#### /remove 

Removes all requirements from the database

*params*
* id (optional): deletes only the requirement with the specified id
* sceneId (optional): deletes only the requirements associated with the scene represented by sceneId

## /scenes 

Returns all scene objects

*params*
* id (optional): returns only the scene with the specified id

#### /add 

Adds a provided requirement object to the database, or replaces one if id is specified

*body:*

* A JSON representation of the scene to be added

#### /remove 

Removes all scenes from the database

*params*
* id (optional): deletes only the scene with the specified id

#### /reset 

Sets the 'occurrence' field of every scene object to 0. This resets their relative frequencies, essentially 
'forgetting' which scenes have occurred how often, which is usually used to balance things out.

#### /set-occurrences 

Sets the 'occurrence' field of a given scene to the provided value. This marks how many times the scene was used this
session, and that information is used to ensure scenes make roughly equal appearances.

*params*
* id: the id of the scene to set

*body:*

A text field containing the number of occurrences to set

## /statuses 

Returns all status objects

*params*
* id (optional): returns only the status with the specified id
* actorId (optional): returns only the statuses associated with the actor represented by actorId

#### /add 

Adds a provided status object to the database

*body:*

* A JSON representation of the status to be added

#### /remove 

Removes all statuses from the database

*params*
* id (optional): removes only the status with the specified id

## /time 

Returns the simulation's time value, which begins at 1 and increments every time a new batch of performances are resolved

#### /advance

Increments the value of the simulation's time by one unit

#### /reset

Reverts the simulation's time value back to 1

## /users

Returns all user objects

*params*
* id (optional): returns only the user with the specified id
* username (optional): returns only the user with the specified username

#### /add

Adds a provided user to the database

*body:*

A JSON representation of the user to be added

#### /remove

Removes all user objects from the database

*params*
* id (optional): removes only the user with the specified id

#### /reset

Resets the voting history of every user in the database

#### /set-voting-chances
Updates the number of times that a specified user has had the chance to vote

*params*
* id: the id of the user to update

*body:*
A text representation of the number of times the user has had the chance to vote

#### /set-[positive OR negative OR neutral]-votes

Sets the number of positive or negative or neutral votes that this user has made

*params*
* id: the id of the user to update

*body:*
A text representation of the number of positive/negative/neutral votes the user has made

#### /modify-[positive OR negative OR neutral]-votes

Modifies the number of positive or negative or neutral votes that this user has made

*params*
* id: the id of the user to update

*body:*
A text representation of the number of positive/negative/neutral to add to the user's count. Use a negative value to subtract.

## /votes

Returns all vote objects

*params*
* id (optional): returns only the vote with the specified id

#### /add

Adds a provided vote to the database

*body:*

A JSON representation of the vote to be added

#### /remove

Removes all vote objects from the database

*params*
* id (optional): removes only the vote with the specified id

#### /set-in-progress

Update the 'inProgress' field of all votes, which determines if they can be edited

*params*
* id (optional): updates only the vote with the specified id

*body:*
A text representation of the value to assign. Can be either `true` or `false`.

#### /set-chosen-outcome
Update the 'chosenOutcome' field of a specified vote, which determines what outcome the vote is in favor for

*params*
* id: the id of the vote to update

*body:*
A text representation of the desired outcome's id


## Simulation Endpoints
The following endpoints control the simulation itself, instead of being associated with specific objects:

