# Cfn-Compose 

Cfn-Compose is a tool that allows creating a CloudFormation template in YAML format by combining several fragments into one file. It's based on the [YamlDotNet](https://github.com/aaubry/YamlDotNet/) library and implements a custom tag !Inlcude to refer to fragment to include.

# Installing

It can be installed as a .NET Global tool from nuget https://www.nuget.org/packages/cfn-compose

```shell
$ dotnet tool install -g cfn-compose
```
# Usage
```shell
$ cfn-compose <input-yaml-file> [-o <output-yaml-file>]
```

# Example

input.yaml
```yaml
swagger: "2.0"

info:
  version: 1.0.0
  title: Simple API
  description: A simple API to learn how to write OpenAPI Specification

paths:
  /persons: !Include
    File: persons.yaml
  /pets: !Include
    File: pets.yaml
```

persons.yaml
```yaml
get:
  summary: Gets some persons
  description: Returns a list containing all persons.
  responses:
    200:
      description: A list of Person
      schema:
        type: array
        items:
          required:
            - username
          properties:
            firstName:
              type: string
            lastName:
              type: string
            username:
              type: string
```

pets.yaml
```yaml
get:
  summary: Gets some pets
  description: Returns a list containing all pets.
  responses:
    200:
      description: A list of pets
      schema:
        type: array
        items:
          required:
            - petname
          properties:
            petname:
              type: string
            ownerName:
              type: string
            breed:
              type: string
```

Run cfn-compose
```shell
$ cfn-compose input.yaml -o output.yaml
```

output.yaml
```yaml
swagger: 2.0
info:
  version: 1.0.0
  title: Simple API
  description: A simple API to learn how to write OpenAPI Specification
paths:
  /persons:
    get:
      summary: Gets some persons
      description: Returns a list containing all persons.
      responses:
        200:
          description: A list of Person
          schema:
            type: array
            items:
              required:
              - username
              properties:
                firstName:
                  type: string
                lastName:
                  type: string
                username:
                  type: string
  /pets:
    get:
      summary: Gets some pets
      description: Returns a list containing all pets.
      responses:
        200:
          description: A list of pets
          schema:
            type: array
            items:
              required:
              - petname
              properties:
                petname:
                  type: string
                ownerName:
                  type: string
                breed:
                  type: string
```

# How does it work ?

See this [blog post](https://abelperezmartinez.blogspot.com/2018/09/how-to-implement-cloudformation-include-custom-tag-using-YamlDotNet.html) where I get  more in detail about creating custom tags using YamlDotNet library.

# Changelog

TODO