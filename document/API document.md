# TicketSystme API document

## Common
1. use jwt token for authorization
2. response common format
```
{
  "status_code": 200,
  "message": "string",
  "data": {
      ...
  }
}
```
Parameters|   Require|  description
--        |   :--:   |  -- 
status_code  |    y     |  follow http status code, 200 means 'OK'
message  |    Y     | response message
data  |    N     |  response data

## API Interface

#### `Post` **/api/account/login/**

get jwt by user login

> Request body:
```
{
  "username": "ziyang",
  "password": "password"
}
```
Parameters|   Require|  description
--        |   :--:   |  -- 
username  |    y     |  user name
password  |    y     |  user password

> Response:

* 200:Ok
```
{
  "status_code": 200,
  "message": "",
  "data": {
    "accessToken": "this_is_jwt",
    "tokenType": "Bearer",
    "user": {
      "userID": 0,
      "name": "ziyang"
    }
  }
}
```
Parameters|   Require|  description
--        |   :--:   |  -- 
accessToken  |    y     |  jwt
tokenType  |    y     |  always 'Bearer'
user | y | user model

##### user
Parameters|   Require|  description
--        |   :--:   |  -- 
userID  |    y     |  user ID
name  |    y     | user Name

---
#### `Get` **/api/issue/{issueID}**

get issue by issueid

> Path parameters:

Parameters|   Require|  description
--        |   :--:   |  -- 
issueID        |    y     |  issue ID

> Response:

* 200:Ok
```
{
  "status_code": 200,
  "message": "",
  "data": {
    "issueID": 0,
    "issueType": 0,
    "issueStatus": 1,
    "summary": "summary",
    "description": "description",
    "isDeleted": true,
    "createdUser": "",
    "createdTime": "2022-03-13T13:01:02.037Z",
    "updatedUser": "",
    "updatedTime": "2022-03-13T13:01:02.037Z"
  }
}
```
Parameters|   Require|  description
--        |   :--:   |  -- 
issueID  | y  |  issue id
issueType  | y |  always 'Bearer'
issueStatus | y | see Attachment
summary | y | summary
description | y | description
isDeleted | y | isDeleted
createdUser | y | created User
createdTime | y | created Time
updatedUser | y | updated User
updatedTime | y | updated Time

---
#### `Delete` **/api/issue/{issueID}**

delete issue by issueid

> Path parameters:

Parameters|   Require|  description
--        |   :--:   |  -- 
issueID   |    y     |  issue ID

> Response:

* 200:Ok
```
{
  "status_code": 200,
  "message": "",
}
```

---
#### `Post` **/api/issue**

create a new issue

> Request body:
```
{
  "ticketType": 0,
  "issueStatus": 1,
  "summary": "",
  "description": ""
}
```
Parameters|   Require|  description
--        |   :--:   |  -- 
issueType |    y     |  issue type ID
issueStatus|    y     |  see Attachment
summary    |    y     |  summary
description|    y     |  description

> Response:

* 200:Ok
```
{
  "status_code": 200,
  "message": "",
}
```

---
#### `Put` **/api/issue**

modify issue summary or description

> Request body:
```
{
  "issueID": 0,
  "summary": "string",
  "description": "string"
}
```
Parameters|   Require|  description
--        |   :--:   |  -- 
issueID |    y     |  issue ID
summary    |    y     |  summary
description|    y     |  description

> Response:

* 200:Ok
```
{
  "status_code": 200,
  "message": "",
}
```

---
#### `Put` **/api/issue/status**

modify issue status

> Request body:
```
{
  "issueID": 0,
  "issueStatus": 1
}
```
Parameters|   Require|  description
--        |   :--:   |  -- 
issueID |   y   |  issue ID
issueStatus  |  y    |  see Attachment

> Response:

* 200:Ok
```
{
  "status_code": 200,
  "message": "",
}
```

## Attachment

### IssueStatus

value |  description
--        |  -- 
1 |  New Issue
2 | Issue Resolved
3 | Issue Closed
4 | Issue Rejected

