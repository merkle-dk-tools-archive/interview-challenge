# interview assignment

## intro
This interview assignment contains tasks that aim to serve as a starting point for discussion, rather than a display of skills that can either be accepted or rejected. 

It is not a trap-infested test, its a discussion-baseline. It is expected that all questions will be read and discussed, not all tasks are expected to be solved!

Please do not distribute the interview assignment or leave complete questions and solutions online in neither forums nor repos or gists, for the fairness towards other candidates and the work associated in creating this assignment.

## boot 

### boot 0x0 - clone and branch
Make sure that you clone and branch out of main. Create your own feature/<your-name> branch.

### boot 0x1 - open and run the application
Open the solution in Visual Studio 2022 (you can get free community edition here: https://visualstudio.microsoft.com/vs/community/).
This task is simply to make sure that you have the IDE up and running, that you can compile and start the application.

### boot 0x2 - run the tests
Run the unit tests associated with the solution. Note that only a single test is validating from the beginning
Make sure that tests continuously run and validate during the individual tasks.

### boot 0x3 - commits
Each task should be committed to your private repo as individual commits.
Once you complete all the tasks you are going to deliver, please create a pull-request of the branch that you created. This pull-request will form the basis of our interview.

## task #1 - topic: data modeling, refactoring, DTO's
Extend the solution by adding author and a category to an article (author object and category objects are already available in Interview.Repository.Models)
- You can enrich the articles through the seeds within in RepositoryContext.cs
- Refactor the controller to abstract the repository further

## task #2 - topics : API, response management, validation
Extend the ArticlesController by adding endpoints for getting articles
- Get articles within a time range
- Get articles by author with the latest article first
- Get articles by category names alphabetically 
- Make sure that empty results etc. have meaningful response codes
- Support implementation with unit tests

## task #3 -  topics : abstraction, modelling 
Add "like" feature related to articles (or other) similar to liking a post on social media.
- A user should be able to "like" an article, hence we need an endpoint to add a like to an article
- Extend the article response to include how many "likes" the articles has
- Add endpoints that allow the user to "like" an author or a category

## task #4 - topics: logging, dependency injection
Add logging to the articles controller (lets keep it simple, logging at the controller level, no need for application insights or similar). 
- Add info-logging on all incoming requests - a generalization would be great, if possible
- Abstract the logging during unit-testing (to not kill/bloat/overload the log with data from unit-test execution)

## task #5 - topics: async, threading, exceptions
Given the very poor service present in InterviewChallenge.Services.UnstableMediaService, the task is to utilize this service when returning articles and return any images it might return
- Extend Articles to contain images returned live from the service
- If the service does not respond within 2 seconds skip the images
- but be aware, the service might throw exceptions now and then - mitigate the problem
