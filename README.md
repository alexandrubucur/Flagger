# Flagger

Asp.net Core Feature flag api.

## API Reference

- [`GET` /api/user](#get-apiuser) - Get user list
- [`GET` /api/user/:id](#get-apiuser-1) - Get user by id
- [`POST` /api/user](#post-add-an-user) - Add an user
- [`DELETE` /api/user/:id](#delete-delete-user) - Delete user

### API Documentation
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
- Endpoint: `/api/feare`
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
 - Endpoint: `/api/feature/:id` 
 - Responses:
    * 200 Ok

    * 400 Bad Request
    ```json
    {
      "message":":Cannot delete an used feature flag."
    }
    ```

