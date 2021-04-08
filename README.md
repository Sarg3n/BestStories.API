# BestStories.API

How to run the application
 -> Just download and open. Run it and call the created controller localhost route.

Any assumptions you have made, and any enhancements or changes you would make, given the time.
 -> Took some hours to do it cause i really didn't get why "time" has that format value and how to convert it... 
 
Enhancements or changes you would make, given the time
 -> In a real project of course I would use model object classes, where each call would have a RequestDataModel and a ResponseDataModel linking object class properties and json with help of JsonProperty, however here for a small example I didn't went for that approach and tryed to use Jtoken to work the json data as requested in interview. 
 -> Also thought using linq queries. Maybe put all the uri strings to call in one place (appsettings.json), all depends of each programmer imagination.
 -> Also thought that using that "for" approach is consuming much time in order to make the necessary changes to the output array, however... it was what I initially did and 
 I got stuck with it till the end. Also at Bold interview it was told that was needed some knowledge working with JToken so ... that's what I used.
 -> Also instead of using WebRequest I could use a better service with newer HttpClient.
 -> The HttpGet also could be made with a received RequestDataModel Object like public async Task<string> GetTopStoriesIds([FromBody] Models.GetTopStories.ResquestDataModel.Root requestData){...}
 
 
 
With no further issues, with best regards, Nuno.
