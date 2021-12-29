# **.netcore5** <small style="float:right;font-size: 12px;">Updated: 29 Dec 2021</small>
## **netcore**
> Console Application that to name the project

## **WebApplication**
- To view available apis visits `https://localhost/swagger/index.html` on `Development Environment`.
![Swagger](/src/SwaggerOpenApi.JPG)
- Middleware
- Filters/Attributes
- Exceptions
- Action filters
- Cors
- JWT

## **Services**
- On `UserService.cs`, `Authenticate` overload methods use to authenticate users.

## **Database**
- Run sql script at [here](/netcore/netcoreDbScript.sql) to add `Tables` and `Store Procedures`.
- Add connection string on `appsettings.json`.
```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=databasename;User Id=username;Password=userpassword;"
  }
```

- User table

| Id | Email | PasswordHash | Username | Created | Updated | Deleted |
| --- | --- | --- | --- | --- | --- | --- |
| 00000000-0000-0000-0001-000000000001 | email1@mail.com | 7NcYcNGWMxapfjrDQIyYNa2M8PPBvHA1J8MCZVNPda4= | user1 | 2022-12-31 00:00:01.443 | 2022-12-31 00:00:01.443 | 0 |
| 00000000-0000-0000-0001-000000000002 | email2@mail.com | 7NcYcNGWMxapfjrDQIyYNa2M8PPBvHA1J8MCZVNPda4= | user22 | 2022-12-31 00:00:01.443 | 2022-12-31 00:00:01.443 | 0 |

## **Models**

## **Common**
- Utils
  - On `CryptographyUtil.cs`, methods uses to encrypt or decrypt input string. Asymmetric encryption. Symmetric encryption. Hash.
<br/><br/>

# **NUnitTest**
## **NUnitTestWebApplication**

## **NUnitTestServices**

## **NUnitTestDatabase**

## **NUnitTestCommon**

![NUnit](/src/NUnitTest.JPG)

<br/>

# 
<details>
<summary>For Education Resources</summary>
<p>

[LICENSE](/LICENSE), for more visit [website](https://demowebapplication-waikhaisheng.azurewebsites.net/)

```javascript
console.log("javascript debug here")
```

</p>
</details>