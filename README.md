# Hi

## How does this works
All calls to the API are done with HTTP basic authentication, with user:password used in Login Page. These are managed in UserAuthorization class.

Users and Roles are stored in Disk using CacheService class, in folder %TEMP_FOLDER%\SchibstedLover\XXX.json

You can create all users you want, and every user can have one or multiple Roles. Your created Users won't be lost in new startups.

When you loggin with one user, you will see all the pages that you can access. If you go to a page that requires a Role that you don't have, a fantastic "HTTP Error 403.0 - Forbidden" will appear :)


###### Default Users
There are 5 defined Users. UserName / Password / ROLE are:
	ADMIN / ADMIN / ADMIN
	USER1 / USER1 / PAGE_1
	USER2 / USER2 / PAGE_2
	USER3 / USER3 / PAGE_3
	USER4 / USER4 / PAGE_1, PAGE_2, PAGE_3

These users will be created (if are deleted) everytime the applications starts.


###### API Calls
There are X API Calls that you can run over Web or using Postman / Fiddler. All of them uses basic auth, so, users MUST exists.

GET: 	http://%HOST%/api/user (no parameters)
	This will get the User (if exists) that matches UserName / Password used in Basic Auth.
	If User does not exists, will return a 401 status
POST:	http://%HOST%/api/user (using a User as body parameter)
	This will create the User (if it doesn't exists). In that case, it will return an HTTP.Conflict (409) error.
GET:	http://%HOST%/api/user/list (no parameters)
	ADMIN user will not be returned.
	This will list all UserNames available in "DB".
DELETE:	http://%HOST%/api/user/{UserName}
	This will delete the given UserName from "DB"
	ADMIN user is not available to be deleted.
	This call always will return true, in order to not show if an user is created or not.
	
	
## How To Run
You can run it:
1. If you know about Visual Studio, you can open .sln file and debug the project. It will open a Web with the Login Page.
2. You can execute "IIS Installation\installation.bat". This batch will:
   - Ask for Admin permission
   - ~~Install IIS (if you don't have it already)~~ --> Removed, your IIS, you should install it (I will configure)
   - The name for the Virtual Directory will be **"GuillemSchibstedLover"**
   - Create the AppPool with name **"GuillemSchibstedLoverPool"** (if you don't have it already)
   - Create the Site **"Default Web Site"** (if you don't have it already) pointing to **"C:\inetpub\wwwroot"**. If you have it already, it will stop it. 
   - Create the Virtual Directory **GuillemSchibstedLover** in "Default Web Site" (if you don't have it already) 
   - Copy all binaries needed in **"C:\inetpub\wwwroot\GuillemSchibstedLover"**
   - Start site "Default Web Site"
3. You can install it manually if you do not trust the batch (I will understand). In **"IIS Installation\GuillemSchibstedLover"** are the binaries.


## About tests
I know that these needs A LOT more tests... My fault :(

Some of them, are done using Moq library.


## Consider this
Session time is up to 5 minutes. You can change this modifying 'sessionState' value on Web.Config.

No IoC is used. Code would be better using it.

I was having problems using Runtime Cache with Roles. Because of this, That's why I decided to deactivate it and only use Disk Cache (worst performance).

One library is added intentionally... Moq (in tests projects)

I didn't use C# Roles... to use them I had to add a library "Microsoft.AspNet.Identity.EntityFramework", with dependencies with EntityFramework and Microsoft.Asp.Net.Identity.Core... I decided not to use it, because of the requirements.

**User Edition are not finished...**
