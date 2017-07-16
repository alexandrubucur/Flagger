# Flagger <img src="https://www.umpqua.edu/images/areas-of-study/community-workforce/Health_Safety/Flagger_1_200_Fotolia_52130866.jpg " width="70" height="70" />

Asp.net Core Wep Api for Feature Flag (aka Feature Toggle) management and configuration.

## Installation

In order to run Flagger you need:
- Visual Studio 201x
- Sql Server 20xx

## API Reference

- [`GET` /api/user](#get-apiuser) - Get user list
- [`GET` /api/user/:id](#get-apiuser-1) - Get user by id
- [`POST` /api/user](#post-add-an-user) - Add an user
- [`DELETE` /api/user/:id](#delete-delete-user) - Delete user
- [`GET` /api/featureflag](#get-apifeatureflag-2) - Get feature list
- [`GET` /api/featureflag/:id](#get-apifeatureflag-3) - Get feature by id
- [`POST` /api/featureflag](#post-add-a-feature) - Add an feature
- [`DELETE` /api/featureflag/:id](#delete-delete-feature) - Delete feature
- [`GET` /api/flagconfiguration](#get-apiflagconfiguration) - Get all configurations
- [`GET` /api/flagconfiguration/:username](#get-apiflagconfiguration-1) - Get configurations by username
- [`POST` /api/flagconfiguration](#post-setup-configuration) - Add a configuration
- [`DELETE` /api/flagconfiguration](#delete-delete-configuration) - Delete configuration

### API Documentation
#### `GET` /api/featureflag
 Get user list.
- Method: `GET`
- Endpoint: `/api/featureflag`
- Responses:
    * 200 OK
    ```json
    [
        {
            "id_User": 1,
            "userName": "Admin",
            "admin": true
        },
        {
            "id_User": 3,
            "userName": "Service1",
            "admin": false
        },
        {
            "id_User": 4,
            "userName": "Service2",
            "admin": false
        }
    ]
    ```

#### `GET` /api/featureflag
 Get user by id.
- Method: `GET`
- Endpoint: `/api/featureflag/:id`
- Responses:
    * 200 OK
    ```json
    {
        "id_User": 1,
        "userName": "Admin",
        "admin": true
    }
    ```

#### `POST` `Add an user`
Create a new feature flag.
- Method: `POST`
- Endpoint: `/api/user`
- Input:
    The `Content-Type` HTTP header should be set to `application/json`

    ```json
   "Admin"
    ```
- Responses:
    * 200 Ok

    * 400 Bad Request
    ```json
    {
      "message":":userName user already exists"
    }
    ```
    
#### `DELETE` `Delete user`
 Delete user.
 - Method: `DELETE`
 - Endpoint: `/api/user/:id` 
 - Responses:
    * 200 Ok

    * 400 Bad Request
    ```json
    {
      "message":":Cannot delete an used user."
    }
    ```


#### `GET` /api/user
 Get user list.
- Method: `GET`
- Endpoint: `/api/user`
- Responses:
    * 200 OK
    ```json
    [
        {
            "id_User": 1,
            "userName": "Admin",
            "admin": true
        },
        {
            "id_User": 3,
            "userName": "Service1",
            "admin": false
        },
        {
            "id_User": 4,
            "userName": "Service2",
            "admin": false
        }
    ]
    ```

#### `GET` /api/user
 Get user by id.
- Method: `GET`
- Endpoint: `/api/user/:id`
- Responses:
    * 200 OK
    ```json
    {
        "id_User": 1,
        "userName": "Admin",
        "admin": true
    }
    ```

#### `POST` `Add an user`
Create a new feature flag.
- Method: `POST`
- Endpoint: `/api/user`
- Input:
    The `Content-Type` HTTP header should be set to `application/json`

    ```json
   "Admin"
    ```
- Responses:
    * 200 Ok

    * 400 Bad Request
    ```json
    {
      "message":":userName user already exists"
    }
    ```
    
#### `DELETE` `Delete user`
 Delete user.
 - Method: `DELETE`
 - Endpoint: `/api/user/:id` 
 - Responses:
    * 200 Ok

    * 400 Bad Request
    ```json
    {
      "message":":Cannot delete an used user."
    }
    ```
   
#### `GET` /api/featureflag
 Get feature list.
- Method: `GET`
- Endpoint: `/api/featureflag`
- Responses:
    * 200 OK
    ```json
    [
        {
            "id_User": 1,
            "userName": "Admin",
            "admin": true
        },
        {
            "id_User": 3,
            "userName": "Service1",
            "admin": false
        },
        {
            "id_User": 4,
            "userName": "Service2",
            "admin": false
        }
    ]
    ```

#### `GET` /api/featureflag
 Get feature by id.
- Method: `GET`
- Endpoint: `/api/featureflag/:id`
- Responses:
    * 200 OK
    ```json
    {
	    "id_Flag": 12,
	    "name": "Flag1",
	    "status": false
   	}
    ```

#### `POST` `Add a feature`
Create a new feature.
- Method: `POST`
- Endpoint: `/api/featureflag`
- Input:
    The `Content-Type` HTTP header should be set to `application/json`

    ```json
   "Feature"
    ```
- Responses:
    * 200 Ok

    * 400 Bad Request
    ```json
    {
      "message":":featureName user already exists"
    }
    ```
    
#### `DELETE` `Delete feature`
 Delete feature.
 - Method: `DELETE`
 - Endpoint: `/api/featureflag/:id` 
 - Responses:
    * 200 Ok

    * 400 Bad Request
    ```json
    {
      "message":":Cannot delete an used feature flag."
    }
    ```
    
#### `GET` /api/flagconfiguration
 Get all configurations.
- Method: `GET`
- Endpoint: `/api/flagconfiguration`
- Responses:
    * 200 OK
    ```json
    [
	    {
	        "user": "Service1",
	        "features": [
	            {
	                "name": "Flag2",
	                "active": false
	            },
	            {
	                "name": "Flag3",
	                "active": true
	            },
	            {
	                "name": "Flag1",
	                "active": true
	            }
	        ]
	    },
	    {
	        "user": "Service2",
	        "features": [
	            {
	                "name": "Flag3",
	                "active": false
	            }
	        ]
	    }
	]
    ```

#### `GET` /api/flagconfiguration
 Get configurations by user.
- Method: `GET`
- Endpoint: `/api/flagconfiguration/:username`
- Responses:
    * 200 OK
    ```json
    {
    "user": "Service1",
	"features": [
	        {
	            "name": "Flag2",
	            "active": false
	        },
	        {
	            "name": "Flag3",
	            "active": true
	        },
	        {
	            "name": "Flag1",
	            "active": true
	        }
	    ]
	}
    ```

#### `POST` `Setup configuration`
Configure feature for user.
- Method: `POST`
- Endpoint: `/api/flagconfiguration`
- Input:
    The `Content-Type` HTTP header should be set to `application/json`

    ```json
   {
	"User" : "Service1",
	"Features" : 
		[
			{"Name" : "Flag1", "Active" : true},
			{"Name" : "Flag2", "Active" : false}
		]
    }
    ```
- Responses:
    * 200 Ok

#### `DELETE` `Delete configuration`
Delete user configurations.
- Method: `DELETE`
- Endpoint: `/api/flagconfiguration`
- Input:
    The `Content-Type` HTTP header should be set to `application/json`

    ```json
    {
	"User" : "Service1",
	"Features" : ["Flag1", "flag2"]
    }
    ```
- Responses:
    * 200 Ok


#TODO

- Authentication.
- Send ConfigurationChanged command to services.
- Docker scripts.
